using System.Data.Entity;
using System.Windows;

using Entities.Database;
using Entities.Entities;

using Tricentis.TCAPI;

namespace Client.ViewModel {
    public class MainViewModel : ViewModelBase {

        public MainViewModel( Window window ) {
            Window = window;
            TCAPI.CreateInstance();
            Database.SetInitializer(new UserAccountInitializer());
            ChangeToLoginView();
        }

        ~MainViewModel() {
            TCAPI.Instance.Close();
        }

        private Window Window { get; set; }

        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel {
            get {
                return currentViewModel;
            }
            set {
                if (currentViewModel != value) {
                    currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ChangeToLoginView() {
            var loginViewModel = new LoginViewModel();
            loginViewModel.CancelEvent += Window.Close;
            loginViewModel.LoginSuccessfullEvent += ChangeToWorkspacesView;
            CurrentViewModel = loginViewModel;
        }

        private void ChangeToWorkspacesView( User user ) {
            CurrentViewModel = new WorkspacesViewModel(user);
        }

    }
}