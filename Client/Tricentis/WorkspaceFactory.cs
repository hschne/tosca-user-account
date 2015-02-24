using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

using Entities.Entities;

using Tricentis.TCAPI;

namespace Client.Tricentis {
    public class WorkspaceFactory {

        private TCAPI Tcapi { get; set; }

        private string WorkspaceDirectory { get; set; }

        public delegate void ActionFinished();

        public event ActionFinished ActionFinishedEvent;

        public WorkspaceFactory() {
            WorkspaceDirectory = Properties.Settings.Default.WorkspaceDirectory;
            Tcapi = TCAPI.Instance;
        }

        public bool WorkspaceExists(string workspaceName) {
            IEnumerable<string> directories = Directory.EnumerateDirectories(WorkspaceDirectory);
            if (directories.Contains(workspaceName)) return true;
            return false;
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
            worker.RunWorkerCompleted += delegate {
                if (ActionFinishedEvent != null) {
                    ActionFinishedEvent();
                }
            };

            worker.RunWorkerAsync();
        }

    }
}