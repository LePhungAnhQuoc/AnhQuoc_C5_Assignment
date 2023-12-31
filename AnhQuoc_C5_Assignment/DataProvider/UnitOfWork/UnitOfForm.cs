﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UnitOfForm
    {
        private UnitOfRepository _UnitOfRepo;

        #region Fields
        // UserControl
        private ucBooksTable ucBooksTable;
        private ucLibrarianDashBoard ucLibrarianDashBoard;
        private ucLoanSlipInformation ucLoanSlipInformation;
        private ucLoanSlipInformationStyle2 ucLoanSlipInformationStyle2;
        private ucFunctionInformation ucFunctionInformation;
        private ucDisplayFunction ucDisplayFunction;
        private ucAddBookTitle ucAddBookTitle;
        private ucAddBookISBN ucAddBookISBN;
        private ucAddBook ucAddBook;
        private ucAddAdult ucAddAdult;
        private ucAddChild ucAddChild;
        private ucUserInformation ucUserInformation;
        private ucUserManagement ucUserManagement;
        private ucFunctionManagement ucFunctionManagement;
        private ucRoleManagement ucRoleManagement;
        private ucUserRoleManagement ucUserRoleManagement;
        private ucRoleFunctionManagement ucRoleFunctionManagement;
        private ucReaderManagement ucReaderManagement;
        private ucBookISBNManagement ucBookISBNManagement;
        private ucBookManagement ucBookManagement;
        private ucBookTitleManagement ucBookTitleManagement;
        private ucLoanHistoryManagement ucLoanHistoryManagement;
        private ucLoanSlipManagement ucLoanSlipManagement;
        private ucLoanSlipPayment ucLoanSlipPayment;
        private ucReaderLoanInformation ucReaderLoanInformation;


        // Form
        private frmTransferGuardian frmTransferGuardian;
        private frmAddLoanHistory frmAddLoanHistory;
        private frmInputServerName frmInputServerName;
        private frmAddRole frmAddRole;
        private frmAddUserRole frmAddUserRole;
        private frmLogin frmLogin;
        private frmAddReader frmAddReader;
        private frmAddUser frmAddUser;
        private frmAdultReaderInformation frmAdultReaderInformation;
        private frmChildReaderInformation frmChildReaderInformation;
        private frmChildFunctionInformation frmChildFunctionInformation;
        private frmBookISBNInformation frmBookISBNInformation;
        private frmAddFunction frmAddFunction;
        private frmAddLoan frmAddLoan;
        


        #endregion

        #region Delegates
        #region UserControls
        private Func<bool, ucFunctionInformation> _UcFunctionInformation;
        public Func<bool, ucFunctionInformation> UcFunctionInformation
        {
            get { return _UcFunctionInformation; }
        }

        private Func<bool, ucReaderLoanInformation> _UcReaderLoanInformation;
        public Func<bool, ucReaderLoanInformation> UcReaderLoanInformation
        {
            get { return _UcReaderLoanInformation; }
        }

        private Func<bool, ucLoanSlipPayment> _UcLoanSlipPayment;
        public Func<bool, ucLoanSlipPayment> UcLoanSlipPayment
        {
            get { return _UcLoanSlipPayment; }
        }

        private Func<bool, ucLoanSlipManagement> _UcLoanSlipManagement;
        public Func<bool, ucLoanSlipManagement> UcLoanSlipManagement
        {
            get { return _UcLoanSlipManagement; }
        }

        private Func<bool, ucBooksTable> _UcBooksTable;
        public Func<bool, ucBooksTable> UcBooksTable
        {
            get { return _UcBooksTable; }
        }

        private Func<bool, ucLibrarianDashBoard> _UcLibrarianDashBoard;
        public Func<bool, ucLibrarianDashBoard> UcLibrarianDashBoard
        {
            get { return _UcLibrarianDashBoard; }
        }

        private Func<bool, ucLoanSlipInformation> _UcLoanSlipInformation;
        public Func<bool, ucLoanSlipInformation> UcLoanSlipInformation
        {
            get { return _UcLoanSlipInformation; }
        }

        private Func<bool, ucLoanSlipInformationStyle2> _UcLoanSlipInformationStyle2;
        public Func<bool, ucLoanSlipInformationStyle2> UcLoanSlipInformationStyle2
        {
            get { return _UcLoanSlipInformationStyle2; }
        }

        private Func<bool, ucDisplayFunction> _UcDisplayFunction;
        public Func<bool, ucDisplayFunction> UcDisplayFunction
        {
            get { return _UcDisplayFunction; }
        }

        private Func<bool, ucBookTitleManagement> _UcBookTitleManagement;
        public Func<bool, ucBookTitleManagement> UcBookTitleManagement
        {
            get { return _UcBookTitleManagement; }
        }

        private Func<bool, ucLoanHistoryManagement> _UcLoanHistoryManagement;
        public Func<bool, ucLoanHistoryManagement> UcLoanHistoryManagement
        {
            get { return _UcLoanHistoryManagement; }
        }
        
        private Func<bool, ucBookManagement> _UcBookManagement;
        public Func<bool, ucBookManagement> UcBookManagement
        {
            get { return _UcBookManagement; }
        }

        private Func<bool, ucBookISBNManagement> _UcBookISBNManagement;
        public Func<bool, ucBookISBNManagement> UcBookISBNManagement
        {
            get { return _UcBookISBNManagement; }
        }

        private Func<bool, ucReaderManagement> _UcReaderManagement;
        public Func<bool, ucReaderManagement> UcReaderManagement
        {
            get { return _UcReaderManagement; }
        }

        private Func<bool, ucAddBookTitle> _UcAddBookTitle;
        public Func<bool, ucAddBookTitle> UcAddBookTitle
        {
            get { return _UcAddBookTitle; }
        }

        private Func<bool, ucAddBookISBN> _UcAddBookISBN;
        public Func<bool, ucAddBookISBN> UcAddBookISBN
        {
            get { return _UcAddBookISBN; }
            set { _UcAddBookISBN = value; }
        }

        private Func<bool, ucAddBook> _UcAddBook;
        public Func<bool, ucAddBook> UcAddBook
        {
            get { return _UcAddBook; }
            set { _UcAddBook = value; }
        }

        private Func<bool, ucAddAdult> _UcAddAdult;
        public Func<bool, ucAddAdult> UcAddAdult
        {
            get { return _UcAddAdult; }
            set { _UcAddAdult = value; }
        }

        private Func<bool, ucAddChild> _UcAddChild;
        public Func<bool, ucAddChild> UcAddChild
        {
            get { return _UcAddChild; }
            set { _UcAddChild = value; }
        }

        private Func<bool, ucUserInformation> _UcUserInformation;
        public Func<bool, ucUserInformation> UcUserInformation
        {
            get { return _UcUserInformation; }
        }

        private Func<bool, ucUserManagement> _UcUserManagement;
        public Func<bool, ucUserManagement> UcUserManagement
        {
            get { return _UcUserManagement; }
        }

        private Func<bool, ucFunctionManagement> _UcFunctionManagement;
        public Func<bool, ucFunctionManagement> UcFunctionManagement
        {
            get { return _UcFunctionManagement; }
        }

        private Func<bool, ucRoleManagement> _UcRoleManagement;
        public Func<bool, ucRoleManagement> UcRoleManagement
        {
            get { return _UcRoleManagement; }
        }

        private Func<bool, ucUserRoleManagement> _UcUserRoleManagement;
        public Func<bool, ucUserRoleManagement> UcUserRoleManagement
        {
            get { return _UcUserRoleManagement; }
        }

        private Func<bool, ucRoleFunctionManagement> _UcRoleFunctionManagement;
        public Func<bool, ucRoleFunctionManagement> UcRoleFunctionManagement
        {
            get { return _UcRoleFunctionManagement; }
        }
        #endregion

        #region Forms
        private Func<bool, frmTransferGuardian> _FrmTransferGuardian;
        public Func<bool, frmTransferGuardian> FrmTransferGuardian
        {
            get { return _FrmTransferGuardian; }
        }

        private Func<bool, frmAddLoanHistory> _FrmAddLoanHistory;
        public Func<bool, frmAddLoanHistory> FrmAddLoanHistory
        {
            get { return _FrmAddLoanHistory; }
        }

        private Func<bool, frmChildFunctionInformation> _FrmChildFunctionInformation;
        public Func<bool, frmChildFunctionInformation> FrmChildFunctionInformation
        {
            get { return _FrmChildFunctionInformation; }
        }

        private Func<bool, frmInputServerName> _FrmInputServerName;
        public Func<bool, frmInputServerName> FrmInputServerName
        {
            get { return _FrmInputServerName; }
        }

        private Func<bool, frmLogin> _FrmLogin;
        public Func<bool, frmLogin> FrmLogin
        {
            get { return _FrmLogin; }
        }

        private Func<bool, frmAddReader> _FrmAddReader;
        public Func<bool, frmAddReader> FrmAddReader
        {
            get { return _FrmAddReader; }
        }

        private Func<bool, frmAdultReaderInformation> _FrmAdultReaderInformation;
        public Func<bool, frmAdultReaderInformation> FrmAdultReaderInformation
        {
            get { return _FrmAdultReaderInformation; }
        }

        private Func<bool, frmChildReaderInformation> _FrmChildReaderInformation;
        public Func<bool, frmChildReaderInformation> FrmChildReaderInformation
        {
            get { return _FrmChildReaderInformation; }
        }

        private Func<bool, frmBookISBNInformation> _FrmBookISBNInformation;
        public Func<bool, frmBookISBNInformation> FrmBookISBNInformation
        {
            get { return _FrmBookISBNInformation; }
        }

        private Func<bool, frmAddUser> _FrmAddUser;
        public Func<bool, frmAddUser> FrmAddUser
        {
            get { return _FrmAddUser; }
        }
        
        private Func<bool, frmAddRole> _FrmAddRole;
        public Func<bool, frmAddRole> FrmAddRole
        {
            get { return _FrmAddRole; }
        }

        private Func<bool, frmAddUserRole> _FrmAddUserRole;
        public Func<bool, frmAddUserRole> FrmAddUserRole
        {
            get { return _FrmAddUserRole; }
        }

        private Func<bool, frmAddFunction> _FrmAddFunction;
        public Func<bool, frmAddFunction> FrmAddFunction
        {
            get { return _FrmAddFunction; }
        }

        private Func<bool, frmAddLoan> _FrmAddLoan;
        public Func<bool, frmAddLoan> FrmAddLoan
        {
            get { return _FrmAddLoan; }
        }
        #endregion

        #endregion

        public UnitOfForm(UnitOfRepository unitOfRepo)
        {
            _UnitOfRepo = unitOfRepo;

            #region UserControls
            _UcFunctionInformation = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucFunctionInformation = new ucFunctionInformation();
                }
                return ucFunctionInformation;
            };

            _UcReaderLoanInformation = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucReaderLoanInformation = new ucReaderLoanInformation();
                }
                return ucReaderLoanInformation;
            };

            _UcLoanSlipPayment = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucLoanSlipPayment = new ucLoanSlipPayment();
                }
                return ucLoanSlipPayment;
            };

            _UcDisplayFunction = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucDisplayFunction = new ucDisplayFunction();
                }
                return ucDisplayFunction;
            };

            _UcLoanSlipInformation = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucLoanSlipInformation = new ucLoanSlipInformation();
                }
                return ucLoanSlipInformation;
            };

            _UcLoanSlipInformationStyle2 = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucLoanSlipInformationStyle2 = new ucLoanSlipInformationStyle2();
                }
                return ucLoanSlipInformationStyle2;
            };

            _UcBookTitleManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucBookTitleManagement = new ucBookTitleManagement();
                    ucBookTitleManagement.getBookTitleRepo = () => _UnitOfRepo.BookTitleRepo;
                    ucBookTitleManagement.getBookISBNRepo = () => _UnitOfRepo.BookISBNRepo;
                    ucBookTitleManagement.getBookRepo = () => _UnitOfRepo.BookRepo;
                    ucBookTitleManagement.getAuthorRepo = () => _UnitOfRepo.AuthorRepo;
                    ucBookTitleManagement.getCategoryRepo = () => _UnitOfRepo.CategoryRepo;
                    ucBookTitleManagement.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return ucBookTitleManagement;
            };

            _UcLoanHistoryManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucLoanHistoryManagement = new ucLoanHistoryManagement();
                    ucLoanHistoryManagement.getLoanHistoryRepo = () => _UnitOfRepo.LoanHistoryRepo;
                    ucLoanHistoryManagement.getLoanSlipRepo = () => _UnitOfRepo.LoanSlipRepo;
                    ucLoanHistoryManagement.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return ucLoanHistoryManagement;
            };
            
            _UcBookManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucBookManagement = new ucBookManagement();
                    ucBookManagement.getBookTitleRepo = () => _UnitOfRepo.BookTitleRepo;
                    ucBookManagement.getBookISBNRepo = () => _UnitOfRepo.BookISBNRepo;
                    ucBookManagement.getBookRepo = () => _UnitOfRepo.BookRepo;
                    ucBookManagement.getAuthorRepo = () => _UnitOfRepo.AuthorRepo;
                    ucBookManagement.getCategoryRepo = () => _UnitOfRepo.CategoryRepo;
                    ucBookManagement.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return ucBookManagement;
            };

            _UcLoanSlipManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucLoanSlipManagement = new ucLoanSlipManagement();
                    ucLoanSlipManagement.getLoanSlipRepo = () => _UnitOfRepo.LoanSlipRepo;
                    ucLoanSlipManagement.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return ucLoanSlipManagement;
            };

            _UcBookISBNManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucBookISBNManagement = new ucBookISBNManagement();
                    ucBookISBNManagement.getBookTitleRepo = () => _UnitOfRepo.BookTitleRepo;
                    ucBookISBNManagement.getBookISBNRepo = () => _UnitOfRepo.BookISBNRepo;
                    ucBookISBNManagement.getBookRepo = () => _UnitOfRepo.BookRepo;
                    ucBookISBNManagement.getAuthorRepo = () => _UnitOfRepo.AuthorRepo;
                    ucBookISBNManagement.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return ucBookISBNManagement;
            };

            _UcReaderManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucReaderManagement = new ucReaderManagement();
                    ucReaderManagement.getReaderRepo = () => _UnitOfRepo.ReaderRepo;
                    ucReaderManagement.getAdultRepo = () => _UnitOfRepo.AdultRepo;
                    ucReaderManagement.getChildRepo = () => _UnitOfRepo.ChildRepo;
                    ucReaderManagement.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                    ucReaderManagement.getProvinceRepo = () => _UnitOfRepo.ProvinceRepo;
                }
                return ucReaderManagement;
            };

            _UcAddBookTitle = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucAddBookTitle = new ucAddBookTitle();
                    ucAddBookTitle.getBookTitleRepo = () => _UnitOfRepo.BookTitleRepo;
                    ucAddBookTitle.getCategoryRepo = () => _UnitOfRepo.CategoryRepo;
                    ucAddBookTitle.getAuthorRepo = () => _UnitOfRepo.AuthorRepo;
                }
                return ucAddBookTitle;
            };

            _UcAddBookISBN = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucAddBookISBN = new ucAddBookISBN();
                    ucAddBookISBN.getBookISBNRepo = () => _UnitOfRepo.BookISBNRepo;
                    ucAddBookISBN.getBookTitleRepo = () => _UnitOfRepo.BookTitleRepo;
                    ucAddBookISBN.getAuthorRepo = () => _UnitOfRepo.AuthorRepo;
                }
                return ucAddBookISBN;
            };

            _UcAddBook = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucAddBook = new ucAddBook();
                    ucAddBook.getBookISBNRepo = () => _UnitOfRepo.BookISBNRepo;
                    ucAddBook.getBookRepo = () => _UnitOfRepo.BookRepo;
                }
                return ucAddBook;
            };

            _UcAddAdult = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucAddAdult = new ucAddAdult();
                    ucAddAdult.getProvinceRepo = () => _UnitOfRepo.ProvinceRepo;
                }
                return ucAddAdult;
            };

            _UcAddChild = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucAddChild = new ucAddChild();
                    ucAddChild.getReaderRepo = () => _UnitOfRepo.ReaderRepo;
                    ucAddChild.getAdultRepo = () => _UnitOfRepo.AdultRepo;
                    ucAddChild.getChildRepo = () => _UnitOfRepo.ChildRepo;
                    ucAddChild.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return ucAddChild;
            };

            _UcUserInformation = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    ucUserInformation = new ucUserInformation();
                }
                return ucUserInformation;
            };

            _UcBooksTable = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    ucBooksTable = new ucBooksTable();
                }
                return ucBooksTable;
            };

            _UcUserManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucUserManagement = new ucUserManagement();
                    ucUserManagement.getUserRepo = () => _UnitOfRepo.UserRepo;
                    ucUserManagement.getUserRoleRepo = () => _UnitOfRepo.UserRoleRepo;
                    ucUserManagement.getUserInfoRepo = () => _UnitOfRepo.UserInfoRepo;
                    ucUserManagement.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return ucUserManagement;
            };

            _UcFunctionManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucFunctionManagement = new ucFunctionManagement();
                    ucFunctionManagement.getFunctionRepo = () => MainWindow.UnitOfRepo.FunctionRepo;
                    ucFunctionManagement.getRoleFunctionRepo = () => MainWindow.UnitOfRepo.RoleFunctionRepo;
                    ucFunctionManagement.getParameterRepo = () => MainWindow.UnitOfRepo.ParameterRepo;
                }
                return ucFunctionManagement;
            };

            _UcRoleManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucRoleManagement = new ucRoleManagement();

                    ucRoleManagement.getRoleRepo = () => MainWindow.UnitOfRepo.RoleRepo;
                    ucRoleManagement.getRoleFunctionRepo = () => MainWindow.UnitOfRepo.RoleFunctionRepo;
                    ucRoleManagement.getParameterRepo = () => MainWindow.UnitOfRepo.ParameterRepo;
                }
                return ucRoleManagement;
            };

            _UcUserRoleManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucUserRoleManagement = new ucUserRoleManagement();
                    ucUserRoleManagement.getRoleRepo = () => MainWindow.UnitOfRepo.RoleRepo;
                    ucUserRoleManagement.getUserRepo = () => MainWindow.UnitOfRepo.UserRepo;

                    ucUserRoleManagement.getUserRoleRepo = () => MainWindow.UnitOfRepo.UserRoleRepo;
                    ucUserRoleManagement.getUserInfoRepo = () => MainWindow.UnitOfRepo.UserInfoRepo;
                    ucUserRoleManagement.getParameterRepo = () => MainWindow.UnitOfRepo.ParameterRepo;
                }
                return ucUserRoleManagement;
            };

            _UcRoleFunctionManagement = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucRoleFunctionManagement = new ucRoleFunctionManagement();

                    ucRoleFunctionManagement.getRoleFunctionRepo = () => MainWindow.UnitOfRepo.RoleFunctionRepo;
                    ucRoleFunctionManagement.getRoleRepo = () => MainWindow.UnitOfRepo.RoleRepo;
                    ucRoleFunctionManagement.getUserRepo = () => MainWindow.UnitOfRepo.UserRepo;
                    ucRoleFunctionManagement.getUserInfoRepo = () => MainWindow.UnitOfRepo.UserInfoRepo;
                    ucRoleFunctionManagement.getUserRoleRepo = () => MainWindow.UnitOfRepo.UserRoleRepo;
                    ucRoleFunctionManagement.getFunctionRepo = () => MainWindow.UnitOfRepo.FunctionRepo;
                    ucRoleFunctionManagement.getParameterRepo = () => MainWindow.UnitOfRepo.ParameterRepo;

                }
                return ucRoleFunctionManagement;
            };

            _UcLibrarianDashBoard = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    ucLibrarianDashBoard = new ucLibrarianDashBoard();

                    ucLibrarianDashBoard.getReaderRepo = () => MainWindow.UnitOfRepo.ReaderRepo;
                    ucLibrarianDashBoard.getAdultRepo = () => MainWindow.UnitOfRepo.AdultRepo;
                    ucLibrarianDashBoard.getChildRepo = () => MainWindow.UnitOfRepo.ChildRepo;
                    ucLibrarianDashBoard.getParameterRepo = () => MainWindow.UnitOfRepo.ParameterRepo;

                    ucLibrarianDashBoard.getBookTitleRepo = () => MainWindow.UnitOfRepo.BookTitleRepo;
                    ucLibrarianDashBoard.getBookISBNRepo = () => MainWindow.UnitOfRepo.BookISBNRepo;
                    ucLibrarianDashBoard.getBookRepo = () => MainWindow.UnitOfRepo.BookRepo;

                    ucLibrarianDashBoard.getAuthorRepo = () => MainWindow.UnitOfRepo.AuthorRepo;
                    ucLibrarianDashBoard.getCategoryRepo = () => MainWindow.UnitOfRepo.CategoryRepo;
                    ucLibrarianDashBoard.getProvinceRepo = () => MainWindow.UnitOfRepo.ProvinceRepo;
                    ucLibrarianDashBoard.getUserRepo = () => MainWindow.UnitOfRepo.UserRepo;
                    ucLibrarianDashBoard.getUserRoleRepo = () => MainWindow.UnitOfRepo.UserRoleRepo;
                    ucLibrarianDashBoard.getUserInfoRepo = () => MainWindow.UnitOfRepo.UserInfoRepo;
                    ucLibrarianDashBoard.getRoleRepo = () => MainWindow.UnitOfRepo.RoleRepo;
                    ucLibrarianDashBoard.getRoleGroups = () => MainWindow.RoleGroups;
                    ucLibrarianDashBoard.getRoleFunctionRepo = () => MainWindow.UnitOfRepo.RoleFunctionRepo;
                    ucLibrarianDashBoard.getFunctionRepo = () => MainWindow.UnitOfRepo.FunctionRepo;

                }
                return ucLibrarianDashBoard;
            };
            #endregion

            #region Forms
            _FrmTransferGuardian = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmTransferGuardian = new frmTransferGuardian();
                    frmTransferGuardian.getChildRepo = () => _UnitOfRepo.ChildRepo;
                }
                return frmTransferGuardian;
            };

            _FrmAddLoanHistory = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmAddLoanHistory = new frmAddLoanHistory();
                    frmAddLoanHistory.getLoanSlipRepo = () => _UnitOfRepo.LoanSlipRepo;
                    frmAddLoanHistory.getLoanDetailRepo = () => _UnitOfRepo.LoanDetailRepo;
                    frmAddLoanHistory.getLoanHistoryRepo = () => _UnitOfRepo.LoanHistoryRepo;
                    frmAddLoanHistory.getLoanDetailHistoryRepo = () => _UnitOfRepo.LoanDetailHistoryRepo;
                    frmAddLoanHistory.getBookRepo = () => _UnitOfRepo.BookRepo;
                    frmAddLoanHistory.getBookISBNRepo = () => _UnitOfRepo.BookISBNRepo;
                    frmAddLoanHistory.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return frmAddLoanHistory;
            };

            _FrmChildFunctionInformation = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmChildFunctionInformation = new frmChildFunctionInformation();
                }
                return frmChildFunctionInformation;
            };

            _FrmInputServerName = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmInputServerName = new frmInputServerName();
                    frmInputServerName.getServerNameRepo = () => _UnitOfRepo.ServerNameRepo;
                    frmInputServerName.getDatabaseNameRepo = () => _UnitOfRepo.DatabaseNameRepo;
                }
                return frmInputServerName;
            };

            _FrmLogin = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmLogin = new frmLogin();
                    frmLogin.getUserRepo = () => _UnitOfRepo.UserRepo;
                }
                return frmLogin;
            };

            _FrmAddReader = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmAddReader = new frmAddReader();
                    frmAddReader.getReaderRepo = () => _UnitOfRepo.ReaderRepo;
                    frmAddReader.getProvinceRepo = () => _UnitOfRepo.ProvinceRepo;
                    frmAddReader.getAdultRepo = () => _UnitOfRepo.AdultRepo;
                    frmAddReader.getChildRepo = () => _UnitOfRepo.ChildRepo;
                    frmAddReader.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return frmAddReader;
            };
            
            _FrmAdultReaderInformation = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    frmAdultReaderInformation = new frmAdultReaderInformation();
                    frmAdultReaderInformation.getAdultRepo = () => _UnitOfRepo.AdultRepo;
                    frmAdultReaderInformation.getReaderRepo = () => _UnitOfRepo.ReaderRepo;
                }
                return frmAdultReaderInformation;
            };

            _FrmChildReaderInformation = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    frmChildReaderInformation = new frmChildReaderInformation();
                    frmChildReaderInformation.getReaderRepo = () => _UnitOfRepo.ReaderRepo;
                    frmChildReaderInformation.getChildRepo = () => _UnitOfRepo.ChildRepo;
                }
                return frmChildReaderInformation;
            };

            _FrmBookISBNInformation = (isReAllocate) =>
            {
                if (isReAllocate)
                {
                    frmBookISBNInformation = new frmBookISBNInformation();
                }
                return frmBookISBNInformation;
            };

            _FrmAddUser = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmAddUser = new frmAddUser();
                    frmAddUser.getUserRepo = () => _UnitOfRepo.UserRepo;
                    frmAddUser.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return frmAddUser;
            };

            
            _FrmAddRole = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmAddRole = new frmAddRole();
                    frmAddRole.getRoleRepo = () => _UnitOfRepo.RoleRepo;
                    frmAddRole.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                    frmAddRole.getRoleGroups = () => MainWindow.RoleGroups;
                }
                return frmAddRole;
            };

            _FrmAddUserRole = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmAddUserRole = new frmAddUserRole();
                    frmAddUserRole.getUserRepo = () => _UnitOfRepo.UserRepo;
                    frmAddUserRole.getRoleRepo = () => _UnitOfRepo.RoleRepo;
                    frmAddUserRole.getUserRoleRepo = () => _UnitOfRepo.UserRoleRepo;
                    frmAddUserRole.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return frmAddUserRole;
            };


            _FrmAddFunction = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmAddFunction = new frmAddFunction();
                    frmAddFunction.getFunctionRepo = () => _UnitOfRepo.FunctionRepo;
                    frmAddFunction.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                }
                return frmAddFunction;
            };

            _FrmAddLoan = (isReAllocate) =>
            {
                if (isReAllocate == true)
                {
                    frmAddLoan = new frmAddLoan();

                    frmAddLoan.getReaderRepo = () => _UnitOfRepo.ReaderRepo;
                    frmAddLoan.getAdultRepo = () => _UnitOfRepo.AdultRepo;
                    frmAddLoan.getChildRepo = () => _UnitOfRepo.ChildRepo;
                    frmAddLoan.getProvinceRepo = () => _UnitOfRepo.ProvinceRepo;
                    frmAddLoan.getParameterRepo = () => _UnitOfRepo.ParameterRepo;
                    frmAddLoan.getLoanDetailRepo = () => _UnitOfRepo.LoanDetailRepo;
                    frmAddLoan.getLoanSlipRepo = () => _UnitOfRepo.LoanSlipRepo;
                    frmAddLoan.getBookTitleRepo = () => _UnitOfRepo.BookTitleRepo;
                    frmAddLoan.getBookISBNRepo = () => _UnitOfRepo.BookISBNRepo;
                    frmAddLoan.getBookRepo = () => _UnitOfRepo.BookRepo;
                }
                return frmAddLoan;
            };

            #endregion
        }

        public void Load()
        {
            // UserControls
            ucFunctionInformation = UcFunctionInformation(true);
            ucReaderLoanInformation = UcReaderLoanInformation(true);
            ucLoanSlipPayment = UcLoanSlipPayment(true);
            ucDisplayFunction = UcDisplayFunction(true);
            ucLoanSlipInformation = UcLoanSlipInformation(true);
            ucLoanSlipInformationStyle2 = UcLoanSlipInformationStyle2(true);
            ucAddBookTitle = UcAddBookTitle(true);
            ucAddBookISBN = UcAddBookISBN(true);
            ucAddBook = UcAddBook(true);
            ucAddAdult = UcAddAdult(true);
            ucAddChild = UcAddChild(true);
            ucUserInformation = UcUserInformation(true);
            ucBooksTable = UcBooksTable(true);
            ucFunctionManagement = UcFunctionManagement(true);
            ucUserManagement = UcUserManagement(true);
            ucRoleManagement = UcRoleManagement(true);
            ucUserRoleManagement = UcUserRoleManagement(true);
            ucRoleFunctionManagement = UcRoleFunctionManagement(true);
            ucBookTitleManagement = UcBookTitleManagement(true);
            ucLoanHistoryManagement = UcLoanHistoryManagement(true);
            ucBookManagement = UcBookManagement(true);
            ucBookISBNManagement = UcBookISBNManagement(true);
            ucLoanSlipManagement = UcLoanSlipManagement(true);
            ucReaderManagement = UcReaderManagement(true);
            ucLibrarianDashBoard = UcLibrarianDashBoard(true);

            // Forms
            frmChildFunctionInformation = FrmChildFunctionInformation(true);
            frmTransferGuardian = FrmTransferGuardian(true);
            frmAddLoanHistory = FrmAddLoanHistory(true);
            frmLogin = FrmLogin(true);
            frmAddReader = FrmAddReader(true);
            frmAdultReaderInformation = FrmAdultReaderInformation(true);
            frmChildReaderInformation = FrmChildReaderInformation(true);
            frmBookISBNInformation = FrmBookISBNInformation(true);
            frmAddUser = FrmAddUser(true);
            frmAddRole = FrmAddRole(true);
            frmAddUserRole = FrmAddUserRole(true);
            frmAddFunction = FrmAddFunction(true);
            frmAddLoan = FrmAddLoan(true);
        }

        public void LoadServerNameForm()
        {
            frmInputServerName = FrmInputServerName(true);
        }
    }
}
