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

namespace NoteTakingApp
{
    public partial class MainWindow : Window
    {
        private NoteDbContext dbContext;
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

        public ObservableCollection<Note> LoadNotesFromFile()
        {
            var loadedNotes = new ObservableCollection<Note>();
            var filePath = "SavedNotes.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i += 6)
                {
                    var number = int.Parse(lines[i]);
                    var author = lines[i + 1];
                    var theme = lines[i + 2];
                    var content = lines[i + 3];
                    var privacy = (PrivacySetting)Enum.Parse(typeof(PrivacySetting), lines[i + 4]);
                    var tag = lines[i + 5];

                    Note note = new Note(number, author, theme, content, privacy, tag);
                    loadedNotes.Add(note);
                }
            }

            return loadedNotes;
        }


        public void SaveNotesToFile()
        {
            var linesToWrite = new List<string>();

            foreach (Note note in Notes)
            {
                linesToWrite.Add(note.Number.ToString());
                linesToWrite.Add(note.Author);
                linesToWrite.Add(note.Theme);
                linesToWrite.Add(note.Content);
                linesToWrite.Add(note.Privacy.ToString());
                linesToWrite.Add(note.Tag);
            }

            File.WriteAllLines("SavedNotes.txt", linesToWrite);
        }

        public ObservableCollection<Note> LoadNotesFromDatabase()
        {
            return new ObservableCollection<Note>(dbContext.Notes.ToList());
        }

        public void SaveNotesToDatabase()
        {
            dbContext.Database.ExecuteSqlRaw("DELETE FROM Notes");
            foreach (var note in Notes)
            {
                dbContext.Notes.Add(note);
            }
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
