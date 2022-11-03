﻿using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using DL_Projectwerk;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UIBezoeker;

namespace UIBezoeker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
            MainView Main = new MainView();
            Main.Show();
        }
    }
}
