using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using Entities.Database;
using Entities.Entities;

using MahApps.Metro.Controls.Dialogs;

namespace Client.ViewModel {
    public class LoginViewModel : ViewModelBase {

        private MainWindow Window { get; set; }

        public delegate void LoginSuccessfull( User user );

        public event LoginSuccessfull LoginSuccessfullEvent;

        private string username;

        public LoginViewModel(MainWindow window) {
            Window = window;
            LoginCommand = new RelayCommand(Login);
            CancelCommand = new RelayCommand(param => Window.Close());
        }

        public ICommand LoginCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public string Username {
            get {
                return username;
            }
            set {
                if (value != username) {
                    username = value;
                }
            }
        }

        

        private async void Login( object parameter ) {
            var passwordBox = (PasswordBox)parameter;
            var controller = await Window.ShowProgressAsync("Please wait...", "Login in progress");
            Boolean sucess = await Task<bool>.Run(() => ActualLogin(parameter));
            await controller.CloseAsync();
            if (!sucess) {
                await Window.ShowMessageAsync("Error", "Login has failed...");
                passwordBox.Clear();
            }
           

        }

        private bool ActualLogin(object parameter) {
            var context = new UserAccountContext();
            var passwordBox = (PasswordBox)parameter;
            User currentUser = null;
            try {
                currentUser = context.Users.FirstOrDefault(x => x.Name == username);
                if (currentUser == null) {
                    throw new ArgumentException();
                }
                if (currentUser.Password != passwordBox.Password) {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException) {
                return false;
            }
            if (LoginSuccessfullEvent != null) {
                LoginSuccessfullEvent(currentUser);    
            }
            return true;
        }

    }
}