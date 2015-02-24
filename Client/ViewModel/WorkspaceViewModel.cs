using System.Windows.Input;

using Client.Tricentis;

using Entities.Entities;

namespace Client.ViewModel {
    public class WorkspaceViewModel : ViewModelBase {

        private WorkspaceFactory WorkspaceFactory { get; set; }

        public WorkspaceViewModel( Workspace workspace ) {
            Workspace = workspace;
            WorkspaceFactory = new WorkspaceFactory();
            WorkspaceFactory.ActionFinishedEvent += () => { ProcessingAction = false; };
            DownloadWorkspaceCommand = new RelayCommand(param => CreateWorkspaceOnDisk());
        }

        public bool WorkspaceExists {
            get {
                return WorkspaceFactory.WorkspaceExists(Workspace.Name);
            }
        }

        public bool ProcessingAction { get; set; }

        private Workspace Workspace { get; set; }

        public ICommand DownloadWorkspaceCommand { get; set; }

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