using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Net;
using System.Collections.Immutable;
using System.Collections;
using static Azure.Core.HttpHeader;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace NoteTakingApp
{
    public class NoteNotFoundException : Exception
{
    public NoteNotFoundException(string message) : base(message)
    {
    }
}
    public partial class MainWindow : Window
    {
        private NoteDbContext dbContext = new NoteDbContext();
        
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

        private async Task LoadDataAsync()
        {
            using (var dbContext = new NoteDbContext())
            {
                UserNotes = await dbContext.GetAllUserNotesAsync();
                Notes = await dbContext.GetAllNotesAsync();
            }
        }

        private void UpdateView(List<Note> notes)
        {
            ShowNotes.Clear();
            foreach (var note in notes)
            {
                ShowNotes.Add(note);
            }
        }


        private async void UpdateView()
        {
            if (userRadioButton.IsChecked ?? false)
            {
                UpdateView(await LoadUserNotesAsync());
            }
            else if (publicRadioButton.IsChecked ?? false)
            {
                UpdateView(await GetNotesSavedByAuthorAsync());
            }
        }

        private bool UsernameDialog()
        {
            var signUpWindow = new SignUpWindow();
            Author = signUpWindow.GetUsername();
            return signUpWindow.ShowDialog() ?? false;
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
            var newAddNote = new AddNote(this);
            newAddNote.Show();
        }

        public List<Note> GetPublicNotes()
        {
            var authorSavedNotes = GetNotesSavedByAuthorAsync();

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

        public async Task<List<Note>> GetNotesSavedByAuthorAsync()
        {
            await LoadDataAsync();
            var authorSavedNoteNumbers = AuthorNoteNumbers();
            return new List<Note>(Notes
                .Where(note => authorSavedNoteNumbers.Contains(note.Number))
                .ToList());
        }

        public async Task<List<Note>> LoadUserNotesAsync()
        {
            await LoadDataAsync();
            return new List<Note>(
                Notes
                    .Where(note => note.Author.Equals(Author, StringComparison.OrdinalIgnoreCase))
                    .ToList());
        }

        public async void SaveNoteAsync(Note newNote)
        {
            dbContext.Notes.Add(newNote);
            await dbContext.SaveChangesAsync();
            UpdateView();
        }

        public async void SavePublicNoteAsync(Note note)
        {
            var saved = await GetNotesSavedByAuthorAsync();
            if (saved.Any(n => n.Number == note.Number))
            {
                return;
            }
            var userNote = new UserNote(Author, note.Number);
            dbContext.UserNotes.Add(userNote);
            await dbContext.SaveChangesAsync();
            UpdateView();
        }

        public void UpdateNote(Note updatedNote)
        {
            try
            {
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
                    // Throw custom exception if the note is not found
                    throw new NoteNotFoundException($"Note with ID {updatedNote.Number} was not found.");
                }
            }
            catch (NoteNotFoundException ex)
            {
                // Log the exception (you can replace this with your preferred logging mechanism)
                //LogException(ex);
                // Optionally rethrow the exception if you want the caller to handle it
                throw;
            }
        }

            private void NotesCardClick(object sender, RoutedEventArgs e){
            var button = (Button)sender;
            NoteWindow noteWindow;
            if (button.DataContext is Note selectedNote)
            {
                if(selectedNote.Author != Author)
                {
                    noteWindow = new NoteWindow(this, selectedNote, selectedNote.Sharing);
                }
                else
                {
                    noteWindow = new NoteWindow(this, selectedNote);
                }
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
