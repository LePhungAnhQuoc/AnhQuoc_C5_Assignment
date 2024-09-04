using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AddCategoryViewModel: BaseViewModel<Category>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }

        private frmAddCategory thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
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

        #region Commands
        //public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand ClosingCmd { get; private set; }

        public RelayCommand btnConfirmClickCmd { get; private set; }
        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnUpdateClickCmd { get; private set; }
        public RelayCommand btnResetClickCmd { get; private set; }
        #endregion

        public AddCategoryViewModel()
        {
            categoryVM = UnitOfViewModel.Instance.CategoryViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            categoryMap = UnitOfMap.Instance.CategoryMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddCategory_Loaded(para, null));
            ClosingCmd = new RelayCommand(para => onClosing(para, null));
            btnConfirmClickCmd = new RelayCommand((para) => BtnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            #endregion
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            thisForm = sender as frmAddCategory;

            mainContentControls = new List<DependencyObject>();
            foreach (DependencyObject child in thisForm.mainContent.Children)
            {
                mainContentControls.AddRange(Utilitys.GetControlHaveValidationRules(child));
            }

            TextBoxes = mainContentControls.Where(obj => obj is TextBox).Select(obj => obj as TextBox).ToList();
            foreach (TextBox textBox in TextBoxes)
            {
                textBox.MaxLength = Constants.textBoxMaxLength;
                textBox.LostFocus += Txt_LostFocus;
            }



            if (thisForm.getItemToUpdate == null)
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

        private void onClosing(object sender, CancelEventArgs e)
        {
            BtnCancel_Click(null, null, true);
        }



        private void NewItem()
        {
            Item = new CategoryDto(thisForm.getIdCategory());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as CategoryDto;

            Item.ModifiedAt = DateTime.Now;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilitys.RunAllValidations(mainContentControls);

            bool isHasError = Utilitys.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            // Kiểm tra thông tin Name của item có tồn tại trong danh sách
            Category getCategory = categoryVM.CreateByDto(Item);
            bool isCheck = Utilitys.IsExistInformation(thisForm.getCategoryRepo().Gets(), getCategory, true, Constants.checkPropCategory);
            if (isCheck)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInTheList("category"));
                return;
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilitys.RunAllValidations(mainContentControls);

            bool isHasError = Utilitys.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            Category normalItem = categoryVM.CreateByDto(Item);
            Category normalSourceItem = categoryVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilitys.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropCategory);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilitys.IsExistInformation(thisForm.getCategoryRepo().Gets(), normalItem, true, Constants.checkPropCategory);
                if (isExistInformation)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInTheList("category"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }


        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            categoryVM.Copy(Item, thisForm.getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
                Item = null;
            if (!isClosed)
                thisForm.Close();
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
                    thisForm.Title = "Add New Category Form";
                    thisForm.lblTitle.Content = "Add New Category";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Category Information Form";
                    thisForm.lblTitle.Content = "Update Category Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
