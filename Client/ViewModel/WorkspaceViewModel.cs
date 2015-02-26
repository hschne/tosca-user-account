using System.Windows.Input;

using Client.Tricentis;

using Entities.Entities;

namespace Client.ViewModel {
    public class WorkspaceViewModel : ViewModelBase {


        public WorkspaceViewModel( Workspace workspace ) {
            Workspace = workspace;
            WorkspaceFactory = new WorkspaceFactory();
            WorkspaceFactory.ActionFinishedEvent += () => {
                ProcessingAction = false;
                OnPropertyChanged("CreateWorkspaceEnabled");
                OnPropertyChanged("OpenWorkspaceEnabled");
                ExistsLocally = true;
            };
            DownloadWorkspaceCommand = new RelayCommand(param => CreateWorkspaceOnDisk());
            OpenWorkspaceCommand = new RelayCommand(param => WorkspaceFactory.OpenWorkspace(workspace));
        }

        private WorkspaceFactory WorkspaceFactory { get; set; }

        private Workspace Workspace { get; set; }

        public bool ExistsLocally { get; set; }

        public bool CreateWorkspaceEnabled {
            get {
                return !WorkspaceFactory.WorkspaceExists(Workspace.Name);
            }
        }

        public bool OpenWorkspaceEnabled {
            get {
                return WorkspaceFactory.WorkspaceExists(Workspace.Name);
            }
        }

        public bool ProcessingAction { get; set; }


        public ICommand DownloadWorkspaceCommand { get; set; }

        public ICommand OpenWorkspaceCommand { get; set; }

        public string Name {
            get {
                return Workspace.Name;
            }
        }

        public string ConnectionString {
            get {
                return Workspace.ConnectionString;
            }
        }

        private void CreateWorkspaceOnDisk() {
            WorkspaceFactory.CreateWorkspace(Workspace);
            ProcessingAction = true;
        }

    }
}