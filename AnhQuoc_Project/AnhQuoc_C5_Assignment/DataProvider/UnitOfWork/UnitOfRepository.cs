using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UnitOfRepository
    {
        private LoanDetailRepository _LoanDetailRepo;
        public LoanDetailRepository LoanDetailRepo
        {
            get
            {
                if (_LoanDetailRepo == null)
                    _LoanDetailRepo = new LoanDetailRepository(new APIProvider<LoanDetail>(nameof(LoanDetail)));
                return _LoanDetailRepo;
            }
        }
    
        private StatisticalRepository _StatisticalRepo;
        public StatisticalRepository StatisticalRepo
        {
            get
            {
                if (_StatisticalRepo == null)
                    _StatisticalRepo = new StatisticalRepository(new APIProvider<Statistical>(nameof(Statistical)));
                return _StatisticalRepo;
            }
        }

        private BookStatusRepository _BookStatusRepo;
        public BookStatusRepository BookStatusRepo
        {
            get
            {
                if (_BookStatusRepo == null)
                    _BookStatusRepo = new BookStatusRepository(new APIProvider<BookStatu>("BookStatus"));
                return _BookStatusRepo;
            }
        }

        private PenaltyReasonRepository _PenaltyReasonRepo;
        public PenaltyReasonRepository PenaltyReasonRepo
        {
            get
            {
                if (_PenaltyReasonRepo == null)
                    _PenaltyReasonRepo = new PenaltyReasonRepository(new APIProvider<PenaltyReason>(nameof(PenaltyReason)));
                return _PenaltyReasonRepo;
            }
        }

        private LoanHistoryRepository _LoanHistoryRepo;
        public LoanHistoryRepository LoanHistoryRepo
        {
            get
            {
                if (_LoanHistoryRepo == null)
                    _LoanHistoryRepo = new LoanHistoryRepository(new APIProvider<LoanHistory>(nameof(LoanHistory)));
                return _LoanHistoryRepo;
            }
        }

        private LoanDetailHistoryRepository _LoanDetailHistoryRepo;
        public LoanDetailHistoryRepository LoanDetailHistoryRepo
        {
            get
            {
                if (_LoanDetailHistoryRepo == null)
                    _LoanDetailHistoryRepo = new LoanDetailHistoryRepository(new APIProvider<LoanDetailHistory>(nameof(LoanDetailHistory)));
                return _LoanDetailHistoryRepo;
            }
        }

        private PublisherRepository _PublisherRepo;
        public PublisherRepository PublisherRepo
        {
            get
            {
                if (_PublisherRepo == null)
                    _PublisherRepo = new PublisherRepository(new APIProvider<Publisher>(nameof(Publisher)));
                return _PublisherRepo;
            }
        }

        private LoanSlipRepository _LoanSlipRepo;
        public LoanSlipRepository LoanSlipRepo
        {
            get
            {
                if (_LoanSlipRepo == null)
                    _LoanSlipRepo = new LoanSlipRepository(new APIProvider<LoanSlip>(nameof(LoanSlip)));
                return _LoanSlipRepo;
            }
        }

        private ReaderRepository _ReaderRepo;
        public ReaderRepository ReaderRepo
        {
            get
            {
                if (_ReaderRepo == null)
                    _ReaderRepo = new ReaderRepository(new APIProvider<Reader>(nameof(Reader)));
                return _ReaderRepo;
            }
        }

        private AdultRepository _AdultRepo;
        public AdultRepository AdultRepo
        {
            get
            {
                if (_AdultRepo == null)
                    _AdultRepo = new AdultRepository(new APIProvider<Adult>(nameof(Adult)));
                return _AdultRepo;
            }
        }
        
        private ChildRepository _ChildRepo;
        public ChildRepository ChildRepo
        {
            get
            {
                if (_ChildRepo == null)
                    _ChildRepo = new ChildRepository(new APIProvider<Child>(nameof(Child)));
                return _ChildRepo;
            }
        }

        private ParameterRepository _ParameterRepo;
        public ParameterRepository ParameterRepo
        {
            get
            {
                if (_ParameterRepo == null)
                    _ParameterRepo = new ParameterRepository(new APIProvider<Parameter>(nameof(Parameter)));
                return _ParameterRepo;
            }
        }

        private BookTitleRepository _BookTitleRepo;
        public BookTitleRepository BookTitleRepo
        {
            get
            {
                if (_BookTitleRepo == null)
                    _BookTitleRepo = new BookTitleRepository(new APIProvider<BookTitle>(nameof(BookTitle)));
                return _BookTitleRepo;
            }
        }

        private BookISBNRepository _BookISBNRepo;
        public BookISBNRepository BookISBNRepo
        {
            get
            {
                if (_BookISBNRepo == null)
                    _BookISBNRepo = new BookISBNRepository(new APIProvider<BookISBN>(nameof(BookISBN)));
                return _BookISBNRepo;
            }
        }

        private BookRepository _BookRepo;
        public BookRepository BookRepo
        {
            get
            {
                if (_BookRepo == null)
                    _BookRepo = new BookRepository(new APIProvider<Book>(nameof(Book)));
                return _BookRepo;
            }
        }

        private AuthorRepository _AuthorRepo;
        public AuthorRepository AuthorRepo
        {
            get
            {
                if (_AuthorRepo == null)
                    _AuthorRepo = new AuthorRepository(new APIProvider<Author>(nameof(Author)));
                return _AuthorRepo;
            }
        }

        private CategoryRepository _CategoryRepo;
        public CategoryRepository CategoryRepo
        {
            get
            {
                if (_CategoryRepo == null)
                {
                    _CategoryRepo = new CategoryRepository(new APIProvider<Category>(nameof(Category)));
                }
                return _CategoryRepo;
            }
        }

        private ProvinceRepository _ProvinceRepo;
        public ProvinceRepository ProvinceRepo
        {
            get
            {
                if (_ProvinceRepo == null)
                {
                    _ProvinceRepo = new ProvinceRepository(new APIProvider<Province>(nameof(Province)));
                }
                return _ProvinceRepo;
            }
        }

        private TranslatorRepository _TranslatorRepo;
        public TranslatorRepository TranslatorRepo
        {
            get
            {
                if (_TranslatorRepo == null)
                {
                    _TranslatorRepo = new TranslatorRepository(new APIProvider<Translator>(nameof(Translator)));
                }
                return _TranslatorRepo;
            }
        }

        private UserRepository _UserRepo;
        public UserRepository UserRepo
        {
            get
            {
                if (_UserRepo == null)
                {
                    _UserRepo = new UserRepository(new APIProvider<User>(nameof(User)));
                }
                return _UserRepo;
            }
        }

        private UserInfoRepository _UserInfoRepo;
        public UserInfoRepository UserInfoRepo
        {
            get
            {
                if (_UserInfoRepo == null)
                {
                    _UserInfoRepo = new UserInfoRepository(new APIProvider<UserInfo>(nameof(UserInfo)));
                }
                return _UserInfoRepo;
            }
        }

        private RoleRepository _RoleRepo;
        public RoleRepository RoleRepo
        {
            get
            {
                if (_RoleRepo == null)
                {
                    _RoleRepo = new RoleRepository(new APIProvider<Role>(nameof(Role)));
                }
                return _RoleRepo;
            }
        }

        private UserRoleRepository _UserRoleRepo;
        public UserRoleRepository UserRoleRepo
        {
            get
            {
                if (_UserRoleRepo == null)
                {
                    _UserRoleRepo = new UserRoleRepository(new APIProvider<UserRole>(nameof(UserRole)));
                }
                return _UserRoleRepo;
            }
        }

        private FunctionRepository _FunctionRepo;
        public FunctionRepository FunctionRepo
        {
            get
            {
                if (_FunctionRepo == null)
                {
                    _FunctionRepo = new FunctionRepository(new APIProvider<Function>(nameof(Function)));
                }
                return _FunctionRepo;
            }
        }

        private RoleFunctionRepository _RoleFunctionRepo;
        public RoleFunctionRepository RoleFunctionRepo
        {
            get
            {
                if (_RoleFunctionRepo == null)
                {
                    _RoleFunctionRepo = new RoleFunctionRepository(new APIProvider<RoleFunction>(nameof(RoleFunction)));
                }
                return _RoleFunctionRepo;
            }
        }

        // Others
        private ServerNameRepository _ServerNameRepo;
        public ServerNameRepository ServerNameRepo
        {
            get
            {
                if (_ServerNameRepo == null)
                    _ServerNameRepo = new ServerNameRepository(new APIProvider<ServerName>(nameof(ServerName)));
                return _ServerNameRepo;
            }
        }

        private DatabaseNameRepository _DatabaseNameRepo;
        public DatabaseNameRepository DatabaseNameRepo
        {
            get
            {
                if (_DatabaseNameRepo == null)
                    _DatabaseNameRepo = new DatabaseNameRepository(new APIProvider<DatabaseName>(nameof(DatabaseName)));
                return _DatabaseNameRepo;
            }
        }

        public UnitOfRepository()
        {
        }

        public void LoadServerNameRepo()
        {
            _ServerNameRepo = new ServerNameRepository(new APIProvider<ServerName>(nameof(ServerName)));
            _ServerNameRepo.LoadList();

            _DatabaseNameRepo = new DatabaseNameRepository(new APIProvider<DatabaseName>(nameof(DatabaseName)));
            _DatabaseNameRepo.LoadList();
        }

        public void Load()
        {
            AdultRepo.LoadList();
            AuthorRepo.LoadList();
            LoanDetailRepo.LoadList();
            StatisticalRepo.LoadList();
            BookStatusRepo.LoadList();
            PenaltyReasonRepo.LoadList();
            LoanHistoryRepo.LoadList();
            LoanDetailHistoryRepo.LoadList();
            PublisherRepo.LoadList();
            LoanSlipRepo.LoadList();
            ReaderRepo.LoadList();
            ChildRepo.LoadList();
            ParameterRepo.LoadList();
            BookTitleRepo.LoadList();
            BookISBNRepo.LoadList();
            BookRepo.LoadList();
            CategoryRepo.LoadList();
            ProvinceRepo.LoadList();
            TranslatorRepo.LoadList();
            UserRepo.LoadList();
            UserInfoRepo.LoadList();
            RoleRepo.LoadList();
            UserRoleRepo.LoadList();
            FunctionRepo.LoadList();
            RoleFunctionRepo.LoadList();
        }
    }
}
