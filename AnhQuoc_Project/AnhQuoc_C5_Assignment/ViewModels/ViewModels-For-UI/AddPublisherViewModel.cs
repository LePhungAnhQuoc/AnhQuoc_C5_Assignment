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
    public class AddPublisherViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddPublisher thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region ViewModels
        private PublisherViewModel publisherVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private PublisherMap publisherMap;
        #endregion

        #region ResultProperty
        private PublisherDto _Item;
        public PublisherDto Item
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

        public AddPublisherViewModel()
        {
            publisherVM = UnitOfViewModel.Instance.PublisherViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            publisherMap = UnitOfMap.Instance.PublisherMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddPublisher_Loaded(para, null));
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
            thisForm = sender as frmAddPublisher;

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
            Item = new PublisherDto(thisForm.getIdPublisher());
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as PublisherDto;
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

            Publisher getPublisher = publisherVM.CreateByDto(Item);
            bool isCheck = Utilities.IsExistInformation(thisForm.getPublisherRepo().Gets(), getPublisher, true, Constants.checkPropPublisher);
            if (isCheck)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("publisher"));
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

            Publisher normalItem = publisherVM.CreateByDto(Item);
            Publisher normalSourceItem = publisherVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropPublisher);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(thisForm.getPublisherRepo().Gets(), normalItem, true, Constants.checkPropPublisher);
                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("publisher"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            publisherVM.Copy(Item, thisForm.getItemToUpdate());
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
                    thisForm.Title = "Add New Publisher Form";
                    thisForm.lblTitle.Content = "Add New Publisher";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Publisher Information Form";
                    thisForm.lblTitle.Content = "Update Publisher Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
