using Labb2HenrikVuDB.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Labb2HenrikVuDB.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for UpdateTitleWindow.xaml
    /// </summary>
    public partial class UpdateTitleWindow : Window
    {
        private readonly ConfigurationViewModel _viewModel;
        internal UpdateTitleWindow(MainWindowViewModel mainWindowViewModel, ListBookViewModel ListBookViewModel)
        {
            InitializeComponent();

            DataContext = _viewModel = new ConfigurationViewModel(mainWindowViewModel, ListBookViewModel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
