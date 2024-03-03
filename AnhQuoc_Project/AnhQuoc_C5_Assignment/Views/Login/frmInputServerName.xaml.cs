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
using System.Xml;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmInputServerName.xaml
    /// </summary>
    public partial class frmInputServerName : Window, INotifyPropertyChanged
    {
        #region GetData
        public Func<ServerNameRepository> getServerNameRepo { get; set; }
        public Func<DatabaseNameRepository> getDatabaseNameRepo { get; set; }
        #endregion

        #region Fields

        private bool isCheckRemember;
        public bool? IsRememberMe { get; set; }
        #endregion

        #region Properties
        public bool IsNewServerName { get; set; }
        public bool IsNewDatabaseName { get; set; }


        private DatabaseName _DatabaseName;
        public DatabaseName DatabaseName
        {
            get { return _DatabaseName; }
            set
            {
                _DatabaseName = value;
                OnPropertyChanged();
            }
        }

        private ServerName _ServerName;
        public ServerName ServerName
        {
            get
            {
                return _ServerName;
            }
            set
            {
                _ServerName = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<ServerName> _ServerNames;
        public ObservableCollection<ServerName> ServerNames
        {
            get { return _ServerNames; }
            set
            {
                _ServerNames = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DatabaseName> _DatabaseNames;
        public ObservableCollection<DatabaseName> DatabaseNames
        {
            get { return _DatabaseNames; }
            set
            {
                _DatabaseNames = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ViewModels
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

        public frmInputServerName()
        {
            InitializeComponent();

            serverNameVM = UnitOfViewModel.Instance.ServerNameViewModel;
            databaseNameVM = UnitOfViewModel.Instance.DatabaseNameViewModel;

            IsNewServerName = false;
            IsNewDatabaseName = false;

            ServerName = null;
            DatabaseName = null;

            btnConfirm.Click += BtnConfirm_Click;
            btnExit.Click += BtnExit_Click;
            txtServerName.PreviewKeyDown += TxtServerName_PreviewKeyDown;
            txtDatabaseName.PreviewKeyDown += TxtDatabaseName_PreviewKeyDown;

            this.Loaded += FrmInputServerName_Loaded;
            this.DataContext = this;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FrmInputServerName_Loaded(object sender, RoutedEventArgs e)
        {
            IsRememberMe = true;

            isCheckRemember = false;

            ServerName = null;
            DatabaseName = null;
            IsNewDatabaseName = false;
            IsNewServerName = false;

            ServerNames = getServerNameRepo().Gets();
            DatabaseNames = getDatabaseNameRepo().Gets();

            ServerName = serverNameVM.FindTrue();
            DatabaseName = databaseNameVM.FindTrue();

            if (ServerName != null && DatabaseName != null)
            {
                isCheckRemember = true;
            }

            string serverName = serverNameVM.GetServerName();
            string serverName2 = serverNameVM.GetServerName2();

            ServerName newServer1 = new ServerName(serverNameVM.GetId(), serverName);

            string propName = nameof(newServer1.Name);
            XmlNode finded = null;

            XmlProvider.Instance.Open(Constants.fServerNames);
            finded = XmlProvider.Instance.FindNode(Constants.childServerName, propName, newServer1.Name);
            if (finded == null)
            {
                getServerNameRepo().Prepend(newServer1);
                getServerNameRepo().WritePrepend(newServer1);
            }

            ServerName newServer2 = new ServerName(serverNameVM.GetId(), serverName2);
            
            finded = XmlProvider.Instance.FindNode(Constants.childServerName, propName, newServer2.Name);
            if (finded == null)
            {
                getServerNameRepo().Prepend(newServer2);
                getServerNameRepo().WritePrepend(newServer2);
            }

            if (ServerName == null)
            {
                ServerName = ServerNames.ElementAtOrDefault(0);
            }
            if (DatabaseName == null)
            {
                DatabaseName = DatabaseNames.ElementAtOrDefault(0);
            }

            XmlProvider.Instance.Close();

            if (isCheckRemember == true)
            {
                BtnConfirm_Click(null, null);
            }
        }

        private void TxtServerName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Utilities.MoveComboBox(cbServerName, e);
        }

        private void TxtDatabaseName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Utilities.MoveComboBox(cbDatabaseName, e);
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (isCheckRemember)
            {
                this.Close();
                return;
            }
            bool getServerName = GetServerName();
            bool getDatabaseName = GetDatabaseName();

            if (getServerName && getDatabaseName)
            {
                this.Close();
            }
        }

        private bool GetServerName()
        {
            if (ServerName == null)
            {
                if (Utilities.IsCheckEmptyString(txtServerName.Text))
                {
                    Utilities.ShowMessageBox1(Utilities.ValidateNoteFormNotEmptyRule());
                    return false;
                }
                else
                {
                    IsNewServerName = true;
                }
            }

            if (ServerName != null)
            {
                if (Utilities.IsCheckEmptyString(txtServerName.Text))
                {
                    Utilities.ShowMessageBox1(Utilities.ValidateNoteFormNotEmptyRule());
                    return false;
                }
                else
                {
                    if (txtServerName.Text == ServerName.Name)
                    {
                    }
                    else
                    {
                        IsNewServerName = true;
                    }
                }
            }
            return true;
        }

        private bool GetDatabaseName()
        {
            if (DatabaseName == null)
            {
                if (Utilities.IsCheckEmptyString(txtDatabaseName.Text))
                {
                    Utilities.ShowMessageBox1(Utilities.ValidateNoteFormNotEmptyRule());
                    return false;
                }
                else
                {
                    IsNewDatabaseName = true;
                }
            }

            if (DatabaseName != null)
            {
                if (Utilities.IsCheckEmptyString(txtDatabaseName.Text))
                {
                    Utilities.ShowMessageBox1(Utilities.ValidateNoteFormNotEmptyRule());
                    return false;
                }
                else
                {
                    if (txtDatabaseName.Text == DatabaseName.Name)
                    {
                    }
                    else
                    {
                        IsNewDatabaseName = true;
                    }
                }
            }
            return true;
        }

        private void chkRememberMe_CheckedChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
