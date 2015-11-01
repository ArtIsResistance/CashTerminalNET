﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CashTerminal.Commons;
using CashTerminal.Models;
using CashTerminal.Models.Data;
using SettingsProvider = System.Configuration.SettingsProvider;

namespace CashTerminal.ViewModels
{
    internal class MainViewModel : ViewModelBase, IOverlayable
    {
        public string UserName => "Пользователь: " + Environment.UserName;
        public string Uptime => "Время сеанса: " + _timer.SessionTime.ToString(@"hh\:mm\:ss");

        private ObservableCollection<ViewModelBase> _overlayedControl;
        public ObservableCollection<ViewModelBase> OverlayedControl
        {
            get
            {
                return _overlayedControl;
            }
            set
            {
                _overlayedControl = value;
                OnPropertyChanged("OverlayVisibility");
                OnPropertyChanged();
            }
        }
        public Visibility OverlayVisibility
        {
            get
            {
                return OverlayedControl?.Count != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public ICommand LogoffCommand { get; set; }
        public ICommand LockCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private readonly SessionTimer _timer;
        public SessionTimer Timer => _timer;

        public string TotalValue => $"{0m:F} грн.";

        public MainViewModel()
        {
            _timer=new SessionTimer();
            _timer.PropertyChanged += (sender, args) => {OnPropertyChanged("Uptime"); };

            LogoffCommand=new RelayCommand(Logoff);
            LockCommand=new RelayCommand(Lock);
            SettingsCommand=new RelayCommand(ShowSettings);

            //_overlayedControl= new ObservableCollection<ViewModelBase> { new LoginControlViewModel(this) };
            _overlayedControl = new ObservableCollection<ViewModelBase> { new SettingsControlViewModel(this)};
        }

        
        public void Logoff(object obj)
        {
            if (OverlayedControl.Count<1)
                OverlayedControl = new ObservableCollection<ViewModelBase> {new LoginControlViewModel(this)};
        }

        public void Lock(object obj)
        {
            if (OverlayedControl.Count < 1)
                OverlayedControl = new ObservableCollection<ViewModelBase> { new UnlockControlViewModel(this)};
        }

        public void ShowSettings(object obj)
        {
            if (OverlayedControl.Count < 1)
                OverlayedControl = new ObservableCollection<ViewModelBase> { new SettingsControlViewModel(this) };
        }

        public void CloseOverlay()
        {
            OverlayedControl=new ObservableCollection<ViewModelBase>();
        }

        
    }
}
