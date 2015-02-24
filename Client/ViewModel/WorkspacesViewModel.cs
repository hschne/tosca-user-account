using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Client.Tricentis;

using Entities.Entities;

namespace Client.ViewModel
{
    public class WorkspacesViewModel : ViewModelBase
    {
        private UserSettingsFactory Factory { get; set; }

        public ICommand DownloadSettingsCommand { get; set; }

        public ICommand UploadSettingsCommand { get; set; }

        public WorkspacesViewModel(User user)
        {
            User = user.Name;
            Factory = new UserSettingsFactory(user);
            Workspaces = CreateObservableCollection(user.Workspaces);
            DownloadSettingsCommand = new RelayCommand(param => Factory.DownloadSetting() );
            UploadSettingsCommand = new RelayCommand(param => Factory.UploadSetting());
        }

        public String User { get; set; }

        public ObservableCollection<WorkspaceViewModel> Workspaces { get; set; }

        private ObservableCollection<WorkspaceViewModel> CreateObservableCollection(IEnumerable<Workspace> workspaces)
        {
            var result = new ObservableCollection<WorkspaceViewModel>();
            foreach (WorkspaceViewModel viewModel in workspaces.Select(workspace => new WorkspaceViewModel(workspace)))
            {
                result.Add(viewModel);
            }
            return result;
        }
    }
}