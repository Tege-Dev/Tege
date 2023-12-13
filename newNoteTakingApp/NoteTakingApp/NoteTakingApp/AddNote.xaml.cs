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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace NoteTakingApp
{
    public partial class AddNote : Window
    {
        private ObservableCollection<Note> Notes;
        private MainWindow mainWindow;
        public NoteDbContext dbContext;

        public AddNote(ObservableCollection<Note> notes, MainWindow mainwindow)
        {
            InitializeComponent();
            Notes = notes;
            mainWindow = mainwindow;

            dbContext = new NoteDbContext();

            privacyComboBox.Items.Clear();
            privacyComboBox.ItemsSource = Enum.GetValues(typeof(PrivacySetting));
            privacyComboBox.SelectedItem = PrivacySetting.Private;
        }

        public void AddNewNote(object sender, RoutedEventArgs e)
        {
            var content = noteContentTextBox.Text.Trim();
            var title = titleTextBox.Text.Trim();
            var privacy = (PrivacySetting)privacyComboBox.SelectedItem;

                if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(title))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var noteNumber = Notes.Count + 1;
                var newNote = new Note(title, content, privacy);
                Notes.Add(newNote);
                mainWindow.SaveNotesToDatabase();

                Close();
        }
    }


}
