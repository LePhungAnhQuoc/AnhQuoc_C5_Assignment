using Microsoft.Win32;
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
    public class AddFunctionViewModel: BaseViewModel<object>, IPageViewModel
    {
        #region Fields
        public bool IsCancel { get; set; }
        private frmAddFunction thisForm;
        private List<DependencyObject> mainContentControls;
        private List<TextBox> TextBoxes;

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

        #region Commands
        //public RelayCommand LoadedCmd { get; private set; }
        public RelayCommand ClosingCmd { get; private set; }

        public RelayCommand btnCancelClickCmd { get; private set; }
        public RelayCommand btnUpdateClickCmd { get; private set; }
        public RelayCommand btnResetClickCmd { get; private set; }
        public RelayCommand btnBrowseImageClickCmd { get; private set; }
        #endregion

        public AddFunctionViewModel()
        {
            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
            paraVM = UnitOfViewModel.Instance.ParameterViewModel;

            functionMap = UnitOfMap.Instance.FunctionMap;

            #region Init-Commands
            //LoadedCmd = new RelayCommand((para) => frmAddFunction_Loaded(para, null));
            ClosingCmd = new RelayCommand(para => onClosing(para, null));
            btnCancelClickCmd = new RelayCommand((para) => BtnCancel_Click(para, null));
            btnUpdateClickCmd = new RelayCommand((para) => BtnUpdate_Click(para, null));
            btnResetClickCmd = new RelayCommand((para) => BtnReset_Click(para, null));
            btnBrowseImageClickCmd = new RelayCommand((para) => 
            BtnBrowseImage_Click(para, null));
            #endregion
        }


        public void onLoaded(object sender, RoutedEventArgs e)
        {
            IsCancel = true;

            thisForm = sender as frmAddFunction;

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

            bool? statusParentFunction = true;

            AllFunctions = thisForm.getFunctionRepo().Gets();

            Parents = functionVM.FillParent(AllFunctions, statusParentFunction);

            if (thisForm.getItemToUpdate().IdParent == null)
            {
                thisForm.gdFuncParent.Visibility = Visibility.Collapsed;
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
            tempUrlImage = Item.UrlImage;

        }

        private void onClosing(object sender, CancelEventArgs e)
        {
            BtnCancel_Click(null, null, true);
        }


        private void NewItem()
        {
            string id = functionVM.GetId();
            Item = new FunctionDto(id);
            Item.Status = true;
        }

        private void CopyItem()
        {
            var getItem = thisForm.getItemToUpdate();
            Item = getItem.Clone() as FunctionDto;
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

            // Kiểm tra thông tin của item có tồn tại trong danh sách
            var normalItem = functionVM.CreateByDto(Item);
            var normalSourceItem = functionVM.CreateByDto(thisForm.getItemToUpdate());

            bool isExistItemToUpdate = Utilities.IsExistInformation(normalSourceItem, normalItem, false, Constants.checkPropFunction);

            if (!isExistItemToUpdate)
            {
                bool isExistInformation = Utilities.IsExistInformation(thisForm.getFunctionRepo().Gets(), normalItem, true, Constants.checkPropFunction);

                if (isExistInformation)
                {
                    Utilities.ShowMessageBox1(Utilities.NotifyItemExistInfo("function"));
                    return;
                }
            }
            SaveImage();

            IsCancel = false;
            thisForm.Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            functionVM.Copy(Item, thisForm.getItemToUpdate());
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e, bool isClosed = false)
        {
            if (IsCancel)
                Item = null;
            if (!isClosed)
                thisForm.Close();
        }


        public void SaveImage()
        {
            Constants.rememberDirectoryOpenFile = openFile.FileName.Replace(openFile.SafeFileName, string.Empty);
            Utilities.SaveImage(openFile);

            if (tempUrlImage != null)
            {
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


        private void SetFormByAddOrUpdate(string feature)
        {
            switch (feature)
            {
                case "ADD":
                    thisForm.Title = "Add New Function Form";
                    thisForm.lblTitle.Content = "Add New Function";

                    thisForm.stkUpdateButton.Visibility = Visibility.Collapsed;
                    break;
                case "UPDATE":
                    thisForm.Title = "Update Function Information Form";
                    thisForm.lblTitle.Content = "Update Function Information";

                    thisForm.stkUpdateButton.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.Trim();
        }
    }
}
