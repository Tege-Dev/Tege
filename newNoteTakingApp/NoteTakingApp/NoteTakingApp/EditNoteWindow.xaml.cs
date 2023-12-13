// EditNoteWindow.xaml.cs
using System.Linq;
using System.Windows;

namespace NoteTakingApp
{
    public partial class EditNoteWindow : Window
    {
        private Note selectedNote;
        private MainWindow mainWindow;

        public EditNoteWindow(MainWindow mainWindow, Note selectedNote)
        {
            InitializeComponent();
            this.selectedNote = selectedNote;
            this.mainWindow = mainWindow;
            editNoteTitleText.Text = selectedNote.Title;
            editNoteDetailsText.Text = selectedNote.Content;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Update note content
            selectedNote.Title = editNoteTitleText.Text;
            selectedNote.Content = editNoteDetailsText.Text;

            // Save changes to the database
            mainWindow.UpdateNote(selectedNote);

            var noteWindow = new NoteWindow(mainWindow, selectedNote);
            noteWindow.Show();
            // Close the window
            Close();
        }
    }
}
