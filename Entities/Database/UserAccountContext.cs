using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Entities.Entities;

namespace Entities.Database {
    public class UserAccountContext : DbContext{

        public UserAccountContext() : base("UserAccountConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
