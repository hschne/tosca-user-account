using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Client.Tricentis;

using Entities.Entities;

namespace Client.ViewModel {
    public class WorkspacesViewModel : ViewModelBase {

        public WorkspacesViewModel( User user ) {
            User = user.Name;
            ApiInitialized = false;
            Factory = new UserSettingsFactory(user);
            Workspaces = CreateObservableCollection(user.Workspaces);
            DownloadSettingsCommand = new RelayCommand(param => Factory.DownloadSetting());
            UploadSettingsCommand = new RelayCommand(param => Factory.UploadSetting());
        }

        private UserSettingsFactory Factory { get; set; }

        public ICommand DownloadSettingsCommand { get; set; }

        public ICommand UploadSettingsCommand { get; set; }

        public String User { get; set; }

        public bool ApiInitialized { get; set; }

        public ObservableCollection<WorkspaceViewModel> Workspaces { get; set; }

        public void SetApiInitialzed() {
            ApiInitialized = true;
            OnPropertyChanged("ApiInitialized");
        }

        private ObservableCollection<WorkspaceViewModel> CreateObservableCollection( IEnumerable<Workspace> workspaces ) {
            var result = new ObservableCollection<WorkspaceViewModel>();
            var workspaceFactory = new WorkspaceFactory();
            foreach (WorkspaceViewModel viewModel in workspaces.Select(workspace => new WorkspaceViewModel(workspace))) {
                viewModel.ExistsLocally = true;
                if (!workspaceFactory.LocalWorkspaces().Contains(viewModel.Name)) {
                    viewModel.ExistsLocally = false;
                }
                result.Add(viewModel);
            }

            return result;
        }

    }
}