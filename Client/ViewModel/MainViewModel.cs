using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Entities.Database;
using Entities.Entities;

using MahApps.Metro.Controls.Dialogs;

using Tricentis.TCAPI;

namespace Client.ViewModel {
    public class MainViewModel : ViewModelBase {

        public delegate void ApiInitialized();

        private ViewModelBase currentViewModel;

        public MainViewModel( MainWindow window ) {
            Window = window;
            InitializeApi();
            Database.SetInitializer(new UserAccountInitializer());
            ChangeToLoginView();
        }

        private MainWindow Window { get; set; }

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

        public event ApiInitialized ApiInitializedEvent;

        ~MainViewModel() {
            TCAPI.Instance.Close();
        }

        private void ChangeToLoginView() {
            var loginViewModel = new LoginViewModel(Window);
            loginViewModel.LoginSuccessfullEvent += ChangeToWorkspacesView; ;
            CurrentViewModel = loginViewModel;
        }

        private void ChangeToWorkspacesView( User user ) {
            WorkspacesViewModel workspacesViewModel = new WorkspacesViewModel(user);
            if (TCAPI.Instance != null) {
                workspacesViewModel.ApiInitialized = true;
            }
            ApiInitializedEvent += () => { workspacesViewModel.SetApiInitialzed(); };
            CurrentViewModel = workspacesViewModel;
        }

        public void InitializeApi() {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate { TCAPI.CreateInstance(); };
            worker.RunWorkerCompleted += delegate {
                if (ApiInitializedEvent != null) {
                    ApiInitializedEvent();
                }
            };
            worker.RunWorkerAsync();
        }
    }
}