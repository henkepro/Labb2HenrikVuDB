using Labb2HenrikVuDB.Domain;
using Labb2HenrikVuDB.Infrastructure.Data.Model;
using Labb2HenrikVuDB.Presentation.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Labb2HenrikVuDB.Presentation.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<LagerSaldo> InventoryBalance { get; set; }
        public ObservableCollection<Butiker> Stores { get; set; }
        public ListBookViewModel ListBookViewModel { get; set; }
        public DelegateCommand ManageStoreOnCommand { get; }
        private Butiker _selectedStore;
        public Butiker SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;
                LoadInventoryBalance();
                RaisePropertyChanged();
                RaisePropertyChanged("InventoryBalance");
                ListBookViewModel.RaisePropertyChanged("SelectedStore");
            }
        }
        public Action ShowBookList { get; set; }
        public MainWindowViewModel()
        {
            ListBookViewModel = new ListBookViewModel(this);
            ManageStoreOnCommand = new DelegateCommand(ManageStore);

            LoadStores();
        }

        private void ManageStore(object obj) => ShowBookList();
        public void LoadInventoryBalance()
        {
            using var db = new Labb1HenrikVuContext();

            InventoryBalance = new ObservableCollection<LagerSaldo>
            (
                db.LagerSaldo
                .Include(o => o.Butik)
                .Where(o => o.Butik.Namn == SelectedStore.Namn)
                .Select(o => new LagerSaldo()
                {
                    Isbn = o.Isbn,
                    Antal = o.Antal,
                })
                .ToList()
            );

            SelectedStore.LagerSaldo = InventoryBalance;
        }
        private void LoadStores()
        {
            using var db = new Labb1HenrikVuContext();

            Stores = new ObservableCollection<Butiker>
            (
                db.Butiker.ToList()
            );

            SelectedStore = Stores.FirstOrDefault();
        }
    }
}
