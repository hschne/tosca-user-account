using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Client.Annotations;
using Client.ViewModel;
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
            DataContext = new MainViewModel(this);
        }

    }
}