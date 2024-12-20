using Labb2HenrikVuDB.Presentation.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb2HenrikVuDB.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        private ListBookWindow listBookWindow;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new MainWindowViewModel()
            {
                ShowBookList = NewBookListWindow
            };
        }
        private void NewBookListWindow()
        {
            if(listBookWindow == null || !listBookWindow.IsVisible)
            {
                listBookWindow = new ListBookWindow(viewModel);
                listBookWindow.Show();
            }
            else
            {
                listBookWindow.Activate();
            }
        }
    }
}