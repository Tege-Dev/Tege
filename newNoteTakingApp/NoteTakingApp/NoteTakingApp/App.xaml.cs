﻿using Microsoft.IdentityModel.Tokens;
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
            
        }

        /*protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            var loginWindow = new LoginWindow();
            //var mainWindow = new MainWindow();
            var result = loginWindow.ShowDialog();

            if (result == false)
            {
                Shutdown();
            }

        }*/
    }
}

