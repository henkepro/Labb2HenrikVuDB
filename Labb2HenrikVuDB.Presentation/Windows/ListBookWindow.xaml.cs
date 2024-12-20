using Labb2HenrikVuDB.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderDetailsWindow.xaml
    /// </summary>
    public partial class ListBookWindow : Window
    {
        private readonly ListBookViewModel _viewModel;
        private readonly MainWindowViewModel _mainViewModel;
        private AddTitleWindow addTitleWindow;
        private AddAuthorWindow addAuthorWindow;
        private UpdateTitleWindow updateTitleWindow;
        private UpdateAuthorWindow updateAuthorWindow;
        internal ListBookWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainViewModel = mainWindowViewModel;
            _viewModel = new ListBookViewModel(mainWindowViewModel)
            {
                ShowAddTitleWindow = NewAddTitleWindow,
                ShowAddAuthorWindow = NewAddAuthorWindow,
                ShowUpdateTitleWindow = NewUpdateTitleWindow,
                ShowUpdateAuthorWindow = NewUpdateAuthorWindow,
            };

            DataContext = _viewModel;
        }

        private void NewAddTitleWindow()
        {
            if(addTitleWindow == null || !addTitleWindow.IsVisible)
            {
                addTitleWindow = new AddTitleWindow(_mainViewModel, _viewModel);
                addTitleWindow.Show();
            }
            else
            {
                addTitleWindow.Activate();
            }
        }
        private void NewAddAuthorWindow()
        {
            if(addAuthorWindow == null || !addAuthorWindow.IsVisible)
            {
                addAuthorWindow = new AddAuthorWindow(_mainViewModel, _viewModel);
                addAuthorWindow.Show();
            }
            else
            {
                addAuthorWindow.Activate();
            }
        }
        private void NewUpdateTitleWindow()
        {
            if(updateTitleWindow == null || !updateTitleWindow.IsVisible)
            {
                updateTitleWindow = new UpdateTitleWindow(_mainViewModel, _viewModel);
                updateTitleWindow.Show();
            }
            else
            {
                updateTitleWindow.Activate();
            }
        }
        private void NewUpdateAuthorWindow()
        {
            if(updateAuthorWindow == null || !updateAuthorWindow.IsVisible)
            {
                updateAuthorWindow = new UpdateAuthorWindow(_mainViewModel, _viewModel);
                updateAuthorWindow.Show();
            }
            else
            {
                updateAuthorWindow.Activate();
            }
        }
    }
}
