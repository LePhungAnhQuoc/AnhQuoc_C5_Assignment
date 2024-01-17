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
    /// Interaction logic for ucLoanDetailHistoryInfo.xaml
    /// </summary>
    public partial class ucLoanDetailHistoryInfo : UserControl, INotifyPropertyChanged
    {
        public Func<LoanDetailHistoryDto> getItem { get; set; }

        private LoanDetailHistoryDto _Item;
        public LoanDetailHistoryDto Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }

        #region Events
        public event RoutedEventHandler btnBackClick;
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

        public ucLoanDetailHistoryInfo()
        {
            InitializeComponent();

            #region Events
            btnBack.Click += BtnBack_Click;
            #endregion

            this.DataContext = this;
            this.Loaded += ucLoanDetailHistoryInformation_Loaded;
        }

        private void ucLoanDetailHistoryInformation_Loaded(object sender, RoutedEventArgs e)
        {
            Item = getItem();

            if (btnBackClick == null)
            {
                stkWrapButton.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            btnBackClick?.Invoke(sender, e);
        }

    }
}
