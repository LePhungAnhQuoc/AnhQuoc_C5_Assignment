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
    public class AddAuthorViewModel: BaseViewModel<Author>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }  
        private frmAddAuthor thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion

        #region ViewModels
        private AuthorViewModel authorVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private AuthorMap authorMap;
        #endregion

        #region ResultProperty
        private AuthorDto _Item;
        public AuthorDto Item
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
        public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand ClosingCmd { get; private set; }

        public RelayCommand btnConfirmClickCmd { get; private set; }
        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnUpdateClickCmd { get; private set; }
        public RelayCommand btnResetClickCmd { get; private set; }
        #endregion

        public AddAuthorViewModel()
        {
            authorVM = UnitOfViewModel.Instance.AuthorViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            authorMap = UnitOfMap.Instance.AuthorMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => onLoaded(para, null));
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

            thisForm = sender as frmAddAuthor;

            mainContentControls = new List<DependencyObject>();
            foreach (DependencyObject child in thisForm.mainContent.Children) // get all stkPanel in grid
            {
                mainContentControls.AddRange(Utilitys.GetControlHaveValidationRules(child)); // get all textbox in stkPanel
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
            Item = new AuthorDto(thisForm.getIdAuthor());
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as AuthorDto;

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

            Author getAuthor = authorVM.CreateByDto(Item);
            bool isCheck = Utilitys.IsExistInformation(thisForm.getAuthorRepo().Gets(), getAuthor, true, Constants.checkPropAuthor);
            if (isCheck)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInTheList("author"));
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

            Author normalItem = authorVM.CreateByDto(Item);
            Author normalSourceItem = authorVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilitys.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropAuthor);
            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilitys.IsExistInformation(thisForm.getAuthorRepo().Gets(), normalItem, true, Constants.checkPropAuthor);
                if (isExistInformation)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInTheList("author"));
                    return;
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            authorVM.Copy(Item, thisForm.getItemToUpdate());
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
            {
                Item = null;
            }
            
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
                    thisForm.Title = "Add New Author Form";
                    thisForm.lblTitle.Content = "Add New Author";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Author Information Form";
                    thisForm.lblTitle.Content = "Update Author Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    thisForm.btnConfirm.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
