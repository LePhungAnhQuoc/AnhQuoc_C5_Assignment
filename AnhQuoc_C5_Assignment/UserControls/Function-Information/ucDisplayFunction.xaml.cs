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
    /// Interaction logic for ucDisplayFunction.xaml
    /// </summary>
    public partial class ucDisplayFunction : UserControl
    {
        #region GetDatas
        public Func<FunctionDto> getItem { get; set; }
        #endregion

        #region ViewModels
        private FunctionViewModel functionVM;
        #endregion

        public ucDisplayFunction()
        {
            InitializeComponent();

            functionVM = UnitOfViewModel.Instance.FunctionViewModel;

            this.DataContext = this;
            this.Loaded += UcDisplayFunction_Loaded;
        }

        private void UcDisplayFunction_Loaded(object sender, RoutedEventArgs e)
        {
            var childDtos = getItem().Childs;
            var childs = functionVM.CreateByDto(childDtos);
            ucFunctionsTable.getFunctions = () => childs.ToObservableCollection();
            ucFunctionsTable.getItem_Status = () => true;
        }
    }
}
