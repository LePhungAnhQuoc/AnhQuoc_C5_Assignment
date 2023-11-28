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
    /// Interaction logic for frmBorrowBook.xaml
    /// </summary>
    public partial class frmBorrowBook : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<LoanDetailRepository> getLoanDetailRepo { get; set; }
        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }

        #endregion

        #region Properties
        private ObservableCollection<ReaderType> _AllReaderTypes;
        public ObservableCollection<ReaderType> AllReaderTypes
        {
            get { return _AllReaderTypes; }
            set
            {
                _AllReaderTypes = value;
                OnPropertyChanged();
            }
        }

        private ReaderType? _SelectedReaderType;
        public ReaderType? SelectedReaderType
        {
            get { return _SelectedReaderType; }
            set
            {
                _SelectedReaderType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Reader> FillReaderByType { get; set; }
        public bool? StatusValue { get; set; } = true;
        public int TotalQuantity { get; set; }



        private DateTime? _SelectedBoF;
        public DateTime? SelectedBoF
        {
            get
            {
                return _SelectedBoF;
            }
            set
            {
                _SelectedBoF = value;
                OnPropertyChanged();
            }
        }
        #endregion



        #region ViewModels
        private ReaderViewModel readerVM;
        private LoanSlipViewModel loanSlipVM;
        private LoanDetailViewModel loanDetailVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private AdultMap adultMap;
        #endregion

        #region ResultProperty
        private ReaderDto _SelectedReader;
        public ReaderDto SelectedReader
        {
            get { return _SelectedReader; }
            set
            {
                _SelectedReader = value;
                OnPropertyChanged();
            }
        }


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

        private LoanSlip _LoanSlip;
        public LoanSlip LoanSlip
        {
            get { return _LoanSlip; }
            set
            {
                _LoanSlip = value;
                OnPropertyChanged();
            }
        }

        private LoanDetail _LoanDetail;
        public LoanDetail LoanDetail
        {
            get { return _LoanDetail; }
            set
            {
                _LoanDetail = value;
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

        public frmBorrowBook()
        {
            InitializeComponent();

            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();


            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;
            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;

            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            //txtLName.MaxLength = maxLength;
            //txtFName.MaxLength = maxLength;
            #endregion

            //btnConfirm.Click += BtnConfirm_Click;
            //btnBookDetailConfirm.Click += BtnBookDetailConfirm_Click;
            //btnCancel.Click += BtnCancel_Click;

            //cbSelectReaderType.SelectionChanged += CbSelectReaderType_SelectionChanged;
            //txtInputIdentify.TextChanged += TxtInputIdentify_TextChanged;
            //cbTxtReaderName.SelectionChanged += CbTxt_SelectionChanged;

            this.Loaded += frmBorrowBook_Loaded;
            this.DataContext = this;
        }

        private void NewItem()
        {
            const int expDay = 7;

            LoanSlip = new LoanSlip();
            LoanSlip.Id = loanSlipVM.GetId();
            LoanSlip.LoanDate = DateTime.Now;
            LoanSlip.ExpDate = LoanSlip.LoanDate.AddDays(expDay);
            LoanSlip.LoanPaid = 0.000M;
        }
        
        private void NewDetail()
        {
            LoanDetail = new LoanDetail();
            LoanDetail.Id = loanDetailVM.GetId();
            LoanDetail.IdLoan = LoanSlip.Id;
        }

        private void frmBorrowBook_Loaded(object sender, RoutedEventArgs e)
        {
            NewItem();

            ucTemporaryBooksTable.getBooks = () => new ObservableCollection<BookDto>();
        }
        

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool? statusValue = null;

            if (!IsAllSelecting())
            {
                RunAllValidations();
                return;
            }

            // IsCheckEmptyItem
            bool isCheckEmptyItem = loanSlipVM.IsCheckEmptyItem(LoanSlip);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            // Truyền dữ liệu cho item
            PassValueToItem(LoanSlip);

            // Kiểm tra thông tin của item có tồn tại trong danh sách
            //bool isExistReaderInformation = Utilities.IsExistInformation(getReaderRepo().Gets(), Item, true, Constants.checkPropReader);
            //if (itemCheckIdentify != null)
            //{
            //    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("Adult Identify"));
            //    return;
            //}

            this.Close();
        }

        private void BtnBookDetailConfirm_Click(object sender, RoutedEventArgs e)
        {
            NewDetail();

            bool? statusValue = null;

            if (!IsAllSelectingDetail())
            {
                RunAllValidations();
                return;
            }

            // IsCheckEmptyItem
            bool isCheckEmptyItem = loanSlipVM.IsCheckEmptyItem(LoanSlip);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }

            // Truyền dữ liệu cho item
            PassValueToItem(LoanSlip);

            // Kiểm tra thông tin của item có tồn tại trong danh sách
            //bool isExistReaderInformation = Utilities.IsExistInformation(getReaderRepo().Gets(), Item, true, Constants.checkPropReader);
            //if (itemCheckIdentify != null)
            //{
            //    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("Adult Identify"));
            //    return;
            //}

            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;

            this.Close();
        }

        private void PassValueToItem(LoanSlip item)
        {
            //item.IdReader = SelectedReader.Id;
            //item.Quantity = TotalQuantity;
            //item.Deposit = Convert.ToDecimal(txtBookDeposit.Text);
        }

        private bool IsAllSelecting()
        {
            if (SelectedReader == null)
            {
                Utilities.ShowMessageBox1("Please select reader");
                return false;
            }
            return true;
        }

        private bool IsAllSelectingDetail()
        {
            return true;
        }

        public bool IsValidationGetHasError()
        {
            //if (Validation.GetHasError(txtLName))
            //    return true;
            //if (Validation.GetHasError(txtFName))
            //    return true;
            //if (Validation.GetHasError(dateboF))
            //    return true;

            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            //be = txtLName.GetBindingExpression(TextBox.TextProperty);
            //be.UpdateSource();
            //be = txtFName.GetBindingExpression(TextBox.TextProperty);
            //be.UpdateSource();
            //be = dateboF.GetBindingExpression(DatePicker.SelectedDateProperty);
            //be.UpdateSource();
        }


        #region Filter
        private void TxtInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            return;
            var listSource = FillReaderByType;
            bool ignoreCase = true;
            if (cbSelectReaderType.SelectedIndex == -1)
            {
                Utilities.ShowMessageBox1("Please select reader type");
                txtInputIdentify.Text = string.Empty;
                return;
            }

            if (Utilities.IsCheckEmptyString(txtInputIdentify.Text))
            {
                cbTxtIdentify.IsDropDownOpen = false;
                return;
            }




            //if (txtInputIdentify.Text.Equals(adultFind.Identify))
            //{
            //    cbTxtIdentify.IsDropDownOpen = false;
            //}

            var adultListSource = adultVM.GetListFromReaders(listSource, StatusValue);

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(adultListSource, txtInputIdentify.Text, ignoreCase, StatusValue);
            ObservableCollection<AdultDto> getfillListDto = adultMap.ConvertToDto(getfillList);
            cbTxtIdentify.ItemsSource = getfillListDto;
        }

        private void CbTxt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        #endregion

        // Others methods
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TxtFormatValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }

        private void CbSelectReaderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillReaderByType = readerVM.FillListByType((ReaderType)cbSelectReaderType.SelectedItem, StatusValue);

            var FillReaderByTypeDto = readerMap.ConvertToDto(FillReaderByType);
            var adults = adultVM.GetListFromReaders(FillReaderByType, StatusValue);
            var adultDTOs = adultMap.ConvertToDto(adults);

            cbTxtIdentify.ItemsSource = adultDTOs;
        }
    }
}
