using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace NoteTakingApp
{
    public partial class AddNote : Window
    {
        private MainWindow mainWindow;
        public NoteDbContext dbContext;

        public AddNote(MainWindow mainwindow)
        {
            InitializeComponent();
            mainWindow = mainwindow;

            privacyComboBox.Items.Clear();
            privacyComboBox.ItemsSource = Enum.GetValues(typeof(PrivacySetting));
            privacyComboBox.SelectedItem = PrivacySetting.Private;

            sharingComboBox.Items.Clear();
            sharingComboBox.ItemsSource = Enum.GetValues(typeof (SharingSetting));
            sharingComboBox.SelectedItem = SharingSetting.Viewing;
            sharingComboBox.Visibility = Visibility.Collapsed; // Initially hidden
        }

        private void PrivacyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (privacyComboBox.SelectedItem.ToString() == "Public")
            {
                sharingLabel.Visibility = Visibility.Visible;
                sharingComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                sharingLabel.Visibility = Visibility.Collapsed;
                sharingComboBox.Visibility = Visibility.Collapsed;
            }
        }

        public void AddNewNote(object sender, RoutedEventArgs e)
        {
            var content = noteContentTextBox.Text.Trim();
            var title = titleTextBox.Text.Trim();
            var privacy = (PrivacySetting)privacyComboBox.SelectedItem;
            var sharing = (SharingSetting)sharingComboBox.SelectedItem;

            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newNote = new Note(Properties.Settings.Default.SavedUsername, title, content, privacy, sharing);

            mainWindow.SaveNoteAsync(newNote);

            Close();
        }
    }
}