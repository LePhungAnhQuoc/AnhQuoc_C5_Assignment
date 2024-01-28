using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for frmAddFunction.xaml
    /// </summary>
    public partial class frmAddFunction : Window, INotifyPropertyChanged
    {
        #region getDatas
        public Func<FunctionRepository> getFunctionRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }

        public Func<FunctionDto> getItemToUpdate { get; set; }
        #endregion

        #region Fields
        private OpenFileDialog openFile;
        private string tempUrlImage;
        #endregion

        #region Properties
        private ObservableCollection<Function> _AllFunctions;
        public ObservableCollection<Function> AllFunctions
        {
            get { return _AllFunctions; }
            set
            {
                _AllFunctions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Function> _Parents;
        public ObservableCollection<Function> Parents
        {
            get { return _Parents; }
            set
            {
                _Parents = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ViewModels
        private FunctionViewModel functionVM;
        private ParameterViewModel paraVM;
        #endregion

        #region Mapping
        private FunctionMap functionMap;
        #endregion

        #region ResultProperty
        private FunctionDto _Item;
        public FunctionDto Item
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

        public frmAddFunction()
        {
            InitializeComponent();

            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            functionMap = UnitOfMap.Instance.FunctionMap;

            #region SetTextBoxMaxLength
            int maxLength = Constants.textBoxMaxLength;
            txtName.MaxLength = maxLength;
            txtDescription.MaxLength = Constants.textAreaMaxLength;

            txtUrlImage.MaxLength = Constants.textAreaMaxLength;
            #endregion
            
            btnCancel.Click += BtnCancel_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnReset.Click += BtnReset_Click;
            btnBrowseImage.Click += BtnBrowseImage_Click;

            txtName.LostFocus += TxtFormatValue_LostFocus;
            txtDescription.LostFocus += TxtFormatValue_LostFocus;
            txtUrlImage.LostFocus += TxtFormatValue_LostFocus;

            this.DataContext = this;
            this.Loaded += frmAddFunction_Loaded;
        }

        private void frmAddFunction_Loaded(object sender, RoutedEventArgs e)
        {
            bool? statusParentFunction = true;

            AllFunctions = getFunctionRepo().Gets();
            Parents = functionVM.FillParent(AllFunctions, statusParentFunction).ToObservableCollection();

            if (getItemToUpdate().Parent == null)
            {
                gdFuncParent.Visibility = Visibility.Collapsed;
            }
            if (getItemToUpdate == null)
            {
                NewItem();
                SetFormByAddOrUpdate("ADD");
            }
            else
            {
                CopyItem();
                SetFormByAddOrUpdate("UPDATE");
            }

            tempUrlImage = Item.UrlImage;
        }

        private void NewItem()
        {
            string id = functionVM.GetId();
            Item = new FunctionDto(id);
            Item.Status = true;
        }

        private void CopyItem()
        {
            var getItem = getItemToUpdate();
            Item = getItem.Clone() as FunctionDto;
        }

        
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            RunAllValidations();
            bool isHasError = this.IsValidationGetHasError();
            if (isHasError)
            {
                return;
            }

            // Kiểm tra thông tin của item có tồn tại trong danh sách
            var normalItem = functionVM.CreateByDto(Item);
            var normalSourceItem = functionVM.CreateByDto(getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropFunction);

            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(getFunctionRepo().Gets(), normalItem, true, Constants.checkPropFunction);

                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInfo("function"));
                    return;
                }
            }
            SaveImage();

            this.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            functionVM.Copy(Item, getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Item = null;
            this.Close();
        }


        private void BtnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            openFile = new OpenFileDialog();
            openFile.Title = "Select Image";
            openFile.InitialDirectory = Constants.rememberDirectoryOpenFile;
            
            openFile.Filter = "Bitmaps|*.bmp|PNG files|*.png|JPEG files|*.jpg|GIF files|*.gif|TIFF files|*.tif|Image files|*.bmp;*.jpg;*.gif;*.png;*.tif";

            openFile.FilterIndex = 6;
            openFile.Multiselect = false;
            openFile.RestoreDirectory = false;

            if (openFile.ShowDialog() == true)
            {
                Item.UrlImage = Utilities.GetUrlImage(openFile);
            }
        }
        
        public void SaveImage()
        {
            Constants.rememberDirectoryOpenFile = openFile.FileName.Replace(openFile.SafeFileName, string.Empty);
            Utilities.SaveImage(openFile);

            tempUrlImage = tempUrlImage.Replace("/", "\\");
            string tempImageFile = tempUrlImage.Replace(Constants.StartUrlImage, "");
            if (tempUrlImage != null && tempImageFile != openFile.SafeFileName)
            {
                if (MessageBox.Show("Do you want remove an old image?", string.Empty,
          MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK) == MessageBoxResult.OK)
                {
                    Utilities.RemoveImage(tempUrlImage);
                }
            }
        }


        public bool IsValidationGetHasError()
        {
            if (Validation.GetHasError(txtName))
                return true;
            return false;
        }

        private void RunAllValidations()
        {
            BindingExpression be = null;

            be = txtName.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        // Others methods

        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    this.Title = "Add New Function Form";
                    lblTitle.Content = "Add New Function";

                    stkUpdateButton.Visibility = Visibility.Collapsed;
                    break;
                case "UPDATE":
                    this.Title = "Update Function Information Form";
                    lblTitle.Content = "Update Function Information";

                    stkUpdateButton.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void TxtFormatValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }
    }
}
