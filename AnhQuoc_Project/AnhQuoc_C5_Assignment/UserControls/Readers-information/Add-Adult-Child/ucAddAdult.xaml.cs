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
    /// Interaction logic for ucAddAdult.xaml
    /// </summary>
    public partial class ucAddAdult : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<string> getIdReader { get; set; }
        public Func<DateTime> getExpireDate { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        #endregion

        #region Properties
        private ProvinceDto _SelectedProvinceDto;
        public ProvinceDto SelectedProvinceDto
        {
            get { return _SelectedProvinceDto; }
            set
            {
                _SelectedProvinceDto = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ProvinceDto> _AllProvinceDtos;
        public ObservableCollection<ProvinceDto> AllProvinceDtos
        {
            get { return _AllProvinceDtos; }
            set
            {
                _AllProvinceDtos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ResultProperties
        private Adult _Item;
        public Adult Item
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

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        public ucAddAdult()
        {
            InitializeComponent();

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtIdentify.MaxLength = maxLength;
            txtPhone.MaxLength = maxLength;
            txtAddress.MaxLength = Constants.textBoxMaxLength;
            #endregion

            txtAddress.LostFocus += TxtFormatValue_LostFocus;
            txtExpireDate.LostFocus += TxtFormatValue_LostFocus;
            txtPhone.LostFocus += TxtFormatValue_LostFocus;

            this.DataContext = this;
            this.Loaded += UcAddAdult_Loaded;
        }

        private void UcAddAdult_Loaded(object sender, RoutedEventArgs e)
        {
            Item = new Adult();
            Item.IdReader = getIdReader();
            Item.ExpireDate = getExpireDate();

            var allProvinces = getProvinceRepo().Gets();
            AllProvinceDtos = UnitOfMap.Instance.ProvinceMap.ConvertToDto(allProvinces);
        }

        public void PassValueToItem(Adult item, Reader reader, ProvinceDto selectedProvinceDto)
        {
            item.City = selectedProvinceDto.Name;
            item.Status = reader.Status;
            item.CreatedAt = reader.CreatedAt;
            item.ModifiedAt = reader.ModifiedAt;
        }

        public bool IsAllSelecting()
        {
            if (SelectedProvinceDto == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyPleaseSelect("province"));
                return false;
            }
            return true;
        }

        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtIdentify))
                return true;
            if (Validation.GetHasError(txtAddress))
                return true;
            if (Validation.GetHasError(txtPhone))
                return true;
            if (Validation.GetHasError(cbCity))
                return true;

            return false;
        }

        public void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtIdentify.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtAddress.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtPhone.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            be = cbCity.GetBindingExpression(ComboBox.SelectedItemProperty);
            be.UpdateSource();
        }

        private void TxtFormatValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }

    }
}
