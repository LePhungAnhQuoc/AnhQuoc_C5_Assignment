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
    public partial class frmAddCategory : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<CategoryDto> getItemToUpdate { get; set; }

        public Func<string> getIdCategory { get; set; }
        public Func<CategoryRepository> getCategoryRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region ViewModels
        private CategoryViewModel categoryVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private CategoryMap categoryMap;
        #endregion

        #region ResultProperty
        private CategoryDto _Item;
        public CategoryDto Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }
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

        public frmAddCategory()
        {
            InitializeComponent();

            categoryVM = UnitOfViewModel.Instance.CategoryViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            categoryMap = UnitOfMap.Instance.CategoryMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            #endregion

            #region Register-Event
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;
            #endregion
            
            txtName.LostFocus += Txt_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddCategory_Loaded;
        }

        private void frmAddCategory_Loaded(object sender, RoutedEventArgs e)
        {            
            if (getItemToUpdate == null)
            {
                NewItem();
                SetFormByAddOrUpdate("ADD");
            }
            else
            {
                CopyItem();
                SetFormByAddOrUpdate("UPDATE");
            }
        }

        private void NewItem()
        {
            Item = new CategoryDto(getIdCategory());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as CategoryDto;

            Item.ModifiedAt = DateTime.Now;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {            
            // IsCheckEmptyItem
            bool isCheckEmptyItem = categoryVM.IsCheckEmptyItem(Item);

            // FormatValues
            FormatValues();

            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            // Kiểm tra thông tin Name của item có tồn tại trong danh sách
            Category getCategory = categoryVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(getCategoryRepo().Gets(), getCategory, true, Constants.checkPropCategory);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("category"));
                return;
            }
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // IsCheckEmptyItem
            bool isCheckEmptyItem = categoryVM.IsCheckEmptyItem(Item);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }
            
            // Kiểm tra thông tin Name của item có tồn tại trong danh sách
            Category normalItem = categoryVM.CreateByDto(Item);
            Category normalSourceItem = categoryVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropCategory);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getCategoryRepo().Gets(), normalItem, true, Constants.checkPropCategory);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("category"));
                    return;
                }
            }
            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            categoryVM.Copy(Item, getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }
        
        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtName))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void FormatValues()
        {
            txtName.Text = txtName.Text.Trim();
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }

        // Others methods
        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    this.Title = "Add New Category Form";
                    lblTitle.Content = "Add New Category";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    this.Title = "Update Category Information Form";
                    lblTitle.Content = "Update Category Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
