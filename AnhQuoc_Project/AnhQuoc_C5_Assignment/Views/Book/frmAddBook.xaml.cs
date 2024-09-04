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
    /// Interaction logic for frmAddBook.xaml
    /// </summary>
    public partial class frmAddBook : Window
    {
        #region getDatas
        public Func<BookDto> getItemToUpdate { get; set; }

        public Func<int> getIdBook { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddBookViewModel Context;

        public frmAddBook()
        {
            InitializeComponent();

            this.Loaded += FrmAddBook_Loaded;
            Context = new AddBookViewModel();

            txtPrice.TextChanged += TxtPrice_TextChanged;
            txtQuantity.TextChanged += TxtQuantity_TextChanged;
            this.DataContext = Context;
        }

        private void FrmAddBook_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }

        private void TxtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            Context.TxtQuantity_TextChanged(sender, e);
        }

        private void TxtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            Context.TxtPrice_TextChanged(sender, e);
        }

        private void ButtonExpand_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = LogicalTreeHelper.GetParent((sender as Button)) as Grid;
            TextBox txt = LogicalTreeHelper.GetChildren(grid).ToTypedCollection<List<DependencyObject>, DependencyObject>()[0] as TextBox;
            Utilitys.ExpandMore(txt, false);
        }
    }
}
