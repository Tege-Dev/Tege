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

namespace NoteTakingApp
{
    public partial class MainWindow : Window
    {

        private NoteDbContext dbContext;
        private String Author = LoginWindow.Username;
        public ObservableCollection<Note> Notes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DependencyInjector injector = new DependencyInjector();
            dbContext = injector.GetNoteDbContext();
            Notes = new ObservableCollection<Note>(LoadNotesFromDatabase());
            DataContext = this;
        }

        public void DisplayNotes(object sender, RoutedEventArgs e)
        {
            var newDisplayNotes = new DisplayNotes(Notes, this);
            newDisplayNotes.Show();
        }
        private void ClearNotes(object sender, RoutedEventArgs e)
        {
            Notes.Clear();
            ClearDatabaseTable();
        }
        public void ClearDatabaseTable()
        {
            dbContext.Database.ExecuteSqlRaw("DELETE FROM Notes");
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            var newAddNote = new AddNote(this, dbContext);
            newAddNote.Show();
        }

        public ObservableCollection<Note> LoadNotesFromDatabase()
        {
            return new ObservableCollection<Note>(dbContext.Notes.Where(note => note.Author.Contains(Author)).ToList());
        }

        public void SaveNote(Note newNote)
        {
            Notes.Add(newNote);
            dbContext.Notes.Add(newNote);
            dbContext.SaveChanges();
        }

        public void UpdateNote(Note updatedNote)
        {
            var existingNote = dbContext.Notes.Find(updatedNote.Number);

            if (existingNote != null)
            {
                Notes.Remove(existingNote);
                Notes.Add(updatedNote);

                existingNote.Title = updatedNote.Title;
                existingNote.Content = updatedNote.Content;
                existingNote.Privacy = updatedNote.Privacy;

                dbContext.SaveChanges();
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
