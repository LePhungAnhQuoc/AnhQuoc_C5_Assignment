using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucStatisticalPage.xaml
    /// </summary>
    public partial class ucStatisticalPage : UserControl
    {
        #region ViewModels
        private BookViewModel bookVM;
        private LoanSlipViewModel loanSlipVM;
        private LoanHistoryViewModel loanHistoryVM;
        private LoanDetailViewModel loanDetailVM;
        private BookISBNViewModel bookISBNVM;
        private ReaderViewModel readerVM;
        private BookStatusViewModel bookStatusVM;
        private StatisticalViewModel statisticalVM;
        #endregion

        #region Properties
        public SeriesCollection BookStatusSeries { get; set; }
        public Func<double, string> BookStatusFormatter { get; set; }

        public SeriesCollection LoanExpSeries { get; set; }
        public Func<double, string> LoanExpFormatter { get; set; }

        public SeriesCollection LoanSlipLoanHistorySeries { get; set; }
        public string[] LoanSlipLoanHistoryLabels { get; set; }
        public Func<double, string> LoanSlipLoanHistoryFormatter { get; set; }

        public SeriesCollection BorrowedBookSeries { get; set; }
        public string[] BorrowedBookLabels { get; set; }
        public Func<double, string> BorrowedBookFormatter { get; set; }

        #endregion

        public ucStatisticalPage()
        {
            InitializeComponent();

            #region Init-ViewModels
            statisticalVM = UnitOfViewModel.Instance.StatisticalViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;
            bookStatusVM = UnitOfViewModel.Instance.BookStatusViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            #endregion

            #region Statistical2
            const int indexLoanSlip = 0;
            const int indexLoanHistory = 1;
            var firstColor = this.FindResource("firstColor") as Brush;
            var secondColor = this.FindResource("secondColor") as Brush;
            const int indexBorrowedBooks = 2; var dateLabels = statisticalVM.Repo.Gets().Select(i => i.DateTime.ToString("dd/MM")).ToArray();

            var borrowBooks = bookVM.GetBooksInLoanSlips(loanSlipVM.Repo.Gets());

            var normalBookQty = bookVM.FillByIdBookStatus(bookVM.Repo.Gets(), Constants.bookStatusNormal).Count;
            var lostBooksQty = bookVM.FillByIdBookStatus(bookVM.Repo.Gets(), Constants.bookStatusLost).Count;
            var damageBooksQty = bookVM.FillByIdBookStatus(bookVM.Repo.Gets(), Constants.bookStatusSpoil).Count;

            var bookStatusTypeNames = bookStatusVM.FillName();

            const string formatPercentage = "P0";
            BookStatusFormatter = (value) => value.ToString(formatPercentage);
            BookStatusSeries = new SeriesCollection
            {
                new StackedRowSeries
                {
                    Title = bookStatusTypeNames[0],
                    Values = new ChartValues<int>(){ normalBookQty },
                    StackMode = StackMode.Percentage,
                    DataLabels = true,
                    LabelPoint = p => p.X.ToString("N0"),
                    Fill = this.FindResource("firstColor") as Brush,
                },
                new StackedRowSeries
                {
                    Title = bookStatusTypeNames[1],
                    Values = new ChartValues<int>(){ lostBooksQty },
                    StackMode = StackMode.Percentage,
                    DataLabels = true,
                    LabelPoint = p => p.X.ToString("N0"),
                },
                new StackedRowSeries
                {
                    Title = bookStatusTypeNames[2],
                    Values = new ChartValues<int>(){ damageBooksQty },
                    StackMode = StackMode.Percentage,
                    DataLabels = true,
                    LabelPoint = p => p.X.ToString("N0"),
                },
            };
            
            int countLoanOutOfExpire = loanSlipVM.FillExpire().Count;
            int countLoanNormal = loanSlipVM.Repo.Count - countLoanOutOfExpire;
          
            LoanExpFormatter = (value) => value.ToString(formatPercentage);
            LoanExpSeries = new SeriesCollection
            {
                new StackedRowSeries
                {
                    Title = "Normal",
                    Values = new ChartValues<int>() { countLoanNormal },
                    DataLabels = true,
                    LabelPoint = (p) => p.X.ToString("N0"),
                    StackMode = StackMode.Percentage,
                    Fill = this.FindResource("firstColor") as Brush,
                },
                new StackedRowSeries
                {
                    Title = "Out of expire",
                    Values = new ChartValues<int>() { countLoanOutOfExpire },
                    DataLabels = true,
                    LabelPoint = (p) => p.X.ToString("N0"),
                    StackMode = StackMode.Percentage,
                    Fill = this.FindResource("secondColor") as Brush,
                },
            };

            var loanSlipQtys = statisticalVM.Repo.Gets().Select(i => i.Data.Split('|')[indexLoanSlip]).ToArray();
            var loanHistoryQtys = statisticalVM.Repo.Gets().Select(i => i.Data.Split('|')[indexLoanHistory]).ToArray();
            var borrowedBookQtys = statisticalVM.Repo.Gets().Select(i => i.Data.Split('|')[indexBorrowedBooks]).ToArray();


            LoanSlipLoanHistoryLabels = dateLabels;
            LoanSlipLoanHistoryFormatter = (value) => value.ToString("N0");
            LoanSlipLoanHistorySeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Loan slip",
                    DataLabels = true,
                    Values = new ChartValues<int>(),
                    Fill = this.FindResource("firstColor") as Brush,
                },
                new ColumnSeries
                {
                    Title = "Loan history",
                    DataLabels = true,
                    Values = new ChartValues<int>(),
                    Fill = this.FindResource("secondColor") as Brush,
                },
            };

            loanSlipQtys.ForEach(value =>
            {
                LoanSlipLoanHistorySeries[0].Values.Add(Convert.ToInt32(value));
            });
            loanHistoryQtys.ForEach(value =>
            {
                LoanSlipLoanHistorySeries[1].Values.Add(Convert.ToInt32(value));
            });

            #region get-Gradient
            var linearGradient = new LinearGradientBrush();
            linearGradient.StartPoint = new Point(0.5, 1.1);
            linearGradient.EndPoint = new Point(0.5, 0);
            linearGradient.GradientStops.Add(new GradientStop(Utilitys.GetColorFromBrush(firstColor), 0));
            linearGradient.GradientStops.Add(new GradientStop(Utilitys.GetColorFromBrush(secondColor), 1));
            #endregion

            BorrowedBookLabels = dateLabels;
            BorrowedBookFormatter = (value) => value.ToString("N0");
            BorrowedBookSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = ".",
                    DataLabels = true,
                    Values = new ChartValues<int>(),
                    Fill = linearGradient,
                },
            };
            borrowedBookQtys.ForEach(item =>
            {
                BorrowedBookSeries[0].Values.Add(Convert.ToInt32(item));
            });


            int readerTypeCount = readerVM.FillListByType(ReaderType.Adult).Count;
            pieAdults.Values = new ChartValues<int>() { readerTypeCount };
            readerTypeCount = readerVM.FillListByType(ReaderType.Child).Count;
            pieChilds.Values = new ChartValues<int>() { readerTypeCount };
            #endregion

            #region Old-Statistical
            ucBookQuantities.Content = bookVM.Repo.Count.ToString();
            ucLoanQuantitiesLastYear.Content = loanSlipVM.FillByLastYear().Count.ToString();

            var loanSlipFind = loanSlipVM.FindByMostQuantity();
            ucBookQuantitiesMostOfReader.Content = loanSlipFind != null ? loanSlipFind.Quantity.ToString() : 0.ToString();

            ucBorrowedBooks.Content = borrowBooks.Count.ToString();

            var reserveBooks = bookVM.GetBooksInLibrary();
            ucReserveBooks.Content = reserveBooks.Count.ToString();

            ucLoanQuantities.Content = loanSlipVM.Repo.Gets().Count.ToString();
            ucLoanHistoryQuantities.Content = loanHistoryVM.Repo.Count.ToString();

            ucLoanSlipExp.Content = loanSlipVM.FillExpire().Count.ToString();

            var isbnCheck = bookISBNVM.MostOfISBN(loanSlipVM.Repo.Gets());
            ucMostBorrowedISBN.Content = isbnCheck == null ? "None" : isbnCheck.ISBN;

            ucDamagedBooks.Content = damageBooksQty.ToString();
            ucLostBooks.Content = lostBooksQty.ToString();

            foreach (object card in LogicalTreeHelper.GetChildren(wrapContainer))
            {
                var getCard = card as ucStatisticalCard;
                if (getCard != null)
                {
                    getCard.MouseDoubleClick += Card_MouseDoubleClick;
                }
            }
            #endregion

            this.DataContext = this;
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
