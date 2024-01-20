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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucInputBookInfo.xaml
    /// </summary>
    public partial class ucInputBookInfo : UserControl
    {
        public ucInputBookInfo()
        {
            InitializeComponent();

            #region SetTextBoxMaxLength
            #endregion

            var context = MainWindow.borrowBookContext;
            context.getucInputBookInfo = () => this;
            context.getwrapLoanDetail = () => wrapLoanDetail;
            context.getwrapBookISBN = () => wrapBookISBN;
            context.gettxtInputBookName = () => txtInputBookName;
            context.getgdInputBookName = () => gdInputBookName;
            context.getcbTxtBookName = () => cbTxtBookName;
            
            this.DataContext = context;
        }
    }
}
