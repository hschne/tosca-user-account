using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using Entities.Entities;

namespace Entities.Database {
    public class UserAccountInitializer : CreateDatabaseIfNotExists<UserAccountContext> {

        public override void InitializeDatabase( UserAccountContext context ) {
            base.InitializeDatabase(context);
            User user1 = context.Users.FirstOrDefault(x => x.Name == "Alice");
            if (user1 == null) {
                user1 = new User {
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
                context.Users.AddOrUpdate(user1);
            }
            User user2 = context.Users.FirstOrDefault(x => x.Name == "Bill");
            if (user2 == null) {
                user2 = new User() {
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
                    },
                            
                    
                };
            context.Users.AddOrUpdate(user2);
            }
            context.SaveChanges();
        }

    }
}