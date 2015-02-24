using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Entities.Entities;

using Tricentis.TCAPI;

namespace Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel(Window window)
        {
            Window = window;
            TCAPI.CreateInstance();
            //ChangeToLoginView();
            User user = new User() {
                Name = "Hansi",
                Workspaces = new Collection<Workspace>()
                {
                    new Workspace() {Name = "Test", ConnectionString = "Server=mssqlserv1.tricentis.com;Database=SOA;Uid=SOA_User;Pwd=fk.N2#5Q"},
                    new Workspace() {Name = "Worke", ConnectionString = "Something"}
                }
            };
            ChangeToWorkspacesView(user);
        }

        ~MainViewModel() {
            TCAPI.Instance.Close();
        }

        private Window Window { get; set; }

        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                if (currentViewModel != value)
                {
                    currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ChangeToLoginView()
        {
            var loginViewModel = new LoginViewModel();
            loginViewModel.CancelEvent += Window.Close;
            loginViewModel.LoginSuccessfullEvent += ChangeToWorkspacesView;
            CurrentViewModel = loginViewModel;
        }

        private void ChangeToWorkspacesView(User user)
        {
            CurrentViewModel = new WorkspacesViewModel(user);
        }

    }
}