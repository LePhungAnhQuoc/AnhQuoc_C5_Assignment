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
    /// Interaction logic for ucParameterManagement.xaml
    /// </summary>
    public partial class ucParameterManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private ParameterViewModel parameterVM;
        #endregion

        #region Mapping
        private ParameterMap parameterMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Parameter> listFillParameters;
        #endregion

        #region Properties

        #endregion

        #region propDp

        #region IsAllow-Features
        public bool IsAllowViewInformation
        {
            get { return (bool)GetValue(IsAllowViewInformationProperty); }
            set { SetValue(IsAllowViewInformationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowViewInformation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowViewInformationProperty =
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucParameterManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucParameterManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucParameterManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucParameterManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucParameterManagement), new PropertyMetadata(true));


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

        public ucParameterManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillParameters = new ObservableCollection<Parameter>();

            parameterVM = UnitOfViewModel.Instance.ParameterViewModel;

            parameterMap = UnitOfMap.Instance.ParameterMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucParametersTable.btnInfoClick += UcParametersTable_btnInfoClick;
            ucParametersTable.btnUpdateClick += UcParametersTable_btnUpdateClick;
            ucParametersTable.btnDeleteClick += UcParametersTable_btnDeleteClick;
            ucParametersTable.btnRestoreClick += UcParametersTable_btnRestoreClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucParameterManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucParameterManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucParametersTable.getItem_Status = () => null;

            AddToListFill(getParameterRepo().Gets());
            AddItemsToDataGrid(listFillParameters);

            #region IsAllow-Feature
            ucParametersTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucParametersTable.IsAllowUpdate = IsAllowUpdate;
            ucParametersTable.IsAllowDelete = IsAllowDelete;
            ucParametersTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getParameterRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Parameter> FillByTextSearch(ObservableCollection<Parameter> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Parameter> results = new ObservableCollection<Parameter>();
            foreach (Parameter item in source)
            {
                var itemDto = parameterMap.ConvertToDto(item);
                bool isCheck = itemDto.Name.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }
        #endregion

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddParameter frmAddParameter = MainWindow.UnitOfForm.FrmAddParameter(true);
            frmAddParameter.getIdParameter = () => parameterVM.GetId();
            frmAddParameter.ShowDialog();
         
            if (frmAddParameter.Context.Item == null) // Cancel the operation
            {
                return;
            }

            ParameterDto newParameterDto = frmAddParameter.Context.Item;

            #region AddNewItem
            Parameter newParameter = parameterVM.CreateByDto(newParameterDto);
            getParameterRepo().Add(newParameter);
            getParameterRepo().WriteAdd(newParameter);
            #endregion

            #region AddTo-listFill
            listFillParameters.Add(newParameter);
            AddItemsToDataGrid(listFillParameters);
            #endregion

            Utilities.ShowMessageBox1(Utilities.NotifyAddSuccessfully("parameter"));
        }

        private void UcParametersTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucParametersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("parameter"));
                return;
            }

            ParameterDto parameterDtoSelect = ucParametersTable.SelectedParameterDto;
            Parameter parameterSelect = parameterVM.FindById(parameterDtoSelect.Id);
            
            ucParameterInformation ucParameterInformation = MainWindow.UnitOfForm.UcParameterInformation(true);
            ucParameterInformation.getItem = () => parameterDtoSelect;

            Window frmParameterInformation = Utilities.CreateDefaultForm();
            frmParameterInformation.Content = ucParameterInformation;
            frmParameterInformation.Show();

            ucParameterInformation.btnBack.Click += (_sender, _e) =>
            {
                frmParameterInformation.Close();
            };
        }

        private void UcParametersTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(false);
        }

        private void UcParametersTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(true);
        }

        private void ChangeStatus(bool updateStatus)
        {
            string message = string.Empty;
            if (ucParametersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("parameter"));
                return;
            }

            ParameterDto parameterDtoSelect = ucParametersTable.SelectedParameterDto;
            Parameter parameterSelect = parameterVM.FindById(parameterDtoSelect.Id);

            if (updateStatus == false)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyNotValidToDelete("parameter"));
                return;

                message = Utilities.NotifySureToDelete();
                if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;
            }

            parameterSelect.Status = updateStatus;
            getParameterRepo().WriteUpdateStatus(parameterSelect, updateStatus);

            ucParametersTable.ModifiedPagination();

            if (updateStatus == false)
                message = Utilities.NotifyDeleteSuccessfully("parameter");
            else
                message = Utilities.NotifyRestoreSuccessfully("parameter");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcParametersTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucParametersTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("parameter"));
                return;
            }

            ParameterDto parameterDtoSelect = ucParametersTable.SelectedParameterDto;
            Parameter parameterSelect = parameterVM.FindById(parameterDtoSelect.Id);

            frmAddParameter frmAddParameter = MainWindow.UnitOfForm.FrmAddParameter(true);
            frmAddParameter.getItemToUpdate = () => parameterDtoSelect;
            frmAddParameter.ShowDialog();

            if (frmAddParameter.Context.Item == null) // Cancel the operation
            {
                return;
            }

            ParameterDto newParameterDto = frmAddParameter.Context.Item;

            #region UpdateItemInformation
            Parameter getParameter = parameterVM.FindById(newParameterDto.Id);
            parameterVM.Copy(getParameter, newParameterDto);
            getParameterRepo().WriteUpdate(getParameter);

            #endregion

            ucParametersTable.ModifiedPagination();

            Utilities.ShowMessageBox1(Utilities.NotifyUpdateSuccessfully("parameter"));
        }

        private void AddToListFill(ObservableCollection<Parameter> items)
        {
            listFillParameters.Clear();
            listFillParameters.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Parameter> items)
        {
            ucParametersTable.getParameters = () => items;
            ucParametersTable.ModifiedPagination();
        }
    }
}
