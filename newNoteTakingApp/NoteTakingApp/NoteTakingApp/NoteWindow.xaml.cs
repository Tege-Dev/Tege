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
        private Note selectedNote;
        private MainWindow mainWindow;

        public NoteWindow(MainWindow mainWindow, Note selectedNote)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.selectedNote = selectedNote;
            noteThemeText.Text = selectedNote.Title;
            noteDetailsText.Text = selectedNote.Content;
        }
        private void EditNote_Click(object sender, RoutedEventArgs e)
        {
            var editNoteWindow = new EditNoteWindow(mainWindow, selectedNote);
            editNoteWindow.Show();
            Close();
        }

        private void BackToMainWidnow_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenMainWindow();
            Close();
        }
    }
}