using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UnitOfMap
    {
        #region SingleTonPattern
        private static readonly object _InstanceLock = new object();

        private static UnitOfMap _Instance;
        public static UnitOfMap Instance
        {
            get
            {
                lock (_InstanceLock)
                {
                    if (_Instance == null)
                        _Instance = new UnitOfMap();
                }
                return _Instance;
            }
        }
        #endregion

        #region Properties
        public UnitOfRepository UnitOfRepo { get; set; }

        private BookTitleMap _BookTitleMap;
        public BookTitleMap BookTitleMap
        {
            get
            {
                if (_BookTitleMap == null)
                    _BookTitleMap = new BookTitleMap();
                return _BookTitleMap;
            }
        }

        private BookISBNMap _BookISBNMap;
        public BookISBNMap BookISBNMap
        {
            get
            {
                if (_BookISBNMap == null)
                    _BookISBNMap = new BookISBNMap();
                return _BookISBNMap;
            }
        }

        private LoanSlipMap _LoanSlipMap;
        public LoanSlipMap LoanSlipMap
        {
            get
            {
                if (_LoanSlipMap == null)
                    _LoanSlipMap = new LoanSlipMap();
                return _LoanSlipMap;
            }
        }

        private LoanDetailMap _LoanDetailMap;
        public LoanDetailMap LoanDetailMap
        {
            get
            {
                if (_LoanDetailMap == null)
                    _LoanDetailMap = new LoanDetailMap();
                return _LoanDetailMap;
            }
        }

        private BookMap _BookMap;
        public BookMap BookMap
        {
            get
            {
                if (_BookMap == null)
                    _BookMap = new BookMap();
                return _BookMap;
            }
        }

        private ProvinceMap _ProvinceMap;
        public ProvinceMap ProvinceMap
        {
            get
            {
                if (_ProvinceMap == null)
                    _ProvinceMap = new ProvinceMap();
                return _ProvinceMap;
            }
        }

        private AuthorMap _AuthorMap;
        public AuthorMap AuthorMap
        {
            get
            {
                if (_AuthorMap == null)
                    _AuthorMap = new AuthorMap();
                return _AuthorMap;
            }
        }

        private ReaderMap _ReaderMap;
        public ReaderMap ReaderMap
        {
            get
            {
                if (_ReaderMap == null)
                    _ReaderMap = new ReaderMap();
                return _ReaderMap;
            }
        }

        private UserMap _UserMap;
        public UserMap UserMap
        {
            get
            {
                if (_UserMap == null)
                    _UserMap = new UserMap();
                return _UserMap;
            }
        }

        private FunctionMap _FunctionMap;
        public FunctionMap FunctionMap
        {
            get
            {
                if (_FunctionMap == null)
                    _FunctionMap = new FunctionMap();
                return _FunctionMap;
            }
        }

        private RoleMap _RoleMap;
        public RoleMap RoleMap
        {
            get
            {
                if (_RoleMap == null)
                    _RoleMap = new RoleMap();
                return _RoleMap;
            }
        }

        private UserRoleMap _UserRoleMap;
        public UserRoleMap UserRoleMap
        {
            get
            {
                if (_UserRoleMap == null)
                    _UserRoleMap = new UserRoleMap();
                return _UserRoleMap;
            }
        }

        private RoleFunctionMap _RoleFunctionMap;
        public RoleFunctionMap RoleFunctionMap
        {
            get
            {
                if (_RoleFunctionMap == null)
                    _RoleFunctionMap = new RoleFunctionMap();
                return _RoleFunctionMap;
            }
        }

        private ChildMap _ChildMap;
        public ChildMap ChildMap
        {
            get
            {
                if (_ChildMap == null)
                    _ChildMap = new ChildMap();
                return _ChildMap;
            }
        }

        private AdultMap _AdultMap;
        public AdultMap AdultMap
        {
            get
            {
                if (_AdultMap == null)
                    _AdultMap = new AdultMap();
                return _AdultMap;
            }
        }

        #endregion

        private UnitOfMap() { }

        public void Load()
        {
            _BookISBNMap = new BookISBNMap();
            _LoanSlipMap = new LoanSlipMap();
            _LoanDetailMap = new LoanDetailMap();
            _BookTitleMap = new BookTitleMap();
            _BookMap = new BookMap();        
            _ProvinceMap = new ProvinceMap();
            _AuthorMap = new AuthorMap();
            _ReaderMap = new ReaderMap();
            _UserMap = new UserMap();
            _FunctionMap = new FunctionMap();
            _RoleMap = new RoleMap();
            _UserRoleMap = new UserRoleMap();
            _RoleFunctionMap = new RoleFunctionMap();
            _ChildMap = new ChildMap();
            _AdultMap = new AdultMap();
        }
    }
}
