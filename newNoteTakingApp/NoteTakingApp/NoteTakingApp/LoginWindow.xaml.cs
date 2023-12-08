using Microsoft.IdentityModel.Tokens;
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
        public static string Username { get; private set; } = string.Empty;
        public static bool RememberMe { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            LoadSavedUsername();
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

            SaveUsername(Username, RememberMe);

            DialogResult = true;
        }

        private void RememberCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void LoadSavedUsername()
        {
            if (Properties.Settings.Default.RememberMeOption)
            {
                usernameTextBox.Text = Properties.Settings.Default.SavedUsername;
                rememberCheckBox.IsChecked = true;
            }
        }

        private void SaveUsername(String? userName, bool rememberMeOption)
        {
            // Save the username and option to remember to application settings
            Properties.Settings.Default.SavedUsername = userName;
            Properties.Settings.Default.RememberMeOption = rememberMeOption;
            Properties.Settings.Default.Save();
        }
    }
}
