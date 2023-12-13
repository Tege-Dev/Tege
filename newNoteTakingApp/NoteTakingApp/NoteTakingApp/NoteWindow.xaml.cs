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
    public partial class NoteWindow : Window
    {
        private Note note;
        public NoteWindow(Note note)
        {
            InitializeComponent();
            this.note = note;
            noteThemeText.Text = note.Theme;
            noteDetailsText.Text = note.Content;
        }
        private void EditNote_Click(object sender, RoutedEventArgs e)
        {
            var editNoteWindow = new EditNoteWindow(note);
            editNoteWindow.Show();
            Close();
        }

        private void BackToMainWidnow_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.OpenMainWindow();
            Close();
        }


    }
}