using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;

using Entities.Database;
using Entities.Entities;

using Tricentis.TCAPI;

namespace Client.ViewModel {
    public class MainViewModel : ViewModelBase {

        public delegate void ApiInitialized();

        public event ApiInitialized ApiInitializedEvent;

        public MainViewModel( Window window ) {
            Window = window;
            InitializeApi();
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
            WorkspacesViewModel workspacesViewModel = new WorkspacesViewModel(user);
            ApiInitializedEvent += () => { workspacesViewModel.SetApiInitialzed(); };
            CurrentViewModel = workspacesViewModel;

        }

        public void InitializeApi() {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate {
                TCAPI.CreateInstance();
            };
            worker.RunWorkerCompleted += delegate {
                if (ApiInitializedEvent != null) {
                    ApiInitializedEvent();
                }
            };
            worker.RunWorkerAsync();
        }

    }
}