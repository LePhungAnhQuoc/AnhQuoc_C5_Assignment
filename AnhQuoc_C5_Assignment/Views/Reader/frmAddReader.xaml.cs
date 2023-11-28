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
    /// Interaction logic for frmAddReader.xaml
    /// </summary>
    public partial class frmAddReader : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<string> getIdReader { get; set; }

        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        private ucAddChild ucAddChild;
        private ucAddAdult ucAddAdult;
        private DateTime AdultReader_ExpireDate;
        #endregion

        #region Properties
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
        private AdultViewModel adultVM;
        private ChildViewModel childVM;
        private ParameterViewModel paraVM;
        #endregion

        #region ResultProperty
        private Reader _Item;
        public Reader Item
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

        public Adult NewAdult { get; set; }
        public Child NewChild { get; set; }

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

        public frmAddReader()
        {
            InitializeComponent();

            readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            adultVM = UnitOfViewModel.Instance.AdultViewModel;
            childVM = UnitOfViewModel.Instance.ChildViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            #region UserControls
            ucAddAdult = MainWindow.UnitOfForm.UcAddAdult(true);
            ucAddChild = MainWindow.UnitOfForm.UcAddChild(true);
            #endregion

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtLName.MaxLength = maxLength;
            txtFName.MaxLength = maxLength;
            #endregion

            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            dateboF.SelectedDateChanged += DateboF_SelectedDateChanged;
            txtLName.TextChanged += Txt_TextChanged;
            txtFName.TextChanged += Txt_TextChanged;
            ucAddAdult.txtIdentify.TextChanged += Txt_TextChanged;
            ucAddAdult.txtAddress.TextChanged += Txt_TextChanged;
            ucAddAdult.txtPhone.TextChanged += Txt_TextChanged;
            ucAddAdult.cbCity.SelectionChanged += Cb_SelectionChanged;
            dateboF.SelectedDateChanged += DatePicker_SelectedDateChanged;

            txtFName.LostFocus += TxtFormatValue_LostFocus;
            txtLName.LostFocus += TxtFormatValue_LostFocus;

            this.Loaded += FrmAddReader_Loaded;
            this.DataContext = this;
        }

        private void FrmAddReader_Loaded(object sender, RoutedEventArgs e)
        {
            NewItem();
            Parameter paraQD = paraVM.FindById(Constants.paraQD8);
            AdultReader_ExpireDate = adultVM.GetExpireDate(paraQD, Item.CreatedAt);
        }

        private void NewItem()
        {
            Item = new Reader();
            Item.Id = getIdReader();
            Item.Status = true;
            Item.CreatedAt = DateTime.Now;
            Item.ModifiedAt = Item.CreatedAt;
        }

        private void DateboF_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            #region CheckAge
            int readerAge = -1;
            if (SelectedBoF == null)
            {
                return;
            }

            readerAge = DateTime.Now.Date.Year - ((DateTime)SelectedBoF).Date.Year;

            #region GetParameterRule
            Parameter parameter = paraVM.FindById(Constants.paraQD7);

            int ageValueRule = int.Parse(parameter.Value);
            #endregion

            if (readerAge < ageValueRule)
            {
                SelectedReaderType = ReaderType.Child;

                LoadUcAddChild();
            }
            else
            {
                SelectedReaderType = ReaderType.Adult;

                LoadUcAddAdult();
            }
            #endregion

            tblAge.Text = string.Format("{0} years old", readerAge);
        }

        private void LoadUcAddAdult()
        {
            ucAddAdult.getIdReader = getIdReader;
            ucAddAdult.getExpireDate = () => AdultReader_ExpireDate;

            gdContent.Children.Clear();
            gdContent.Children.Add(ucAddAdult);
        }

        private void LoadUcAddChild()
        {            
            ucAddChild.getIdReader = getIdReader;

            gdContent.Children.Clear();
            gdContent.Children.Add(ucAddChild);
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
            bool isCheckEmptyItem = readerVM.IsCheckEmptyItem(Item);
            bool isHasError = this.IsValidationGetHasError();
            if (isCheckEmptyItem == false || isHasError)
            {
                RunAllValidations();
                return;
            }
          
            // Truyền dữ liệu cho item
            PassValueToItem(Item, (ReaderType)SelectedReaderType, (DateTime)SelectedBoF);

            // Kiểm tra thông tin của item có tồn tại trong danh sách
            bool isExistReaderInformation = Utilities.IsExistInformation(getReaderRepo().Gets(), Item, true, Constants.checkPropReader);
            
            if (SelectedReaderType == ReaderType.Adult)
            {
                NewAdult = ucAddAdult.Item;

                if (!ucAddAdult.IsAllSelecting())
                {
                    ucAddAdult.RunAllValidations();
                    return;
                }
                    

                // IsCheck Empty Item
                bool isCheckEmptyItemAdult = adultVM.IsCheckEmptyItem(ucAddAdult.Item);
                bool isHasErrorAdult = ucAddAdult.IsValidationGetHasError();

                if (!isCheckEmptyItemAdult || isHasErrorAdult)
                {
                    ucAddAdult.RunAllValidations();
                    return;
                }

                // Pass value
                ucAddAdult.PassValueToItem(ucAddAdult.Item, Item, ucAddAdult.SelectedProvinceDto);

                // Kiểm tra thông tin Identify item có tồn tại trong danh sách
                var itemCheckIdentify = adultVM.FindByIdentify(getAdultRepo().Gets(), NewAdult.Identify, statusValue);

                if (itemCheckIdentify != null)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInTheList("Adult Identify"));
                    return;
                }
            }
            else if (SelectedReaderType == ReaderType.Child)
            {
                NewChild = ucAddChild.Item;
                
                // Check IsAllSelecting
                if (!ucAddChild.IsAllSelecting())
                {
                    return;
                }

                ReaderDto selectedAdultReaderDto = ucAddChild.ucReadersTable.SelectedReaderDto;

                // Pass Value
                ucAddChild.PassValueToItem(NewChild, Item, selectedAdultReaderDto);

                // Kiểm tra thông tin của item có tồn tại trong danh sách
                if (isExistReaderInformation)
                {
                    bool IsExistChild = Utilities.IsExistInformation(getChildRepo().Gets(), NewChild, true, Constants.checkPropChild);

                    if (IsExistChild)
                    {
                        Utilities.ShowMessageBox1(Utilities.NotifyItemExistInfo("Child reader"));
                        return;
                    }
                }
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;

            this.Close();
        }

        private void PassValueToItem(Reader item, ReaderType selectedReaderType, DateTime selectedBoF)
        {
            item.ReaderType = selectedReaderType.ConvertValue();
            item.boF = selectedBoF;
        }

        private bool IsAllSelecting()
        {
            if (SelectedBoF == null)
            {
                Utilities.ShowMessageBox1("Please select boF date information");
                return false;
            }
            return true;
        }

        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtLName))
                return true;
            if (Validation.GetHasError(txtFName))
                return true;
            if (Validation.GetHasError(dateboF))
                return true;

            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtLName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtFName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = dateboF.GetBindingExpression(DatePicker.SelectedDateProperty);
            be.UpdateSource();
        }

        // Others methods
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
