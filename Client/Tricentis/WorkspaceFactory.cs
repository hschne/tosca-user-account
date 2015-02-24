using System.ComponentModel;
using System.IO;

using Entities.Entities;

using Tricentis.TCAPI;

namespace Client.Tricentis {
    public class WorkspaceFactory {

        private TCAPI Tcapi { get; set; }

        private string WorkspaceDirectory { get; set; }

        public WorkspaceFactory() {
            WorkspaceDirectory = Properties.Settings.Default.WorkspaceDirectory;
            Tcapi = TCAPI.Instance;
        }

        public void CreateWorkspace( Workspace workspace ) {
            string workspaceName = workspace.Name;
            string thisWorkspaceDirectory = WorkspaceDirectory + "/" + workspaceName;
            Directory.CreateDirectory(thisWorkspaceDirectory);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate {
                Tcapi.CreateMultiuserWorkspaceWithSQLServerCommon(thisWorkspaceDirectory, workspace.ConnectionString);
                Tcapi.CloseWorkspace();
            };
            worker.RunWorkerAsync();
        }

    }
}