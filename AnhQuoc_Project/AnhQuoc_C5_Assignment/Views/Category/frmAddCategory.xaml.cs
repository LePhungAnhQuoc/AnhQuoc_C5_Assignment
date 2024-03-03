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
    /// Interaction logic for frmAddCategory.xaml
    /// </summary>
    public partial class frmAddCategory : Window
    {
        #region getDatas
        public Func<CategoryDto> getItemToUpdate { get; set; }

        public Func<string> getIdCategory { get; set; }
        public Func<CategoryRepository> getCategoryRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddCategoryViewModel Context;

        public frmAddCategory()
        {
            InitializeComponent();
            
            
            Context = new AddCategoryViewModel();

            this.Loaded += FrmAddCategory_Loaded;
            this.DataContext = Context;
        }

        private void FrmAddCategory_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }
    }
}
