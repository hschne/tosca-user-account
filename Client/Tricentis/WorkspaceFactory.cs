using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public bool WorkspaceExists( string workspaceName ) {
            if (Directory.Exists(WorkspaceDirectory))
            {
                IEnumerable<string> directories = Directory.EnumerateDirectories(WorkspaceDirectory);
                if (directories.Contains(Path.Combine(WorkspaceDirectory,workspaceName))) {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<string> LocalWorkspaces() {
            IEnumerable<string> directories = Directory.EnumerateDirectories(WorkspaceDirectory);
            return directories;
        }


        public void CreateWorkspace( Workspace workspace ) {
            Tcapi = TCAPI.Instance;
            string workspaceName = workspace.Name;
            string thisWorkspaceDirectory = WorkspaceDirectory + "\\" + workspaceName;
            Directory.CreateDirectory(thisWorkspaceDirectory);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate {
                if (workspace.Type.ToLowerInvariant() == "sql") {
                    Tcapi.CreateMultiuserWorkspaceWithSQLServerCommon(thisWorkspaceDirectory, workspace.ConnectionString);
                }
                else if (workspace.Type.ToLowerInvariant() == "sqlite") {
                    Tcapi.CreateMultiuserWorkspaceWithSQLITECommon(thisWorkspaceDirectory, workspace.ConnectionString,"Admin","");
                }
                Tcapi.CloseWorkspace();
            };
            worker.RunWorkerCompleted += delegate {
                if (ActionFinishedEvent != null) {
                    ActionFinishedEvent();
                }
            };

            worker.RunWorkerAsync();
        }

        public void OpenWorkspace( Workspace workspace ) {
            string workspaceName = workspace.Name;
            string thisWorkspaceDirectory = WorkspaceDirectory + "/" + workspaceName;
            string workspacefile = Path.Combine(thisWorkspaceDirectory, workspace + ".tws");
            Process process = new Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = "cmd";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.Arguments = "/C " + workspacefile;
            // Go
            process.Start();
        }

    }
}