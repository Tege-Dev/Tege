using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NoteTakingApp
{
    public delegate int NoteComparison(Note note1, Note note2);

    public partial class DisplayNotes : Window
    {
        public ObservableCollection<Note> Notes;
        private string sortedColumn;
        private bool ascendingOrder;
        private NoteComparison sortFunction;
        private MainWindow mainWindow;

        public DisplayNotes(ObservableCollection<Note> notes, MainWindow mainwindow)
        {
            InitializeComponent();

            Notes = notes;
            mainWindow = mainwindow;
            sortedColumn = "";
            ascendingOrder = true;
            DisplayNotesInListBox(Notes);
        }
        private void DisplayNotesInListBox(ObservableCollection<Note> notes)
        {
            notesListBox.Items.Clear();
            foreach (Note note in notes)
            {
                var displayText = $"{note.Number} - Privacy: {note.Privacy} -  ({note.Tag}): {note.Author.CapitalizeFirstLetter()} {note.Theme.CapitalizeFirstLetter()}. {note.Content.CapitalizeFirstLetter()}";
                notesListBox.Items.Add(displayText);
            }
        }


        public void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var keyword = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var matchingNotes = new ObservableCollection<Note>(Notes.Where(note => note.Theme.Contains(keyword)).ToList());
                DisplayNotesInListBox(matchingNotes);
            }
            else
            {
                DisplayNotesInListBox(Notes);
            }
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayNotesInListBox(Notes);
            searchTextBox.Text = "";
        }

        public void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var columnName = button.Tag.ToString();

            if (sortedColumn == columnName)
            {
                ascendingOrder = !ascendingOrder;
            }
            else
            {
                sortedColumn = columnName;
                ascendingOrder = true;
            }

            switch (columnName)
            {
                case "Number":
                    sortFunction = CompareByNumber;
                    break;
                case "Author":
                    sortFunction = CompareByAuthor;
                    break;
                case "Theme":
                    sortFunction = CompareByTheme;
                    break;
            }

            List<Note> sortedList = ascendingOrder
                ? Notes.OrderBy(n => sortFunction(n, null)).ToList()
                : Notes.OrderByDescending(n => sortFunction(n, null)).ToList();

            Notes.Clear();

            foreach (var note in sortedList)
            {
                Notes.Add(note);
            }

            DisplayNotesInListBox(Notes);
        }


        private int CompareByNumber(Note note1, Note? note2)
        {
            if (note2 == null)
                return -1;
            return note1.Number.CompareTo(note2.Number);
        }

        private int CompareByAuthor(Note note1, Note? note2)
        {
            if(note2 == null)
                return -1;
            return note1.Author.CompareTo(note2.Author);
        }

        private int CompareByTheme(Note note1, Note? note2)
        {
            if (note2 == null)
                return -1;
            return note1.Theme.CompareTo(note2.Theme);
        }
    }
}
