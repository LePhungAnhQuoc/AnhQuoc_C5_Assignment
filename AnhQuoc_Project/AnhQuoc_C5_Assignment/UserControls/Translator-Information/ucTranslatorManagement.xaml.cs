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
    /// Interaction logic for ucTranslatorManagement.xaml
    /// </summary>
    public partial class ucTranslatorManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<TranslatorRepository> getTranslatorRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region ViewModels
        private TranslatorViewModel translatorVM;
        private BookViewModel bookVM;
        #endregion

        #region Mapping
        private TranslatorMap translatorMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Translator> listFillTranslators;
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
            DependencyProperty.Register("IsAllowViewInformation", typeof(bool), typeof(ucTranslatorManagement), new PropertyMetadata(true));


        public bool IsAllowAdd
        {
            get { return (bool)GetValue(IsAllowAddProperty); }
            set { SetValue(IsAllowAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowAdd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowAddProperty =
            DependencyProperty.Register("IsAllowAdd", typeof(bool), typeof(ucTranslatorManagement), new PropertyMetadata(true));


        public bool IsAllowUpdate
        {
            get { return (bool)GetValue(IsAllowUpdateProperty); }
            set { SetValue(IsAllowUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowUpdateProperty =
            DependencyProperty.Register("IsAllowUpdate", typeof(bool), typeof(ucTranslatorManagement), new PropertyMetadata(true));

        public bool IsAllowDelete
        {
            get { return (bool)GetValue(IsAllowDeleteProperty); }
            set { SetValue(IsAllowDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowDelete.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowDeleteProperty =
            DependencyProperty.Register("IsAllowDelete", typeof(bool), typeof(ucTranslatorManagement), new PropertyMetadata(true));



        public bool IsAllowRestore
        {
            get { return (bool)GetValue(IsAllowRestoreProperty); }
            set { SetValue(IsAllowRestoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAllowRestore.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAllowRestoreProperty =
            DependencyProperty.Register("IsAllowRestore", typeof(bool), typeof(ucTranslatorManagement), new PropertyMetadata(true));


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

        public ucTranslatorManagement()
        {
            InitializeComponent();

            #region Allocations
            listFillTranslators = new ObservableCollection<Translator>();

            translatorVM = UnitOfViewModel.Instance.TranslatorViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            translatorMap = UnitOfMap.Instance.TranslatorMap;
            #endregion

            btnAdd.Click += BtnAdd_Click;
            ucTranslatorsTable.btnInfoClick += UcTranslatorsTable_btnInfoClick;
            ucTranslatorsTable.btnUpdateClick += UcTranslatorsTable_btnUpdateClick;
            ucTranslatorsTable.btnDeleteClick += UcTranslatorsTable_btnDeleteClick;
            ucTranslatorsTable.btnRestoreClick += UcTranslatorsTable_btnRestoreClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            this.Loaded += ucTranslatorManagement_Loaded;
            this.DataContext = this;
        }
        
        private void ucTranslatorManagement_Loaded(object sender, RoutedEventArgs e)
        {
            ucTranslatorsTable.getItem_Status = () => null;

            AddToListFill(getTranslatorRepo().Gets());
            AddItemsToDataGrid(listFillTranslators);

            #region IsAllow-Feature
            ucTranslatorsTable.Visibility = (IsAllowViewInformation) ? Visibility.Visible : Visibility.Collapsed;
            btnAdd.Visibility = (IsAllowAdd) ? Visibility.Visible : Visibility.Collapsed;
            ucTranslatorsTable.IsAllowUpdate = IsAllowUpdate;
            ucTranslatorsTable.IsAllowDelete = IsAllowDelete;
            ucTranslatorsTable.IsAllowRestore = IsAllowRestore;
            #endregion
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(getTranslatorRepo().Gets());

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Translator> FillByTextSearch(ObservableCollection<Translator> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Translator> results = new ObservableCollection<Translator>();
            foreach (Translator item in source)
            {
                var itemDto = translatorMap.ConvertToDto(item);
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
            frmAddTranslator frmAddTranslator = MainWindow.UnitOfForm.FrmAddTranslator(true);
            frmAddTranslator.getIdTranslator = () => translatorVM.GetId();
            frmAddTranslator.ShowDialog();
         
            if (frmAddTranslator.Context.Item == null) // Cancel the operation
            {
                return;
            }

            TranslatorDto newTranslatorDto = frmAddTranslator.Context.Item;

            #region AddNewItem
            Translator newTranslator = translatorVM.CreateByDto(newTranslatorDto);
            getTranslatorRepo().Add(newTranslator);
            getTranslatorRepo().WriteAdd(newTranslator);
            #endregion

            #region AddTo-listFill
            listFillTranslators.Add(newTranslator);
            AddItemsToDataGrid(listFillTranslators);
            #endregion

            Utilitys.ShowMessageBox1(Utilitys.NotifyAddSuccessfully("translator"));

        }

        private void UcTranslatorsTable_btnInfoClick(object sender, RoutedEventArgs e)
        {
            if (ucTranslatorsTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("translator"));
                return;
            }

            TranslatorDto translatorDtoSelect = ucTranslatorsTable.SelectedTranslatorDto;
            Translator translatorSelect = translatorVM.FindById(translatorDtoSelect.Id);
            
            ucTranslatorInformation ucTranslatorInformation = MainWindow.UnitOfForm.UcTranslatorInformation(true);
            ucTranslatorInformation.getItem = () => translatorDtoSelect;

            Window frmTranslatorInformation = Utilitys.CreateDefaultForm();
            frmTranslatorInformation.Content = ucTranslatorInformation;
            frmTranslatorInformation.Show();

            ucTranslatorInformation.btnBack.Click += (_sender, _e) =>
            {
                frmTranslatorInformation.Close();
            };
        }

        private void UcTranslatorsTable_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(false);
        }

        private void UcTranslatorsTable_btnRestoreClick(object sender, RoutedEventArgs e)
        {
            ChangeStatus(true);
        }

        private void ChangeStatus(bool updateStatus)
        {
            string message = string.Empty;
            if (ucTranslatorsTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("translator"));
                return;
            }

            TranslatorDto translatorDtoSelect = ucTranslatorsTable.SelectedTranslatorDto;
            Translator translatorSelect = translatorVM.FindById(translatorDtoSelect.Id);

            if (updateStatus == false)
            {
                var listTemp = bookVM.FillByIdTranslator(bookVM.Repo.Gets(), translatorDtoSelect.Id, null);
                if (listTemp.Count > 0)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyNotValidToDelete("translator"));
                    return;
                }


                message = Utilitys.NotifySureToDelete();
                if (Utilitys.ShowMessageBox2(message) == MessageBoxResult.Cancel)
                    return;
            }

            translatorSelect.Status = updateStatus;
            getTranslatorRepo().WriteUpdate(translatorSelect);

            ucTranslatorsTable.ModifiedPagination();

            if (updateStatus == false)
                message = Utilitys.NotifyDeleteSuccessfully("translator");
            else
                message = Utilitys.NotifyRestoreSuccessfully("translator");
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.None);
        }

        private void UcTranslatorsTable_btnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (ucTranslatorsTable.dgDatas.SelectedIndex == -1)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyPleaseSelect("translator"));
                return;
            }

            TranslatorDto translatorDtoSelect = ucTranslatorsTable.SelectedTranslatorDto;
            Translator translatorSelect = translatorVM.FindById(translatorDtoSelect.Id);

            frmAddTranslator frmAddTranslator = MainWindow.UnitOfForm.FrmAddTranslator(true);
            frmAddTranslator.getItemToUpdate = () => translatorDtoSelect;
            frmAddTranslator.ShowDialog();

            if (frmAddTranslator.Context.Item == null) // Cancel the operation
            {
                return;
            }

            TranslatorDto newTranslatorDto = frmAddTranslator.Context.Item;

            #region UpdateItemInformation
            Translator getTranslator = translatorVM.FindById(newTranslatorDto.Id);
            translatorVM.Copy(getTranslator, newTranslatorDto);
            getTranslatorRepo().WriteUpdate(getTranslator);

            #endregion

            ucTranslatorsTable.ModifiedPagination();

            Utilitys.ShowMessageBox1(Utilitys.NotifyUpdateSuccessfully("translator"));

        }

        private void AddToListFill(ObservableCollection<Translator> items)
        {
            listFillTranslators.Clear();
            listFillTranslators.AddRange(items);
        }
        
        private void AddItemsToDataGrid(ObservableCollection<Translator> items)
        {
            ucTranslatorsTable.getTranslators = () => items;
            ucTranslatorsTable.ModifiedPagination();
        }
    }
}
