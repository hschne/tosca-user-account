using System;
using System.Linq;
using System.Windows;
using Entities.Database;
using Entities.Entities;

namespace Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestConnection();
        }

        public void TestConnection()
        {
            UserAccountContext context = new UserAccountContext();
            User user = context.Users.First();
            Console.WriteLine(user.Name);
        }
    }
}