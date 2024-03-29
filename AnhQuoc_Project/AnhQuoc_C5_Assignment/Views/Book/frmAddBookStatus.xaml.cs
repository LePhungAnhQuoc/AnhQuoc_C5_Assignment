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
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmAddBookStatus.xaml
    /// </summary>
    public partial class frmAddBookStatus : Window
    {
        #region getDatas
        public Func<BookStatusDto> getItemToUpdate { get; set; }

        public Func<string> getIdBookStatus { get; set; }
        public Func<BookStatusRepository> getBookStatusRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddBookStatusViewModel<BookStatu, BookStatusDto> Context;
        
        public frmAddBookStatus()
        {
            InitializeComponent();
            Context = new AddBookStatusViewModel<BookStatu, BookStatusDto>();

            this.Loaded += FrmAddBookStatus_Loaded;
            this.DataContext = Context;
        }

        private void FrmAddBookStatus_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }

        private void ButtonExpand_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = LogicalTreeHelper.GetParent((sender as Button)) as Grid;
            TextBox txt = LogicalTreeHelper.GetChildren(grid).ToTypedCollection<List<DependencyObject>, DependencyObject>()[0] as TextBox;
            Utilities.ExpandMore(txt, false);
        }
    }
}
