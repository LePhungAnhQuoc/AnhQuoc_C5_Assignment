using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using LiveCharts;
using LiveCharts.Wpf;
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
using AnhQuoc_C5_Assignment.Animations;
using System.Windows.Threading;
using System.Diagnostics;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Contexts
        public static BorrowBookViewModel borrowBookContext;
        public static ReturnBookViewModel returnBookContext;
        #endregion

        #region Static-Var
        public static User loginUser;
        public static string DataSource;
        public static string InitCatalog;
        public static bool IntegratedSecurity;
        #endregion

        #region Fields

        public static UnitOfRepository UnitOfRepo;
        public static UnitOfForm UnitOfForm;
        public static ObservableCollection<string> RoleGroups;        
        

        private ucLibrarianDashBoard _ucLibrarianDashBoard;
        private frmLogin frmLogin;
        private LoadingSpinnerWD loadingSpinnerWD;


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

        #region Properties
        public SeriesCollection BookStatusSeries { get; set; }
        public string[] BookStatusTypes { get; set; }
        public Func<double, string> BookStatusFormatter { get; set; }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            serverName = null;
            databaseName = null;

            #region Settings
            CultureInfo defaultCulture = new CultureInfo(Constants.Culture);
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
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

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }


        #region Loading
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            GetServerNameAndLoading();
            LoginAndGo();
        }

        public void LoginAndGo()
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



                // Checking
                var wait = new LoadingSpinnerWD(() => 
                { 
                    isCheckConnectionString = CheckIsRightConnection();
                });
                wait.ShowDialog();


                if (!isCheckConnectionString)
                {
                    Utilitys.ShowMessageBox1("Invalid Connections string!", "Error Connection", MessageBoxImage.Error);
                    ResetServerName();
                    continue;
                }

            }
            //  CheckingModelHasDecimalProp();
        }

        private bool CheckIsRightConnection()
        {
            // Access the database or perform other operations
            var dbSource = DatabaseFirst.Instance.dbSource;

            foreach (PropertyInfo tableProperty in Utilitys.getDerivePropsFromType(dbSource))
            {
                IEnumerable value = null;
                try
                {
                    value = (IEnumerable)Utilitys.getValueFromProperty(tableProperty, dbSource);
                    value.ToListObject(); // Check EntityException
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

            LoadingSpinnerWD wait = new LoadingSpinnerWD(() =>
            {
                #region LoadUnit2
                UnitOfRepo.Load();

                UnitOfViewModel.Instance._UnitOfRepo = UnitOfRepo;
                UnitOfViewModel.Instance.Load();

                UnitOfMap.Instance.UnitOfRepo = UnitOfRepo;
                UnitOfMap.Instance.Load();
                #endregion

                #region ViewModels
                roleVM = UnitOfViewModel.Instance.RoleViewModel;
                functionVM = UnitOfViewModel.Instance.FunctionViewModel;
                userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
                roleFunctionVM = UnitOfViewModel.Instance.RoleFunctionViewModel;
                #endregion

                RoleGroups = roleVM.GetGroups();

                UpdateAtFirstLoadProgram();
            });
            wait.ShowDialog();
        }

        #endregion

        // Update DataRecord at first load Program
        private void UpdateAtFirstLoadProgram()
        {
            #region Check-ExpireDate
            AdultViewModel adultVM = UnitOfViewModel.Instance.AdultViewModel;
            ReaderViewModel readerVM = UnitOfViewModel.Instance.ReaderViewModel;
            ChildViewModel childVM = UnitOfViewModel.Instance.ChildViewModel;

            bool updateStatus = false;
            foreach (Adult adult in adultVM.Repo)
            {
                if (DateTime.Now > adult.ExpireDate)
                {
                    Reader adultReader = readerVM.FindById(adult.IdReader);

                    if (adult.Status != updateStatus)
                    {
                        adult.Status = updateStatus;
                        adultReader.Status = updateStatus;

                        var childs = childVM.GetChildrenFromAdult(adult);
                        foreach (Child child in childs)
                        {
                            Reader childReader = readerVM.FindById(child.IdReader);

                            if (child.Status != updateStatus)
                            {
                                child.Status = updateStatus;
                                childReader.Status = updateStatus;

                                childVM.Repo.WriteUpdate(child);
                                readerVM.Repo.WriteUpdate(childReader);
                            }
                        }
                        adultVM.Repo.WriteUpdate(adult);
                        readerVM.Repo.WriteUpdate(adultReader);
                    }
                }
            }
            #endregion
        }

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


            // Order Statistical function at top
            var statisticalF = layThongTinDsTinhNang.FirstOrDefault(item => item.Id == Constants.StatisticalPage_FunctionId);
            if (statisticalF != null)
            {
                layThongTinDsTinhNang.Remove(statisticalF);
                layThongTinDsTinhNang.Insert(0, statisticalF);
            }


            LoadUcDashBoard(layThongTinDsTinhNang);
            return true;
        }

        private bool IsCheckValidAccount(User user, ref IEnumerable<RoleFunction> functionsByRole)
        {
            UserRole userRoleFinded = userRoleVM.FindByIdUser(user.Id);
            if (userRoleFinded == null)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyNotHaveRole());
                return false;
            }

            // Check Role Status
            Role role = roleVM.FindById(userRoleFinded.IdRole);
            if (role.Status == false)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyRoleLock());
                return false;
            }

            functionsByRole = roleFunctionVM.GetListFunctionByRole(userRoleFinded.IdRole);
            if (functionsByRole.Count() == 0)
            {
                Utilitys.ShowMessageBox1(Utilitys.NotifyNotHaveFunctions("user"));
                return false;
            }
            return true;
        }
        #endregion

        #region Others
        private void CheckingDtos()
        {
            // Checking Dto
            foreach (PropertyInfo tableProperty in Utilitys.getDerivePropsFromType(DatabaseFirst.Instance.dbSource))
            {
                RunAgain:
                IEnumerable value = null;
                try
                {
                    value = (IEnumerable)Utilitys.getValueFromProperty(tableProperty, DatabaseFirst.Instance.dbSource);
                }
                catch
                {
                    continue;
                }
                Type itemDataType = Utilitys.GetItemDataTypeInGenericList(value);
                string modelName = itemDataType.Name;

                if (modelName != "Book")
                {
                    continue;
                }

                var props = Utilitys.getPropsFromType(itemDataType);
                var propsInDto = Utilitys.getPropsFromType(typeof(BookDto));

                string checkingResult = modelName + "\n";



                var list = Utilitys.OuterJoin(props, propsInDto, "Name");

                if (list.Count() > 0)
                    checkingResult += "Dto thieu: " + string.Join(", ", list.ToArray()) + "\n";

                list = Utilitys.OuterJoin(propsInDto, props, "Name");

                if (list.Count() > 0)
                    checkingResult += "Model chinh thuc thieu: " + string.Join(", ", list.ToArray()) + "\n";

                Utilitys.ShowMessageBox1(checkingResult);

                int a = 5;
                //goto RunAgain;
            }
        }
        #endregion
    }

    public class BindingProxy : System.Windows.Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
    }

}
