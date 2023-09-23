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

namespace NoteTakingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> Notes;
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("SavedNotes.txt")) Notes = File.ReadAllLines("SavedNotes.txt").ToList();
            else Notes = new List<string>();
            NoteVisibilityToggle(Notes);
        }
        private void DisplayNotes(object sender, RoutedEventArgs e)
        {
            var newDisplayNotes = new DisplayNotes(Notes);
            newDisplayNotes.Show();
        }
        private void ClearNotes(object sender, RoutedEventArgs e)
        {
            Notes.Clear();
            NoteVisibilityToggle(Notes);
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            var newAddNote = new AddNote(Notes, this);
            newAddNote.Show();
        }
        public void NoteVisibilityToggle(List<string> Notes)
        {
            if (Notes.Count > 0) pageDisplay.Visibility = Visibility.Visible;
            else pageDisplay.Visibility = Visibility.Hidden;
        }
    }
}