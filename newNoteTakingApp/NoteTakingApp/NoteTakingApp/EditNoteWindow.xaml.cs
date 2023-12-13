// EditNoteWindow.xaml.cs
using System.Linq;
using System.Windows;

namespace NoteTakingApp
{
    public partial class EditNoteWindow : Window
    {
        private Note note;

        public EditNoteWindow(Note selectedNote)
        {
            InitializeComponent();
            note = selectedNote;
            editNoteThemeText.Text = note.Theme;
            editNoteDetailsText.Text = note.Content;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Update note content
            note.Theme = editNoteThemeText.Text;
            note.Content = editNoteDetailsText.Text;

            // Save changes to the database
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            main.SaveNotesToDatabase();

            var noteWindow = new NoteWindow(note);
            noteWindow.Show();
            // Close the window
            Close();
        }
    }
}
