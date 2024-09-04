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
    public class AddReaderViewModel : BaseViewModel<Reader>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddReader thisForm;

        private List<DependencyObject> readerContentControls;
        private List<DependencyObject> adultContentControls;
        private List<DependencyObject> childContentControls;
        private List<DependencyObject> mainContentControls;

        private List<TextBox> TextBoxes;

        private DateTime AdultReader_ExpireDate;
        #endregion

        #region Properties
        private ObservableCollection<Province> _AllProvinces;
        public ObservableCollection<Province> AllProvinces
        {
            get { return _AllProvinces; }
            set
            {
                _AllProvinces = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Adult> _AllAdults;
        public ObservableCollection<Adult> AllAdults
        {
            get { return _AllAdults; }
            set
            {
                _AllAdults = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ViewModels
        private ReaderViewModel readerVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        #endregion

        #region ResultProperty
        private ReaderDto _Item;
        public ReaderDto Item
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

        private AdultDto _AdultItem;
        public AdultDto AdultItem
        {
            get { return _AdultItem; }
            set
            {
                _AdultItem = value;
                OnPropertyChanged();
            }
        }

        private ChildDto _ChildItem;
        public ChildDto ChildItem
        {
            get { return _ChildItem; }
            set
            {
                _ChildItem = value;
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
        public RelayCommand dateboFSelectedDateChangedCmd { get; private set; }
        #endregion

        public AddReaderViewModel()
        {
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            readerMap = UnitOfMap.Instance.ReaderMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => FrmAddReader_Loaded(para, null));
            ClosingCmd = new RelayCommand((para) => onClosing(para, null));

            btnConfirmClickCmd = new RelayCommand((para) => BtnConfirm_Click(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            //btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            //btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            dateboFSelectedDateChangedCmd = new RelayCommand((para) => DateboF_SelectedDateChanged(para, null));
            #endregion
        }

        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            thisForm = sender as frmAddReader;

            AllProvinces = thisForm.getProvinceRepo().Gets();
            AllAdults = thisForm.getAdultRepo().Gets();

            mainContentControls = new List<DependencyObject>();
            readerContentControls = new List<DependencyObject>();
            adultContentControls = new List<DependencyObject>();
            childContentControls = new List<DependencyObject>();

            DependencyObject child = thisForm.mainContent.Children[0];
            readerContentControls.AddRange(Utilitys.GetControlHaveValidationRules(child));
            child = thisForm.mainContent.Children[1];
            adultContentControls.AddRange(Utilitys.GetControlHaveValidationRules(child));
            child = thisForm.mainContent.Children[2];
            childContentControls.AddRange(Utilitys.GetControlHaveValidationRules(child));

            mainContentControls.AddRange(readerContentControls);


            var allControls = new List<DependencyObject>();
            allControls.AddRange(readerContentControls);
            allControls.AddRange(adultContentControls);
            allControls.AddRange(childContentControls);

            TextBoxes = allControls.Where(obj => obj is TextBox).Select(obj => obj as TextBox).ToList();
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
                if (Item.ReaderType == ReaderType.Adult)
                {
                    CopyAdult();
                }
                else if (Item.ReaderType == ReaderType.Child)
                {
                    CopyChild();
                }
                SetFormByAddOrUpdate("UPDATE");
            }


            Parameter paraQD = paraVM.FindById(Constants.paraQD8);
            AdultReader_ExpireDate = adultVM.GetExpireDate(paraQD, Item.CreatedAt);

        }

        private void onClosing(object sender, CancelEventArgs e)
        {
            BtnCancel_Click(null, null, true);
        }



        private void DateboF_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Item == null) return;
            if (Item.boF == null)
            {
                return;
            }
         
            #region CheckAge

            int readerAge = DateTime.Now.Date.Year - ((DateTime)Item.boF).Date.Year;

            #region GetParameterRule
            Parameter parameter = paraVM.FindById(Constants.paraQD7);
            int ageValueRule = int.Parse(parameter.Value);
            #endregion

            if (readerAge < ageValueRule)
            {
                Item.ReaderType = ReaderType.Child;


                
                mainContentControls.RemoveRange(readerContentControls.Count, mainContentControls.Count - readerContentControls.Count);
                mainContentControls.AddRange(childContentControls);

                LoadUcAddChild();


            }
            else
            {
                Item.ReaderType = ReaderType.Adult;

                mainContentControls.RemoveRange(readerContentControls.Count, mainContentControls.Count - readerContentControls.Count);
                mainContentControls.AddRange(adultContentControls);

                LoadUcAddAdult();
            }
            #endregion
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

            // Kiểm tra thông tin của item có tồn tại trong danh sách
            Reader getReader = readerVM.CreateByDto(Item);
            bool isExistReaderInformation = Utilitys.IsExistInformation(thisForm.getReaderRepo().Gets(), getReader, true, Constants.checkPropReader);

            if (Item.ReaderType == ReaderType.Adult)
            {
                Adult getAdult = adultVM.CreateByDto(AdultItem);
                bool isCheck = Utilitys.IsExistInformation(thisForm.getAdultRepo().Gets(), getAdult, true, Constants.checkPropAdult);

                // Kiểm tra thông tin của item có tồn tại trong danh sách
                if (isCheck)
                {
                    Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInTheList("adult reader"));
                    return;
                }
            }
            else if (Item.ReaderType == ReaderType.Child)
            {
                // Kiểm tra thông tin của item có tồn tại trong danh sách
                if (isExistReaderInformation)
                {
                    Child getChild = childVM.CreateByDto(ChildItem);
                    bool IsExistChild = Utilitys.IsExistInformation(thisForm.getChildRepo().Gets(), getChild, true, Constants.checkPropChild);

                    if (IsExistChild)
                    {
                        Utilitys.ShowMessageBox1(Utilitys.NotifyItemExistInfo("child reader"));
                        return;
                    }
                }
            }

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
                Item = null;
            if (!isClosed)
                thisForm.Close();
        }

        private void LoadUcAddAdult()
        {
            NewAdult();
            thisForm.stkAdult.Visibility = Visibility.Visible;
            thisForm.stkChild.Visibility = Visibility.Collapsed;
        }

        private void LoadUcAddChild()
        {
            NewChild();
            thisForm.stkChild.Visibility = Visibility.Visible;
            thisForm.stkAdult.Visibility = Visibility.Collapsed;
        }


        private void NewItem()
        {
            Item = new ReaderDto(thisForm.getIdReader());

            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void NewAdult()
        {
            AdultItem = new AdultDto(thisForm.getIdReader());

            AdultItem.ExpireDate = AdultReader_ExpireDate;
            AdultItem.Status = true;

            AdultItem.CreatedAt = Item.CreatedAt;
            AdultItem.ModifiedAt = Item.ModifiedAt;
        }

        private void NewChild()
        {
            ChildItem = new ChildDto(thisForm.getIdReader());

            ChildItem.Status = true;

            ChildItem.CreatedAt = Item.CreatedAt;
            ChildItem.ModifiedAt = Item.ModifiedAt;
        }



        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as ReaderDto;

            Item.ModifiedAt = DateTime.Now;
        }

        private void CopyAdult()
        {
            var getAdult = thisForm.getAdultToUpdate();
            AdultItem = getAdult.Clone() as AdultDto;

            AdultItem.ModifiedAt = DateTime.Now;
        }

        private void CopyChild()
        {
            var getChild = thisForm.getChildToUpdate();
            ChildItem = getChild.Clone() as ChildDto;

            ChildItem.ModifiedAt = DateTime.Now;
        }


        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    thisForm.Title = "Add New Reader Form";
                    thisForm.lblTitle.Content = "Add New Reader";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    thisForm.btnConfirm.Visibility = Visibility.Visible;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Reader Information Form";
                    thisForm.lblTitle.Content = "Update Reader Information";

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
