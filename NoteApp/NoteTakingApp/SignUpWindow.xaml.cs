using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteTakingApp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class SignUpWindow : Window {
        private MainWindow _mainWindow;
        private string Username { get; set; } = string.Empty;
        private string Name { get; set; } = string.Empty;
        private string Surname { get; set; } = string.Empty;

        public SignUpWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Add your sign up logic here
            Username = usernameTextBox.Text;
            Name = nameTextBox.Text;
            Surname = SurnameTextBox.Text;
            DialogResult = true;
            if (!Username.IsNullOrEmpty())
            {
                var user = new User(Username, Name, Surname);
                 _mainWindow.SaveUser(user);
            }
            // After signing up, navigate to the login window
        }

        public String GetUsername()
        {
            return Username;
        }

        private void GoToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the login window
            LoginWindow loginWindow = new LoginWindow();
            bool? dialogResult = loginWindow.ShowDialog();

            if (dialogResult == true)
            {
                DialogResult = true;
                this.Close();
            }
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int caretIndex = usernameTextBox.CaretIndex;

            string namePattern = @"^[A-Za-z0-9]+$";
            string username = usernameTextBox.Text.ToLower();

            usernameTextBox.Text = username;
            usernameTextBox.CaretIndex = caretIndex;

            if (string.IsNullOrEmpty(username))
            {
                validationMessage.Text = "Username cannot be empty.";
                validationMessage.Foreground = Brushes.Red;
                SignUpButton.IsEnabled = false;
            }
            else if (username.Length > 20)
            {
                validationMessage.Text = "Username is too long.";
                validationMessage.Foreground = Brushes.Red;
                SignUpButton.IsEnabled = false;
            }
            else if (!Regex.IsMatch(username, namePattern))
            {
                validationMessage.Text = "Invalid characters. Use only letters and numbers.";
                validationMessage.Foreground = Brushes.Red;
                SignUpButton.IsEnabled = false;
            }
            else
            {
                validationMessage.Text = "Valid";
                validationMessage.Foreground = Brushes.Green;
                SignUpButton.IsEnabled = true;
            }
        }
        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SignUpButton.IsEnabled)
            {
                SignUpButton_Click(sender, e);
            }
        }

        private void SurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
    
