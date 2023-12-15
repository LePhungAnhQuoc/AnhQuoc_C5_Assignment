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
                    _LoanDetailRepo = new LoanDetailRepository();
                return _LoanDetailRepo;
            }
        }

        private LoanHistoryRepository _LoanHistoryRepo;
        public LoanHistoryRepository LoanHistoryRepo
        {
            get
            {
                if (_LoanHistoryRepo == null)
                    _LoanHistoryRepo = new LoanHistoryRepository();
                return _LoanHistoryRepo;
            }
        }

        private LoanDetailHistoryRepository _LoanDetailHistoryRepo;
        public LoanDetailHistoryRepository LoanDetailHistoryRepo
        {
            get
            {
                if (_LoanDetailHistoryRepo == null)
                    _LoanDetailHistoryRepo = new LoanDetailHistoryRepository();
                return _LoanDetailHistoryRepo;
            }
        }

        private PublisherRepository _PublisherRepo;
        public PublisherRepository PublisherRepo
        {
            get
            {
                if (_PublisherRepo == null)
                    _PublisherRepo = new PublisherRepository();
                return _PublisherRepo;
            }
        }

        private LoanSlipRepository _LoanSlipRepo;
        public LoanSlipRepository LoanSlipRepo
        {
            get
            {
                if (_LoanSlipRepo == null)
                    _LoanSlipRepo = new LoanSlipRepository();
                return _LoanSlipRepo;
            }
        }

        private ReaderRepository _ReaderRepo;
        public ReaderRepository ReaderRepo
        {
            get
            {
                if (_ReaderRepo == null)
                    _ReaderRepo = new ReaderRepository();
                return _ReaderRepo;
            }
        }

        private AdultRepository _AdultRepo;
        public AdultRepository AdultRepo
        {
            get
            {
                if (_AdultRepo == null)
                    _AdultRepo = new AdultRepository();
                return _AdultRepo;
            }
        }

        private EnrollRepository _EnrollRepo;
        public EnrollRepository EnrollRepo
        {
            get
            {
                if (_EnrollRepo == null)
                    _EnrollRepo = new EnrollRepository();
                return _EnrollRepo;
            }
        }

        private ChildRepository _ChildRepo;
        public ChildRepository ChildRepo
        {
            get
            {
                if (_ChildRepo == null)
                    _ChildRepo = new ChildRepository();
                return _ChildRepo;
            }
        }

        private ParameterRepository _ParameterRepo;
        public ParameterRepository ParameterRepo
        {
            get
            {
                if (_ParameterRepo == null)
                    _ParameterRepo = new ParameterRepository();
                return _ParameterRepo;
            }
        }

        private BookTitleRepository _BookTitleRepo;
        public BookTitleRepository BookTitleRepo
        {
            get
            {
                if (_BookTitleRepo == null)
                    _BookTitleRepo = new BookTitleRepository();
                return _BookTitleRepo;
            }
        }

        private BookISBNRepository _BookISBNRepo;
        public BookISBNRepository BookISBNRepo
        {
            get
            {
                if (_BookISBNRepo == null)
                    _BookISBNRepo = new BookISBNRepository();
                return _BookISBNRepo;
            }
        }

        private BookRepository _BookRepo;
        public BookRepository BookRepo
        {
            get
            {
                if (_BookRepo == null)
                    _BookRepo = new BookRepository();
                return _BookRepo;
            }
        }

        private AuthorRepository _AuthorRepo;
        public AuthorRepository AuthorRepo
        {
            get
            {
                if (_AuthorRepo == null)
                    _AuthorRepo = new AuthorRepository();
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
                    _CategoryRepo = new CategoryRepository();
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
                    _ProvinceRepo = new ProvinceRepository();
                }
                return _ProvinceRepo;
            }
        }

        private UserRepository _UserRepo;
        public UserRepository UserRepo
        {
            get
            {
                if (_UserRepo == null)
                {
                    _UserRepo = new UserRepository();
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
                    _UserInfoRepo = new UserInfoRepository();
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
                    _RoleRepo = new RoleRepository();
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
                    _UserRoleRepo = new UserRoleRepository();
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
                    _FunctionRepo = new FunctionRepository();
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
                    _RoleFunctionRepo = new RoleFunctionRepository();
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
                    _ServerNameRepo = new ServerNameRepository();
                return _ServerNameRepo;
            }
        }

        private DatabaseNameRepository _DatabaseNameRepo;
        public DatabaseNameRepository DatabaseNameRepo
        {
            get
            {
                if (_DatabaseNameRepo == null)
                    _DatabaseNameRepo = new DatabaseNameRepository();
                return _DatabaseNameRepo;
            }
        }

        public UnitOfRepository()
        {
        }

        public void LoadServerNameRepo()
        {
            _ServerNameRepo = new ServerNameRepository();
            _ServerNameRepo.LoadList();

            _DatabaseNameRepo = new DatabaseNameRepository();
            _DatabaseNameRepo.LoadList();
        }

        public void Load()
        {
            _AdultRepo = new AdultRepository();
            _AdultRepo.LoadList();
          
            _AuthorRepo = new AuthorRepository();
            _AuthorRepo.LoadList();

            _LoanDetailRepo = new LoanDetailRepository();
            _LoanDetailRepo.LoadList();

            _LoanHistoryRepo = new LoanHistoryRepository();
            _LoanHistoryRepo.LoadList();

            _LoanDetailHistoryRepo = new LoanDetailHistoryRepository();
            _LoanDetailHistoryRepo.LoadList();

            _PublisherRepo = new PublisherRepository();
            _PublisherRepo.LoadList();

            _LoanSlipRepo = new LoanSlipRepository();
            _LoanSlipRepo.LoadList();

            _ReaderRepo = new ReaderRepository();
            _ReaderRepo.LoadList();

            _ChildRepo = new ChildRepository();
            _ChildRepo.LoadList();

            _ParameterRepo = new ParameterRepository();
            _ParameterRepo.LoadList();

            _BookTitleRepo = new BookTitleRepository();
            _BookTitleRepo.LoadList();

            _BookISBNRepo = new BookISBNRepository();
            _BookISBNRepo.LoadList();

            _BookRepo = new BookRepository();
            _BookRepo.LoadList();

            _CategoryRepo = new CategoryRepository();
            _CategoryRepo.LoadList();

            _ProvinceRepo = new ProvinceRepository();
            _ProvinceRepo.LoadList();

            _UserRepo = new UserRepository();
            _UserRepo.LoadList();

            _UserInfoRepo = new UserInfoRepository();
            _UserInfoRepo.LoadList();

            _RoleRepo = new RoleRepository();
            _RoleRepo.LoadList();

            _UserRoleRepo = new UserRoleRepository();
            _UserRoleRepo.LoadList();

            _FunctionRepo = new FunctionRepository();
            _FunctionRepo.LoadList();

            _RoleFunctionRepo = new RoleFunctionRepository();
            _RoleFunctionRepo.LoadList();

            _EnrollRepo = new EnrollRepository();
            _EnrollRepo.LoadList();
        }
    }
}
