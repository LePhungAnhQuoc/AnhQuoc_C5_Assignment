using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnhQuoc_C5_Assignment
{
    public class ReturnBookViewModel : BaseViewModel<object>, IPageViewModel
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #region getDatas
        #endregion

        #region Fields
        private bool handle = true;

        private ucAddLoanHistory ucAddLoanHistory;
        private ucRetureBookInfo ucRetureBookInfo;
        private ucInputReaderLoanHistory ucInputReaderLoanHistory;

        private Stack<object> _storeContent;
        public Stack<object> storeContent
        {
            get
            {
                if (_storeContent == null)
                    _storeContent = new Stack<object>();
                return _storeContent;
            }
        }

        #endregion

        #region Constants
        private const double CardWidth = 200;
        private const double CardMargin = 10;
        #endregion

        #region Properties
        public bool IsCancel { get; set; } = true;
        public bool? StatusValue { get; set; } = true;

        #region Datas
        private ObservableCollection<AdultDto> _AllAdults;
        public ObservableCollection<AdultDto> AllAdults
        {
            get { return _AllAdults; }
            set
            {
                _AllAdults = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanSlip> _AllAdultLoan;
        public ObservableCollection<LoanSlip> AllAdultLoan
        {
            get { return _AllAdultLoan; }
            set
            {
                _AllAdultLoan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanDetail> _AllAdultLoanDetail;
        public ObservableCollection<LoanDetail> AllAdultLoanDetail
        {
            get { return _AllAdultLoanDetail; }
            set
            {
                _AllAdultLoanDetail = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanSlip> _AllChildLoan;
        public ObservableCollection<LoanSlip> AllChildLoan
        {
            get { return _AllChildLoan; }
            set
            {
                _AllChildLoan = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<LoanDetail> _AllChildLoanDetail;
        public ObservableCollection<LoanDetail> AllChildLoanDetail
        {
            get { return _AllChildLoanDetail; }
            set
            {
                _AllChildLoanDetail = value;
                OnPropertyChanged();
            }
        }







        private ObservableCollection<AdultDto> _AllGuardians;
        public ObservableCollection<AdultDto> AllGuardians
        {
            get { return _AllGuardians; }
            set
            {
                _AllGuardians = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ChildDto> _AllChilds;
        public ObservableCollection<ChildDto> AllChilds
        {
            get { return _AllChilds; }
            set
            {
                _AllChilds = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BookTitleDto> _AllBookNames;
        public ObservableCollection<BookTitleDto> AllBookNames
        {
            get { return _AllBookNames; }
            set
            {
                _AllBookNames = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _AllLanguages;
        public ObservableCollection<string> AllLanguages
        {
            get { return _AllLanguages; }
            set
            {
                _AllLanguages = value;
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<LoanSlip> _AllLoanOfReader;
        public ObservableCollection<LoanSlip> AllLoanOfReader
        {
            get { return _AllLoanOfReader; }
            set
            {
                _AllLoanOfReader = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PenaltyReasonDto> _AllReason;
        public ObservableCollection<PenaltyReasonDto> AllReason
        {
            get { return _AllReason; }
            set
            {
                _AllReason = value;
                OnPropertyChanged();
            }
        }



        #endregion

        public ObservableCollection<BookDto> UnPaidBooksOfReader { get; set; }
        public ObservableCollection<BookDto> PaidBooksOfReader { get; set; }
        public ObservableCollection<LoanDetail> UnPaidLoanDetailsOfReader { get; set; }
        public ObservableCollection<LoanDetail> PaidLoanDetailsOfReader { get; set; }

        public ObservableCollection<Reader> FillReaderByType { get; set; }
        public ObservableCollection<ucBookCard> AllUnPaidBookCard { get; set; }
        public ObservableCollection<ucBookCard> AllPaidBookCard { get; set; }

        private ucBookCard _SelectedUnPaidBookCard;
        public ucBookCard SelectedUnPaidBookCard
        {
            get { return _SelectedUnPaidBookCard; }
            set
            {
                _SelectedUnPaidBookCard = value;
                OnPropertyChanged();
            }
        }

        private ucBookCard _SelectedPaidBookCard;

        public ucBookCard SelectedPaidBookCard
        {
            get { return _SelectedPaidBookCard; }
            set
            {
                _SelectedPaidBookCard = value;
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

        private ChildDto _SelectedChild;
        public ChildDto SelectedChild
        {
            get { return _SelectedChild; }
            set
            {
                _SelectedChild = value;
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

        private LoanSlipDto _SelectedLoanSlip;
        public LoanSlipDto SelectedLoanSlip
        {
            get { return _SelectedLoanSlip; }
            set
            {
                _SelectedLoanSlip = value;
                OnPropertyChanged();
            }
        }


        private string _InputReason;
        public string InputReason
        {
            get { return _InputReason; }
            set
            {
                _InputReason = value;
                OnPropertyChanged();
            }
        }

        private int _BorrowedDays;
        public int BorrowedDays
        {
            get { return _BorrowedDays; }
            set
            {
                _BorrowedDays = value;
                OnPropertyChanged();
            }
        }

        private decimal _TotalFineAmount;
        public decimal TotalFineAmount
        {
            get { return _TotalFineAmount; }
            set
            {
                _TotalFineAmount = value;
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
        private PenaltyReasonViewModel reasonVM;
        #endregion

        #region Mapping
        private PenaltyReasonMap reasonMap;
        private ReaderMap readerMap;
        private AdultMap adultMap;
        private ChildMap childMap;
        private BookTitleMap bookTitleMap;
        private BookISBNMap bookISBNMap;
        private BookMap bookMap;
        private LoanDetailMap loanDetailMap;
        private LoanDetailHistoryMap loanDetailHistoryMap;
        #endregion

        #region ResultProperty
        private LoanHistoryDto _Item;
        public LoanHistoryDto Item
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


        private LoanDetailHistoryDto _Detail;
        public LoanDetailHistoryDto Detail
        {
            get { return _Detail; }
            set
            {
                _Detail = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LoanDetailHistory> LoanDetailHistorys { get; set; }


        #endregion


        #region RelayCommands
        public RelayCommand ucAddLoanHistoryLoadedCmd { get; private set; }
        public RelayCommand ReaderLoadedCmd { get; private set; }
        public RelayCommand BookLoadedCmd { get; private set; }
        public RelayCommand LoanConfirmLoadedCmd { get; private set; }


        public RelayCommand ReaderBtnConfirmClickCmd { get; private set; }
        public RelayCommand ReaderBtnCancelClickCmd { get; private set; }

        public RelayCommand ReadercbSelectReaderTypeSelectionChangedCmd { get; private set; }


        public RelayCommand ReadercbAdultTxtIdentifyDropDownClosedCmd { get; private set; }
        public RelayCommand ReadercbAdultTxtIdentifySelectionChangedCmd { get; private set; }
        public RelayCommand ReadertxtAdultInputIdentifyTextChangedCmd { get; private set; }

        public RelayCommand ReadercbGuardianTxtIdentifyDropDownClosedCmd { get; set; }
        public RelayCommand ReadercbGuardianTxtIdentifySelectionChangedCmd { get; set; }
        public RelayCommand ReadertxtGuardianInputIdentifyTextChangedCmd { get; set; }

        public RelayCommand ReadercbChildTxtReaderNameDropDownClosedCmd { get; set; }
        public RelayCommand ReadercbChildTxtReaderNameSelectionChangedCmd { get; set; }
        public RelayCommand ReadertxtChildInputReaderNameTextChangedCmd { get; set; }

        public RelayCommand BookBtnReturnBookClickCmd { get; private set; }
        public RelayCommand BookBtnFinishClickCmd { get; private set; }
        public RelayCommand BookBtnCancelClickCmd { get; private set; }


        public RelayCommand BookcbTxtReasonDropDownClosedCmd { get; set; }
        public RelayCommand BookcbTxtReasonSelectionChangedCmd { get; set; }
        public RelayCommand BooktxtReasonTextChangedCmd { get; set; }

        #endregion


        public ReturnBookViewModel()
        {
            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();
            AllLoanOfReader = new ObservableCollection<LoanSlip>();
            AllUnPaidBookCard = new ObservableCollection<ucBookCard>();
            AllPaidBookCard = new ObservableCollection<ucBookCard>();

            #region Allocates
            reasonVM = UnitOfViewModel.Instance.PenaltyReasonViewModel;
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanHistoryVM = UnitOfViewModel.Instance.LoanHistoryViewModel;
            loanDetailHistoryVM = UnitOfViewModel.Instance.LoanDetailHistoryViewModel;

            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;

            reasonMap = UnitOfMap.Instance.PenaltyReasonMap;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;
            loanDetailHistoryMap = UnitOfMap.Instance.LoanDetailHistoryMap;

            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;
            #endregion
                        
            #region Init-RelayCmds
            ucAddLoanHistoryLoadedCmd = new RelayCommand(ucAddLoanHistoryLoaded);
            ReaderLoadedCmd = new RelayCommand(ReaderLoaded);
            BookLoadedCmd = new RelayCommand(BookLoaded);
            LoanConfirmLoadedCmd = new RelayCommand(LoanConfirmLoaded);



            ReaderBtnConfirmClickCmd = new RelayCommand(ReaderBtnConfirmClick);
            ReaderBtnCancelClickCmd = new RelayCommand(ReaderBtnCancelClick);


            ReadercbSelectReaderTypeSelectionChangedCmd = new RelayCommand(ReadercbSelectReaderTypeSelectionChanged);


            ReadercbAdultTxtIdentifyDropDownClosedCmd = new RelayCommand(null, ReadercbAdultTxtIdentifyDropDownClosed);
            ReadercbAdultTxtIdentifySelectionChangedCmd = new RelayCommand(null, ReadercbAdultTxtIdentifySelectionChanged);
            ReadertxtAdultInputIdentifyTextChangedCmd = new RelayCommand(null, ReadertxtAdultInputIdentifyTextChanged);


            ReadercbGuardianTxtIdentifyDropDownClosedCmd = new RelayCommand(null, ReadercbGuardianTxtIdentifyDropDownClosed);
            ReadercbGuardianTxtIdentifySelectionChangedCmd = new RelayCommand(null, ReadercbGuardianTxtIdentifySelectionChanged);
            ReadertxtGuardianInputIdentifyTextChangedCmd = new RelayCommand(null, ReadertxtGuardianInputIdentifyTextChanged);


            ReadercbChildTxtReaderNameDropDownClosedCmd = new RelayCommand(null, ReadercbChildTxtReaderNameDropDownClosed);
            ReadercbChildTxtReaderNameSelectionChangedCmd = new RelayCommand(null, ReadercbChildTxtReaderNameSelectionChanged);
            ReadertxtChildInputReaderNameTextChangedCmd = new RelayCommand(null, ReadertxtChildInputReaderNameTextChanged);

            BookBtnReturnBookClickCmd = new RelayCommand(BookBtnReturnBookClick);
            BookBtnFinishClickCmd = new RelayCommand(BookBtnFinishClick);
            BookBtnCancelClickCmd = new RelayCommand(BookBtnCancelClick);

            BookcbTxtReasonDropDownClosedCmd = new RelayCommand(null, BookcbTxtReasonDropDownClosed);
            BookcbTxtReasonSelectionChangedCmd = new RelayCommand(null, BookcbTxtReasonSelectionChanged);
            BooktxtReasonTextChangedCmd = new RelayCommand(null, BooktxtReasonTextChanged);
            #endregion


        }

        #region AddLoanHistory-Commands
        private void ucAddLoanHistoryLoaded(object para)
        {
            ucAddLoanHistory = para as ucAddLoanHistory;

            AllReason = reasonMap.ConvertToDto(ucAddLoanHistory.getReasonRepo().Gets());

            ucInputReaderLoanHistory = MainWindow.UnitOfForm.UcInputReaderLoanHistory(true);
            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectionChanged += DgDatas_SelectionChanged;

            LoanDetailHistorys = new ObservableCollection<LoanDetailHistory>();
            PaidBooksOfReader = new ObservableCollection<BookDto>();

            storeContent.Push(ucAddLoanHistory.Content);
            ucAddLoanHistory.Content = ucInputReaderLoanHistory;
        }
        #endregion

        #region Reader-Implement-Commands
        private void ReaderLoaded(object para)
        {
            SelectedReaderType = ReaderType.Adult;
            ReadercbSelectReaderTypeSelectionChanged(ucInputReaderLoanHistory.cbSelectReaderType);

            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllLoanOfReader;
            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();

            ucInputReaderLoanHistory.stkAdultInformation.Visibility = Visibility.Visible;
            ucInputReaderLoanHistory.stkChildInformation.Visibility = Visibility.Collapsed;
        }

        private void ReaderBtnConfirmClick(object para)
        {
            if (SelectedReader == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("reader"));
                return;
            }
            SelectedReader = SelectedReader;

            if (ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectedItem == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("loan slip"));
                return;
            }

            NewItem();

            ucRetureBookInfo = MainWindow.UnitOfForm.UcRetureBookInfo(true);

            storeContent.Push(ucInputReaderLoanHistory.Content);
            ucInputReaderLoanHistory.Content = ucRetureBookInfo;
        }

        private void ReaderBtnCancelClick(object para)
        {
            CloseNotSave();
        }

        private void ReadercbSelectReaderTypeSelectionChanged(object para)
        {
            if (SelectedReaderType == null) return;

            var readerType = (ReaderType)SelectedReaderType;
            if (readerType == ReaderType.Adult)
            {
                handle = true;
                FillReaderByType = readerVM.FillListByType(readerType, StatusValue);

                var FillReaderByTypeDto = readerMap.ConvertToDto(FillReaderByType);
                var adults = adultVM.GetListFromReaders(FillReaderByType, StatusValue);

                AllAdults = adultMap.ConvertToDto(adults);
                ucInputReaderLoanHistory.cbAdultTxtIdentify.ItemsSource = AllAdults;

                ucInputReaderLoanHistory.stkAdultInformation.Visibility = Visibility.Visible;
                ucInputReaderLoanHistory.stkChildInformation.Visibility = Visibility.Collapsed;
            }
            else
            {
                handle = true;
                FillReaderByType = readerVM.FillListByType(readerType, StatusValue);


                var FillReaderByTypeDto = readerMap.ConvertToDto(FillReaderByType);
                var childs = childVM.GetListFromReaders(FillReaderByType, StatusValue);
                var childDTOs = childMap.ConvertToDto(childs);


                var adults = adultVM.GetListFromChilds(childs, StatusValue);

                AllGuardians = adultMap.ConvertToDto(adults);
                ucInputReaderLoanHistory.cbGuardianTxtIdentify.ItemsSource = AllGuardians;

                ucInputReaderLoanHistory.stkAdultInformation.Visibility = Visibility.Collapsed;
                ucInputReaderLoanHistory.stkChildInformation.Visibility = Visibility.Visible;
            }
        }
        

        private bool HandleAdult(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((AdultDto)cmb.SelectedItem).Identify;
            return true;
        }

        #region AdultInput
        private void ReadercbAdultTxtIdentifyDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleAdult(ucInputReaderLoanHistory.txtAdultInputIdentify, cmb);
            handle = true;
        }

        private void ReadercbAdultTxtIdentifySelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(ucInputReaderLoanHistory.txtAdultInputIdentify, cmb);
        }

        private void ReadertxtAdultInputIdentifyTextChanged(object para)
        {
            TxtInputIdentify_Filter_TextChanged(ucInputReaderLoanHistory.txtAdultInputIdentify, ucInputReaderLoanHistory.gdInputAdultIndentify, AllAdults);
        }

        private void TxtInputIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Reader reader = readerVM.FindById(getfillList.First().IdReader);
                SelectedReader = readerMap.ConvertToDto(reader);

                GetReaderLoanAndLoanDetails();

                if (SelectedReaderType == ReaderType.Adult)
                    ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllAdultLoan;
                else if (SelectedReaderType == ReaderType.Child)
                    ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllChildLoan;

                ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();
                return;
            }
            else
            {
                SelectedReader = null;
            }
            comBoBox.ItemsSource = adultMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        private void GetReaderLoanAndLoanDetails()
        {
            if (SelectedReaderType == ReaderType.Adult)
            {
                AllAdultLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllAdultLoanDetail = loanDetailVM.FillListByIdLoans(AllAdultLoan);
            }
            else if (SelectedReaderType == ReaderType.Child)
            {
                AllChildLoan = loanSlipVM.FillByIdReader(SelectedReader.Id);
                AllChildLoanDetail = loanDetailVM.FillListByIdLoans(AllChildLoan);
            }
        }


        #endregion

        #region GuardianInput
        private void ReadercbGuardianTxtIdentifyDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleAdult(ucInputReaderLoanHistory.txtGuardianInputIdentify, cmb);
            handle = true;
        }

        private void ReadercbGuardianTxtIdentifySelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleAdult(ucInputReaderLoanHistory.txtGuardianInputIdentify, cmb);
        }

        private void GetAllChildOfAdult(AdultDto adultDto)
        {
            AllChilds = childMap.ConvertToDto(adultDto.ListChild);
            ucInputReaderLoanHistory.txtChildInputReaderName.Text = string.Empty;
            ucInputReaderLoanHistory.cbChildTxtReaderName.ItemsSource = AllChilds;
        }

        private void ReadertxtGuardianInputIdentifyTextChanged(object para)
        {
            TxtInputGuardianIdentify_Filter_TextChanged(ucInputReaderLoanHistory.txtGuardianInputIdentify, ucInputReaderLoanHistory.gdGuardianInput, AllGuardians);
        }

        private void TxtInputGuardianIdentify_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<AdultDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<Adult> getfillList = adultVM.FillContainsIdentify(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Identify == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                Adult guardian = adultVM.FindByIdentify(getfillList.First().Identify, null);
                AdultDto guardianDto = adultMap.ConvertToDto(guardian);

                GetAllChildOfAdult(guardianDto);

                ucInputReaderLoanHistory.gdInputChildName.IsEnabled = true;
                return;
            }
            else
            {
                ucInputReaderLoanHistory.gdInputChildName.IsEnabled = false;
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

        private void ReadercbChildTxtReaderNameDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleChild(ucInputReaderLoanHistory.txtChildInputReaderName, cmb);
            handle = true;
        }

        private void ReadercbChildTxtReaderNameSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleChild(ucInputReaderLoanHistory.txtChildInputReaderName, cmb);
        }

        private void ReadertxtChildInputReaderNameTextChanged(object para)
        {
            TxtInputChildReaderName_Filter_TextChanged(ucInputReaderLoanHistory.txtChildInputReaderName, ucInputReaderLoanHistory.gdInputChildName, AllChilds);
        }

        private void TxtInputChildReaderName_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<ChildDto> source)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<Child> getfillList = childVM.FillContainsFullName(source, txtInput.Text, ignoreCase, StatusValue);
            ObservableCollection<ChildDto> getfillListDto = childMap.ConvertToDto(getfillList);

            if (getfillListDto.Count == 1 && getfillListDto.First().FullName == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;
                Reader reader = readerVM.FindById(getfillListDto.First().IdReader);

                SelectedReader = readerMap.ConvertToDto(reader);



                GetReaderLoanAndLoanDetails();

                if (SelectedReaderType == ReaderType.Adult)
                    ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllAdultLoan;
                else if (SelectedReaderType == ReaderType.Child)
                    ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.getLoanSlips = () => AllChildLoan;

                ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();
                
                return;
            }
            else
            {
                SelectedReader = null;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion



        private void DgDatas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectedItem == null) return;

            SelectedLoanSlip = ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.dgDatas.SelectedItem as LoanSlipDto;
        }

        private void NewItem()
        {
            string newId = loanHistoryVM.GetId();
            Item = new LoanHistoryDto(newId);
            Utilities.Copy(Item, SelectedLoanSlip);
            Item.Id = newId;
            Item.CreateAt = DateTime.Now;
        }

        private void GetLoanSlipFromReader()
        {
            ObservableCollection<LoanSlip> loans = loanSlipVM.FillByIdReader(SelectedReader.Id);
            AllLoanOfReader.AddRange(loans);

            ucInputReaderLoanHistory.ucLoanSlipsBorrowedTable.ModifiedPagination();
        }
        #endregion

        #region ReturnBookInfo-Commands
        private void BookLoaded(object para)
        {
            ucRetureBookInfo.cbTxtReason.ItemsSource = AllReason;

            BorrowedDays = (DateTime.Now - SelectedLoanSlip.LoanDate).Days;

            GetDetailsFromLoanSlip();
            GetBooksFromLoanSlip();

            ConvertToUnPaidBookCard(UnPaidBooksOfReader);
            AddUnPaidBookCardToWrap();
            ConvertToPaidBookCard(PaidBooksOfReader);
            AddPaidBookCardToWrap();

            if (AllUnPaidBookCard.Count > 0)
            {
                UcUnPaidBookCard_MouseLeftButtonDown(AllUnPaidBookCard.FirstOrDefault(), null);
            }
            if (AllPaidBookCard.Count > 0)
            {
                UcPaidBookCard_MouseLeftButtonDown(AllPaidBookCard.FirstOrDefault(), null);
            }
        }


        private void BookBtnReturnBookClick(object para)
        {
            if (SelectedUnPaidBookCard == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                return;
            }

            BookDto getBook = bookVM.FindById(UnPaidBooksOfReader, SelectedUnPaidBookCard.Item.Id, null);
            Detail.IdBook = getBook.Id;

            bool isCheckEmptyItem = loanDetailHistoryVM.IsCheckEmptyItem(Detail, true, nameof(Detail.Note), nameof(Detail.PaidMoney));

            bool isHasError = ucRetureBookInfo.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                ucRetureBookInfo.RunAllValidations();
                return;
            }

            UnPaidBooksOfReader.Remove(getBook);
            PaidBooksOfReader.Add(getBook);

            // Add to list details
            var getDetail = loanDetailHistoryVM.CreateByDto(Detail);
            LoanDetailHistorys.Add(getDetail);

            ConvertToUnPaidBookCard(UnPaidBooksOfReader);
            AddUnPaidBookCardToWrap();
            ConvertToPaidBookCard(PaidBooksOfReader);
            AddPaidBookCardToWrap();

            Item.FineMoney += Detail.PaidMoney;
            Item.Total = Item.LoanPaid + Item.FineMoney;

            // Change button status Visibility
            if (UnPaidBooksOfReader.Count == 0)
            {
                ucRetureBookInfo.btnFinish.Visibility = Visibility.Visible;
                ucRetureBookInfo.btnReturnBook.Visibility = Visibility.Collapsed;
            }
            else
            {
                ucRetureBookInfo.btnFinish.Visibility = Visibility.Collapsed;
                ucRetureBookInfo.btnReturnBook.Visibility = Visibility.Visible;
            }

            if (AllUnPaidBookCard.Count > 0)
            {
                UcUnPaidBookCard_MouseLeftButtonDown(AllUnPaidBookCard.FirstOrDefault(), null);
            }
            if (AllPaidBookCard.Count > 0)
            {
                UcPaidBookCard_MouseLeftButtonDown(AllPaidBookCard.FirstOrDefault(), null);
            }
        }

        private void BookBtnFinishClick(object para)
        {
            frmDefault frmConfirmInformation = new frmDefault();

            ucLoanHistoryConfirm ucLoanHistoryConfirm = new ucLoanHistoryConfirm();

            ucLoanHistoryConfirm.Margin = new Thickness(10);
            
            Button btnConfirm = ucLoanHistoryConfirm.btnConfirm;
            Button btnCancel = ucLoanHistoryConfirm.btnCancel;

            btnConfirm.Click += (_sender, _e) =>
            {
                CloseAndSave();
                frmConfirmInformation.Close();
            };

            btnCancel.Click += (_sender, _e) =>
            {
                frmConfirmInformation.Close();
            };
            
            Utilities.AddItemToFormDefault(frmConfirmInformation, ucLoanHistoryConfirm);

            frmConfirmInformation.Width = 800;
            frmConfirmInformation.SizeToContent = SizeToContent.Height;

            frmConfirmInformation.ShowDialog();
        }

        private void BookBtnCancelClick(object para)
        {
            ucRetureBookInfo.Content = storeContent.Pop();
        }

        #region InputReason

        private bool HandleReason(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return false;
            txt.Text = ((PenaltyReasonDto)cmb.SelectedItem).Name;
            return true;
        }


        private void BookcbTxtReasonDropDownClosed(object para)
        {
            ComboBox cmb = para as ComboBox;
            if (handle) HandleReason(ucRetureBookInfo.txtReason, cmb);
            handle = true;
        }

        private void BookcbTxtReasonSelectionChanged(object para)
        {
            ComboBox cmb = para as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleReason(ucRetureBookInfo.txtReason, cmb);
        }

        private void BooktxtReasonTextChanged(object para)
        {
            TxtInputReason_Filter_TextChanged(ucRetureBookInfo.txtReason, ucRetureBookInfo.gdInputReason, AllReason);
        }

        private void TxtInputReason_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<PenaltyReasonDto> sourceDto)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;

            ObservableCollection<PenaltyReason> getfillList = reasonVM.FillContainsName(sourceDto, txtInput.Text, ignoreCase, StatusValue);

            if (getfillList.Count == 1 && getfillList.First().Name == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                PenaltyReason reason = getfillList.First();

                if (reason.Name == "None")
                {
                    Detail.PaidMoney = 0;
                }
                else if (reason.Name == "Lost the book")
                {
                    Detail.PaidMoney = SelectedUnPaidBookCard.Item.PriceCurrent;
                }
                else if (reason.Name == "Spoil the book")
                {
                    Detail.PaidMoney = 0;
                }
                return;
            }
            else
            {
            }
            comBoBox.ItemsSource = reasonMap.ConvertToDto(getfillList);
            comBoBox.IsDropDownOpen = true;
        }

        #endregion
        

        private void NewDetail()
        {
            int indexId = ucAddLoanHistory.getLoanDetailHistoryRepo().Count + LoanDetailHistorys.Count;
            Detail = new LoanDetailHistoryDto(loanDetailHistoryVM.GetId(indexId));
            Detail.IdLoanHistory = Item.Id;
            Detail.ExpDate = Item.ExpDate;
        }

        private void UnPaidBookCards_SelectionChanged()
        {
            NewDetail();
            ucRetureBookInfo.txtReason.Text = string.Empty;
        }


        private void ChangeBookStatus(bool updateStatus)
        {
            foreach (var loanDetail in LoanDetailHistorys)
            {
                Book book = bookVM.FindById(loanDetail.IdBook, null);

                if (book.Status != updateStatus)
                {
                    book.Status = updateStatus;

                    // Save change to database
                    ucAddLoanHistory.getBookRepo().WriteUpdateStatus(book, updateStatus);
                }
            }
            foreach (BookISBN isbn in ucAddLoanHistory.getBookISBNRepo())
            {
                var books = bookVM.FillByBookISBN(isbn.ISBN, true);
                if (books.Count == 0)
                {
                    isbn.Status = false;
                }
                else
                {
                    isbn.Status = true;
                }
                ucAddLoanHistory.getBookISBNRepo().WriteUpdateStatus(isbn, isbn.Status);
            }
        }

        private void CalculatePayMent()
        {
            foreach (var detail in LoanDetailHistorys)
            {
                Item.FineMoney += detail.PaidMoney;
            }
            Item.Total = Item.FineMoney + Item.LoanPaid;
        }

        private void PassValueToItem(LoanHistory item)
        {
            Utilities.Copy(item, Item);
        }

        private void GetDetailsFromLoanSlip()
        {
            UnPaidLoanDetailsOfReader = loanDetailVM.FillListByIdLoan(SelectedLoanSlip.Id);
        }

        private void GetBooksFromLoanSlip()
        {
            UnPaidBooksOfReader = bookMap.ConvertToDto(bookVM.GetBooksInLoanDetails(UnPaidLoanDetailsOfReader));
        }




        private void UcPaidBookCard_btnDeleteClick(object sender, RoutedEventArgs e)
        {
            BookDto getBook = bookVM.FindById(PaidBooksOfReader, SelectedPaidBookCard.Item.Id, null);
            LoanDetailHistory detail = loanDetailHistoryVM.FindByIdBook(LoanDetailHistorys, getBook.Id);


            UnPaidBooksOfReader.Add(getBook);
            PaidBooksOfReader.Remove(getBook);


            // Remove in list details
            LoanDetailHistorys.Remove(detail);

            ConvertToUnPaidBookCard(UnPaidBooksOfReader);
            AddUnPaidBookCardToWrap();
            ConvertToPaidBookCard(PaidBooksOfReader);
            AddPaidBookCardToWrap();

            Item.FineMoney -= detail.PaidMoney;
            Item.Total = Item.LoanPaid + Item.FineMoney;



            // Change button status Visibility
            if (UnPaidBooksOfReader.Count == 0)
            {
                ucRetureBookInfo.btnFinish.Visibility = Visibility.Visible;
                ucRetureBookInfo.btnReturnBook.Visibility = Visibility.Collapsed;
            }
            else
            {
                ucRetureBookInfo.btnFinish.Visibility = Visibility.Collapsed;
                ucRetureBookInfo.btnReturnBook.Visibility = Visibility.Visible;
            }

            if (AllUnPaidBookCard.Count > 0)
            {
                UcUnPaidBookCard_MouseLeftButtonDown(AllUnPaidBookCard.FirstOrDefault(), null);
            }
            if (AllPaidBookCard.Count > 0)
            {
                UcPaidBookCard_MouseLeftButtonDown(AllPaidBookCard.FirstOrDefault(), null);
            }
        }

        private void ConvertToUnPaidBookCard(ObservableCollection<BookDto> books)
        {
            AllUnPaidBookCard.Clear();
            foreach (var book in books)
            {
                ucBookCard ucUnPaidBookCard = new ucBookCard();
                ucUnPaidBookCard.Width = CardWidth;
                ucUnPaidBookCard.Margin = new Thickness(CardMargin);
                ucUnPaidBookCard.Focusable = true;
                ucUnPaidBookCard.MouseLeftButtonDown += UcUnPaidBookCard_MouseLeftButtonDown;
                ucUnPaidBookCard.getItem = () => book;
                AllUnPaidBookCard.Add(ucUnPaidBookCard);
            }
        }

        private void UcUnPaidBookCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookCard card = sender as ucBookCard;
            if (SelectedUnPaidBookCard != null && SelectedUnPaidBookCard == card) return;

            SelectedUnPaidBookCard = card;
            UnPaidBookCards_SelectionChanged();
        }

        private void AddUnPaidBookCardToWrap()
        {
            ucRetureBookInfo.wrapUnPaidBooksTable.Children.Clear();
            foreach (var ucCard in AllUnPaidBookCard)
            {
                ucRetureBookInfo.wrapUnPaidBooksTable.Children.Add(ucCard);
            }
        }

        private void ConvertToPaidBookCard(ObservableCollection<BookDto> books)
        {
            AllPaidBookCard.Clear();
            foreach (var book in books)
            {
                ucBookCard ucPaidBookCard = new ucBookCard();

                ucPaidBookCard.btnInfoClick += (_sender, _e) =>
                {
                    frmDefault frmInfo = new frmDefault();
                    ucLoanDetailHistoryInfo ucLoanDetailHistoryInfo = new ucLoanDetailHistoryInfo();

                    LoanDetailHistory getDetailHistory = LoanDetailHistorys.FirstOrDefault(item => item.IdBook == book.Id);
                    ucLoanDetailHistoryInfo.getItem = () => loanDetailHistoryMap.ConvertToDto(getDetailHistory);

                    ucLoanDetailHistoryInfo.btnBackClick += (_subSender, _subE) =>
                    {
                        frmInfo.Close();
                    };

                    Utilities.AddItemToFormDefault(frmInfo, ucLoanDetailHistoryInfo);
                    frmInfo.ShowDialog();
                };

                ucPaidBookCard.btnDeleteClick += (_sender, _e) =>
                {
                    ucBookCard card = ucPaidBookCard;
                    if (SelectedPaidBookCard != null && SelectedPaidBookCard == card) return;

                    SelectedPaidBookCard = card;
                };
                ucPaidBookCard.btnDeleteClick += UcPaidBookCard_btnDeleteClick;

                ucPaidBookCard.Width = CardWidth;
                ucPaidBookCard.Margin = new Thickness(CardMargin);
                ucPaidBookCard.Focusable = true;
                ucPaidBookCard.MouseLeftButtonDown += UcPaidBookCard_MouseLeftButtonDown;
                ucPaidBookCard.getItem = () => book;
                AllPaidBookCard.Add(ucPaidBookCard);
            }
        }

        private void UcPaidBookCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookCard card = sender as ucBookCard;
            if (SelectedPaidBookCard != null && SelectedPaidBookCard == card) return;

            SelectedPaidBookCard = card;
        }

        private void AddPaidBookCardToWrap()
        {
            ucRetureBookInfo.wrapPaidBooksTable.Children.Clear();
            foreach (var ucCard in AllPaidBookCard)
            {
                ucRetureBookInfo.wrapPaidBooksTable.Children.Add(ucCard);
            }
        }

        #endregion

        #region LoanConfirm-Commands
        private void LoanConfirmLoaded(object para)
        {
        }
        #endregion


        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (sender as TextBox);
            txt.Text = txt.Text.Trim();
        }

        private void Closing()
        {
            if (IsCancel)
                Item = null;

            ucAddLoanHistory.getParentUc().BackToMainPage();
        }

        public void CloseAndSave()
        {
            if (LoanDetailHistorys.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't return any book yet");
                return;
            }

            LoanSlip loanSlip = loanSlipVM.FindById(SelectedLoanSlip.Id);
            ObservableCollection<LoanDetail> loanDetails = loanDetailVM.FillListByIdLoan(loanSlip.Id);

            if (LoanDetailHistorys.Count < loanDetails.Count)
            {
                Utilities.ShowMessageBox1("Please return all the books of this loan slip");
                return;
            }

            #region CheckIsExistInformation

            #endregion

            // Truyền dữ liệu cho item
            var loanHistory = new LoanHistory();
            PassValueToItem(loanHistory);

            #region Remove-LoanSlip

            ucAddLoanHistory.getLoanDetailRepo().RemoveRange(loanDetails);
            ucAddLoanHistory.getLoanDetailRepo().WriteDeleteRange(loanDetails);

            ucAddLoanHistory.getLoanSlipRepo().Remove(loanSlip);
            ucAddLoanHistory.getLoanSlipRepo().WriteDelete(loanSlip);
            #endregion

            #region Add-LoanHistory
            ucAddLoanHistory.getLoanHistoryRepo().Add(loanHistory);
            ucAddLoanHistory.getLoanHistoryRepo().WriteAdd(loanHistory);

            ucAddLoanHistory.getLoanDetailHistoryRepo().AddRange(LoanDetailHistorys);
            ucAddLoanHistory.getLoanDetailHistoryRepo().WriteAddRange(LoanDetailHistorys);
            #endregion

            ChangeBookStatus(true);
            LoanDetailHistorys.Clear();

            IsCancel = false;
            Closing();
        }

        public void CloseNotSave()
        {
            Item = null;
            Closing();
        }
    }
}
