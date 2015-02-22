using System.Windows;
using Client.ViewModel;

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
            CurrentViewModel = new LoginViewModel();
            DataContext = CurrentViewModel;
        }

        public ViewModelBase CurrentViewModel { get; set; }
    }
}