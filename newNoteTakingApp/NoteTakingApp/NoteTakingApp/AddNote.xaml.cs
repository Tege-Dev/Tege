using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace NoteTakingApp
{
    public partial class AddNote : Window
    {
        private MainWindow mainWindow;
        public NoteDbContext dbContext;

        public AddNote(MainWindow mainwindow, NoteDbContext context)
        {
            InitializeComponent();
            mainWindow = mainwindow;
            dbContext = context;

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

            var newNote = new Note(title, content, privacy);

            mainWindow.SaveNote(newNote);

            Close();
        }
    }
}
