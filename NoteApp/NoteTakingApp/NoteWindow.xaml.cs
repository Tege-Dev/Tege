using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            noteTitleText.Text = selectedNote.Title;
            noteDetailsText.Text = selectedNote.Content;
            noteAuthorText.Text = "Author: " + selectedNote.Author.CapitalizeFirstLetter();
            editButton.Visibility = Visibility.Visible;
            commentButton.Visibility = Visibility.Visible;
        }

        public NoteWindow(MainWindow mainWindow, Note selectedNote, SharingSetting sharing)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.selectedNote = selectedNote;
            noteTitleText.Text = selectedNote.Title;
            noteDetailsText.Text = selectedNote.Content;
            noteAuthorText.Text = "Author: " + selectedNote.Author.CapitalizeFirstLetter();
            switch ( sharing )
            {
                case SharingSetting.Editing:
                    editButton.Visibility = Visibility.Visible;
                    commentButton.Visibility = Visibility.Visible;
                    break;
                case SharingSetting.Commenting:
                    commentButton.Visibility = Visibility.Visible; 
                    break;
                case SharingSetting.Viewing:
                    break;
            }
        }

        private void EditNote_Click(object sender, RoutedEventArgs e)
        {
            
            viewMode.Visibility = Visibility.Collapsed;
            editMode.Visibility = Visibility.Visible;

            editNoteTitleText.Text = selectedNote.Title;
            editNoteDetailsText.Text = selectedNote.Content;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            selectedNote.Title = noteTitleText.Text = editNoteTitleText.Text;
            selectedNote.Content = noteDetailsText.Text = editNoteDetailsText.Text;

            mainWindow.UpdateNote(selectedNote);

            viewMode.Visibility = Visibility.Visible;
            editMode.Visibility = Visibility.Collapsed;
        }

        private void CancelEdit_Click(object sender, RoutedEventArgs e)
        {
            viewMode.Visibility = Visibility.Visible;
            editMode.Visibility = Visibility.Collapsed;
        }

        private void BackToMainWidnow_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenMainWindow();
            Close();
        }

        private void CommentNote_Click(object sender, RoutedEventArgs e)
        {
            viewMode.Visibility = Visibility.Visible;
        }
    }

}