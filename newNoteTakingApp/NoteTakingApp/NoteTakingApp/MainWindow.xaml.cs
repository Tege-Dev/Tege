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
            var newAddNote = new AddNote(Notes, this);
            newAddNote.Show();
        }

        public ObservableCollection<Note> LoadNotesFromDatabase()
        {
            return new ObservableCollection<Note>(dbContext.Notes.Where(note => note.Author.Contains(Author)).ToList());
        }

        public void SaveNotesToDatabase()
        {
            foreach (var note in Notes)
            {
                // If the note with the same ID exists in the database, update it
                var existingNote = dbContext.Notes.Find(note.Number);
                if (existingNote != null)
                {
                    dbContext.Entry(existingNote).CurrentValues.SetValues(note);
                }
                else
                {
                    // If the note doesn't exist in the database, add it
                    dbContext.Notes.Add(note);
                }
            }
            //TODO: change tracking, only SaveChanges();
            //TODO: endpoint i db || api
            //TODO: (Usage of middleware and at)? least one interceptor;
            dbContext.SaveChanges();
        }

        private void NotesCardClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.DataContext is Note selectedNote)
            {
                var noteDetails = $"Number: {selectedNote.Number}\nAuthor: {selectedNote.Author}\nTheme: {selectedNote.Theme}\nContent: {selectedNote.Content}";

                var noteWindow = new NoteWindow(noteDetails);
                noteWindow.Show();
            }
        }
    }
}
