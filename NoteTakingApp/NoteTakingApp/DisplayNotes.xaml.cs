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
using System.Windows.Shapes;

namespace NoteTakingApp
{
    /// <summary>
    /// Interaction logic for DisplayNotes.xaml
    /// </summary>
    public partial class DisplayNotes : Window
    {
        private List<string> Notes;
        public DisplayNotes(List<string> notes)
        {
            InitializeComponent();
            Notes = notes;
            foreach (string note in notes)
            {
                notesListBox.Items.Add(note);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = searchTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                List<string> matchingNotes = Notes.Where(note => note.Contains(keyword)).ToList();
                notesListBox.Items.Clear();
                foreach (string note in matchingNotes)
                {
                    notesListBox.Items.Add(note);
                }
            }
            else
            {
                // If the search box is empty, show all notes
                notesListBox.Items.Clear();
                foreach (string note in Notes)
                {
                    notesListBox.Items.Add(note);
                }
            }
        }
        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            notesListBox.Items.Clear();
            foreach (string note in Notes)
            {
                notesListBox.Items.Add(note);
            }
            searchTextBox.Text = "";
        }
    }
}
