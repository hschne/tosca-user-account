using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities.Entities;

namespace Client.ViewModel
{
    public class WorkspacesViewModel : ViewModelBase
    {
        public WorkspacesViewModel(User user)
        {
            User = user.Name;
            Workspaces = CreateObservableCollection(user.Workspaces);
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