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
    /// Interaction logic for ucPenaltyReasonManagement.xaml
    /// </summary>
    public partial class ucPenaltyReasonManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<PenaltyReasonRepository> getPenaltyReasonRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private PenaltyReasonViewModel penaltyReasonVM;
        #endregion

        #region Mapping
        private PenaltyReasonMap penaltyReasonMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<PenaltyReason> listFillPenaltyReasons;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucPenaltyReasonManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucPenaltyReasonManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucPenaltyReasonManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucPenaltyReasonManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucPenaltyReasonManagement), new PropertyMetadata(true));


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

        public ucPenaltyReasonManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillPenaltyReasons = new ObservableCollection<PenaltyReason>();

            penaltyReasonVM = UnitOfViewModel.Instance.PenaltyReasonViewModel;

            penaltyReasonMap = UnitOfMap.Instance.PenaltyReasonMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucPenaltyReasonsTable.btnInfoClick += UcPenaltyReasonsTable_btnInfoClick;
            ucPenaltyReasonsTable.btnUpdateClick += UcPenaltyReasonsTable_btnUpdateClick;
            ucPenaltyReasonsTable.btnDeleteClick += UcPenaltyReasonsTable_btnDeleteClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucPenaltyReasonManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucPenaltyReasonManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucPenaltyReasonsTable.getItem_Status = () => null;

            AddToListFill(getPenaltyReasonRepo().Gets());
            AddItemsToDataGrid(listFillPenaltyReasons);

            #region IsAllow-Feature
            ucPenaltyReasonsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucPenaltyReasonsTable.IsAllowUpdate = IsAllowUpdate;
            ucPenaltyReasonsTable.IsAllowDelete = IsAllowDelete;
            ucPenaltyReasonsTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getPenaltyReasonRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<PenaltyReason> FillByTextSearch(ObservableCollection<PenaltyReason> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<PenaltyReason> results = new ObservableCollection<PenaltyReason>();
            foreach (PenaltyReason item in source)
            {
                var itemDto = penaltyReasonMap.ConvertToDto(item);
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
            frmAddPenaltyReason frmAddPenaltyReason = MainWindow.UnitOfForm.FrmAddPenaltyReason(true);
            frmAddPenaltyReason.getIdPenaltyReason = () => penaltyReasonVM.GetId();
            frmAddPenaltyReason.ShowDialog();
         
            if (frmAddPenaltyReason.Item == null) // Cancel the operation
            {
                return;
            }

            PenaltyReasonDto newPenaltyReasonDto = frmAddPenaltyReason.Item;

            #region AddNewItem
            PenaltyReason newPenaltyReason = penaltyReasonVM.CreateByDto(newPenaltyReasonDto);
            getPenaltyReasonRepo().Add(newPenaltyReason);
            getPenaltyReasonRepo().WriteAdd(newPenaltyReason);
            #endregion

            #region AddTo-listFill
            listFillPenaltyReasons.Add(newPenaltyReason);
            AddItemsToDataGrid(listFillPenaltyReasons);
            #endregion
        }

        private void UcPenaltyReasonsTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucPenaltyReasonsTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("penaltyReason"));
                return;
            }

            PenaltyReasonDto penaltyReasonDtoSelect = ucPenaltyReasonsTable.SelectedPenaltyReasonDto;
            PenaltyReason penaltyReasonSelect = penaltyReasonVM.FindById(penaltyReasonDtoSelect.Id);
            
            ucPenaltyReasonInformation ucPenaltyReasonInformation = MainWindow.UnitOfForm.UcPenaltyReasonInformation(true);
            ucPenaltyReasonInformation.getItem = () => penaltyReasonDtoSelect;

            Window frmPenaltyReasonInformation = Utilities.CreateDefaultForm();
            frmPenaltyReasonInformation.Content = ucPenaltyReasonInformation;
            frmPenaltyReasonInformation.Show();

            ucPenaltyReasonInformation.btnBack.Click += (_sender, _e) =>
            {
                frmPenaltyReasonInformation.Close();
            };
        }

        private void UcPenaltyReasonsTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            if (ucPenaltyReasonsTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("penaltyReason"));
                return;
            }

            PenaltyReasonDto penaltyReasonDtoSelect = ucPenaltyReasonsTable.SelectedPenaltyReasonDto;
            PenaltyReason penaltyReasonSelect = penaltyReasonVM.FindById(penaltyReasonDtoSelect.Id);

            message = Utilities.NotifySureToDelete();
            if (Utilities.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                return;
            
            getPenaltyReasonRepo().Remove(penaltyReasonSelect);
            getPenaltyReasonRepo().WriteDelete(penaltyReasonSelect);

            ucPenaltyReasonsTable.ModifiedPagination();
         
            message = Utilities.NotifyDeleteSuccessfully("penaltyReason");
         
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcPenaltyReasonsTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucPenaltyReasonsTable.dgDatas.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("penaltyReason"));
                return;
            }

            PenaltyReasonDto penaltyReasonDtoSelect = ucPenaltyReasonsTable.SelectedPenaltyReasonDto;
            PenaltyReason penaltyReasonSelect = penaltyReasonVM.FindById(penaltyReasonDtoSelect.Id);

            frmAddPenaltyReason frmAddPenaltyReason = MainWindow.UnitOfForm.FrmAddPenaltyReason(true);
            frmAddPenaltyReason.getItemToUpdate = () => penaltyReasonDtoSelect;
            frmAddPenaltyReason.ShowDialog();

            if (frmAddPenaltyReason.Item == null) // Cancel the operation
            {
                return;
            }

            PenaltyReasonDto newPenaltyReasonDto = frmAddPenaltyReason.Item;

            #region UpdateItemInformation
            PenaltyReason getPenaltyReason = penaltyReasonVM.FindById(newPenaltyReasonDto.Id);
            penaltyReasonVM.Copy(getPenaltyReason, newPenaltyReasonDto);
            getPenaltyReasonRepo().WriteUpdate(getPenaltyReason);

            #endregion

            ucPenaltyReasonsTable.ModifiedPagination();
        }

        private void AddToListFill(ObservableCollection<PenaltyReason> items)
        {
            listFillPenaltyReasons.Clear();
            listFillPenaltyReasons.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<PenaltyReason> items)
        {
            ucPenaltyReasonsTable.getPenaltyReasons = () => items;
            ucPenaltyReasonsTable.ModifiedPagination();
        }
    }
}
