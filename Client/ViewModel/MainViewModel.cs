using System.Collections.ObjectModel;
using Entities.Database;
using Entities.Entities;

namespace Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string testString;

        public MainViewModel()
        {
            var context = new UserAccountContext();
            Users = new ObservableCollection<User>(context.Users);
        }

        public string TestString
        {
            get { return testString; }
            set
            {
                if (value != testString)
                {
                    testString = value;
                }
            }
        }


        public ObservableCollection<User> Users { get; set; }
    }
}