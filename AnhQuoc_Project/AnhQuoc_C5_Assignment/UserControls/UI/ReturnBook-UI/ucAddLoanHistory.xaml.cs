using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucAddLoanHistory.xaml
    /// </summary>
    public partial class ucAddLoanHistory : UserControl
    {
        public ReturnBookViewModel Context;

        #region getDatas
        public Func<ucLoanHistoryManagement> getParentUc { get; set; }

        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<LoanDetailRepository> getLoanDetailRepo { get; set; }
        public Func<LoanHistoryRepository> getLoanHistoryRepo { get; set; }
        public Func<LoanDetailHistoryRepository> getLoanDetailHistoryRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<PenaltyReasonRepository> getReasonRepo { get; set; }
        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion
        
        public ucAddLoanHistory()
        {
            InitializeComponent();

            #region SetTextBoxMaxLength

            #endregion

            #region LostFocus
            #endregion

            MainWindow.returnBookContext = new ReturnBookViewModel();
            Context = MainWindow.returnBookContext;
            
            this.DataContext = Context;
        }
    }
}
