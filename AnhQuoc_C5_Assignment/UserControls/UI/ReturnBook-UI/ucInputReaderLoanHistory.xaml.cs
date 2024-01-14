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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucInputReaderLoanHistory.xaml
    /// </summary>
    public partial class ucInputReaderLoanHistory : UserControl, INotifyPropertyChanged
    {
        #region getDatas
        public Func<Stack<object>> getStoreContent { get; set; }
        public Func<ucAddLoanHistory> getParentUc { get; set; }
        public Func<LoanHistoryDto> getItem { get; set; }


        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<LoanDetailRepository> getLoanDetailRepo { get; set; }
        public Func<LoanHistoryRepository> getLoanHistoryRepo { get; set; }
        public Func<LoanDetailHistoryRepository> getLoanDetailHistoryRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        private bool handle = true;
        private ucAddLoanHistory ucAddLoanHistory;
        private ucRetureBookInfo ucRetureBookInfo;
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

        private LoanHistoryDto _Item;
        public LoanHistoryDto Item
        {
            get { return _Item; }
            set
            {
                _Item = value;
                OnPropertyChanged();
            }
        }

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

        private AdultDto _SelectedAdult;
        public AdultDto SelectedAdult
        {
            get { return _SelectedAdult; }
            set
            {
                _SelectedAdult = value;
                OnPropertyChanged();
            }
        }
        
        private AdultDto _SelectedGuardian;
        public AdultDto SelectedGuardian
        {
            get { return _SelectedGuardian; }
            set
            {
                _SelectedGuardian = value;
                OnPropertyChanged();
            }
        }


        #endregion


        #region ViewModels
        private ReaderViewModel readerVM;
        private LoanHistoryViewModel loanHistoryVM;
        private LoanDetailHistoryViewModel loanDetailHistoryVM;
        private LoanSlipViewModel loanSlipVM;
        private LoanDetailViewModel loanDetailVM;
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private BookTitleViewModel bookTitleVM;
        private BookISBNViewModel bookISBNVM;
        private BookViewModel bookVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        private LoanDetailMap loanDetailMap;
        private LoanSlipMap loanSlipMap;
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

        public ucInputReaderLoanHistory()
        {
            InitializeComponent();

            #region Allocates
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailHistoryVM = UnitOfViewModel.Instance.LoanDetailHistoryViewModel;

            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;
            loanSlipMap = UnitOfMap.Instance.LoanSlipMap;

            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            #endregion

            #region SetTextBoxMaxLength

            #endregion

            #region Events
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            cbSelectReaderType.SelectionChanged += CbSelectReaderType_SelectionChanged;
            ucLoanSlipsBorrowedTable.dgDatas.SelectionChanged += DgDatas_SelectionChanged;

            cbAdultTxtIdentify.DropDownClosed += CbAdultTxtIdentify_DropDownClosed;
            cbAdultTxtIdentify.SelectionChanged += CbAdultTxtIdentify_SelectionChanged;
            txtAdultInputIdentify.TextChanged += TxtInputIdentify_TextChanged;

            cbGuardianTxtIdentify.DropDownClosed += CbGuardianTxtIdentify_DropDownClosed;
            cbGuardianTxtIdentify.SelectionChanged += CbGuardianTxtIdentify_SelectionChanged;
            txtGuardianInputIdentify.TextChanged += TxtGuardianInputIdentify_TextChanged;

            cbChildTxtReaderName.DropDownClosed += CbChildTxtReaderName_DropDownClosed;
            cbChildTxtReaderName.SelectionChanged += CbChildTxtReaderName_SelectionChanged;
            txtChildInputReaderName.TextChanged += TxtChildInputReaderName_TextChanged;

            #endregion

            #region LostFocus
            #endregion

            this.DataContext = this;
            this.Loaded += ucInputReaderLoanHistory_Loaded;
        }

        private void DgDatas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ucLoanSlipsBorrowedTable.dgDatas.SelectedItem == null) return;

            ucAddLoanHistory.SelectedLoanSlip = ucLoanSlipsBorrowedTable.dgDatas.SelectedItem as LoanSlipDto;
        }

        private void ucInputReaderLoanHistory_Loaded(object sender, RoutedEventArgs e)
        {
            ucAddLoanHistory = getParentUc();

            AllReaderTypes = ucAddLoanHistory.AllReaderTypes;

            ucAddLoanHistory.SelectedReaderType = ReaderType.Adult;
            SelectedReaderType = ucAddLoanHistory.SelectedReaderType;

            ucLoanSlipsBorrowedTable.getLoanSlips = () => ucAddLoanHistory.AllLoanOfReader;
            ucLoanSlipsBorrowedTable.ModifiedPagination();

            stkAdultInformation.Visibility = Visibility.Visible;
            stkChildInformation.Visibility = Visibility.Collapsed;
        }

        private void CbSelectReaderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSelectReaderType.SelectedItem == null) return;

            var readerType = (ReaderType)cbSelectReaderType.SelectedItem;
            if (readerType == ReaderType.Adult)
            {
                handle = true;
                ucAddLoanHistory.FillReaderByType = readerVM.FillListByType(readerType, ucAddLoanHistory.StatusValue);

                var FillReaderByTypeDto = readerMap.ConvertToDto(ucAddLoanHistory.FillReaderByType);
                var adults = adultVM.GetListFromReaders(ucAddLoanHistory.FillReaderByType, ucAddLoanHistory.StatusValue);

                ucAddLoanHistory.AllAdults = adultMap.ConvertToDto(adults);
                cbAdultTxtIdentify.ItemsSource = ucAddLoanHistory.AllAdults;

                stkAdultInformation.Visibility = Visibility.Visible;
                stkChildInformation.Visibility = Visibility.Collapsed;
            }
            else
            {
                handle = true;
                ucAddLoanHistory.FillReaderByType = readerVM.FillListByType(readerType, ucAddLoanHistory.StatusValue);


                var FillReaderByTypeDto = readerMap.ConvertToDto(ucAddLoanHistory.FillReaderByType);
                var childs = childVM.GetListFromReaders(ucAddLoanHistory.FillReaderByType, ucAddLoanHistory.StatusValue);
                var childDTOs = childMap.ConvertToDto(childs);


                var adults = adultVM.GetListFromChilds(childs, ucAddLoanHistory.StatusValue);

                ucAddLoanHistory.AllGuardians = adultMap.ConvertToDto(adults);
                cbGuardianTxtIdentify.ItemsSource = ucAddLoanHistory.AllGuardians;

                stkAdultInformation.Visibility = Visibility.Collapsed;
                stkChildInformation.Visibility = Visibility.Visible;
            }
        }

        private void NewItem()
        {
            string newId = loanHistoryVM.GetId();
            ucAddLoanHistory.Item = new LoanHistoryDto(newId);
            Utilities.Copy(ucAddLoanHistory.Item, ucAddLoanHistory.SelectedLoanSlip);
            ucAddLoanHistory.Item.Id = newId;
            ucAddLoanHistory.Item.CreateAt = DateTime.Now;

            Item = ucAddLoanHistory.Item;
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReader == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("reader"));
                return;
            }
            ucAddLoanHistory.SelectedReader = SelectedReader;

            if (ucLoanSlipsBorrowedTable.dgDatas.SelectedItem == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("loan slip"));
                return;
            }

            NewItem();

            ucRetureBookInfo = MainWindow.UnitOfForm.UcRetureBookInfo(true);
            ucRetureBookInfo.getItem = getItem;
            ucRetureBookInfo.getStoreContent = getStoreContent;
            ucRetureBookInfo.getParentUc = getParentUc;

            getStoreContent().Push(this.Content);
            this.Content = ucRetureBookInfo;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = getStoreContent().Pop();
        }
        
        private void FormatValues()
        {
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }



        private bool HandleAdult(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((AdultDto)cmb.SelectedItem).Identify;
            return true;
        }

        #region AdultInput
        private void CbAdultTxtIdentify_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleAdult(txtAdultInputIdentify, cmb);
            handle = true;
        }

        private void CbAdultTxtIdentify_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(txtAdultInputIdentify, cmb);
        }

        private void TxtInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputIdentify_Filter_TextChanged(txtAdultInputIdentify, gdInputAdultIndentify, ucAddLoanHistory.AllAdults);
        }

        private void TxtInputIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, ucAddLoanHistory.StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Reader reader = readerVM.FindById(getfillList.First().IdReader);
                ucAddLoanHistory.SelectedReader = readerMap.ConvertToDto(reader);
                SelectedReader = ucAddLoanHistory.SelectedReader;

                GetLoanSlipFromReader();
                return;
            }
            else
            {
                ucAddLoanHistory.SelectedReader = null;
                SelectedReader = ucAddLoanHistory.SelectedReader;
            }
            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion

        #region GuardianInput
        private void CbGuardianTxtIdentify_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleAdult(txtGuardianInputIdentify, cmb);
            handle = true;
        }

        private void CbGuardianTxtIdentify_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(txtGuardianInputIdentify, cbGuardianTxtIdentify);
        }

        private void GetAllChildOfAdult(AdultDto adultDto)
        {
            ucAddLoanHistory.AllChilds = childMap.ConvertToDto(adultDto.ListChild);
            txtChildInputReaderName.Text = string.Empty;
            cbChildTxtReaderName.ItemsSource = ucAddLoanHistory.AllChilds;
        }

        private void TxtGuardianInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputGuardianIdentify_Filter_TextChanged(txtGuardianInputIdentify, gdGuardianInput, ucAddLoanHistory.AllGuardians);
        }

        private void TxtInputGuardianIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, ucAddLoanHistory.StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Adult guardian = adultVM.FindByIdentify(getfillList.First().Identify, null);
                AdultDto guardianDto = adultMap.ConvertToDto(guardian);

                GetAllChildOfAdult(guardianDto);

                gdInputChildName.IsEnabled = true;
                return;
            }
            else
            {
                gdInputChildName.IsEnabled = false;
            }
            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion

        #region ChildInput
        private void HandleChild(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return;
            txt.Text = ((ChildDto)cmb.SelectedItem).FullName;
        }

        private void CbChildTxtReaderName_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleChild(txtChildInputReaderName, cmb);
            handle = true;
        }

        private void CbChildTxtReaderName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleChild(txtChildInputReaderName, cmb);
        }

        private void TxtChildInputReaderName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputChildReaderName_Filter_TextChanged(txtChildInputReaderName, gdInputChildName, ucAddLoanHistory.AllChilds);
        }

        private void TxtInputChildReaderName_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<ChildDto> source)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<Child> getfillList = childVM.FillContainsFullName(source, txtInput.Text, ignoreCase, ucAddLoanHistory.StatusValue);
            ObservableCollection<ChildDto> getfillListDto = childMap.ConvertToDto(getfillList);

            if (getfillListDto.Count == 1 && getfillListDto.First().FullName == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;
                Reader reader = readerVM.FindById(getfillListDto.First().IdReader);
                ucAddLoanHistory.SelectedReader = readerMap.ConvertToDto(reader);
                SelectedReader = ucAddLoanHistory.SelectedReader;

                GetLoanSlipFromReader();

                return;
            }
            else
            {
                ucAddLoanHistory.SelectedReader = null;
                SelectedReader = ucAddLoanHistory.SelectedReader;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion
       
        private void GetLoanSlipFromReader()
        {
            ObservableCollection<LoanSlip> loans = loanSlipVM.FillByIdReader(SelectedReader.Id);
            ucAddLoanHistory.AllLoanOfReader.AddRange(loans);


            ucLoanSlipsBorrowedTable.ModifiedPagination();
        }
    }
}
