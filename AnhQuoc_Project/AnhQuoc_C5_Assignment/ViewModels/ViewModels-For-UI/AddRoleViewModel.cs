using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class AddRoleViewModel: BaseViewModel<Role>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddRole thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region Properties
        private ObservableCollection<string> _AllGroups;
        public ObservableCollection<string> AllGroups
        {
            get { return _AllGroups; }
            set
            {
                _AllGroups = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ViewModels
        private RoleViewModel roleVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private RoleMap roleMap;
        #endregion

        #region ResultProperty
        private RoleDto _Item;
        public RoleDto Item
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

        public AddRoleViewModel()
        {

            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            roleMap = UnitOfMap.Instance.RoleMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddRole_Loaded(para, null));
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
            thisForm = sender as frmAddRole;

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



            AllGroups = new ObservableCollection<string>(thisForm.getRoleGroups());
            AllGroups.RemoveAt(0);

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
            Item = new RoleDto(thisForm.getIdRole());
            Item.Status = true;
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as RoleDto;
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
            var normalItem = roleVM.CreateByDto(Item);
            bool isExistInformation = Utilitys.IsExistInformation(thisForm.getRoleRepo().Gets(), normalItem, true, Constants.checkPropRole);

            if (isExistInformation)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInfo("Role"));
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

            var normalItem = roleVM.CreateByDto(Item);
            var normalSourceItem = roleVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilitys.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropRole);

            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilitys.IsExistInformation(thisForm.getRoleRepo().Gets(), normalItem, true, Constants.checkPropRole);

                if (isExistInformation)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInfo("Role"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            roleVM.Copy(Item, thisForm.getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
                Item = null;
            if (!isClosed)
                thisForm.Close();
        }


        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    thisForm.Title = "Add New Role Form";
                    thisForm.lblTitle.Content = "Add New Role";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Role Information Form";
                    thisForm.lblTitle.Content = "Update Role Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }
    }
}
