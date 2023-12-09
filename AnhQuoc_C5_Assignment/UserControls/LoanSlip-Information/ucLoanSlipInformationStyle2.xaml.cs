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
        public Func<Reader> getItem { get; set; }

        public ucLoanSlipInformationStyle2()
        {
            InitializeComponent();
            this.Loaded += UcLoanSlipInformationStyle2_Loaded;
        }

        private void UcLoanSlipInformationStyle2_Loaded(object sender, RoutedEventArgs e)
        {
            ucLoanSlipInformation.getItem = getItem;
        }
    }
}
