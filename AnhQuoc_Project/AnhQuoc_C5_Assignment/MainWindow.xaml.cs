using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Data.Entity;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Collections;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static BorrowBookViewModel borrowBookContext;
        public static ReturnBookViewModel returnBookContext;

        public static User loginUser;
        public static string DataSource;
        public static string InitCatalog;
        public static bool IntegratedSecurity;

        #region Fields
        public static UnitOfRepository UnitOfRepo;
        public static UnitOfForm UnitOfForm;
        public static ObservableCollection<string> RoleGroups;        
        

        private ucLibrarianDashBoard _ucLibrarianDashBoard;
        private frmLogin frmLogin;

        private ServerName serverName;
        private DatabaseName databaseName;
        #endregion

        #region ViewModels
        private FunctionViewModel functionVM;
        private UserRoleViewModel userRoleVM;
        private RoleFunctionViewModel roleFunctionVM;
        private RoleViewModel roleVM;

        private ServerNameViewModel serverNameVM;
        private DatabaseNameViewModel databaseNameVM;
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

        public MainWindow()
        {
            InitializeComponent();

            serverName = null;
            databaseName = null;

            #region Settings
            CultureInfo vietnameseCulture = new CultureInfo(Constants.Culture);
            Thread.CurrentThread.CurrentCulture = vietnameseCulture;
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = vietnameseCulture.DateTimeFormat;
            #endregion

            #region LoadUnit1
            UnitOfRepo = new UnitOfRepository();
            UnitOfRepo.LoadServerNameRepo();

            UnitOfViewModel.Instance._UnitOfRepo = UnitOfRepo;
            UnitOfViewModel.Instance.LoadServerName();

            serverNameVM = UnitOfViewModel.Instance.ServerNameViewModel;
            databaseNameVM = UnitOfViewModel.Instance.DatabaseNameViewModel;

            UnitOfForm = new UnitOfForm(UnitOfRepo);
            UnitOfForm.LoadServerNameForm();
            #endregion

            GetServerNameAndLoading();
            
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        #region Loading
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loading();
        }

        public void Loading()
        {
            this.Hide();
        Goto:
            frmLogin = UnitOfForm.FrmLogin(true);
            frmLogin.getFrmMain = () => this;
            frmLogin.ClearLogin();

            do
            {
                frmLogin.IsOpenConnectForm = false;
                frmLogin.ShowDialog();

                if (frmLogin.IsOpenConnectForm)
                {
                    GetServerNameAndLoading();
                }
            } while (frmLogin.IsOpenConnectForm);

            loginUser = frmLogin.User;

            if (GoToAccount(loginUser))
            {
                this.Show();
            }
            else
            {
                goto Goto;
            }
        }

        private void LoadUcDashBoard(ObservableCollection<Function> dsTinhNang)
        {
            _ucLibrarianDashBoard = UnitOfForm.UcLibrarianDashBoard(true);
            _ucLibrarianDashBoard.getFunctions = () => dsTinhNang;

            _ucLibrarianDashBoard.getLoginUser = () => loginUser;
            _ucLibrarianDashBoard.getGdView = () => gdContent;
            _ucLibrarianDashBoard.getFrmMain = () => this;

            _ucLibrarianDashBoard.HorizontalAlignment = HorizontalAlignment.Stretch;
            _ucLibrarianDashBoard.VerticalAlignment = VerticalAlignment.Stretch;

            gdDashBoard.Children.Clear();
            gdDashBoard.Children.Add(_ucLibrarianDashBoard);
        }
        #endregion

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #region Connection-Area
        private string ModifyAppConfig(string dataSource, string databaseName)
        {
            string sectionConfig = "connectionStrings";
            int indexDataSource = 2;
            int indexInitCatalog = 1;
            int indexIntegratedSecurity = 1;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection(sectionConfig);

            string connectStr = ConfigurationManager.ConnectionStrings[Constants.DatabaseNameConfig].ConnectionString
                .Clone() as string;
            string[] connectSplit = connectStr.Split(';');

            #region DataSource
            string dataSourcePart = connectSplit.FirstOrDefault(item => item.Contains("Data Source", true));
            var dataSourceSplit = dataSourcePart.Split('=').ToList();
            string dataSourceValue = dataSourceSplit[indexDataSource];

            MainWindow.DataSource = dataSource;

            dataSourceSplit.RemoveAt(indexDataSource);
            dataSourceSplit.Insert(indexDataSource, MainWindow.DataSource);

            string newDataSourcePart = string.Join("=", dataSourceSplit);
            connectStr = connectStr.Replace(dataSourcePart, newDataSourcePart);
            #endregion

            #region initCatalogPart
            string initCatalogPart = connectSplit.FirstOrDefault(item => item.Contains("Initial Catalog", true));
            var initCatalogSplit = initCatalogPart.Split('=').ToList();
            string initCatalogValue = initCatalogSplit[indexInitCatalog];

            MainWindow.InitCatalog = databaseName;

            initCatalogSplit.RemoveAt(indexInitCatalog);
            initCatalogSplit.Insert(indexInitCatalog, MainWindow.InitCatalog);

            string newInitCatalogPart = string.Join("=", initCatalogSplit);
            connectStr = connectStr.Replace(initCatalogPart, newInitCatalogPart);
            #endregion

            #region integratedSecurityPart
            string integratedSecurityPart = connectSplit.FirstOrDefault(item => item.Contains("integrated security", true));
            var integratedSecuritySplit = integratedSecurityPart.Split('=').ToList();
            string integratedSecurityValue = integratedSecuritySplit[indexIntegratedSecurity];

            MainWindow.IntegratedSecurity = true;

            integratedSecuritySplit.RemoveAt(indexIntegratedSecurity);
            integratedSecuritySplit.Insert(indexIntegratedSecurity, MainWindow.IntegratedSecurity.ToString());

            string newIntegratedSecurityPart = string.Join("=", integratedSecuritySplit);
            connectStr = connectStr.Replace(integratedSecurityPart, newIntegratedSecurityPart);
            #endregion

            config.ConnectionStrings.ConnectionStrings[Constants.DatabaseNameConfig].ConnectionString = connectStr;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(sectionConfig);

            return connectStr;
        }

        private void OpenFormServerNameInformation(ref ServerName serverName, ref DatabaseName databaseName)
        {
            frmInputServerName frmInputServerName = UnitOfForm.FrmInputServerName(true);
            frmInputServerName.ShowDialog();

            if (frmInputServerName.IsNewServerName)
            {
                string serverNameId = serverNameVM.GetId();
                serverName = new ServerName(serverNameId);
                serverName.Name = frmInputServerName.txtServerName.Text;
                serverName.Status = false;

                UnitOfRepo.ServerNameRepo.WriteAdd(serverName);
            }
            else
            {
                serverName = frmInputServerName.ServerName;
            }

            if (frmInputServerName.IsNewDatabaseName)
            {
                string id = databaseNameVM.GetId();
                databaseName = new DatabaseName(id);
                databaseName.Name = frmInputServerName.txtDatabaseName.Text;
                databaseName.Status = false;

                UnitOfRepo.DatabaseNameRepo.WriteAdd(databaseName);
            }
            else
            {
                databaseName = frmInputServerName.DatabaseName;
            }

            if (frmInputServerName.IsRememberMe == true)
            {
                serverName.Status = true;
                UnitOfRepo.ServerNameRepo.WriteUpdate(serverName);

                databaseName.Status = true;
                UnitOfRepo.DatabaseNameRepo.WriteUpdate(databaseName);
            }
        }

        private void ResetServerName()
        {
            if (serverName != null && databaseName != null)
            {
                serverName.Status = false; // Giao diện
                UnitOfRepo.ServerNameRepo.WriteUpdate(serverName); // Database

                databaseName.Status = false;
                UnitOfRepo.DatabaseNameRepo.WriteUpdate(databaseName);
            }
        }

        public void SetUpServerName()
        {
            ResetServerName();

            bool isCheckConnectionString = false;
            while (!isCheckConnectionString)
            {
                if (DatabaseFirst.Instance != null && DatabaseFirst.Instance.dbSource != null)
                {
                    DatabaseFirst.Instance.dbSource.Dispose();
                    DatabaseFirst.Instance.dbSource = null;
                }

                DatabaseFirst.Instance = null;
                DatabaseFirst.IsConnectValid = false;

                OpenFormServerNameInformation(ref serverName, ref databaseName);
                DatabaseFirst.ConnStr = ModifyAppConfig(serverName.Name, databaseName.Name);

                DatabaseFirst.IsConnectValid = true;
                
                isCheckConnectionString = CheckIsRightConnection();
                
                if (!isCheckConnectionString)
                {
                    Utilities.ShowMessageBox1("Invalid Connections string!", "Error Connection", MessageBoxImage.Error);
                    ResetServerName();
                    continue;
                }
            }


        //     CheckingModelHasDecimalProp();
        }

        private bool CheckIsRightConnection()
        {
            // Access the database or perform other operations
            var dbSource = DatabaseFirst.Instance.dbSource;

            foreach (PropertyInfo tableProperty in Utilities.getDerivePropsFromType(dbSource))
            {
                IEnumerable value = null;
                try
                {
                    value = (IEnumerable)Utilities.getValueFromProperty(tableProperty, dbSource);
                    foreach (var item in value)
                    {
                        break;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public void GetServerNameAndLoading()
        {
            SetUpServerName();

            //frmLoadingSpinnerControl.ucLoadingSpinnerControl.lblContent.Content = "Load data...";

            #region LoadUnit2
            UnitOfRepo.Load();

            UnitOfViewModel.Instance._UnitOfRepo = UnitOfRepo;
            UnitOfViewModel.Instance.Load();

            UnitOfMap.Instance.UnitOfRepo = UnitOfRepo;
            UnitOfMap.Instance.Load();

            UnitOfForm.Load();
            #endregion

            #region ViewModels
            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            roleFunctionVM = UnitOfViewModel.Instance.RoleFunctionViewModel;
            #endregion

            RoleGroups = roleVM.GetGroups();

            //SetLoadAnimation("", false);
        }
        #endregion

        #region Account-Area
        public bool GoToAccount(User user)
        {
            IEnumerable<RoleFunction> functionsByRole = null;

            if (!IsCheckValidAccount(user, ref functionsByRole))
            {
                return false;
            }

            // Get ucDashBoard ...
            var dsTinhNang = functionsByRole;

            var layThongTinDsTinhNang = functionVM.getFunctionsByListId(dsTinhNang.Select(i => i.IdFunction));

            LoadUcDashBoard(layThongTinDsTinhNang);
            return true;
        }

        private bool IsCheckValidAccount(User user, ref IEnumerable<RoleFunction> functionsByRole)
        {
            UserRole userRoleFinded = userRoleVM.FindByIdUser(user.Id);
            if (userRoleFinded == null)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyNotHaveRole());
                return false;
            }

            // Check Role Status
            Role role = roleVM.FindById(userRoleFinded.IdRole);
            if (role.Status == false)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyRoleLock());
                return false;
            }

            functionsByRole = roleFunctionVM.GetListFunctionByRole(userRoleFinded.IdRole);
            if (functionsByRole.Count() == 0)
            {
                Utilities.ShowMessageBox1(Utilities.NotifyNotHaveFunctions("user"));
                return false;
            }
            return true;
        }
        #endregion

        #region Others
        private void CheckingDtos()
        {
            // Checking Dto
            foreach (PropertyInfo tableProperty in Utilities.getDerivePropsFromType(DatabaseFirst.Instance.dbSource))
            {
                RunAgain:
                IEnumerable value = null;
                try
                {
                    value = (IEnumerable)Utilities.getValueFromProperty(tableProperty, DatabaseFirst.Instance.dbSource);
                }
                catch
                {
                    continue;
                }
                Type itemDataType = Utilities.GetItemDataTypeInGenericList(value);
                string modelName = itemDataType.Name;

                if (modelName != "Book")
                {
                    continue;
                }

                var props = Utilities.getPropsFromType(itemDataType);
                var propsInDto = Utilities.getPropsFromType(typeof(BookDto));

                string checkingResult = modelName + "\n";



                var list = Utilities.OuterJoin(props, propsInDto, "Name");

                if (list.Count() > 0)
                    checkingResult += "Dto thieu: " + string.Join(", ", list.ToArray()) + "\n";

                list = Utilities.OuterJoin(propsInDto, props, "Name");

                if (list.Count() > 0)
                    checkingResult += "Model chinh thuc thieu: " + string.Join(", ", list.ToArray()) + "\n";

                Utilities.ShowMessageBox1(checkingResult);

                int a = 5;
                //goto RunAgain;
            }
        }

        private void CheckingModelHasDecimalProp()
        {
            List<string> resultHere = new List<string>();
            // Checking Dto
            foreach (PropertyInfo tableProperty in Utilities.getDerivePropsFromType(DatabaseFirst.Instance.dbSource))
            {
            RunAgain:
                IEnumerable value = null;
                try
                {
                    value = (IEnumerable)Utilities.getValueFromProperty(tableProperty, DatabaseFirst.Instance.dbSource);
                }
                catch
                {
                    continue;
                }
                Type itemDataType = Utilities.GetItemDataTypeInGenericList(value);


                string modelName = itemDataType.Name;

                List<string> listName = resultHere;


                var listProps = Utilities.getPropsFromType(itemDataType);
                foreach (var prop in listProps)
                {
                    var name = prop.PropertyType.Name;

                    if (name == "Decimal")
                    {
                        listName.Add(modelName);
                        break;
                    }
                }
                continue;

                var props = Utilities.getPropsFromType(itemDataType);
                var propsInDto = Utilities.getPropsFromType(typeof(BookDto));

                string checkingResult = modelName + "\n";



                var list = Utilities.OuterJoin(props, propsInDto, "Name");

                if (list.Count() > 0)
                    checkingResult += "Dto thieu: " + string.Join(", ", list.ToArray()) + "\n";

                list = Utilities.OuterJoin(propsInDto, props, "Name");

                if (list.Count() > 0)
                    checkingResult += "Model chinh thuc thieu: " + string.Join(", ", list.ToArray()) + "\n";

              //  Utilities.ShowMessageBox1(checkingResult);

                int b = 5;
                //goto RunAgain;
            }
            int a = 5;
            string str = string.Join(", ", resultHere.ToArray());
            Utilities.ShowMessageBox1(str);

        }
        #endregion
    }
}
