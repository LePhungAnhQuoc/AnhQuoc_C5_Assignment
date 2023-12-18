using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UnitOfViewModel
    {
        #region SingleTonPattern
        private static readonly object _InstanceLock = new object();

        private static UnitOfViewModel _Instance;
        public static UnitOfViewModel Instance
        {
            get
            {
                lock (_InstanceLock)
                {
                    if (_Instance == null)
                        _Instance = new UnitOfViewModel();
                }
                return _Instance;
            }
        }
        #endregion

        #region Properties
        public UnitOfRepository _UnitOfRepo { get; set; }


        private DatabaseNameViewModel _DatabaseNameViewModel;
        public DatabaseNameViewModel DatabaseNameViewModel
        {
            get
            {
                if (_DatabaseNameViewModel == null)
                    _DatabaseNameViewModel = new DatabaseNameViewModel();
                return _DatabaseNameViewModel;
            }
        }

        private ServerNameViewModel _ServerNameViewModel;
        public ServerNameViewModel ServerNameViewModel
        {
            get
            {
                if (_ServerNameViewModel == null)
                    _ServerNameViewModel = new ServerNameViewModel();
                return _ServerNameViewModel;
            }
        }

        private LoanDetailHistoryViewModel _LoanDetailHistoryViewModel;
        public LoanDetailHistoryViewModel LoanDetailHistoryViewModel
        {
            get
            {
                if (_LoanDetailHistoryViewModel == null)
                    _LoanDetailHistoryViewModel = new LoanDetailHistoryViewModel();
                return _LoanDetailHistoryViewModel;
            }
        }

        private LoanHistoryViewModel _LoanHistoryViewModel;
        public LoanHistoryViewModel LoanHistoryViewModel
        {
            get
            {
                if (_LoanHistoryViewModel == null)
                    _LoanHistoryViewModel = new LoanHistoryViewModel();
                return _LoanHistoryViewModel;
            }
        }

        private PublisherViewModel _PublisherViewModel;
        public PublisherViewModel PublisherViewModel
        {
            get
            {
                if (_PublisherViewModel == null)
                    _PublisherViewModel = new PublisherViewModel();
                return _PublisherViewModel;
            }
        }
        
        private LoanSlipViewModel _LoanSlipViewModel;
        public LoanSlipViewModel LoanSlipViewModel
        {
            get
            {
                if (_LoanSlipViewModel == null)
                    _LoanSlipViewModel = new LoanSlipViewModel();
                return _LoanSlipViewModel;
            }
        }

        private LoanDetailViewModel _LoanDetailViewModel;
        public LoanDetailViewModel LoanDetailViewModel
        {
            get
            {
                if (_LoanDetailViewModel == null)
                    _LoanDetailViewModel = new LoanDetailViewModel();
                return _LoanDetailViewModel;
            }
        }

        private BookTitleViewModel _BookTitleViewModel;
        public BookTitleViewModel BookTitleViewModel
        {
            get
            {
                if (_BookTitleViewModel == null)
                    _BookTitleViewModel = new BookTitleViewModel();
                return _BookTitleViewModel;
            }
        }

        private BookISBNViewModel _BookISBNViewModel;
        public BookISBNViewModel BookISBNViewModel
        {
            get
            {
                if (_BookISBNViewModel == null)
                    _BookISBNViewModel = new BookISBNViewModel();
                return _BookISBNViewModel;
            }
        }

        private BookViewModel _BookViewModel;
        public BookViewModel BookViewModel
        {
            get
            {
                if (_BookViewModel == null)
                    _BookViewModel = new BookViewModel();
                return _BookViewModel;
            }
        }

        private ProvinceViewModel _ProvinceViewModel;
        public ProvinceViewModel ProvinceViewModel
        {
            get
            {
                if (_ProvinceViewModel == null)
                    _ProvinceViewModel = new ProvinceViewModel();
                return _ProvinceViewModel;
            }
        }

        private AuthorViewModel _AuthorViewModel;
        public AuthorViewModel AuthorViewModel
        {
            get
            {
                if (_AuthorViewModel == null)
                    _AuthorViewModel = new AuthorViewModel();
                return _AuthorViewModel;
            }
        }

        private ReaderViewModel _ReaderViewModel;
        public ReaderViewModel ReaderViewModel
        {
            get
            {
                if (_ReaderViewModel == null)
                    _ReaderViewModel = new ReaderViewModel();
                return _ReaderViewModel;
            }
        }

        private UserViewModel _UserViewModel;
        public UserViewModel UserViewModel
        {
            get
            {
                if (_UserViewModel == null)
                    _UserViewModel = new UserViewModel();
                return _UserViewModel;
            }
        }

        private UserInfoViewModel _UserInfoViewModel;
        public UserInfoViewModel UserInfoViewModel
        {
            get
            {
                if (_UserInfoViewModel == null)
                    _UserInfoViewModel = new UserInfoViewModel();
                return _UserInfoViewModel;
            }
        }

        private FunctionViewModel _FunctionViewModel;
        public FunctionViewModel FunctionViewModel
        {
            get
            {
                if (_FunctionViewModel == null)
                    _FunctionViewModel = new FunctionViewModel();
                return _FunctionViewModel;
            }
        }

        private RoleViewModel _RoleViewModel;
        public RoleViewModel RoleViewModel
        {
            get
            {
                if (_RoleViewModel == null)
                    _RoleViewModel = new RoleViewModel();
                return _RoleViewModel;
            }
        }

        private UserRoleViewModel _UserRoleViewModel;
        public UserRoleViewModel UserRoleViewModel
        {
            get
            {
                if (_UserRoleViewModel == null)
                    _UserRoleViewModel = new UserRoleViewModel();
                return _UserRoleViewModel;
            }
        }
        
        private RoleFunctionViewModel _RoleFunctionViewModel;
        public RoleFunctionViewModel RoleFunctionViewModel
        {
            get
            {
                if (_RoleFunctionViewModel == null)
                    _RoleFunctionViewModel = new RoleFunctionViewModel();
                return _RoleFunctionViewModel;
            }
        }

        private ChildViewModel _ChildViewModel;
        public ChildViewModel ChildViewModel
        {
            get
            {
                if (_ChildViewModel == null)
                    _ChildViewModel = new ChildViewModel();
                return _ChildViewModel;
            }
        }

        private CategoryViewModel _CategoryViewModel;
        public CategoryViewModel CategoryViewModel
        {
            get
            {
                if (_CategoryViewModel == null)
                    _CategoryViewModel = new CategoryViewModel();
                return _CategoryViewModel;
            }
        }

        private AdultViewModel _AdultViewModel;
        public AdultViewModel AdultViewModel
        {
            get
            {
                if (_AdultViewModel == null)
                    _AdultViewModel = new AdultViewModel();
                return _AdultViewModel;
            }
        }

        private ParameterViewModel _ParameterViewModel;
        public ParameterViewModel ParameterViewModel
        {
            get
            {
                if (_ParameterViewModel == null)
                    _ParameterViewModel = new ParameterViewModel();
                return _ParameterViewModel;
            }
        }

        #endregion

        private UnitOfViewModel() { }

        public void Load()
        {
            _LoanDetailHistoryViewModel = new LoanDetailHistoryViewModel();
            _LoanDetailHistoryViewModel.Repo = _UnitOfRepo.LoanDetailHistoryRepo;

            _LoanHistoryViewModel = new LoanHistoryViewModel();
            _LoanHistoryViewModel.Repo = _UnitOfRepo.LoanHistoryRepo;

            _PublisherViewModel = new PublisherViewModel();
            _PublisherViewModel.Repo = _UnitOfRepo.PublisherRepo;
            
            _LoanSlipViewModel = new LoanSlipViewModel();
            _LoanSlipViewModel.Repo = _UnitOfRepo.LoanSlipRepo;

            _LoanDetailViewModel = new LoanDetailViewModel();
            _LoanDetailViewModel.Repo = _UnitOfRepo.LoanDetailRepo;

            _BookISBNViewModel = new BookISBNViewModel();
            _BookISBNViewModel.Repo = _UnitOfRepo.BookISBNRepo;
           
            _BookTitleViewModel = new BookTitleViewModel();
            _BookTitleViewModel.Repo = _UnitOfRepo.BookTitleRepo;
        
            _BookViewModel = new BookViewModel();
            _BookViewModel.Repo = _UnitOfRepo.BookRepo;
           
            _ProvinceViewModel = new ProvinceViewModel();
            _ProvinceViewModel.Repo = _UnitOfRepo.ProvinceRepo;

            _AuthorViewModel = new AuthorViewModel();
            _AuthorViewModel.Repo = _UnitOfRepo.AuthorRepo;

            _ReaderViewModel = new ReaderViewModel();
            _ReaderViewModel.Repo = _UnitOfRepo.ReaderRepo;

            _UserViewModel = new UserViewModel();
            _UserViewModel.Repo = _UnitOfRepo.UserRepo;

            _UserInfoViewModel = new UserInfoViewModel();
            _UserInfoViewModel.Repo = _UnitOfRepo.UserInfoRepo;

            _FunctionViewModel = new FunctionViewModel();
            _FunctionViewModel.Repo = _UnitOfRepo.FunctionRepo;

            _RoleViewModel = new RoleViewModel();
            _RoleViewModel.Repo = _UnitOfRepo.RoleRepo;

            _UserRoleViewModel = new UserRoleViewModel();
            _UserRoleViewModel.Repo = _UnitOfRepo.UserRoleRepo;

            _RoleFunctionViewModel = new RoleFunctionViewModel();
            _RoleFunctionViewModel.Repo = _UnitOfRepo.RoleFunctionRepo;

            _ChildViewModel = new ChildViewModel();
            _ChildViewModel.Repo = _UnitOfRepo.ChildRepo;

            _CategoryViewModel = new CategoryViewModel();
            _CategoryViewModel.Repo = _UnitOfRepo.CategoryRepo;

            _AdultViewModel = new AdultViewModel();
            _AdultViewModel.Repo = _UnitOfRepo.AdultRepo;

            _ParameterViewModel = new ParameterViewModel();
            _ParameterViewModel.Repo = _UnitOfRepo.ParameterRepo;
        }

        public void LoadServerName()
        {
            _DatabaseNameViewModel = new DatabaseNameViewModel();
            _DatabaseNameViewModel.Repo = _UnitOfRepo.DatabaseNameRepo;

            _ServerNameViewModel = new ServerNameViewModel();
            _ServerNameViewModel.Repo = _UnitOfRepo.ServerNameRepo;

        }
    }
}
