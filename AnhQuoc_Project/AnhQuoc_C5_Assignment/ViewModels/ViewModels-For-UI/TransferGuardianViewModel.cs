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
    public class TransferGuardianViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }

        private frmTransferGuardian thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;
        #endregion


        #region Properties
        private ObservableCollection<Adult> _Guardians;
        public ObservableCollection<Adult> Guardians
        {
            get { return _Guardians; }
            set
            {
                _Guardians = value;
                OnPropertyChanged();
            }
        }

        private ChildDto _Item;
        public ChildDto Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region ViewModels
        private ReaderViewModel readerVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private ChildMap childMap;
        #endregion

        #region Commands
        //public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand ClosingCmd { get; private set; }

        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnUpdateClickCmd { get; private set; }
        public RelayCommand btnResetClickCmd { get; private set; }
        #endregion

        public TransferGuardianViewModel()
        {
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            readerMap = UnitOfMap.Instance.ReaderMap;
            childMap = UnitOfMap.Instance.ChildMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => FrmTransferGuardian_Loaded(para, null));
            ClosingCmd = new RelayCommand((para) => onClosing(para, null));

            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            #endregion
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            thisForm = sender as frmTransferGuardian;

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
            
            CopyItem();

            thisForm.ucChildInformation.getItem = () => Item;
            thisForm.ucChildInformation.lblTitle.Visibility = Visibility.Collapsed;

            Guardians = thisForm.getGuardians();
        }

        private void onClosing(object sender, CancelEventArgs e)
        {
            BtnCancel_Click(null, null, true);
        }

        private void CopyItem()
        {
            var getItem = thisForm.getChild();
            Item = getItem.Clone() as ChildDto;

            Item.ModifiedAt = DateTime.Now;
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
            
            bool? statusValue = true;
          
            // PassValue
            Adult adult = adultVM.FindByIdReader(Item.IdAdult, statusValue);
            Item.Adult = adult;
            
            #region UpdateItemInformation

            Child childFinded = childVM.FindByIdReader(Item.IdReader, null);
            childVM.Copy(childFinded, Item);
            thisForm.getChildRepo().WriteUpdate(childFinded);
            #endregion

            IsCancel = false;
            thisForm.Close();
        }


        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            childVM.Copy(Item, thisForm.getChild());
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

    }
}
