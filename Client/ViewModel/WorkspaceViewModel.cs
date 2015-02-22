using System.Windows;
using System.Windows.Input;
using Entities.Entities;

namespace Client.ViewModel
{
    public class WorkspaceViewModel : ViewModelBase
    {
        public WorkspaceViewModel(Workspace workspace)
        {
            Workspace = workspace;
            DownloadWorkspaceCommand = new RelayCommand(param => CreateWorkspaceOnDisk());
        }

        private Workspace Workspace { get; set; }

        public ICommand DownloadWorkspaceCommand { get; set; }

        public string Name
        {
            get { return Workspace.Name; }
        }

        public string ConnectionString
        {
            get { return Workspace.ConnectionString; }
        }

        private void CreateWorkspaceOnDisk()
        {
            MessageBox.Show("Workspace Created!");
        }
    }
}