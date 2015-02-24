using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;

using Entities.Entities;

namespace Entities.Database {
    public class UserAccountInitializer : DropCreateDatabaseAlways<UserAccountContext> {

        public override void InitializeDatabase( UserAccountContext context ) {
            base.InitializeDatabase(context);
            User user = new User {
                Name = "tester1",
                Password = "tester1",
                UserSetting = new UserSetting { Name = "Settings.xml" },
                Workspaces =
                    new Collection<Workspace> {
                        new Workspace {
                            Name = "Team Soa",
                            ConnectionString = "Server=mssqlserv1.tricentis.com;Database=SOA;Uid=SOA_User;Pwd=fk.N2#5Q"
                        }
                    }
            };
            context.Users.AddOrUpdate(user);
            context.SaveChanges();
        }

    }
}