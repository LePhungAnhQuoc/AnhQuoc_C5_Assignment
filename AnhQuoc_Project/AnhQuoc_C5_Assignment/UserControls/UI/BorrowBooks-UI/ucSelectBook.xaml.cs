using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment.UserControls.UI.BorrowBooks_UI
{
    /// <summary>
    /// Interaction logic for ucSelectBook.xaml
    /// </summary>
    public partial class ucSelectBook : UserControl, INotifyPropertyChanged
    {


        public ucAddLoan UcAddLoan
        {
            get { return (ucAddLoan)GetValue(UcAddLoanProperty); }
            set { SetValue(UcAddLoanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UcAddLoan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UcAddLoanProperty =
            DependencyProperty.Register(nameof(UcAddLoan), typeof(ucAddLoan), typeof(ucSelectBook), new PropertyMetadata(null));



        private ucBooksTable ucBooksTable;
        private frmDefault selectBookForm;

        // Routed event to notify parent when book detail confirm should be executed
        public static readonly RoutedEvent BookDetailConfirmEvent =
    EventManager.RegisterRoutedEvent(
        "BookDetailConfirm",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(ucSelectBook));

        // CLR event wrapper
        public event RoutedEventHandler BookDetailConfirm
        {
            add { AddHandler(BookDetailConfirmEvent, value); }
            remove { RemoveHandler(BookDetailConfirmEvent, value); }
        }

        public static readonly RoutedEvent BookInfoBtnConfirmEvent =
  EventManager.RegisterRoutedEvent(
      "BookInfoBtnConfirm",
      RoutingStrategy.Bubble,
      typeof(RoutedEventHandler),
      typeof(ucSelectBook));

        // CLR event wrapper
        public event RoutedEventHandler BookInfoBtnConfirm
        {
            add { AddHandler(BookInfoBtnConfirmEvent, value); }
            remove { RemoveHandler(BookInfoBtnConfirmEvent, value); }
        }

        #region Properties
        private ObservableCollection<ucLoanDetailCard> _AllLoanDetailCard;
        public ObservableCollection<ucLoanDetailCard> AllLoanDetailCard
        {
            get { return _AllLoanDetailCard; }
            set 
            {
                _AllLoanDetailCard = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BookISBNDto> _AllBookISBN;
        public ObservableCollection<BookISBNDto> AllBookISBN
        {
            get { return _AllBookISBN; }
            set 
            { 
                _AllBookISBN = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BookDto> _BookByISBN;
        public ObservableCollection<BookDto> BookByISBN
        {
            get { return _BookByISBN; }
            set 
            { 
                _BookByISBN = value;
                OnPropertyChanged();
            }
        }

        private BookDto _SelectedBook;
        public BookDto SelectedBook
        {
            get { return _SelectedBook; }
            set 
            { 
                _SelectedBook = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion



        public ucSelectBook()
        {
            InitializeComponent();
            this.DataContext = this;

            AllLoanDetailCard = new ObservableCollection<ucLoanDetailCard>();
            ucBookISBNsTable.GetParent = this;
        }

        public void BookISBNsTable_SelectionChanged(BookISBNDto selectedItem)
        {
            if (selectedItem != null)
            {
                BookViewModel bookViewModel = UnitOfViewModel.Instance.BookViewModel;
                BookMap bookMap = UnitOfMap.Instance.BookMap;

                var books = bookViewModel.FillByBookISBN(selectedItem.ISBN, true);
                var bookDtos = bookMap.ConvertToDto(books);
                BookByISBN = bookDtos;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var args = new RoutedEventArgs(BookInfoBtnConfirmEvent, this);
            RaiseEvent(args);
        }

        private void SelectBookConfirm_Click(object sender, RoutedEventArgs e)
        {
            SelectedBook = ucBooksTable.SelectedDto;

            if (SelectedBook == null)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("book"));
                return;
            }

            // Kiểm tra tình trạng cuốn sách
            if (!SelectedBook.Status)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyBookStatus());
                return;
            }

            if (SelectedBook.IdBookStatus == Constants.bookStatusSpoil)
            {
                Utilitys.ShowMessageBox1("This book cannot be borrowed because the book is spoiled");
                return;
            }

            var args = new BookDetailConfirmEventArgs(BookDetailConfirmEvent, this, SelectedBook, AllLoanDetailCard);
            RaiseEvent(args);

            if (selectBookForm != null)
            {
                selectBookForm.Close();
            }
        }

        // Parent handler for child DataGrid mouse double-click
        public void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ucBooksTable = new ucBooksTable();
            ucBooksTable.getExceptProperties = () => Constants.exceptDtgBookCreateLoanSlip;
            ucBooksTable.AllowPagination = false;
            ucBooksTable.Books = BookByISBN;
            ucBooksTable.Height = 300;

            Button confirm = new Button();
            confirm.Style = (Style)Application.Current.FindResource("btnConfirm");
            Button cancel = new Button();
            cancel.Style = (Style)Application.Current.FindResource("btnCancel");

            selectBookForm = new frmDefault();
            selectBookForm.lblHeader = "Select Book information";

            selectBookForm.stkBody.Children.Add(ucBooksTable);
            selectBookForm.stkWrapButton.Children.Add(confirm);
            selectBookForm.stkWrapButton.Children.Add(cancel);

            confirm.Click += SelectBookConfirm_Click;
            cancel.Click += (s, args) =>
            {
                if (selectBookForm != null)
                {
                    selectBookForm.Close();
                }
            };
            selectBookForm.ShowDialog();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            UcAddLoan?.getParentUc?.Invoke().GoBack();
        }
    }

    public class BookDetailConfirmEventArgs : RoutedEventArgs
    {
        public BookDto SelectedBook { get; }
        public ObservableCollection<ucLoanDetailCard> AllLoanDetailCard { get; set; }

        public BookDetailConfirmEventArgs(RoutedEvent routedEvent, object source, BookDto selectedBook, ObservableCollection<ucLoanDetailCard> allLoanDetailCard)
            : base(routedEvent, source)
        {
            SelectedBook = selectedBook;
            AllLoanDetailCard = allLoanDetailCard;
        }
    }
}
