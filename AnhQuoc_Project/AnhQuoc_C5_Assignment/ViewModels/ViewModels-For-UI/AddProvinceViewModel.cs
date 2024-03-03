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
    public class AddProvinceViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddProvince thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region ViewModels
        private ProvinceViewModel provinceVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ProvinceMap provinceMap;
        #endregion

        #region ResultProperty
        private ProvinceDto _Item;
        public ProvinceDto Item
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

        public AddProvinceViewModel()
        {
            provinceVM = UnitOfViewModel.Instance.ProvinceViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            provinceMap = UnitOfMap.Instance.ProvinceMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddProvince_Loaded(para, null));
            ClosingCmd = new RelayCommand((para) => onClosing(para, null));

            btnConfirmClickCmd = new RelayCommand((para) => BtnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            #endregion
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            thisForm = sender as frmAddProvince;

            mainContentControls = new List<DependencyObject>();
            foreach (DependencyObject child in thisForm.mainContent.Children)
            {
                mainContentControls.AddRange(Utilities.GetControlHaveValidationRules(child));
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
            Item = new ProvinceDto(thisForm.getIdProvince());
            Item.Status = true;
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as ProvinceDto;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);

            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            Province getProvince = provinceVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(thisForm.getProvinceRepo().Gets(), getProvince, true, Constants.checkPropProvince);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("province"));
                return;
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            Utilities.RunAllValidations(mainContentControls);

            bool isHasError = Utilities.IsValidationGetHasError(mainContentControls);
            if (isHasError)
            {
                return;
            }

            Province normalItem = provinceVM.CreateByDto(Item);
            Province normalSourceItem = provinceVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropProvince);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(thisForm.getProvinceRepo().Gets(), normalItem, true, Constants.checkPropProvince);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("province"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            provinceVM.Copy(Item, thisForm.getItemToUpdate());
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
                    thisForm.Title = "Add New Province Form";
                    thisForm.lblTitle.Content = "Add New Province";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Province Information Form";
                    thisForm.lblTitle.Content = "Update Province Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
