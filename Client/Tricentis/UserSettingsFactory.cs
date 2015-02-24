using System;
using System.IO;
using System.Text;

using Entities.Database;
using Entities.Entities;

namespace Client.Tricentis {
    public class UserSettingsFactory {

        private User User { get; set; }

        public UserSettingsFactory( User user ) {
            User = user;
        }

        public void UploadSetting() {
            string directory = Properties.Settings.Default.SettingsDirectory;
            string file = Path.Combine(directory, "Settings.xml");
            string content = File.ReadAllText(file);
            UserAccountContext context = new UserAccountContext();
            User.UserSetting.Content = Encoding.Default.GetBytes(content);
            context.SaveChanges();
        }

        public void DownloadSetting() {
            byte[] content = User.UserSetting.Content;
            if (User.UserSetting != null && content != null) {
                String name = User.UserSetting.Name;
                var settingsContent = Encoding.Default.GetString(User.UserSetting.Content);
                String directory = Properties.Settings.Default.SettingsDirectory;
                string file = Path.Combine(directory, name);
                if (File.Exists(file)) {
                    File.Move(file, file + ".backup");
                }
                File.WriteAllText(file, settingsContent);
            }
        }

    }
}