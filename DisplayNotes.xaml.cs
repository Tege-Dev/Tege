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
    public partial class DisplayNotes : Window
    {
        private List<string> Notes;
        public DisplayNotes(List<string> notes)
        {
            InitializeComponent();

            foreach (string note in notes)
            {
                notesListBox.Items.Add(note);
            }
        }
    }
}
