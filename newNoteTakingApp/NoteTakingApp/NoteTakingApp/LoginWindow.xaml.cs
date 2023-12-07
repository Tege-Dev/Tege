using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteTakingApp
    {
        public partial class LoginWindow : Window
        {
            public static string Username { get; private set; } = null!;
            public static bool RememberMe { get; private set; }

            public LoginWindow()
            {
                InitializeComponent();
            }

            private void OKButton_Click(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                Username = usernameTextBox.Text;
                RememberMe = rememberCheckBox.IsChecked ?? false;
                DialogResult = true;
            }

        private void rememberCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
    