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
    /// Interaction logic for ucLoanSlipInformationStyle2.xaml
    /// </summary>
    public partial class ucLoanSlipInformationStyle2 : UserControl
    {
        public Func<LoanSlipDto> getItem { get; set; }

        #region Prop-Dp
        
        public bool IsDisplayListDetail
        {
            get { return (bool)GetValue(IsDisplayListDetailProperty); }
            set { SetValue(IsDisplayListDetailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDisplayListDetail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDisplayListDetailProperty =
            DependencyProperty.Register("IsDisplayListDetail", typeof(bool), typeof(ucLoanSlipInformationStyle2), new PropertyMetadata(true));
       

        #endregion


        public ucLoanSlipInformationStyle2()
        {
            InitializeComponent();
            this.Loaded += UcLoanSlipInformationStyle2_Loaded;
        }

        private void UcLoanSlipInformationStyle2_Loaded(object sender, RoutedEventArgs e)
        {
            ucLoanSlipInformation.IsDisplayListDetail = IsDisplayListDetail;
            ucLoanSlipInformation.getItem = getItem;
        }
    }
}
