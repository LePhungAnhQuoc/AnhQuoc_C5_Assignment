using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window, INotifyPropertyChanged
    {
        #region GetData
        public Func<MainWindow> getFrmMain;
        public Func<UserRepository> getUserRepo;
        #endregion

        #region Fields
        private UserViewModel UserVM;
        #endregion

        #region Properties
        private UserAccountLogIn _Account;
        public UserAccountLogIn Account
        {
            get
            { 
                return _Account;
            }
            set
            {
                _Account = value;
                OnPropertyChanged();
            }
        }

        public bool IsOpenConnectForm { get; set; }
        #endregion

        #region Result-Properties
        public User User { get; set; }
        #endregion

        #region PropertyNotify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public frmLogin()
        {
            InitializeComponent();

            Account = new UserAccountLogIn();
            UserVM = UnitOfViewModel.Instance.UserViewModel;

            boxPassword.PreviewTextInput += txtPassEnter_PreviewTextInput;
            txtUsername.PreviewTextInput += txtPassEnter_PreviewTextInput;

            txtUsername.GotKeyboardFocus += Txt_GotKeyboardFocus;
            txtUsername.LostMouseCapture += Txt_LostMouseCapture;
            txtUsername.LostKeyboardFocus += Txt_LostKeyboardFocus;

            boxPassword.GotKeyboardFocus += Txt_GotKeyboardFocus;
            boxPassword.LostMouseCapture += Txt_LostMouseCapture;
            boxPassword.LostKeyboardFocus += Txt_LostKeyboardFocus;

            btnSignIn.Click += btnSignIn_Click;


            this.Closed += FrmLogin_Closed;
            this.DataContext = this;
        }

        private void Txt_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // once we've left the TextBox, return the select all behavior
            textBox.LostMouseCapture += Txt_LostMouseCapture;
        }

        private void Txt_LostMouseCapture(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            // If user highlights some text, don't override it
            if (textBox.SelectionLength == 0)
                textBox.SelectAll();

            // further clicks will not select all
            textBox.LostMouseCapture -= Txt_LostMouseCapture;
        }

        private void Txt_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            // Fixes issue when clicking cut/copy/paste in context menu
            if (textBox.SelectionLength == 0)
                textBox.SelectAll();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsOpenConnectForm = false;
            ClearLogin();
        }
        
        private void FrmLogin_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #region Methods
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Focus();
            var userCheck = UserVM.FindAccount(Account);

            if (userCheck == null)
            {
                MessageBox.Show("Incorrect username or password", string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (userCheck.Status == false)
            {
                string message = string.Format("This account has been deleted, Contact admin for help!");
                MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                User = userCheck;
                this.Hide();
            }
        }

        private void BtnOpenConnectform_Click(object sender, RoutedEventArgs e)
        {
            IsOpenConnectForm = true;
            this.Hide();
        }

        public void ClearLogin()
        {
            txtUsername.Clear();
            boxPassword.Clear();
            txtUsername.Focus();
        }

        private void txtPassEnter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Utilities.IsCheckEnter(e))
            {
                btnSignIn_Click(btnSignIn, null);
            }
        }
        #endregion

        public void LogOut()
        {
            getFrmMain().gdContent.Children.Clear();
            getFrmMain().LoginAndGo();
        }

        private void TblOpenConnectForm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BtnOpenConnectform_Click((object)sender, e);
        }

        private void tblForgetPass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Utilities.ShowMessageBox1(Utilities.NotifyFeatureNotDevelop());
        }
    }
}
