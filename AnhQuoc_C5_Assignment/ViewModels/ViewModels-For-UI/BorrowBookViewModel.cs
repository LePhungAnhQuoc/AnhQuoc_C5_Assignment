﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    class BorrowBookViewModel : BaseViewModel<object>, IPageViewModel
    {

        // DTOs (Data, Pagination, PropertyChanged-tool)
        #region getDatas
        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<LoanDetailRepository> getLoanDetailRepo { get; set; }
        public Func<LoanSlipRepository> getLoanSlipRepo { get; set; }
        public Func<ucLoanSlipManagement> getParentUc { get; set; }
        public Func<ucAddLoan> getUcAddLoan { get; set; }
        public Func<ucSelectReaderInfo> getucSelectReaderInfo { get; set; }
        #endregion

        #region Forms
        private ucSelectReaderInfo ucSelectReaderInfo;
        private ucInputBookInfo ucInputBookInfo;
        #endregion

        #region Constants
        private const int CardWidth = 300;
        #endregion

        #region Fields
        private bool handle;

        private Stack<object> _storeContent;
        public Stack<object> storeContent
        {
            get
            {
                if (_storeContent == null)
                    _storeContent = new Stack<object>();
                return _storeContent;
            }
            set { _storeContent = value; }
        }


        #endregion

        #region Properties
        public bool IsCancel { get; set; } = true;

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
        #endregion

        private ObservableCollection<Book> _BooksOfReader;
        public ObservableCollection<Book> BooksOfReader
        {
            get { return _BooksOfReader; }
            set
            {
                _BooksOfReader = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Book> _TotalBooksOfReader;
        public ObservableCollection<Book> TotalBooksOfReader
        {
            get
            {
                if (BooksOfReader != null && AllChildBook != null)
                {
                    return BooksOfReader.Concat(AllChildBook).ToObservableCollection();
                }
                return null;
            }
        }


        private ObservableCollection<Child> _AllChildOfAdult;
        public ObservableCollection<Child> AllChildOfAdult
        {
            get { return _AllChildOfAdult; }
            set
            {
                _AllChildOfAdult = value;
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

        private ObservableCollection<Book> _AllChildBook;
        public ObservableCollection<Book> AllChildBook
        {
            get { return _AllChildBook; }
            set
            {
                _AllChildBook = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BookISBN> AllBookISBN { get; set; }
        public ObservableCollection<ucBookISBNCard> AllBookISBNCard { get; set; }
        public ObservableCollection<ucLoanDetailCard> AllLoanDetailCard { get; set; }


        public ObservableCollection<Reader> FillReaderByType { get; set; }
        public bool? BookISBNStatusValue { get; set; } = null;
        public bool? BookStatusValue { get; set; } = null;
        public bool? StatusValue { get; set; } = true;
        public int TotalQuantity { get; set; }


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

        private BookDto _SelectedBook;
        public BookDto SelectedBook
        {
            get { return _SelectedBook; }
            set
            {
                _SelectedBook = value;
                OnPropertyChanged();
            }
        }

        private BookISBNDto _SelectedISBN;
        public BookISBNDto SelectedISBN
        {
            get { return _SelectedISBN; }
            set
            {
                _SelectedISBN = value;
                OnPropertyChanged();
            }
        }

        private ucLoanDetailCard _SelectedDetailCard;
        public ucLoanDetailCard SelectedDetailCard
        {
            get { return _SelectedDetailCard; }
            set
            {
                _SelectedDetailCard = value;
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
        #endregion

        #region ResultProperty
        private LoanSlipDto _LoanSlipDto;
        public LoanSlipDto LoanSlipDto
        {
            get { return _LoanSlipDto; }
            set
            {
                _LoanSlipDto = value;
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


        public ObservableCollection<LoanDetail> LoanDetails { get; set; }
        #endregion


        public string Name { get; }



        // All RelayCommand
        public RelayCommand ReaderBtnConfirmClickCmd { get; private set; }
        public RelayCommand ReaderBtnCancelClickCmd { get; private set; }
        public RelayCommand ReadercbSelectReaderTypeSelectionChangedCmd { get; private set; }


        public BorrowBookViewModel()
        {
            Name = "Search Page";

            AllBookISBNCard = new ObservableCollection<ucBookISBNCard>();
            
            AllReaderTypes = Utilities.GetListFromEnum<ReaderType>().ToObservableCollection();

            #region Allocates
            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            loanSlipVM = UnitOfViewModel.Instance.LoanSlipViewModel;
            loanDetailVM = UnitOfViewModel.Instance.LoanDetailViewModel;

            bookTitleVM = UnitOfViewModel.Instance.BookTitleViewModel;
            bookISBNVM = UnitOfViewModel.Instance.BookISBNViewModel;
            bookVM = UnitOfViewModel.Instance.BookViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            readerMap = UnitOfMap.Instance.ReaderMap;
            adultMap = UnitOfMap.Instance.AdultMap;
            childMap = UnitOfMap.Instance.ChildMap;
            bookTitleMap = UnitOfMap.Instance.BookTitleMap;
            bookISBNMap = UnitOfMap.Instance.BookISBNMap;
            bookMap = UnitOfMap.Instance.BookMap;
            loanDetailMap = UnitOfMap.Instance.LoanDetailMap;
            #endregion

            #region SetTextBoxMaxLength
            #endregion

            #region Register-Event
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            cbSelectReaderType.SelectionChanged += CbSelectReaderType_SelectionChanged;

            cbAdultTxtIdentify.DropDownClosed += CbAdultTxtIdentify_DropDownClosed;
            cbAdultTxtIdentify.SelectionChanged += CbAdultTxtIdentify_SelectionChanged;
            txtAdultInputIdentify.TextChanged += TxtInputIdentify_TextChanged;

            cbGuardianTxtIdentify.DropDownClosed += CbGuardianTxtIdentify_DropDownClosed;
            cbGuardianTxtIdentify.SelectionChanged += CbGuardianTxtIdentify_SelectionChanged;
            txtGuardianInputIdentify.TextChanged += TxtGuardianInputIdentify_TextChanged;

            cbChildTxtReaderName.DropDownClosed += CbChildTxtReaderName_DropDownClosed;
            cbChildTxtReaderName.SelectionChanged += CbChildTxtReaderName_SelectionChanged;
            txtChildInputReaderName.TextChanged += TxtChildInputReaderName_TextChanged;

            cbTxtBookName.DropDownClosed += CbTxtBookName_DropDownClosed;
            cbTxtBookName.SelectionChanged += CbTxtBookName_SelectionChanged;
            txtInputBookName.TextChanged += TxtInputBookName_TextChanged;

            gdInputChildName.IsEnabledChanged += GdInputChildName_IsEnabledChanged;
            gdInputBookName.IsEnabledChanged += GdInputBookName_IsEnabledChanged;

            bdInputBookInformation.IsEnabledChanged += BdInputBookInformation_IsEnabledChanged;
            #endregion


            // Load Form
            ucSelectReaderInfo = MainWindow.UnitOfForm.UcSelectReaderInfo(true);
            ucSelectReaderInfo.getItem = () => LoanSlipDto;
            ucSelectReaderInfo.getStoreContent = () => storeContent;
            ucSelectReaderInfo.getParentUc = getUcAddLoan;

            storeContent.Push(getUcAddLoan().Content);
            getUcAddLoan().Content = ucSelectReaderInfo;

            NewItem();

            // Implement command

            ReaderBtnConfirmClickCmd = new RelayCommand(null, ReaderBtnConfirmClick);
            ReaderBtnCancelClickCmd = new RelayCommand(null, ReaderBtnCancelClick);
        }

        private void ReaderBtnConfirmClick(object para)
        {
            ucInputBookInfo = MainWindow.UnitOfForm.UcInputBookInfo(true);
            ucInputBookInfo.getItem = () => LoanSlipDto;
            ucInputBookInfo.getStoreContent = () => storeContent;
            ucInputBookInfo.getParentUc = getUcAddLoan;

            storeContent.Push(getucSelectReaderInfo().Content);
            getucSelectReaderInfo().Content = ucInputBookInfo;
        }

        private void ReaderBtnCancelClick(object para)
        {
            getucSelectReaderInfo().Content = storeContent.Pop();
        }

        private void ReadercbSelectReaderTypeSelectionChanged(object para)
        {
            ComboBox cbSelectReaderType = para as ComboBox;
            if (cbSelectReaderType.SelectedItem == null) return;

            var readerType = (ReaderType)cbSelectReaderType.SelectedItem;
            if (readerType == ReaderType.Adult)
            {
                handle = true;
                ucAddLoan.FillReaderByType = readerVM.FillListByType(readerType, ucAddLoan.StatusValue);

                var FillReaderByTypeDto = readerMap.ConvertToDto(ucAddLoan.FillReaderByType);
                var adults = adultVM.GetListFromReaders(ucAddLoan.FillReaderByType, ucAddLoan.StatusValue);

                ucAddLoan.AllAdults = adultMap.ConvertToDto(adults);
                cbAdultTxtIdentify.ItemsSource = ucAddLoan.AllAdults;

                stkAdultInformation.Visibility = Visibility.Visible;
                stkChildInformation.Visibility = Visibility.Collapsed;
            }
            else
            {
                handle = true;
                ucAddLoan.FillReaderByType = readerVM.FillListByType(readerType, ucAddLoan.StatusValue);


                var FillReaderByTypeDto = readerMap.ConvertToDto(ucAddLoan.FillReaderByType);
                var childs = childVM.GetListFromReaders(ucAddLoan.FillReaderByType, ucAddLoan.StatusValue);
                var childDTOs = childMap.ConvertToDto(childs);


                var adults = adultVM.GetListFromChilds(childs, ucAddLoan.StatusValue);

                ucAddLoan.AllGuardians = adultMap.ConvertToDto(adults);
                cbGuardianTxtIdentify.ItemsSource = ucAddLoan.AllGuardians;

                stkAdultInformation.Visibility = Visibility.Collapsed;
                stkChildInformation.Visibility = Visibility.Visible;
            }
        }


        private void NewItem()
        {
            LoanSlipDto = new LoanSlipDto(loanSlipVM.GetId());
            LoanSlipDto.IdUser = MainWindow.loginUser.Id;
            LoanSlipDto.LoanDate = DateTime.Now;
            LoanSlipDto.ExpDate = LoanSlipDto.LoanDate.AddDays(Constants.LoanSlipExpDate);
        }

        private void NewDetail()
        {
            LoanDetail = new LoanDetail();
            LoanDetail.Id = loanDetailVM.GetId(LoanDetails.Count + getLoanDetailRepo().Count);
            LoanDetail.IdLoan = LoanSlipDto.Id;
            LoanDetail.ExpDate = LoanSlipDto.ExpDate;
        }


        public void CloseAndSave()
        {
            CalculatePayment();

            // Truyền dữ liệu cho item
            LoanSlip = new LoanSlip();

            PassValueToItem(LoanSlip);

            getLoanSlipRepo().Add(LoanSlip);
            getLoanSlipRepo().WriteAdd(LoanSlip);

            getLoanDetailRepo().AddRange(LoanDetails);
            getLoanDetailRepo().WriteAddRange(LoanDetails);

            ChangeBookStatus(false);
            LoanDetails.Clear();

            IsCancel = false;
            getParentUc().BackToMainPage();
        }

        public void CalculatePayment()
        {
            Parameter paraQD10 = paraVM.FindById(Constants.paraQD10);
            Parameter paraQD11 = paraVM.FindById(Constants.paraQD11);
            decimal paraQD11_Value = Convert.ToDecimal(paraQD11.Value.Replace("%", "")) / 100;

            LoanSlipDto.LoanPaid = Convert.ToDecimal(paraQD10.Value.ToString());

            LoanSlipDto.Deposit = CalculateDeposit(paraQD11_Value);
        }

        private decimal CalculateDeposit(decimal percentValue)
        {
            decimal price = 0.0M;
            var listBook = bookVM.GetBooksInLoanDetails(LoanDetails);

            foreach (var book in listBook)
            {
                price += book.Price * percentValue;
            }
            return price;
        }

        public void GoBackPage()
        {
            gdInputLoanSlip.Visibility = Visibility.Visible;
            stkPayment.Visibility = Visibility.Collapsed;
        }


        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (LoanDetails.Count == 0)
            {
                Utilities.ShowMessageBox1("You haven't chosen any book yet");
                return;
            }

            gdInputLoanSlip.Visibility = Visibility.Collapsed;
            stkPayment.Visibility = Visibility.Visible;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoanSlipDto = null;
            getParentUc().BackToMainPage();
        }

        private void BtnBookDetailConfirm_Click(object sender, RoutedEventArgs e)
        {
            BookDto bookDto = SelectedBook;
            var tempCheck = bookVM.FindById(BooksOfReader, bookDto.Id, null);
            if (tempCheck != null)
            {
                Utilities.ShowMessageBox1("This reader already borrow this book!", "Can not borrow this book");
                return;
            }
            if (!bookDto.Status)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyBookStatus());
                return;
            }
            else
            {
                NewDetail();
                LoanDetail.IdBook = bookDto.Id;

                ucLoanDetailsTable.LoanDetails.Add(loanDetailMap.ConvertToDto(LoanDetail));
                LoanDetails.Add(LoanDetail);

                AllBookISBNCard.Remove(AllBookISBNCard.FirstOrDefault(item => item.getItem().ISBN == SelectedISBN.ISBN));
                AllBookISBN.Remove(AllBookISBN.FirstOrDefault(item => item.ISBN == SelectedISBN.ISBN));

                AddBookISBNCardToWrap();
            }
        }


        private void PassValueToItem(LoanSlip item)
        {
            item.Id = LoanSlipDto.Id;
            item.IdUser = LoanSlipDto.IdUser;
            item.IdReader = SelectedReader.Id;
            item.Quantity = LoanDetails.Count;
            item.LoanDate = LoanSlipDto.LoanDate;
            item.ExpDate = LoanSlipDto.ExpDate;
            item.LoanPaid = LoanSlipDto.LoanPaid;
            item.Deposit = LoanSlipDto.Deposit;
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
            TxtInputIdentify_Filter_TextChanged(txtAdultInputIdentify, gdInputAdultIndentify, AllAdults);
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

                GetBooksFromReader();
                GetReaderLoanAndLoanDetails();
                ucLoanDetailsBorrowedTable.getLoanDetails = () => AllAdultLoanDetail;
                ucLoanDetailsBorrowedTable.ModifiedPagination();

                bdInputBookInformation.IsEnabled = true;
                return;
            }
            else
            {
                bdInputBookInformation.IsEnabled = false;
                SelectedReader = null;
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
            AllChilds = childMap.ConvertToDto(adultDto.ListChild);
            txtChildInputReaderName.Text = string.Empty;
            cbChildTxtReaderName.ItemsSource = AllChilds;
        }

        private void TxtGuardianInputIdentify_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputGuardianIdentify_Filter_TextChanged(txtGuardianInputIdentify, gdGuardianInput, AllGuardians);
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
            TxtInputChildReaderName_Filter_TextChanged(txtChildInputReaderName, gdInputChildName, AllChilds);
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
                bdInputBookInformation.IsEnabled = true;
                Reader reader = readerVM.FindById(getfillListDto.First().IdReader);
                SelectedReader = readerMap.ConvertToDto(reader);
                GetBooksFromReader();
                GetReaderLoanAndLoanDetails();
                ucLoanDetailsBorrowedTable.getLoanDetails = () => AllChildLoanDetail;
                ucLoanDetailsBorrowedTable.ModifiedPagination();
                return;
            }
            else
            {
                bdInputBookInformation.IsEnabled = false;
                SelectedReader = null;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion

        #region BookNameInput
        private void HandleBookName(TextBox txt, ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
                return;
            txt.Text = ((BookTitleDto)cmb.SelectedItem).Name;
        }

        private void CbTxtBookName_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) HandleBookName(txtInputBookName, cmb);
            handle = true;
        }

        private void CbTxtBookName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            HandleBookName(txtInputBookName, cmb);
        }

        private void TxtInputBookName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtInputBookName_Filter_TextChanged(txtInputBookName, gdInputBookName, AllBookNames);
        }

        private void TxtInputBookName_Filter_TextChanged(TextBox txtInput, Grid parent, ObservableCollection<BookTitleDto> source)
        {
            bool ignoreCase = true;
            ComboBox comBoBox = Utilities.FindVisualChild<ComboBox>(parent);

            if (Utilities.IsCheckEmptyString(txtInput.Text))
                return;
            comBoBox.IsDropDownOpen = true;

            ObservableCollection<BookTitle> getfillList = bookTitleVM.FillContainsName(source, txtInput.Text, ignoreCase);
            ObservableCollection<BookTitleDto> getfillListDto = bookTitleMap.ConvertToDto(getfillList);

            if (getfillListDto.Count == 1 && getfillListDto.First().Name == txtInput.Text)
            {
                comBoBox.IsDropDownOpen = false;

                if (cbTxtBookName.SelectedItem == null)
                    return;
                BookTitleDto bookTitleDto = cbTxtBookName.SelectedItem as BookTitleDto;

                AllBookISBN = bookISBNVM.FillByIdBookTitle(bookTitleDto.Id, BookISBNStatusValue);
                AllBookISBNCard.Clear();
                ConvertToBookISBNCard(AllBookISBN);
                AddBookISBNCardToWrap();

                return;
            }
            comBoBox.ItemsSource = getfillListDto;
        }

        #endregion

        // Others methods

        #region GetsDatas
        private void CbSelectReaderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSelectReaderType.SelectedItem == null)
                return;
            var readerType = (ReaderType)cbSelectReaderType.SelectedItem;
            if (readerType == ReaderType.Adult)
            {
                handle = true;
                FillReaderByType = readerVM.FillListByType(readerType, StatusValue);

                var FillReaderByTypeDto = readerMap.ConvertToDto(FillReaderByType);
                var adults = adultVM.GetListFromReaders(FillReaderByType, StatusValue);

                AllAdults = adultMap.ConvertToDto(adults);
                cbAdultTxtIdentify.ItemsSource = AllAdults;

                stkAdultInformation.Visibility = Visibility.Visible;
                stkChildInformation.Visibility = Visibility.Collapsed;
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
                cbGuardianTxtIdentify.ItemsSource = AllGuardians;

                stkAdultInformation.Visibility = Visibility.Collapsed;
                stkChildInformation.Visibility = Visibility.Visible;
            }
        }

        private void GdInputChildName_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = (sender as Grid);
            if (!grid.IsEnabled)
                return;
            handle = true;
        }

        private void GdInputBookName_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = (sender as Grid);
            if (!grid.IsEnabled)
                return;
            handle = true;

            AllBookNames = bookTitleMap.ConvertToDto(getBookTitleRepo().Gets());
            cbTxtBookName.ItemsSource = AllBookNames;
        }
        #endregion


        private void BdInputBookInformation_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Border bd = (sender as Border);
            if (bd.IsEnabled)
            {
                gdInputBookName.IsEnabled = true;
            }
        }

        private Book FindTheLastestBook(ObservableCollection<Book> books)
        {
            bool? bookStatus = null;
            string isbn = SelectedISBN.ISBN;
            if (getLoanSlipRepo().Gets().Count == 0)
                return null;


            var temp = getLoanSlipRepo().Gets().Where(item =>
            {
                var details = loanDetailVM.FillListByIdLoan(item.Id);

                details = loanDetailVM.FillListByISBN(details, isbn, bookStatus);
                if (details.Count > 0)
                {
                    return true;
                }
                return false;
            });
            temp = temp.OrderBy(item => item.ExpDate);
            LoanSlip loanLastest = temp.First();

            var loanDetails = loanDetailVM.FillListByIdLoan(loanLastest.Id);
            loanDetails = loanDetailVM.FillListByISBN(loanDetails, isbn, bookStatus);

            return bookVM.FindById(loanDetails.First().IdBook, bookStatus);
        }

        private void ChangeBookStatus(bool updateStatus)
        {
            foreach (var loanDetail in LoanDetails)
            {
                Book book = bookVM.FindById(loanDetail.IdBook, null);

                if (book.Status != updateStatus)
                {
                    book.Status = updateStatus;

                    // Save change to database
                    getBookRepo().WriteUpdateStatus(book, updateStatus);
                }
            }
            foreach (BookISBN isbn in getBookISBNRepo())
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
                getBookISBNRepo().WriteUpdateStatus(isbn, isbn.Status);
            }
        }

        private void GetBooksFromReader()
        {
            ReaderDto reader = SelectedReader;

            var readerLoans = loanSlipVM.FillByIdReader(reader.Id, false);
            var readerBooks = bookVM.GetBooksInLoanSlips(readerLoans);
            BooksOfReader = readerBooks;
        }

        private void GetChildrenFromAdult(Adult adult)
        {
            AllChildOfAdult = childVM.FillByIdAdult(adult.IdReader, null);
        }

        private void GetChildBooks()
        {
            AllChildLoan = loanSlipVM.GetByListReader(AllChildOfAdult.Select(child => readerVM.FindById(child.IdReader)).ToObservableCollection());
            AllChildLoanDetail = loanDetailVM.FillListByIdLoans(AllChildLoan);
            AllChildBook = bookVM.GetBooksInLoanDetails(AllChildLoanDetail);
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

        private void ConvertToBookISBNCard(ObservableCollection<BookISBN> bookISBNs)
        {
            AllBookISBNCard.Clear();
            foreach (var isbn in bookISBNs)
            {
                ucBookISBNCard ucBookISBNCard = new ucBookISBNCard();
                ucBookISBNCard.Width = CardWidth;
                ucBookISBNCard.Margin = new Thickness(10);
                ucBookISBNCard.MouseLeftButtonDown += UcBookISBNCard_MouseLeftButtonDown;
                ucBookISBNCard.getItem = () => bookISBNMap.ConvertToDto(isbn);
                AllBookISBNCard.Add(ucBookISBNCard);
            }
        }

        private void UcBookISBNCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucBookISBNCard ucBookISBNCard = sender as ucBookISBNCard;
            SelectedISBN = ucBookISBNCard.getItem();
            BookISBN isbn = bookISBNVM.FindByISBN(SelectedISBN.ISBN, null);

            var books = bookVM.FillByBookISBN(isbn.ISBN, BookStatusValue);
            if (!isbn.Status)
            {
                Utilities.ShowMessageBox1("All Books of this ISBN has been borrowed already");
                return;
            }
            else if (books.Count == 0)
            {
                Utilities.ShowMessageBox1("This ISBN doesn't have book already");
                return;
            }

            OpenSelectBookForm(books);

            if (SelectedBook == null) // User click cancel button
            {
                return;
            }
            BtnBookDetailConfirm_Click(null, null);
        }

        private void OpenSelectBookForm(ObservableCollection<Book> books)
        {
            ucBooksTable ucSelectBooksTable = MainWindow.UnitOfForm.UcBooksTable(true);

            #region Set-ExceptProperty
            List<PropertyInfo> allProperties = Utilities.getPropsFromType(typeof(BookDto));
            List<PropertyInfo> exceptDtgProperties = Utilities.FillPropertiesByName(allProperties, Constants.exceptDtgCreateLoanSlipBook);
            ucSelectBooksTable.getExceptProperties = () => exceptDtgProperties;
            #endregion

            ucSelectBooksTable.Books = bookMap.ConvertToDto(books);

            Button btnConfirmSelectBook = new Button();
            Button btnCancelSelectBook = new Button();

            btnConfirmSelectBook.Style = Application.Current.FindResource(Constants.styleBtnConfirm) as Style;
            btnCancelSelectBook.Style = Application.Current.FindResource(Constants.styleBtnCancel) as Style;

            frmDefault frmSelectBooksTable = new frmDefault();
            frmSelectBooksTable.Width = 900;
            frmSelectBooksTable.SizeToContent = SizeToContent.Height;
            frmSelectBooksTable.frmTitle = "Select book form";
            frmSelectBooksTable.lblHeader = "Please select book in this ISBN";

            btnConfirmSelectBook.Click += (_sender, _e) =>
            {
                SelectedBook = ucSelectBooksTable.SelectedDto;
                if (SelectedBook == null)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("book"));
                    return;
                }
                frmSelectBooksTable.Close();
            };
            btnCancelSelectBook.Click += (_sender, _e) =>
            {
                frmSelectBooksTable.Close();
                SelectedBook = null;
            };

            Utilities.AddItemToFormDefault(frmSelectBooksTable, ucSelectBooksTable, btnConfirmSelectBook, btnCancelSelectBook);
            frmSelectBooksTable.ShowDialog();
        }

        private void AddBookISBNCardToWrap()
        {
            wrapBookISBN.Children.Clear();
            foreach (var ucCard in AllBookISBNCard)
            {
                wrapBookISBN.Children.Add(ucCard);
            }
        }
    }
}
