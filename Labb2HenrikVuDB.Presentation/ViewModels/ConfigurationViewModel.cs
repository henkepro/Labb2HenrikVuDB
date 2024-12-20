using Labb2HenrikVuDB.Domain;
using Labb2HenrikVuDB.Infrastructure.Data.Model;
using Labb2HenrikVuDB.Presentation.Command;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Labb2HenrikVuDB.Presentation.ViewModels;

class ConfigurationViewModel : ViewModelBase
{
    private readonly MainWindowViewModel mainWindowViewModel;
    private readonly ListBookViewModel ListBookViewModel;
    public DelegateCommand AddNewTitleOnCommand { get; }
    public DelegateCommand AddNewAuthorOnCommand { get; }
    public DelegateCommand RemoveTitleOnCommand { get; }
    public DelegateCommand UpdateTitleOnCommand { get; }
    public DelegateCommand RemoveAuthorOnCommand { get; }
    public DelegateCommand UpdateAuthorOnCommand { get; }
    public ObservableCollection<NewBook> ActiveBookList { get; set; }
    public ObservableCollection<Labb2HenrikVuDB.Domain.Böcker> BookList { get; set; }
    public ObservableCollection<Författare> Author { get; set; }
    private Författare _selectedAuthor;
    public Författare SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            _selectedAuthor = value;
            LoadTitles();
            RaisePropertyChanged();
            RaisePropertyChanged("BookList");
        }
    }
    private Labb2HenrikVuDB.Domain.Författare _newAuthor;
    public Labb2HenrikVuDB.Domain.Författare NewAuthor
    {
        get => _newAuthor;
        set
        {
            _newAuthor = value;
            RaisePropertyChanged();
        }
    }

    private Labb2HenrikVuDB.Domain.Böcker _selectedBook;
    public Labb2HenrikVuDB.Domain.Böcker SelectedBook
    {
        get => _selectedBook;
        set
        {
            _selectedBook = value;
            RaisePropertyChanged();
            UpdateTitleOnCommand.RaiseCanExecuteChanged();
            RemoveTitleOnCommand.RaiseCanExecuteChanged();
        }
    }
    public ConfigurationViewModel() { }
    public ConfigurationViewModel(MainWindowViewModel mainWindowViewModel, ListBookViewModel ListBookViewModel)
    {
        ActiveBookList = new ObservableCollection<NewBook>();
        ActiveBookList.Add(new NewBook() { Release_date = DateOnly.FromDateTime(DateTime.Now) });

        AddNewTitleOnCommand = new DelegateCommand(AddNewTitle);
        AddNewAuthorOnCommand = new DelegateCommand(AddNewAuthor);

        RemoveTitleOnCommand = new DelegateCommand(RemoveTitle, CanRemoveTitle);
        UpdateTitleOnCommand = new DelegateCommand(UpdateTitle, CanUpdateTitle);

        RemoveAuthorOnCommand = new DelegateCommand(RemoveAuthor);
        UpdateAuthorOnCommand = new DelegateCommand(UpdateAuthor);

        this.mainWindowViewModel = mainWindowViewModel;
        this.ListBookViewModel = ListBookViewModel;
        LoadAuthor();
    }

    private bool CanUpdateTitle(object? arg) => SelectedBook is not null;
    private bool CanRemoveTitle(object? arg) => SelectedBook is not null;
    private void AddNewAuthor(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        if(CheckLength())
        {
            var selectedBook = db.Böcker
                .Where(o => o.Isbn13 == ListBookViewModel.SelectedBook.Isbn13)
                .FirstOrDefault();

            var newAuthor = new Labb2HenrikVuDB.Domain.Författare()
            {
                Förnamn = NewAuthor.Förnamn,
                Efternamn = NewAuthor.Efternamn,
                Födelsedatum = NewAuthor.Födelsedatum,
                Dödsdatum = null,
            };

            selectedBook.Författare = new List<Författare>() { newAuthor };

            db.SaveChanges();

            ListBookViewModel.LoadBookList();
        }

        bool CheckLength()
        {
            bool isStringEmpty = string.IsNullOrWhiteSpace(NewAuthor.Förnamn) ||
                         string.IsNullOrWhiteSpace(NewAuthor.Efternamn) ||
                         NewAuthor.Födelsedatum == default(DateOnly);
            return !isStringEmpty;
        }
    }
    private void UpdateAuthor(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        if(CheckLength())
        {
            var relatedAuthor = db.Författare
                .Where(o => o.Id == SelectedAuthor.Id)
                .FirstOrDefault();
            if(relatedAuthor is not null)
            {

                relatedAuthor.Förnamn = SelectedAuthor.Förnamn;
                relatedAuthor.Efternamn = SelectedAuthor.Efternamn;
                relatedAuthor.Födelsedatum = SelectedAuthor.Födelsedatum;

                db.SaveChanges();
            }
            ListBookViewModel.LoadBookList();
        }

        bool CheckLength()
        {
            bool isStringEmpty = SelectedAuthor.Förnamn.Length < 1 ||
                         SelectedAuthor.Efternamn.Length < 1;
            return !isStringEmpty;
        }
    }
    private void RemoveAuthor(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        var relatedAuthor = db.Författare
            .Where(o => o.Id == SelectedAuthor.Id)
            .FirstOrDefault();
        if(relatedAuthor != null)
        {
            db.Entry(relatedAuthor).Collection(o => o.Isbn13).Load();
            relatedAuthor.Isbn13.Clear();

            db.Författare.Remove(relatedAuthor);

            db.SaveChanges();
        }

        ListBookViewModel.LoadBookList();
    }
    private void AddNewTitle(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        if(CheckAddTitleValues())
        {
            var tempBook = ActiveBookList.FirstOrDefault();
            var tempFörfattare = db.Författare.FirstOrDefault(f => f.Id == SelectedAuthor.Id);
            var newBook = new Labb2HenrikVuDB.Domain.Böcker()
            {
                Isbn13 = tempBook.Isbn13,
                Titel = tempBook.Title,
                Pris = tempBook.Price,
                Språk = tempBook.Language,
                Utgivningsdatum = tempBook.Release_date,
                Författare = new List<Författare>() { tempFörfattare },
            };

            db.Böcker.Add(newBook);

            db.SaveChanges();
            ListBookViewModel.LoadBookList();
        }
    }
    private bool CheckAddTitleValues()
    {
        var tempBook = ActiveBookList.FirstOrDefault();
        if(tempBook == null && ActiveBookList.Count > 0) return false;
        if(!CheckLength()) return false;
        if(!CheckISBN()) return false;
        return true;

        bool CheckLength()
        {
            bool isStringEmpty = string.IsNullOrWhiteSpace(tempBook.Language) ||
                         string.IsNullOrWhiteSpace(tempBook.Title) ||
                         string.IsNullOrWhiteSpace(tempBook.Isbn13) ||
                         tempBook.Price < 1;
            return !isStringEmpty;
        }

        bool CheckISBN()
        {
            bool isDigit = tempBook.Isbn13.All(char.IsDigit);
            bool isLength_13 = tempBook.Isbn13.Length == 13;
            if(isDigit && isLength_13) return true;
            else return false;
        }
    }
    private void UpdateTitle(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        if(CheckLength())
        {
            var relatedBook = db.Böcker
                .Where(b => b.Författare.Any(f => f.Id == SelectedAuthor.Id))
                .Where(o => o.Isbn13 == SelectedBook.Isbn13).FirstOrDefault();
            if(relatedBook is not null)
            {

                relatedBook.Titel = SelectedBook.Titel;
                relatedBook.Språk = SelectedBook.Språk;
                relatedBook.Pris = SelectedBook.Pris;
                relatedBook.Utgivningsdatum = SelectedBook.Utgivningsdatum;

                db.SaveChanges();
            }
            ListBookViewModel.LoadBookList();
        }

        bool CheckLength()
        {
            bool isStringEmpty = SelectedBook.Titel.Length < 1 ||
                         SelectedBook.Språk.Length < 1 ||
                         SelectedBook.Pris < 1;
            return !isStringEmpty;
        }
    }
    private void RemoveTitle(object obj)
    {
        using var db = new Labb1HenrikVuContext();

        var relatedBook = db.Böcker
        .Where(o => o.Författare.Any(f => f.Id == SelectedAuthor.Id))
        .Where(o => o.Isbn13 == SelectedBook.Isbn13)
        .FirstOrDefault();

        if(relatedBook != null)
        {
            db.Entry(relatedBook).Collection(b => b.Författare).Load();
            relatedBook.Författare.Clear();

            db.Böcker.Remove(relatedBook);

            db.SaveChanges();
        }
        ListBookViewModel.LoadBookList();
    }
    private void LoadTitles()
    {
        using var db = new Labb1HenrikVuContext();

        var relatedBooks = db.Böcker
            .Where(b => b.Författare.Any(f => f.Id == SelectedAuthor.Id)).ToList();

        BookList = new ObservableCollection<Labb2HenrikVuDB.Domain.Böcker>(relatedBooks);
    }
    private void LoadAuthor()
    {
        using var db = new Labb1HenrikVuContext();

        Author = new ObservableCollection<Författare>
        (
            db.Författare.ToList()
        );

        SelectedAuthor = Author.FirstOrDefault();
        NewAuthor = new Labb2HenrikVuDB.Domain.Författare() { Födelsedatum = DateOnly.FromDateTime(DateTime.Now) };
    }
}
internal class NewBook : ViewModelBase
{
    public string Isbn13 { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public decimal Price { get; set; }
    public DateOnly Release_date { get; set; }
}
