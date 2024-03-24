using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoteTakingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string Username { get; set; } = string.Empty;
        private bool RememberMe { get; set; }
        private NoteDbContext noteDbContext { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
            LoadSavedUsername();
            noteDbContext = new NoteDbContext();
        }

        public String GetUsername()
        {
            return Username;
        }

        // LoginWindow.xaml.cs
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
                okButton.IsEnabled = false;
            }
            else if (username.Length > 20)
            {
                validationMessage.Text = "Username is too long.";
                validationMessage.Foreground = Brushes.Red;
                okButton.IsEnabled = false;
            }
            else if (!Regex.IsMatch(username, namePattern))
            {
                validationMessage.Text = "Invalid characters. Use only letters and numbers.";
                validationMessage.Foreground = Brushes.Red;
                okButton.IsEnabled = false;
            }
            else
            {
                validationMessage.Text = "Valid";
                validationMessage.Foreground = Brushes.Green;
                okButton.IsEnabled = true;
            }
        }

        // LoginWindow.xaml.cs
        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && okButton.IsEnabled)
            {
                OKButton_Click(sender, e);
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Username = usernameTextBox.Text;
            RememberMe = rememberCheckBox.IsChecked ?? false;
            var users = noteDbContext.Users.Select(u => u.Username).ToList();
            if (users.Contains(Username))
            {
                SaveUsername(Username, RememberMe);
                DialogResult = true;
            }
            else
            {
                validationMessage.Text = "Invalid username. Try again.";
                validationMessage.Foreground = Brushes.Red;
                okButton.IsEnabled = false;
                Username = String.Empty;
            }

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

        private void SaveUsername(String userName, bool rememberMeOption)
        {
            // Save the username and option to remember to application settings
            Properties.Settings.Default.SavedUsername = userName;
            Properties.Settings.Default.RememberMeOption = rememberMeOption;
            Properties.Settings.Default.Save();
        }
    }
}
