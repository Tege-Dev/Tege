using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NoteTakingApp
{
    public partial class DisplayNotes : Window
    {
        private List<Note> Notes;
        private string sortedColumn;
        private bool ascendingOrder;

        public DisplayNotes(List<Note> notes)
        {
            InitializeComponent();

            Notes = notes;
            sortedColumn = "";
            ascendingOrder = true;
            DisplayNotesInListBox(Notes);
        }

        private void DisplayNotesInListBox(List<Note> notes)
        {
            notesListBox.Items.Clear();
            foreach (Note note in notes)
            {
                notesListBox.Items.Add(note.Number.ToString() + ": " + note.Author + " - " + note.Theme + ". " + note.Content);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                List<Note> matchingNotes = Notes.Where(note => note.Theme.Contains(keyword)).ToList();
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

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string columnName = button.Tag.ToString();

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
                    Notes.Sort((note1, note2) => ascendingOrder ? note1.Number.CompareTo(note2.Number) : note2.Number.CompareTo(note1.Number));
                    break;
                case "Author":
                    Notes.Sort((note1, note2) => ascendingOrder ? note1.Author.CompareTo(note2.Author) : note2.Author.CompareTo(note1.Author));
                    break;
                case "Theme":
                    Notes.Sort((note1, note2) => ascendingOrder ? note1.Theme.CompareTo(note2.Theme) : note2.Theme.CompareTo(note1.Theme));
                    break;
            }

            DisplayNotesInListBox(Notes);
        }
    }
}
