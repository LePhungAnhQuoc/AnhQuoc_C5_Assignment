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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmChildFunctionInformation.xaml
    /// </summary>
    public partial class frmChildFunctionInformation : Window
    {
        #region getDatas
        public Func<FunctionDto> getParent { get; set; }
        public Func<FunctionDto> getChild { get; set; }
        #endregion

        public frmChildFunctionInformation()
        {
            InitializeComponent();

            this.DataContext = this;
            this.Loaded += FrmChildFunctionInformation_Loaded;
        }

        private void FrmChildFunctionInformation_Loaded(object sender, RoutedEventArgs e)
        {
            ucParentInformation.lblTitle.Content = "Parent Function";
            ucParentInformation.getItem = getParent;
            ucChildInformation.lblTitle.Content = "Child Function";
            ucChildInformation.getItem = getChild;
        }
    }
}
