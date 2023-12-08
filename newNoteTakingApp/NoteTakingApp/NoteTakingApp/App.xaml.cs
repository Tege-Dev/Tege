using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NoteTakingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //var dbContext = new NoteDbContext();
            //var mainWindow = new MainWindow(dbContext);
            //mainWindow.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = new LoginWindow();
            var mainWindow = new MainWindow();
            var result = loginWindow.ShowDialog();

            if (result == true)
            {
                string username = LoginWindow.Username;
                bool rememberMe = LoginWindow.RememberMe;
                // TODO: panaudoti username ir rememberMe
            }
            else
            {
                // The user closed the window or clicked Cancel
                Shutdown();
            }
        }
    }
}

