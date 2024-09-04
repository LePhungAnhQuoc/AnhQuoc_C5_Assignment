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
    /// Interaction logic for frmAddTranslator.xaml
    /// </summary>
    public partial class frmAddTranslator : Window
    {
        #region getDatas
        public Func<TranslatorDto> getItemToUpdate { get; set; }

        public Func<string> getIdTranslator { get; set; }
        public Func<TranslatorRepository> getTranslatorRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        public AddTranslatorViewModel Context;

        public frmAddTranslator()
        {
            InitializeComponent();


            Context = new AddTranslatorViewModel();

            this.Loaded += FrmAddTranslator_Loaded;
            this.DataContext = Context;
        }

        private void FrmAddTranslator_Loaded(object sender, RoutedEventArgs e)
        {
            Context.onLoaded(sender, e);
        }

        private void ButtonExpand_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = LogicalTreeHelper.GetParent((sender as Button)) as Grid;
            TextBox txt = LogicalTreeHelper.GetChildren(grid).ToTypedCollection<List<DependencyObject>, DependencyObject>()[0] as TextBox;
            Utilitys.ExpandMore(txt, false);
        }

    }
}
