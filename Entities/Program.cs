using System.Collections.ObjectModel;
using System.Linq;
using Entities.Database;
using Entities.Entities;

namespace Entities {
    class Program {
        static void Main(string[] args) {

            UserAccountContext context = new UserAccountContext();
            User user = new User()
            {
                Name = "hschroedl",
                Password = "hellothere",
                UserSetting = new UserSetting()
                {
                    FilePath = @"C:\usersetting.xml"
                },
                Workspaces = new Collection<Workspace>()
                {
                    new Workspace()
                    {
                        ConnectionString = "connectionstring",
                        Name = "My Workspace",
                    }
                }
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
