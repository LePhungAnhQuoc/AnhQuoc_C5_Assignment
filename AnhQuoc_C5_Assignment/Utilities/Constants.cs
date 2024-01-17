using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnhQuoc_C5_Assignment
{
    public static class Constants
    {
        #region Parameter-Table-In-Database
        public static int LoanSlipExpDate = 7;
        #endregion

        public static DateTime dateEmptyValue = DateTime.MinValue;
        public static DateTime dateMinValue = DateTime.Parse("01/01/1900");
        public static Func<DateTime> dateMaxValue = () => DateTime.Now;

        public static DateTime boFMinValue = DateTime.Parse("01/01/1900");
        public static Func<DateTime> boFMaxValue = () => DateTime.Now;

        public static int phoneLength = 10;
        public static int identifyLength = 12;
        public static int textBoxMaxLength = 50;
        public static int txtAddressMaxLength = 100;
        public static int txtUrlImageMaxLength = 100;
        public static int txtBookTitleSummaryMaxLength = 100;
        public static int txtDescriptionMaxLength = 50;

        public const int MaxAnsiCode = 127;

        public static List<char> allowCharacterInText = new List<char> { ',', '.' };

        #region XmlFileName
        public static string fServerNames = "Data/ServerNames.xml";
        public static string childServerName = "ServerName";

        public static string fDatabaseNames = "Data/DatabaseNames.xml";
        public static string childDatabaseName = "DatabaseName";
        #endregion

        public static string adminRoleId = "R1";
        public static string adminGroup = "administration";

        public static string paraQD1 = "QD1";
        public static string paraQD8 = "QD8";
        public static string paraQD7 = "QD7";

        public static string paraQD11 = "QD11";
        public static string paraQD10 = "QD10";

        public static string roleFunc_FunctionId = "F19";
        public static string updateBookISBN_FunctionId = "F35";
        public static string[] importantFunction = new string[] { roleFunc_FunctionId, updateBookISBN_FunctionId };

        public const string UserManagement_FunctionId = "F1";
        public const string FeatureManagement_FunctionId = "F7";
        public const string RoleManagement_FunctionId = "F12";
        public const string ReaderManagement_FunctionId = "F21";
        public const string BookTitleManagement_FunctionId = "F29";
        public const string BookISBNManagement_FunctionId = "F32";
        public const string BookManagement_FunctionId = "F36";
        public const string LoanSlipManagement_FunctionId = "F41";
        public const string LoanHistoryManagement_FunctionId = "F43";

        public static string Culture = "vi-VN";
        public static string DatabaseNameConfig = "QuanLyThuVienEntities";

        public static string StartUrlImage = "/Images/";

        #region Prefix
        public static string prefixAuthor = "A";
        public static string prefixBookISBN = "ISBN";
        public static string prefixBookTitle = "BT";
        public static string prefixBook = string.Empty;
        public static string prefixCategory = "C";
        public static string prefixFunction = "F";
        public static string prefixParameter = "QD";
        public static string prefixProvince = string.Empty;
        public static string prefixReader = "R";
        public static string prefixRoleFunction = "RF";
        public static string prefixRole = "R";
        public static string prefixUserInfo = string.Empty;
        public static string prefixUserRole = "UR";
        public static string prefixUser = "U";
        public static string prefixServerName = "SV";
        public static string prefixDatabaseName = "DB";
        public static string prefixLoanDetail = "LDT";
        public static string prefixLoanHistory = "LHT";
        public static string prefixLoan = "L";
        public static string prefixPublisher = "P";
        public static string prefixLoanDetailHistory = "LDTH";

        #endregion

        #region CheckProperties

        public static string[] checkPropBookISBN = new string[] { "IdBookTitle", "IdAuthor", "Language" };
        public static string[] checkPropBookTitle = new string[] { "IdCategory", "Name" };
        public static string[] checkPropFunction = new string[] { "Name", "IdParent" };
        public static string[] checkPropRole = new string[] { "Name" };
        public static string[] checkPropReader = new string[] { "LName", "FName", "boF", "ReaderType" };
        public static string[] checkPropChild = new string[] { "IdAdult" };
        public static string[] checkPropAdult = new string[] { "Username" };


        #endregion

        #region ExceptDataGrid-Display
        public static string[] exceptDtgChilds = new string[] { "Id", "boF", "LName", "FName", "ReaderType", "ChildsQuantity", "Status", "CreatedAt", "ModifiedAt" };

        public static string[] exceptDtgCreateLoanSlipBook = new string[] { "ISBN", "CreatedAt", "ModifiedAt", "PublishDate", "PriceCurrent", "Translator", "Category", "Id", "Author" };

        public static string[] exceptDtgCreateLoanHistoryBook = new string[] { "ISBN", "CreatedAt", "ModifiedAt", "PublishDate", "Author", "Category" };

        #endregion

        #region Style-String
        public static string styledtgLoanOutOfDate = "dtgRowExpireDateLoan";
        public static string styleBtnRestore = "btnRestore";
        public static string styleBtnDelete = "btnDelete";
        public static string styleBtnConfirm = "btnConfirm";
        public static string styleBtnCancel = "btnCancel";
        public static string styleWDGeneral = "wdStyleGeneral";
        public static string styleStkWrapButton = "stkWrapButton";
        public static string stylelblNote = "lblNote";
        public static int maxWDHeight = 700;
        public static int maxWDWidth = 1250;

        public static WindowStartupLocation WDLocation = WindowStartupLocation.CenterScreen;
        #endregion

        public static Func<string> ShortConnStr = () => $"data source={MainWindow.DataSource};initial catalog={MainWindow.InitCatalog};integrated security={MainWindow.IntegratedSecurity.ToString()}";
        public static double FormMaxWidth = 800;
        public static double borderDistance = 10;
        public static double maxHeightTextArea = 200;
    }
}
