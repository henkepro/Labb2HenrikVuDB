
using Labb2HenrikVuDB.Domain;
using Labb2HenrikVuDB.Infrastructure.Data.Model;
using Labb2HenrikVuDB.Presentation.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2HenrikVuDB.Presentation.ViewModels;

internal class ListBookViewModel : ViewModelBase
{
    private readonly MainWindowViewModel mainWindowViewModel;
    public DelegateCommand OpenAddTitleOnCommand { get; }
    public DelegateCommand OpenAddAuthorOnCommand { get; }
    public DelegateCommand OpenUpdateTitleOnCommand { get; }
    public DelegateCommand OpenUpdateAuthorWindow { get; }
    public ObservableCollection<Butiker> Stores { get => mainWindowViewModel.Stores; set => mainWindowViewModel.Stores = value; }
    public Butiker SelectedStore { get => mainWindowViewModel.SelectedStore; set => mainWindowViewModel.SelectedStore = value; }
    public ObservableCollection<Böcker> Books { get; set; }
    public DelegateCommand AddAmountOfBooksOnCommand { get; }
    public DelegateCommand RemoveAmountofBooksOnCommand { get; }
    private Böcker _selectedBook;
    public Böcker SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            RaisePropertyChanged();
            AddAmountOfBooksOnCommand.RaiseCanExecuteChanged();
            RemoveAmountofBooksOnCommand.RaiseCanExecuteChanged();
            OpenAddAuthorOnCommand.RaiseCanExecuteChanged();
        }
    }
    private int _amountofBooks = 1;
    public int AmountOfBooks
    {
        get => _amountofBooks;
        set
        {
            _amountofBooks = value;
            RaisePropertyChanged();
        }
    }
    public Action ShowAddTitleWindow { get; set; }
    public Action ShowAddAuthorWindow { get; set; }
    public Action ShowUpdateTitleWindow { get; set; }
    public Action ShowUpdateAuthorWindow { get; set; }
    public ListBookViewModel(){}
    public ListBookViewModel(MainWindowViewModel mainWindowViewModel)
    {
        this.mainWindowViewModel = mainWindowViewModel;
        OpenAddTitleOnCommand = new DelegateCommand(OpenAddTitle);
        OpenAddAuthorOnCommand = new DelegateCommand(OpenAddAuthor, CanOpenAddAuthor);
        OpenUpdateTitleOnCommand = new DelegateCommand(OpenUpdateTitle);
        OpenUpdateAuthorWindow = new DelegateCommand(OpenUpdateAuthor);
        AddAmountOfBooksOnCommand = new DelegateCommand(AddAmountOfBooks, CanAddBook);
        RemoveAmountofBooksOnCommand = new DelegateCommand(RemoveAmountofBooks, CanRemoveBook);

        LoadBookList();
    }

    private void OpenAddTitle(object obj) => ShowAddTitleWindow();
    private void OpenAddAuthor(object obj) => ShowAddAuthorWindow();
    private void OpenUpdateTitle(object obj) => ShowUpdateTitleWindow();
    private void OpenUpdateAuthor(object obj) => ShowUpdateAuthorWindow();
    private bool CanOpenAddAuthor(object? arg) => SelectedBook is not null;
    private bool CanAddBook(object? arg) => SelectedBook is not null;
    private bool CanRemoveBook(object? arg) => SelectedBook is not null;

    private void AddAmountOfBooks(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        var addBookToStore = db.LagerSaldo
            .FirstOrDefault(s => s.Isbn == SelectedBook.Isbn13 && s.ButikId == SelectedStore.Id);

        if(addBookToStore != null)
        {
            addBookToStore.Antal += AmountOfBooks;
        }
        else
        {
            var addNewBookToStore = new LagerSaldo()
            {
                Isbn = SelectedBook.Isbn13,
                Antal = AmountOfBooks,
                ButikId = SelectedStore.Id
            };

            db.LagerSaldo.Add(addNewBookToStore);
        }

        db.SaveChanges();

        mainWindowViewModel.SelectedStore = SelectedStore;
    }
    private void RemoveAmountofBooks(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        var removeBooksFromStore = db.LagerSaldo
            .FirstOrDefault(s => s.Isbn == SelectedBook.Isbn13 && s.ButikId == SelectedStore.Id);
        if(removeBooksFromStore != null)
        {

            if(AmountOfBooks >= removeBooksFromStore.Antal)
            {
                db.LagerSaldo.Remove(removeBooksFromStore);
            } 
            else
            {
                removeBooksFromStore.Antal -= AmountOfBooks;
            }

            db.SaveChanges();
        } else { Debug.WriteLine("removeBooksFromStore is null."); }

        mainWindowViewModel.SelectedStore = SelectedStore;
    }
    public void LoadBookList()
    {
        using var db = new Labb1HenrikVuContext();
        Books = new ObservableCollection<Böcker>(
            db.Böcker
            .Include(o => o.Författare)
            .Select(o => new Böcker()
            {
                Isbn13 = o.Isbn13,
                Title = o.Titel,
                Language = o.Språk,
                Author = string.Join(", ", o.Författare.Select(f => f.Förnamn)),
                Price = o.Pris,
                Release_date = o.Utgivningsdatum,
            }).ToList()
        );
        RaisePropertyChanged("Books");
    }
}
public class Böcker
{
    public string Isbn13 { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public DateOnly Release_date { get; set; }
}
