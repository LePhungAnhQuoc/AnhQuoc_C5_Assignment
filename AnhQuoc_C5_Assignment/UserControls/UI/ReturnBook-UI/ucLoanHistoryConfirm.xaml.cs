using System;
using System.Collections.Generic;
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

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucLoanHistoryConfirm.xaml
    /// </summary>
    public partial class ucLoanHistoryConfirm : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<LoanHistoryDto> getItem { get; set; }
        public Func<ucAddLoanHistory> getParentUc { get; set; }
        #endregion

        #region Fields
        private ucAddLoanHistory ucAddLoanHistory;
        #endregion

        #region Properties

        private LoanHistoryDto _Item;
        public LoanHistoryDto Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }

        private double _BorrowedDays;
        public double BorrowedDays
        {
            get { return _BorrowedDays; }
            set
            {
                _BorrowedDays = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucLoanHistoryConfirm()
        {
            InitializeComponent();

            this.Loaded += UcLoanHistoryConfirm_Loaded;
            this.DataContext = this;
        }

        private void UcLoanHistoryConfirm_Loaded(object sender, RoutedEventArgs e)
        {
            ucAddLoanHistory = getParentUc();
            BorrowedDays = ucAddLoanHistory.BorrowedDays;
            Item = getItem();
        }
    }
}
