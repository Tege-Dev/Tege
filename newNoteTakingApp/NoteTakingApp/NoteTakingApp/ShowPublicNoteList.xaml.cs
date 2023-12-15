using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NoteTakingApp
{
    public partial class ShowPublicNoteList : Window
    {
        public List<Note> Notes;
        private string sortedColumn;
        private bool ascendingOrder;
        private MainWindow MainWindow;

        public ShowPublicNoteList(MainWindow mainWindow)
        {
            InitializeComponent();

            MainWindow = mainWindow;
            sortedColumn = "";
            ascendingOrder = true;
            Notes = mainWindow.GetPublicNotes();
            DisplayNotesInListView(Notes);
        }

        private void DisplayNotesInListView(List<Note> notes)
        {
            notesListView.ItemsSource = notes;
            notesListView.UpdateLayout();
        }

        private void notesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (notesListView.SelectedItem != null)
            {
                addButton.Visibility = Visibility.Visible;
            }
            else
            {
                addButton.Visibility = Visibility.Hidden;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesListView.SelectedItem is Note selectedNote)
            {
                MainWindow.SavePublicNote(selectedNote);
                notesListView.SelectedItem = null;
            }
        }

        public void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var keyword = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyword) && !Notes.IsNullOrEmpty())
            {
                var matchingNotes = Notes.Where(note =>
                note.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                note.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                note.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

                DisplayNotesInListView(matchingNotes);
            }
            else
            {
                DisplayNotesInListView(Notes);
            }
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayNotesInListView(Notes);
            searchTextBox.Text = "";
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var columnName = button.Tag.ToString();

            UpdateSortOrder(columnName);

            switch (columnName)
            {
                case "Number":
                    SortByNumber();
                    break;
                case "Author":
                    SortByAuthor();
                    break;
                case "Title":
                    SortByTitle();
                    break;
                default:
                    return;
            }
        }

        private void SortByNumber()
        {
            var sortedList = ascendingOrder
                ? Notes.OrderBy(note => note.Number).ToList()
                : Notes.OrderByDescending(note => note.Number).ToList();

            DisplayNotesInListView(sortedList);
        }

        private void SortByAuthor()
        {
            var sortedList = ascendingOrder
                ? Notes.OrderBy(note => note.Author).ToList()
                : Notes.OrderByDescending(note => note.Author).ToList();

            DisplayNotesInListView(sortedList);
        }

        private void SortByTitle()
        {
            var sortedList = ascendingOrder
                ? Notes.OrderBy(note => note.Title).ToList()
                : Notes.OrderByDescending(note => note.Title).ToList();

            DisplayNotesInListView(sortedList);
        }


        private void UpdateSortOrder(string columnName)
        {
            if (sortedColumn == columnName)
            {
                ascendingOrder = !ascendingOrder;
            }
            else
            {
                sortedColumn = columnName;
                ascendingOrder = true;
            }
        }

    }
}
