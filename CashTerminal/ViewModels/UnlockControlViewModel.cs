﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CashTerminal.Commons;
using CashTerminal.Models;

namespace CashTerminal.ViewModels
{
    internal class UnlockControlViewModel : ViewModelBase
    {
        private IOverlayable _parent;
        private string _username;
        private string _password;

        /// <summary>
        /// Комманда закрытия перекрытия.
        /// </summary>
        public ICommand UnlockCommand { get; set; }

        public UnlockControlViewModel(IOverlayable parent)
        {
            UnlockCommand = new RelayCommand(Unlock, IsValid);
            _parent = parent;
            _username = _parent.Model.Validator.Username;
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public void Unlock(object obj)
        {
            _parent.Model.Validator = new Authorization(_username, _password);
            if (!_parent.Model.Validator.IsValid)
            {
                MessageBox.Show("Неверный пароль", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _parent.CloseOverlay();
        }

        private bool IsValid(object obj)
        {
            return !string.IsNullOrWhiteSpace(Password);
        }

        public override string ToString()
        {
            return "UnlockControl";
        }
    }
}