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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteTakingApp
{

    public partial class MainWindow : Window
    {
        private List<string> Notes = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void DisplayNotes(object sender, RoutedEventArgs e)
        {
            DisplayNotes newDisplayNotes = new DisplayNotes(Notes);
            newDisplayNotes.Show();
        }
        private void AddNote(object sender, RoutedEventArgs e)
        {
            AddNote newAddNote = new AddNote(Notes);
            newAddNote.Show();
        }
        private void Quit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
