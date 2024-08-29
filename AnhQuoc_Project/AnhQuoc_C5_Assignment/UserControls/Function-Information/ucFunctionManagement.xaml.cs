using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
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

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucFunctionManagement.xaml
    /// </summary>
    public partial class ucFunctionManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<FunctionRepository> getFunctionRepo { get; set; }
        public Func<RoleFunctionRepository> getRoleFunctionRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private FunctionViewModel functionVM;
        private RoleFunctionViewModel roleFunctionVM;
        #endregion

        #region Mapping
        private FunctionMap functionMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Function> listFills;
        #endregion

        #region Properties
        public bool? StatusValue { get; set; } = null;
        public bool IsAdminValue { get; set; } = false;
        #endregion

        #region Prop-Dp

        #region IsAllow-Features
        public bool IsAllowViewInformation
        {
            get { return (bool)GetValue(IsAllowViewInformationProperty); }
            set { SetValue(IsAllowViewInformationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowViewInformation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowViewInformationProperty =
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucFunctionManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucFunctionManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucFunctionManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucFunctionManagement), new PropertyMetadata(true));


        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucFunctionManagement), new PropertyMetadata(true));


        #endregion

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucFunctionManagement()
        {
            InitializeComponent();

            #region Allocations
            listFills = new ObservableCollection<Function>();

            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
            roleFunctionVM = UnitOfViewModel.Instance.RoleFunctionViewModel;
            functionMap = UnitOfMap.Instance.FunctionMap;
            #endregion

            ucFunctionsTable.btnInfoClick += UcFunctionsTable_btnInfoClick;
            ucFunctionsTable.btnDeleteClick += UcFunctionsTable_btnDeleteClick;
            ucFunctionsTable.btnRestoreClick += UcFunctionsTable_btnRestoreClick;
            ucFunctionsTable.btnUpdateClick += UcFunctionsTable_btnUpdateClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            
            this.Loaded += ucFunctionManagement_Loaded;
            this.DataContext = this;
        }

        private void ucFunctionManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucFunctionsTable.getItem_Status = () => null;

            var source = getFunctionRepo().Gets();
            AddToListFill(source);
            AddItemsToDataGrid(listFills);

            #region IsAllow-Feature
            ucFunctionsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            ucFunctionsTable.dtgbtnUpdate.Visibility = (IsAllowUpdate) ? Visibility.Visible : Visibility.Collapsed;
            ucFunctionsTable.dtgbtnDelete.Visibility = (IsAllowDelete) ? Visibility.Visible : Visibility.Collapsed;
            ucFunctionsTable.dtgbtnRestore.Visibility = (IsAllowRestore) ? Visibility.Visible : Visibility.Collapsed;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getFunctionRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Function> FillByTextSearch(ObservableCollection<Function> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Function> results = new ObservableCollection<Function>();
            foreach (Function item in source)
            {
                var itemDto = functionMap.ConvertToDto(item);
                bool isCheck = itemDto.Name.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }
        #endregion

        private void UcFunctionsTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucFunctionsTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("function"));
                return;
            }

            FunctionDto functionDtoSelect = ucFunctionsTable.SelectedFunctionDto;
            Function functionSelect = functionVM.FindById(functionDtoSelect.Id);

            ucFunctionInformation ucFunctionInformation = MainWindow.UnitOfForm.UcFunctionInformation(true);
            ucFunctionInformation.getItem = () => functionDtoSelect;

            Window frmFunctionInformation = Utilities.CreateDefaultForm();
            frmFunctionInformation.Content = ucFunctionInformation;
            frmFunctionInformation.Show();
        }

        private void UcFunctionsTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucFunctionsTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("function"));
                return;
            }

            var functionDtoSelect = ucFunctionsTable.SelectedFunctionDto;

            frmAddFunction frmAddFunction = MainWindow.UnitOfForm.FrmAddFunction(true);
            frmAddFunction.getItemToUpdate = () => functionDtoSelect;
            frmAddFunction.ShowDialog();

            if (frmAddFunction.Context.Item == null) // Cancel the operation
            {
                return;
            }

            FunctionDto newFunctionDto = frmAddFunction.Context.Item;

            #region UpdateItemInformation
            Function getFunction = functionVM.FindById(newFunctionDto.Id, StatusValue);

            functionVM.Copy(getFunction, newFunctionDto);
            getFunctionRepo().WriteUpdate(getFunction);
            #endregion

            ucFunctionsTable.ModifiedPagination();

            string message = Utilities.NotifyUpdateSuccessfully("function");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcFunctionsTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            FunctionDto functionDtoSelect = ucFunctionsTable.SelectedFunctionDto;
            Function functionSelect = functionVM.FindById(functionDtoSelect.Id, StatusValue);

            var check = roleFunctionVM.FindByIdFunction(roleFunctionVM.Repo.Gets(), functionSelect.Id);
            if (check != null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("function"));
                return;
            }
            
            if (Utilities.FindInList(Constants.importantFunction, functionSelect.Id) != null)
            {
                Utilities.ShowMessageBox1(string.Format("This feature cannot be removed because it is important"));
                return;
            }
            DeleteFunc();
        }

        private void UcFunctionsTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            RestoreFunc();
        }


        private void DeleteFunc()
        {
            bool updateStatus = false;
            string message = string.Empty;
            double dataGridLength = 300;

            if (ucFunctionsTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("function"));
                return;
            }

            FunctionDto functionDtoSelect = ucFunctionsTable.SelectedFunctionDto;
            Function functionSelect = functionVM.FindById(functionDtoSelect.Id, StatusValue);

            #region functionDtoSelect.Parent 
            if (Utilities.IsCheckEmptyString(functionSelect.IdParent))
            {
                ObservableCollection<Function> childs = functionVM.getChildsByIdParent(functionSelect.Id, !updateStatus);
                if (childs.Count == 0)
                {
                    DeleteParentFunc(functionSelect, childs, updateStatus);
                    return;
                }

                #region Form
                Window frmDisplayFunction = Utilities.CreateDefaultForm();
                frmDisplayFunction.SizeToContent = SizeToContent.WidthAndHeight;

                ucDisplayFunction ucDisplayFunction = MainWindow.UnitOfForm.UcDisplayFunction(true);
                ucDisplayFunction.getItem = () => functionDtoSelect;

                ucDisplayFunction.ucFunctionsTable.gdTable.RowDefinitions[0].Height = new GridLength(dataGridLength);
                #endregion

                #region btnDeleteAllChildAndParent
                Button btnDeleteAllChildAndParent = new Button();
                btnDeleteAllChildAndParent.Style = Application.Current.FindResource(Constants.styleBtnDelete) as Style;
                btnDeleteAllChildAndParent.Content = "Delete all child and parent";
                btnDeleteAllChildAndParent.HorizontalAlignment = HorizontalAlignment.Center;
                btnDeleteAllChildAndParent.VerticalAlignment = VerticalAlignment.Center;

                btnDeleteAllChildAndParent.Click += (_sender, _e) =>
                {
                    DeleteParentFunc(functionSelect, childs, updateStatus);
                    frmDisplayFunction.Close();
                };
                #endregion

                #region BtnCancel
                Button btnCancel = new Button();

                btnCancel.Style = Application.Current.FindResource(Constants.styleBtnCancel) as Style;
                btnCancel.Content = "Cancel";
                btnCancel.Margin = new Thickness(10, 0, 0, 0);

                btnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                btnCancel.VerticalAlignment = VerticalAlignment.Center;
                btnCancel.Click += (_sender, _e) =>
                {
                    frmDisplayFunction.Close();
                };
                #endregion

                ucDisplayFunction.stkFooter.Children.Clear();
                ucDisplayFunction.stkFooter.Children.Add(btnDeleteAllChildAndParent);
                ucDisplayFunction.stkFooter.Children.Add(btnCancel);

                frmDisplayFunction.Content = ucDisplayFunction;
                frmDisplayFunction.ShowDialog();
            }
            #endregion

            else
            {
                DeleteChildFunc(functionSelect, updateStatus);
            }
        }

        private void DeleteParentFunc(Function functionSelect, ObservableCollection<Function> childs, bool updateStatus)
        {
            string message;
            message = Utilities.NotifySureToDelete();
            if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                return;

            foreach (Function funcChild in childs)
            {
                funcChild.Status = updateStatus;
                getFunctionRepo().WriteUpdate(funcChild);
            }

            functionSelect.Status = updateStatus;
            getFunctionRepo().WriteUpdate(functionSelect);

            bool isHasChild = childs.Count > 0;

            message = Utilities.NotifyDeleteSuccessfullyParentFunction(isHasChild);
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);

            ucFunctionsTable.ModifiedPagination();
        }

        private void DeleteChildFunc(Function functionSelect, bool updateStatus)
        {
            string message;
            message = Utilities.NotifySureToDelete();
            if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                return;
            
            functionSelect.Status = updateStatus;
            getFunctionRepo().WriteUpdate(functionSelect);

            message = Utilities.NotifyDeleteSuccessfully("function");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);

            ucFunctionsTable.ModifiedPagination();
        }


        private void RestoreFunc()
        {
            Function funcParent = null;
            FunctionDto funcDtoParent = null;
            bool updateStatus = true;
            string message = string.Empty;
            string feature = "Restore";

            if (ucFunctionsTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("function"));
                return;
            }

            FunctionDto funcDtoSelect = ucFunctionsTable.SelectedFunctionDto;
            Function functionSelect = functionVM.FindById(funcDtoSelect.Id, StatusValue);
      
            #region functionDtoSelect.Parent 
            if (!Utilities.IsCheckEmptyString(functionSelect.IdParent))
            {
                funcParent = functionVM.FindById(functionSelect.IdParent, StatusValue);
                funcDtoParent = functionMap.ConvertToDto(funcParent);

                if (funcParent.Status == updateStatus)
                {
                    RestoreFunction(functionSelect);
                    return;
                }

                #region Form
                frmChildFunctionInformation frmChildFunctionInformation = MainWindow.UnitOfForm.FrmChildFunctionInformation(true);
                frmChildFunctionInformation.getParent = () => funcDtoParent;
                frmChildFunctionInformation.getChild = () => funcDtoSelect;
                #endregion

                #region btnRestoreParentAndChild
                Button btnRestoreParentAndChild = new Button();
                btnRestoreParentAndChild.Style = Application.Current.FindResource(Constants.styleBtnRestore) as Style;
                btnRestoreParentAndChild.Content = $"{feature} parent and child";
                btnRestoreParentAndChild.HorizontalAlignment = HorizontalAlignment.Center;
                btnRestoreParentAndChild.VerticalAlignment = VerticalAlignment.Center;

                btnRestoreParentAndChild.Click += (_sender, _e) =>
                {
                    funcParent.Status = updateStatus;
                    getFunctionRepo().WriteUpdate(funcParent);

                    functionSelect.Status = updateStatus;
                    getFunctionRepo().WriteUpdate(functionSelect);

                    message = Utilities.NotifyRestoreSuccessfullyParentFunction(true);
                    MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);

                    ucFunctionsTable.ModifiedPagination();
                    frmChildFunctionInformation.Close();
                };
                #endregion

                #region btnCancel
                Button btnCancel = new Button();

                btnCancel.Style = Application.Current.FindResource(Constants.styleBtnCancel) as Style;
                btnCancel.Content = "Cancel";
                btnCancel.Margin = new Thickness(10, 0, 0, 0);

                btnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                btnCancel.VerticalAlignment = VerticalAlignment.Center;
                btnCancel.Click += (_sender, _e) =>
                {
                    frmChildFunctionInformation.Close();
                };
                #endregion

                frmChildFunctionInformation.stkFooter.Children.Clear();
                frmChildFunctionInformation.stkFooter.Children.Add(btnRestoreParentAndChild);
                frmChildFunctionInformation.stkFooter.Children.Add(btnCancel);

                frmChildFunctionInformation.ShowDialog();
            }
            #endregion

            else
            {
                RestoreFunction(functionSelect);
            }
        }

        private void RestoreFunction(Function functionSelect)
        {
            bool updateStatus = true;
            functionSelect.Status = updateStatus;
            getFunctionRepo().WriteUpdate(functionSelect);

            string message = Utilities.NotifyRestoreSuccessfully("function");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);

            ucFunctionsTable.ModifiedPagination();
        }


        private void AddToListFill(ObservableCollection<Function> items)
        {
            listFills.Clear();
            listFills.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Function> items)
        {
            ucFunctionsTable.getFunctions = () => items;
            ucFunctionsTable.ModifiedPagination();
        }
    }
}
