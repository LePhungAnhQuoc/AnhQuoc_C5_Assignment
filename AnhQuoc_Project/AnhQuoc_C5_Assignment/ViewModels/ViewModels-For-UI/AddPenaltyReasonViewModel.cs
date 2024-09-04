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
    public class AddPenaltyReasonViewModel: BaseViewModel<PenaltyReason>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddPenaltyReason thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region ViewModels
        private PenaltyReasonViewModel penaltyReasonVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private PenaltyReasonMap penaltyReasonMap;
        #endregion

        #region ResultProperty
        private PenaltyReasonDto _Item;
        public PenaltyReasonDto Item
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

        public AddPenaltyReasonViewModel()
        {
            penaltyReasonVM = UnitOfViewModel.Instance.PenaltyReasonViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            penaltyReasonMap = UnitOfMap.Instance.PenaltyReasonMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddPenaltyReason_Loaded(para, null));
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

            thisForm = sender as frmAddPenaltyReason;

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
            Item = new PenaltyReasonDto(thisForm.getIdPenaltyReason());
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as PenaltyReasonDto;

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

            PenaltyReason getPenaltyReason = penaltyReasonVM.CreateByDto(Item);
            bool isCheck = Utilitys.IsExistInformation(thisForm.getPenaltyReasonRepo().Gets(), getPenaltyReason, true, Constants.checkPropPenaltyReason);
            if (isCheck)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInTheList("penalty reason"));
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

            PenaltyReason normalItem = penaltyReasonVM.CreateByDto(Item);
            PenaltyReason normalSourceItem = penaltyReasonVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilitys.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropPenaltyReason);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilitys.IsExistInformation(thisForm.getPenaltyReasonRepo().Gets(), normalItem, true, Constants.checkPropPenaltyReason);
                if (isExistInformation)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInTheList("penalty reason"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            penaltyReasonVM.Copy(Item, thisForm.getItemToUpdate());
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
                    thisForm.Title = "Add New Penalty Reason Form";
                    thisForm.lblTitle.Content = "Add New Penalty Reason";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Penalty Reason Information Form";
                    thisForm.lblTitle.Content = "Update Penalty Reason Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
