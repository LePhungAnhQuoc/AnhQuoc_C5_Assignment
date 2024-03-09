﻿using System;
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
            ucMostBorrowedISBN.Content = bookISBNVM.MostOfISBN(loanSlipVM.Repo.Gets()).ISBN;

            ucDamagedBooks.Content = bookVM.FillByIdBookStatus(bookVM.Repo.Gets(), Constants.bookStatusSpoil).Count.ToString();

            ucLostBooks.Content = bookVM.FillByIdBookStatus(bookVM.Repo.Gets(), Constants.bookStatusLost).Count.ToString();


            foreach (ucStatisticalCard card in LogicalTreeHelper.GetChildren(wrapContainer))
            {
                card.MouseDoubleClick += Card_MouseDoubleClick;
            }
        }

        private void Card_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ucStatisticalCard card = sender as ucStatisticalCard;

            StatisticalDescription frmDescription = new StatisticalDescription();

            frmDescription.Icon = card.Icon;
            frmDescription.Description = card.ToolTip.ToString();
            frmDescription.Value = card.Content;

            frmDescription.Show();
        }
    }
}
