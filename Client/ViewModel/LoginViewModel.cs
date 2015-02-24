using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Entities.Database;
using Entities.Entities;

namespace Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public delegate void Cancel();

        public delegate void LoginSuccessfull(User user);

        private string username;

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            CancelCommand = new RelayCommand(param =>
            {
                if (CancelEvent != null)
                {
                    CancelEvent();
                }
            });
        }

        public ICommand LoginCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public string Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    username = value;
                }
            }
        }


        public event LoginSuccessfull LoginSuccessfullEvent;
        public event Cancel CancelEvent;

        private void Login(object parameter)
        {
            var context = new UserAccountContext();
            var passwordBox = (PasswordBox)parameter;
            User currentUser = null;
            try
            {
                currentUser = context.Users.First(x => x.Name == username);
                if (currentUser.Password != passwordBox.Password) throw new ArgumentException();
            }
            catch (ArgumentException)
            {
                passwordBox.Clear();
                MessageBox.Show("Login failed!");
            }
            if (LoginSuccessfullEvent != null)
            {
                LoginSuccessfullEvent(currentUser);
            }
        }
    }
}