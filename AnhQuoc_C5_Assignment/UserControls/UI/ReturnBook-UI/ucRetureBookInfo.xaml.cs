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
using System.Windows.Threading;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucRetureBookInfo.xaml
    /// </summary>
    public partial class ucRetureBookInfo : UserControl
    {
        public ucRetureBookInfo()
        {
            InitializeComponent();

            #region SetTextBoxMaxLength

            #endregion

            #region LostFocus
            #endregion
           
            var context = MainWindow.returnBookContext;
            this.DataContext = context;
        }
        
        public bool IsValidationGetHasError()
        {
            return false;
        }

        public void RunAllValidations()
        {
            BindingExpression be = null;
        }
    }
}
