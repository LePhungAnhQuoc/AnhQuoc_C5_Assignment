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
        #region Directory-Images
        public static string rememberDirectoryOpenFile = @"C:\";
        public static string StartUrlImage = "\\Assets\\Images\\";
        public static string HumanUrlImage = "\\Assets\\Images\\Others\\human.png";

        public static string fServerNames = "Data/ServerNames.xml";
        public static string childServerName = "ServerName";

        public static string fDatabaseNames = "Data/DatabaseNames.xml";
        public static string childDatabaseName = "DatabaseName";

        #endregion

        public static DateTime dateEmptyValue = DateTime.MinValue;
        public static DateTime dateMinValue = DateTime.Parse("01/01/1900");
        public static Func<DateTime> dateMaxValue = () => DateTime.Now;

        #region txt-Length
        public static int textBoxMaxLength = 500;
        public static int textIdentifyLength = 12;
        public static int textPhoneLength = 10;
        public static int textAreaMaxLength = 1000;
        #endregion

        public const int MaxAnsiCode = 127;
        public static List<char> allowCharacterInText = new List<char> { ',', '.' };

        public static string adminRoleId = "R1";
        public static Func<string> adminGroup = () => (UnitOfViewModel.Instance.RoleViewModel).FindById(adminRoleId).Group;

        public static string paraQD1 = "QD1";
        public static string paraQD8 = "QD8";
        public static string paraQD7 = "QD7";
        public static string paraQD9 = "QD9";
        public static string paraQD11 = "QD11";
        public static string paraQD10 = "QD10";
        public static string paraQD2 = "QD2";

        public static string reason1 = "PR1";
        public static string reason2 = "PR2";
        public static string reason3 = "PR3";


        #region Function-Id
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
        public const string CategoryManagement_FunctionId = "F45";
        public const string PublisherManagement_FunctionId = "F50";
        public const string AuthorManagement_FunctionId = "F54";
        public const string TranslatorManagement_FunctionId = "F59";
        public const string PenaltyReasonManagement_FunctionId = "F69";
        public const string ProvinceManagement_FunctionId = "F64";
        public const string ParameterManagement_FunctionId = "F73";
        public const string StatisticalPage_FunctionId = "F78";
        #endregion

        public static string Culture = "vi-VN";
        public static string DatabaseNameConfig = nameof(QuanLyThuVienEntities);

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
        public static string prefixTranslator = "T";
        public static string prefixBookStatus = "BS";

        #endregion

        #region CheckProperties
        public static string[] checkPropBookISBN = new string[] { "IdBookTitle", "IdAuthor", "OriginLanguage" };
        public static string[] checkPropBookTitle = new string[] { "Name", "IdCategory" };
        public static string[] checkPropFunction = new string[] { "Name", "IdParent" };
        public static string[] checkPropRole = new string[] { "Name" };
        public static string[] checkPropReader = new string[] { "LName", "FName", "boF", "ReaderType" };
        public static string[] checkPropChild = new string[] { "IdAdult" };
        public static string[] checkPropUser = new string[] { "Username" };
        public static string[] checkPropAuthor = new string[] { "Name" };
        public static string[] checkPropAdult = new string[] { "Identify" };
        public static string[] checkPropCategory = new string[] { "Name" };
        public static string[] checkPropPublisher = new string[] { "Name" };
        public static string[] checkPropTranslator = new string[] { "Name" };
        public static string[] checkPropProvince = new string[] { "Name" };
        public static string[] checkPropPenaltyReason = new string[] { "Name" };
        public static string[] checkPropParameter = new string[] { "Name" };
        public static string[] checkPropBook = new string[] {};
        #endregion

        #region ExceptDataGrid-Display
        public static string[] exceptDtgReaderChild = new string[] { "Id", "boF", "LName", "FName", "ReaderType", "ChildsQuantity", "Status", "CreatedAt", "ModifiedAt" };
        public static string[] exceptDtgBookCreateLoanSlip = new string[] { "Id", "ISBN", "Translator.Name", "PublishDate", "PriceCurrent", "Category.Name", "Author.Name", "CreatedAt", "ModifiedAt" };
        #endregion

        #region Style-String
        public static string styleBtnRestore = "btnRestore";
        public static string styleBtnDelete = "btnDelete";
        public static string styleBtnConfirm = "btnConfirm";
        public static string styleBtnCancel = "btnCancel";
        public static string styleWDGeneral = "wdStyleGeneral";
        public static string styleStkWrapButton = "stkWrapButton";
        public static string stylelblNote = "lblNote";
        #endregion

        public static int maxWDHeight = 700;
        public static int maxWDWidth = 1250;
        public static double FormMaxWidth = 800;
        public static double borderDistance = 10;

        public static double maxHeightTextArea = 200;

        public static WindowStartupLocation WDLocation = WindowStartupLocation.CenterScreen;

        public static Func<string> ShortConnStr = () => $"data source={MainWindow.DataSource};initial catalog={MainWindow.InitCatalog};integrated security={MainWindow.IntegratedSecurity.ToString()}";
    }
}
