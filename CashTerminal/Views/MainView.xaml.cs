﻿using System.Globalization;
using System.Windows;

namespace CashTerminal.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            // Установка локали ru-RU.
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            InitializeComponent();
        }
    }
}