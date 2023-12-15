using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Net;
using System.Collections.Immutable;
using System.Collections;
using static Azure.Core.HttpHeader;

namespace NoteTakingApp
{
    public partial class MainWindow : Window
    {
        private NoteDbContext dbContext;
        private string Author;
        public ObservableCollection<Note> ShowNotes { get; set; } = new ObservableCollection<Note>();
        public List<UserNote> UserNotes { get; set; }
        public List<Note> Notes { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            if(UsernameDialog() == false)
            {
                Close();
            }
            Author = Properties.Settings.Default.SavedUsername;
            DataContext = this;
            userRadioButton.IsChecked = true;
            UpdateView();
        }

        private void LoadData()
        {
            using (var dbContext = new NoteDbContext())
            {
                UserNotes = dbContext.GetAllUserNotes();
                Notes = dbContext.GetAllNotes();
            }
        }

        private void UpdateView(List<Note> notes)
        {
            ShowNotes.Clear();
            ShowNotes.AddRange(notes);
        }


        private void UpdateView()
        {
            if (userRadioButton.IsChecked ?? false)
            { 
                UpdateView(LoadUserNotes());
            }
            else if(publicRadioButton.IsChecked ?? false)
            {
                UpdateView(GetNotesSavedByAuthor());
            }
        }

        private bool UsernameDialog()
        {
            var loginWindow = new LoginWindow();
            return loginWindow.ShowDialog() ?? false;
        }

        public void ShowPublicNotes(object sender, RoutedEventArgs e)
        {
            var newDisplayNotes = new ShowPublicNoteList(this);
            newDisplayNotes.Show();
        }

        private void ChangeUser(object sender, RoutedEventArgs e)
        {
            if (UsernameDialog() == false)
            {
                return;
            }

            Properties.Settings.Default.Reload();
            Author = Properties.Settings.Default.SavedUsername;
            UpdateView();
        }
        private void ClearNotes(object sender, RoutedEventArgs e)
        {
            ShowNotes.Clear();
            ClearDatabaseTable();
        }
        public void ClearDatabaseTable()
        {
           // dbContext.Database.ExecuteSqlRaw("DELETE FROM Notes");
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            var newAddNote = new AddNote(this, dbContext);
            newAddNote.Show();
        }

        //Notes = new ObservableCollection<Note>(LoadPublicNotes());

        public List<Note> GetPublicNotes()
        {
            var authorSavedNotes = GetNotesSavedByAuthor();

            return new List<Note>(Notes
                .Where(note => (note.Privacy == PrivacySetting.Public && !note.Author.Equals(Author, StringComparison.OrdinalIgnoreCase)))
                .ToList());
        }

        public List<int> AuthorNoteNumbers()
        {   
            return new List<int>(UserNotes
                .Where(userNote => userNote.UserName.Equals(Author, StringComparison.OrdinalIgnoreCase))
                .Select(userNote => userNote.PublicNoteNumber)
                .ToList());
        }

        public List<Note> GetNotesSavedByAuthor()
        {
            LoadData();
            var authorSavedNoteNumbers = AuthorNoteNumbers();
            return new List<Note>(Notes
                .Where(note => authorSavedNoteNumbers.Contains(note.Number))
                .ToList());
        }

        public List<Note> LoadUserNotes()
        {
            LoadData();
            return new List<Note>(
                Notes
                    .Where(note => note.Author.Equals(Author, StringComparison.OrdinalIgnoreCase))
                    .ToList());
        }

        public void SaveNote(Note newNote)
        {
            dbContext = new NoteDbContext();
           
            dbContext.Notes.Add(newNote);
            dbContext.SaveChanges();
            UpdateView();
        }

        public void SavePublicNote(Note note)
        {
            dbContext = new NoteDbContext();
            var saved = GetNotesSavedByAuthor();
            if (saved.Any(n => n.Number == note.Number))
            {
                return;
            }
            var userNote = new UserNote(Author, note.Number);
            dbContext.UserNotes.Add(userNote);
            dbContext.SaveChanges();
            UpdateView();
        }

        public void UpdateNote(Note updatedNote)
        {
            dbContext = new NoteDbContext();
            var existingNote = dbContext.Notes.Find(updatedNote.Number);

            if (existingNote != null)
            {
                existingNote.Title = updatedNote.Title;
                existingNote.Content = updatedNote.Content;
                existingNote.Privacy = updatedNote.Privacy;

                dbContext.SaveChanges();
                UpdateView();
            }
            else
            {
                throw new InvalidOperationException($"Note with ID {updatedNote.Number} not found.");
            }
        }

        private void NotesCardClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.DataContext is Note selectedNote)
            {
                var noteWindow = new NoteWindow(this, selectedNote);
                noteWindow.Show();
                Visibility = Visibility.Collapsed;
            }
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
                UpdateView();
        }

        public void OpenMainWindow()
        {
            Visibility = Visibility.Visible;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;

            Environment.Exit(0);
        }
    }
}
