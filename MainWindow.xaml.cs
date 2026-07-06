using System.Windows;
using WPF_TaskManager.ViewModels;

namespace WPF_TaskManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
