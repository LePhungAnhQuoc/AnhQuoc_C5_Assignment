using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for ucLibrarianDashBoard.xaml
    /// </summary>
    public partial class ucLibrarianDashBoard : UserControl
    {
        #region GetDatas
        public Func<ObservableCollection<Function>> getFunctions { get; set; }

        public Func<ReaderRepository> getReaderRepo { get; set; }
        public Func<AdultRepository> getAdultRepo { get; set; }
        public Func<ChildRepository> getChildRepo { get; set; }
        public Func<ParameterRepository> getParameterRepo { get; set; }

        public Func<BookTitleRepository> getBookTitleRepo { get; set; }
        public Func<BookISBNRepository> getBookISBNRepo { get; set; }
        public Func<BookRepository> getBookRepo { get; set; }

        public Func<AuthorRepository> getAuthorRepo { get; set; }
        public Func<TranslatorRepository> getTranslatorRepo { get; set; }
        public Func<CategoryRepository> getCategoryRepo { get; set; }
        public Func<ProvinceRepository> getProvinceRepo { get; set; }
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<UserInfoRepository> getUserInfoRepo { get; set; }
        public Func<UserRoleRepository> getUserRoleRepo { get; set; }
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<FunctionRepository> getFunctionRepo { get; set; }
        public Func<ObservableCollection<string>> getRoleGroups { get; set; }
        public Func<RoleFunctionRepository> getRoleFunctionRepo { get; set; }

        public Func<User> getLoginUser { get; set; }
        public Func<Grid> getGdView { get; set; }
        public Func<MainWindow> getFrmMain { get; set; }
        #endregion

        #region Fields
        private ucStatisticalPage ucStatisticalPage;
        private ucPenaltyReasonManagement ucPenaltyReasonManagement;
        private ucTranslatorManagement ucTranslatorManagement;
        private ucProvinceManagement ucProvinceManagement;
        private ucBookStatusManagement ucBookStatusManagement;
        private ucParameterManagement ucParameterManagement;
        private ucAuthorManagement ucAuthorManagement;
        private ucPublisherManagement ucPublisherManagement;
        private ucUserManagement ucUserManagement;
        private ucFunctionManagement ucFunctionManagement;
        private ucRoleManagement ucRoleManagement;
        private ucUserRoleManagement ucUserRoleManagement;
        private ucRoleFunctionManagement ucRoleFunctionManagement;
        private ucReaderManagement ucReaderManagement;
        private ucBookISBNManagement ucBookISBNManagement;
        private ucBookManagement ucBookManagement;
        private ucBookTitleManagement ucBookTitleManagement;
        private ucLoanSlipManagement ucLoanSlipManagement;
        private ucLoanHistoryManagement ucLoanHistoryManagement;
        private ucCategoryManagement ucCategoryManagement;
        #endregion

        #region Properties
        public string HumanUrlImage
        {
            get
            {
                return Constants.HumanUrlImage;
            }
        }
        #endregion

        #region ViewModels
        private UserRoleViewModel userRoleVM;
        private FunctionViewModel functionVM;
        private RoleViewModel roleVM;
        #endregion

        #region Mappings
        private UserMap userMap;
        #endregion  

        public ucLibrarianDashBoard()
        {
            InitializeComponent();
            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            roleVM = UnitOfViewModel.Instance.RoleViewModel;

            ucStatisticalPage = MainWindow.UnitOfForm.UcStatisticalPage(true);
            ucPenaltyReasonManagement = MainWindow.UnitOfForm.UcPenaltyReasonManagement(true);
            ucParameterManagement = MainWindow.UnitOfForm.UcParameterManagement(true);
            ucProvinceManagement = MainWindow.UnitOfForm.UcProvinceManagement(true);
            ucBookStatusManagement = MainWindow.UnitOfForm.UcBookStatusManagement(true);
            ucTranslatorManagement = MainWindow.UnitOfForm.UcTranslatorManagement(true);
            ucUserManagement = MainWindow.UnitOfForm.UcUserManagement(true);
            ucAuthorManagement = MainWindow.UnitOfForm.UcAuthorManagement(true);
            ucPublisherManagement = MainWindow.UnitOfForm.UcPublisherManagement(true);
            ucFunctionManagement = MainWindow.UnitOfForm.UcFunctionManagement(true);
            ucRoleManagement = MainWindow.UnitOfForm.UcRoleManagement(true);
            ucUserRoleManagement = MainWindow.UnitOfForm.UcUserRoleManagement(true);
            ucRoleFunctionManagement = MainWindow.UnitOfForm.UcRoleFunctionManagement(true);
            ucReaderManagement = MainWindow.UnitOfForm.UcReaderManagement(true);
            ucBookISBNManagement = MainWindow.UnitOfForm.UcBookISBNManagement(true);
            ucBookManagement = MainWindow.UnitOfForm.UcBookManagement(true);
            ucBookTitleManagement = MainWindow.UnitOfForm.UcBookTitleManagement(true);
            ucLoanSlipManagement = MainWindow.UnitOfForm.UcLoanSlipManagement(true);
            ucLoanHistoryManagement = MainWindow.UnitOfForm.UcLoanHistoryManagement(true);
            ucCategoryManagement = MainWindow.UnitOfForm.UcCategoryManagement(true);


            userMap = UnitOfMap.Instance.UserMap;

            ucUserLoginInformation.ucLogOut.btnLogOut.Click += BtnLogOut_Click;
            this.DataContext = this;
            this.Loaded += UcLibrarianDashBoard_Loaded;
        }

        private void UcLibrarianDashBoard_Loaded(object sender, RoutedEventArgs e)
        {
            var ucDashBoard = this;
            bool? statusParentFunction = true;
            bool? statusChildFunction = true;

            var treeViewInDashBoard = ucDashBoard.tvDashBoard;

            UserRole userRole = userRoleVM.FindByIdUser(getLoginUser().Id);
            Role role = roleVM.FindById(userRole.IdRole);
            lblAccount.Content = role.Name;

            ucUserLoginInformation.getItem = () => userMap.ConvertToDto(getLoginUser());

            Utilities.GetDashBoardTreeView(treeViewInDashBoard, getFunctions(), TvFunction_MouseLeftButtonUp);

            ObservableCollection<Function> parents = functionVM.FillParent(getFunctions(), statusParentFunction);

            foreach (Function parentItem in parents)
            {
                ObservableCollection<Function> childs = functionVM.FillByIdParent(getFunctions(), parentItem.Id, statusChildFunction);

                switch (parentItem.Id)
                {
                    #region User-Management
                    case Constants.UserManagement_FunctionId:
                        var viewFunc = childs.FirstOrDefault(item => item.Id == "F2");
                        if (viewFunc == null)
                        {
                            ucUserManagement.IsAllowViewInformation = false;
                        }

                        var addFunc = childs.FirstOrDefault(item => item.Id == "F3");
                        if (addFunc == null)
                        {
                            ucUserManagement.IsAllowAdd = false;
                        }

                        var updateFunc = childs.FirstOrDefault(item => item.Id == "F4");
                        if (updateFunc == null)
                        {
                            ucUserManagement.IsAllowUpdate = false;
                        }

                        var deleteFunc = childs.FirstOrDefault(item => item.Id == "F5");
                        if (deleteFunc == null)
                        {
                            ucUserManagement.IsAllowDelete = false;
                        }

                        var restoreFunc = childs.FirstOrDefault(item => item.Id == "F6");
                        if (restoreFunc == null)
                        {
                            ucUserManagement.IsAllowRestore = false;
                        }
                        break;
                    #endregion

                    #region Function-Management
                    case Constants.FeatureManagement_FunctionId:
                        viewFunc = childs.FirstOrDefault(item => item.Id == "F8");
                        if (viewFunc == null)
                        {
                            ucFunctionManagement.IsAllowViewInformation = false;
                        }
                        
                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F10");
                        if (deleteFunc == null)
                        {
                            ucFunctionManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F11");
                        if (restoreFunc == null)
                        {
                            ucFunctionManagement.IsAllowRestore = false;
                        }
                        break;
                    #endregion

                    #region Role-Management
                    case Constants.RoleManagement_FunctionId:
                        viewFunc = childs.FirstOrDefault(item => item.Id == "F13");
                        if (viewFunc == null)
                        {
                            ucRoleManagement.IsAllowViewInformation = false;
                        }

                        addFunc = childs.FirstOrDefault(item => item.Id == "F14");
                        if (addFunc == null)
                        {
                            ucRoleManagement.IsAllowAdd = false;
                        }
                        
                        var userRoleFunc = childs.FirstOrDefault(item => item.Id == "F18");
                        if (userRoleFunc == null)
                        {
                            ucRoleManagement.tabUserRole.Visibility = Visibility.Collapsed;
                        }

                        var roleFunctionFunc = childs.FirstOrDefault(item => item.Id == "F19");
                        if (roleFunctionFunc == null)
                        {
                            ucRoleManagement.tabRoleFunction.Visibility = Visibility.Collapsed;
                        }

                        var switchRoleFunc = childs.FirstOrDefault(item => item.Id == "F20");
                        if (switchRoleFunc == null)
                        {
                            ucRoleManagement.IsAllowSetRole = false;
                        }
                        break;
                    #endregion

                    #region Reader-Management
                    case Constants.ReaderManagement_FunctionId:
                        viewFunc = childs.FirstOrDefault(item => item.Id == "F22");
                        if (viewFunc == null)
                        {
                            ucReaderManagement.IsAllowViewInformation = false;
                        }

                        addFunc = childs.FirstOrDefault(item => item.Id == "F23");
                        if (addFunc == null)
                        {
                            ucReaderManagement.IsAllowAdd = false;
                        }
                        
                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F24");
                        if (deleteFunc == null)
                        {
                            ucReaderManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F25");
                        if (restoreFunc == null)
                        {
                            ucReaderManagement.IsAllowRestore = false;
                        }


                        var searchReaderByIdentifyFunc = childs.FirstOrDefault(item => item.Id == "F26");
                        if (searchReaderByIdentifyFunc == null)
                        {
                            ucReaderManagement.IsAllowSearchByIdentify = false;
                        }

                        var searchReaderByNameFunc = childs.FirstOrDefault(item => item.Id == "F27");
                        if (searchReaderByNameFunc == null)
                        {
                            ucReaderManagement.IsAllowSearchByName = false;
                        }

                        var transferGuardiansFunc = childs.FirstOrDefault(item => item.Id == "F28");
                        if (transferGuardiansFunc == null)
                        {
                            ucReaderManagement.IsAllowTransferGuardians = false;
                        }
                        break;
                    #endregion

                    #region Book-Title-Management
                    case Constants.BookTitleManagement_FunctionId:
                        var viewbookTitleFunc = childs.FirstOrDefault(item => item.Id == "F30");
                        if (viewbookTitleFunc == null)
                        {
                            ucBookTitleManagement.IsAllowViewInformation = false;
                        }
                         
                        var addBookTitleFunc = childs.FirstOrDefault(item => item.Id == "F31");
                        if (addBookTitleFunc == null)
                        {
                            ucBookTitleManagement.IsAllowAdd = false;
                        }
                        break;
                    #endregion

                    #region Book-ISBN-Management
                    case Constants.BookISBNManagement_FunctionId:
                        var viewbookISBNFunc = childs.FirstOrDefault(item => item.Id == "F33");
                        if (viewbookISBNFunc == null)
                        {
                            ucBookISBNManagement.IsAllowViewInformation = false;
                        }

                        var addBookISBNFunc = childs.FirstOrDefault(item => item.Id == "F34");
                        if (addBookISBNFunc == null)
                        {
                            ucBookISBNManagement.IsAllowAdd = false;
                        }

                        var updateBookISBNStatusFunc = childs.FirstOrDefault(item => item.Id == "F35");
                        if (updateBookISBNStatusFunc == null)
                        {
                        }
                        break;
                    #endregion

                    #region Book-Management
                    case Constants.BookManagement_FunctionId:
                        var viewbookFunc = childs.FirstOrDefault(item => item.Id == "F37");
                        if (viewbookFunc == null)
                        {
                            ucBookManagement.IsAllowViewInformation = false;
                        }

                        var addBookFunc = childs.FirstOrDefault(item => item.Id == "F38");
                        if (addBookFunc == null)
                        {
                            ucBookManagement.IsAllowAdd = false;
                        }

                        var searchBookByISBNFunc = childs.FirstOrDefault(item => item.Id == "F39");
                        if (searchBookByISBNFunc == null)
                        {
                            ucBookManagement.IsAllowSearchByBookISBN = false;
                        }

                        var searchBookByNameFunc = childs.FirstOrDefault(item => item.Id == "F40");
                        if (searchBookByNameFunc == null)
                        {
                            ucBookManagement.IsAllowSearchByName = false;
                        }
                        break;
                    #endregion

                    #region LoanSlip-Management
                    case Constants.LoanSlipManagement_FunctionId:
                        var addLoanSlipFunc = childs.FirstOrDefault(item => item.Id == "F42");
                        if (addLoanSlipFunc == null)
                        {
                            ucLoanSlipManagement.IsAllowAdd = false;
                        }
                        break;
                    #endregion

                    #region LoanHistory-Management
                    case Constants.LoanHistoryManagement_FunctionId:
                        var addLoanHistoryFunc = childs.FirstOrDefault(item => item.Id == "F44");
                        if (addLoanHistoryFunc == null)
                        {
                            ucLoanSlipManagement.IsAllowAdd = false;
                        }
                        break;
                    #endregion

                    #region Category-Management
                    case Constants.CategoryManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F46");
                        if (addFunc == null)
                        {
                            ucCategoryManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F47");
                        if (deleteFunc == null)
                        {
                            ucCategoryManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F48");
                        if (restoreFunc == null)
                        {
                            ucCategoryManagement.IsAllowRestore = false;
                        }

                        updateFunc = childs.FirstOrDefault(item => item.Id == "F49");
                        if (updateFunc == null)
                        {
                            ucCategoryManagement.IsAllowUpdate = false;
                        }

                        break;
                    #endregion

                    #region Publisher-Management
                    case Constants.PublisherManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F51");
                        if (addFunc == null)
                        {
                            ucPublisherManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F52");
                        if (deleteFunc == null)
                        {
                            ucPublisherManagement.IsAllowDelete = false;
                        }
                        
                        updateFunc = childs.FirstOrDefault(item => item.Id == "F53");
                        if (updateFunc == null)
                        {
                            ucPublisherManagement.IsAllowUpdate = false;
                        }

                        break;
                    #endregion

                    #region Author-Management
                    case Constants.AuthorManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F55");
                        if (addFunc == null)
                        {
                            ucAuthorManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F56");
                        if (deleteFunc == null)
                        {
                            ucAuthorManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F57");
                        if (restoreFunc == null)
                        {
                            ucAuthorManagement.IsAllowRestore = false;
                        }

                        updateFunc = childs.FirstOrDefault(item => item.Id == "F58");
                        if (updateFunc == null)
                        {
                            ucAuthorManagement.IsAllowUpdate = false;
                        }

                        break;
                    #endregion
                 
                    #region PenaltyReason-Management
                    case Constants.PenaltyReasonManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F70");
                        if (addFunc == null)
                        {
                            ucPenaltyReasonManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F71");
                        if (deleteFunc == null)
                        {
                            ucPenaltyReasonManagement.IsAllowDelete = false;
                        }
                        
                        updateFunc = childs.FirstOrDefault(item => item.Id == "F72");
                        if (updateFunc == null)
                        {
                            ucPenaltyReasonManagement.IsAllowUpdate = false;
                        }

                        break;
                    #endregion

                    #region Translator-Management
                    case Constants.TranslatorManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F60");
                        if (addFunc == null)
                        {
                            ucTranslatorManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F61");
                        if (deleteFunc == null)
                        {
                            ucTranslatorManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F62");
                        if (restoreFunc == null)
                        {
                            ucTranslatorManagement.IsAllowRestore = false;
                        }

                        updateFunc = childs.FirstOrDefault(item => item.Id == "F63");
                        if (updateFunc == null)
                        {
                            ucTranslatorManagement.IsAllowUpdate = false;
                        }

                        break;
                    #endregion

                    #region Parameter-Management
                    case Constants.ParameterManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F74");
                        if (addFunc == null)
                        {
                            ucParameterManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F75");
                        if (deleteFunc == null)
                        {
                            ucParameterManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F76");
                        if (restoreFunc == null)
                        {
                            ucParameterManagement.IsAllowRestore = false;
                        }

                        updateFunc = childs.FirstOrDefault(item => item.Id == "F77");
                        if (updateFunc == null)
                        {
                            ucParameterManagement.IsAllowUpdate = false;
                        }

                        break;
                    #endregion

                    #region Province-Management
                    case Constants.ProvinceManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F65");
                        if (addFunc == null)
                        {
                            ucProvinceManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F66");
                        if (deleteFunc == null)
                        {
                            ucProvinceManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F67");
                        if (restoreFunc == null)
                        {
                            ucProvinceManagement.IsAllowRestore = false;
                        }

                        updateFunc = childs.FirstOrDefault(item => item.Id == "F68");
                        if (updateFunc == null)
                        {
                            ucProvinceManagement.IsAllowUpdate = false;
                        }

                        break;
                    #endregion

                    #region Statistical-Management
                    case Constants.StatisticalPage_FunctionId:
                        break;
                    #endregion

                    #region BookStatus-Management
                    case Constants.BookStatusManagement_FunctionId:
                        addFunc = childs.FirstOrDefault(item => item.Id == "F79");
                        if (addFunc == null)
                        {
                            ucBookStatusManagement.IsAllowAdd = false;
                        }

                        deleteFunc = childs.FirstOrDefault(item => item.Id == "F80");
                        if (deleteFunc == null)
                        {
                            ucBookStatusManagement.IsAllowDelete = false;
                        }

                        restoreFunc = childs.FirstOrDefault(item => item.Id == "F81");
                        if (restoreFunc == null)
                        {
                            ucBookStatusManagement.IsAllowRestore = false;
                        }

                        updateFunc = childs.FirstOrDefault(item => item.Id == "F82");
                        if (updateFunc == null)
                        {
                            ucBookStatusManagement.IsAllowUpdate = false;
                        }

                        break;
                        #endregion

                }
            }
        }

        #region TreeViewItemEvent
        private void TvFunction_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch ((sender as TreeViewItem).Name)
            {
                case Constants.UserManagement_FunctionId:
                    TvUserManagementInformation_Function();
                    break;
                case Constants.CategoryManagement_FunctionId:
                    TvCategoryManagementInformation_Function();
                    break;
                case Constants.PublisherManagement_FunctionId:
                    TvPublisherManagementInformation_Function();
                    break;
                case Constants.PenaltyReasonManagement_FunctionId:
                    TvPenaltyReasonManagementInformation_Function();
                    break;
                case Constants.ParameterManagement_FunctionId:
                    TvParameterManagementInformation_Function();
                    break;
                case Constants.ProvinceManagement_FunctionId:
                    TvProvinceManagementInformation_Function();
                    break;
                case Constants.BookStatusManagement_FunctionId:
                    TvBookStatusManagementInformation_Function();
                    break;
                case Constants.TranslatorManagement_FunctionId:
                    TvTranslatorManagementInformation_Function();
                    break;
                case Constants.AuthorManagement_FunctionId:
                    TvAuthorManagementInformation_Function();
                    break;
                case Constants.FeatureManagement_FunctionId:
                    TvFeatureManagementInformation_Function();
                    break;
                case Constants.RoleManagement_FunctionId:
                    TvRoleManagementInformation_Function();
                    break;
                case Constants.ReaderManagement_FunctionId:
                    TvReaderManagement_Function();
                    break;
                case Constants.BookTitleManagement_FunctionId:
                    TvBookTitleManagement_Function();
                    break;
                case Constants.BookISBNManagement_FunctionId:
                    TvBookISBNManagement_Function();
                    break;
                case Constants.BookManagement_FunctionId:
                    TvBookManagement_Function();
                    break;
                case Constants.LoanSlipManagement_FunctionId:
                    TvLoanSlipManagement_Function();
                    break;
                case Constants.LoanHistoryManagement_FunctionId:
                    TvLoanHistoryManagement_Function();
                    break;
                case Constants.StatisticalPage_FunctionId:
                    TvStatisticalPageInformation_Function();
                    break;
            }
        }

        private void TvUserManagementInformation_Function()
        {
            Grid gdView = getGdView();
            
            gdView.Children.Clear();
            gdView.Children.Add(ucUserManagement);
        }

        private void TvPublisherManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucPublisherManagement);
        }

        private void TvAuthorManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucAuthorManagement);
        }

        private void TvPenaltyReasonManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucPenaltyReasonManagement);
        }

        private void TvStatisticalPageInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucStatisticalPage);
        }

        private void TvParameterManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucParameterManagement);
        }

        private void TvProvinceManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucProvinceManagement);
        }

        private void TvBookStatusManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucBookStatusManagement);
        }

        private void TvTranslatorManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucTranslatorManagement);
        }

        private void TvCategoryManagementInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucCategoryManagement);
        }

        private void TvFeatureManagementInformation_Function()
        {
            Grid gdView = getGdView();
            
            gdView.Children.Clear();
            gdView.Children.Add(ucFunctionManagement);
        }

        private void TvRoleManagementInformation_Function()
        {
            Grid gdView = getGdView();
            ucRoleManagement.getRoleGroups = getRoleGroups;

            gdView.Children.Clear();
            gdView.Children.Add(ucRoleManagement);
        }

        private void TvViewUserRoleInformation_Function()
        {
            Grid gdView = getGdView();
            
            gdView.Children.Clear();
            gdView.Children.Add(ucUserRoleManagement);
        }

        private void TvViewRoleFunctionInformation_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucRoleFunctionManagement);
        }

        private void TvReaderManagement_Function()
        {
            Grid gdView = getGdView();
            
            gdView.Children.Clear();
            gdView.Children.Add(ucReaderManagement);
        }

        private void TvBookISBNManagement_Function()
        {
            Grid gdView = getGdView();
            
            gdView.Children.Clear();
            gdView.Children.Add(ucBookISBNManagement);
        }

        private void TvBookManagement_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucBookManagement);
        }

        private void TvBookTitleManagement_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucBookTitleManagement);
        }

        private void TvLoanSlipManagement_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucLoanSlipManagement);
        }

        private void TvLoanHistoryManagement_Function()
        {
            Grid gdView = getGdView();

            gdView.Children.Clear();
            gdView.Children.Add(ucLoanHistoryManagement);
        }

        #endregion

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            var frmLogin = MainWindow.UnitOfForm.FrmLogin(false);
            frmLogin.LogOut();
        }

        private void btnUserInfo_Click(object sender, RoutedEventArgs e)
        {
            gridUserLoginInformation.Visibility = (gridUserLoginInformation.Visibility == Visibility.Hidden) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
