using System;
using System.Linq;
using System.Windows;
using Client.ViewModel;
using Entities.Database;
using Entities.Entities;

namespace Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }

        public void TestConnection()
        {
            UserAccountContext context = new UserAccountContext();
            User user = context.Users.First();
            Console.WriteLine(user.Name);
        }
    }
}