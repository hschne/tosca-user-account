using System.Collections.ObjectModel;

namespace Entities.Entities {
    public class User {

        public int ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public virtual Collection<Workspace> Workspaces { get; set; }

        public virtual UserSetting UserSetting { get; set; }

    }
}
