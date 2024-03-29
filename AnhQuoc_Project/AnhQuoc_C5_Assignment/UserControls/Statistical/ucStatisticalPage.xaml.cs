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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucStatisticalPage.xaml
    /// </summary>
    public partial class ucStatisticalPage : UserControl
    {
        private BookViewModel bookVM;
        private LoanSlipViewModel loanSlipVM;
        private LoanHistoryViewModel loanHistoryVM;
        private LoanDetailViewModel loanDetailVM;
        private BookISBNViewModel bookISBNVM;

        public ucStatisticalPage()
        {
            InitializeComponent();
            bookVM = UnitOfViewModel.Instance.BookViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;

            ucBookQuantities.Content = bookVM.Repo.Count.ToString();
            ucLoanQuantitiesLastYear.Content = loanSlipVM.FillByLastYear().Count.ToString();

            var loanSlipFind = loanSlipVM.FindByMostQuantity();
            ucBookQuantitiesMostOfReader.Content = loanSlipFind != null ? loanSlipFind.Quantity.ToString() : 0.ToString();

            var borrowBooks = bookVM.GetBooksInLoanSlips(loanSlipVM.Repo.Gets());
            ucBorrowedBooks.Content = borrowBooks.Count.ToString();

            var reserveBooks = bookVM.GetBooksInLibrary();
            ucReserveBooks.Content = reserveBooks.Count.ToString();

            ucLoanQuantities.Content = loanSlipVM.Repo.Gets().Count.ToString();
            ucLoanHistoryQuantities.Content = loanHistoryVM.Repo.Count.ToString();

            ucLoanSlipExp.Content = loanSlipVM.FillExpire().Count.ToString();

            var isbnCheck = bookISBNVM.MostOfISBN(loanSlipVM.Repo.Gets());
            ucMostBorrowedISBN.Content = isbnCheck == null ? "None" : isbnCheck.ISBN;

            ucDamagedBooks.Content = bookVM.FillByIdBookStatus(bookVM.Repo.Gets(), Constants.bookStatusSpoil).Count.ToString();

            ucLostBooks.Content = bookVM.FillByIdBookStatus(bookVM.Repo.Gets(), Constants.bookStatusLost).Count.ToString();


            foreach (object card in LogicalTreeHelper.GetChildren(wrapContainer))
            {
                var getCard = card as ucStatisticalCard;
                if (getCard != null)
                {
                    getCard.MouseDoubleClick += Card_MouseDoubleClick;
                }
            }
        }

        private void Card_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ucStatisticalCard card = sender as ucStatisticalCard;

            frmStatisticalDescription frmDescription = new frmStatisticalDescription();

            frmDescription.Icon = card.Icon;
            frmDescription.Description = card.ToolTip.ToString();
            frmDescription.Value = card.Content;

            if (card.Name == "ucMostBorrowedISBN")
            {
                frmDescription.TextFontSize = 15.0;
            }
            frmDescription.Show();
        }
    }
}
