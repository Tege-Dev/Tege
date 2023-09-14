using System;
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
    public partial class AddNote : Window
    {
        private List<string> Notes;
        private int noteNumber;
        public AddNote(List<string> Notes)
        {
            InitializeComponent();
            this.Notes = Notes;
        }

        private void AddNewNote(object sender, RoutedEventArgs e)
        {
            string author = authorTextBox.Text.Trim();

            if (string.IsNullOrEmpty(author))
            {
                MessageBox.Show("Please enter your name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string content = noteContentTextBox.Text.Trim();

            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Please enter note content.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            noteNumber = Notes.Count + 1;
            Notes.Add($"Note #{noteNumber} by {author}: {content}");
            Close(); 
        }
    }
}
