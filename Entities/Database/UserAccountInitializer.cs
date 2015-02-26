using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;

using Entities.Entities;

namespace Entities.Database {
    public class UserAccountInitializer : DropCreateDatabaseAlways<UserAccountContext> {

        public override void InitializeDatabase( UserAccountContext context ) {
            base.InitializeDatabase(context);
            User user1 = new User {
                Name = "Alice",
                Password = "Alice",
                UserSetting = new UserSetting { Name = "Settings.xml" },
                Workspaces =
                    new Collection<Workspace> {
                        new Workspace {
                            Name = "TeamSoa",
                            Type = "sql",
                            ConnectionString = "Server=mssqlserv1.tricentis.com;Database=SOA;Uid=SOA_User;Pwd=fk.N2#5Q"
                        }
                    }
            };
            User user2 = new User() {
                Name = "Bill",
                Password = "Bill",
                UserSetting = new UserSetting { Name = "Settings.xml" },
                Workspaces =
                    new Collection<Workspace> {
                        new Workspace {
                            Name = "Demo1",
                            Type = "sqlite",
                            ConnectionString = @"C:\Tosca_Projects\Tosca_CommonRepositories\Demo1\CommonRepository\"
                        },
                        new Workspace {
                            Name = "Demo2",
                            Type = "sqlite",
                            ConnectionString = @"C:\Tosca_Projects\Tosca_CommonRepositories\Demo2\CommonRepository\"
                        }
                    }
                     
            };
            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();
        }

    }
}