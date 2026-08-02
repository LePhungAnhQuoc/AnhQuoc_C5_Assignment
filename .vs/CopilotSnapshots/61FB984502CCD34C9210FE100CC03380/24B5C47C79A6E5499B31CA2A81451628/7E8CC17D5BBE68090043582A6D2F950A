using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucRoleFunctionManagement.xaml
    /// </summary>
    public partial class ucRoleFunctionManagement : UserControl, INotifyPropertyChanged
    {
        #region GetDatas
        public Func<FunctionRepository> getFunctionRepo { get; set; }
        public Func<RoleFunctionRepository> getRoleFunctionRepo { get; set; }
        public Func<RoleRepository> getRoleRepo { get; set; }
        public Func<UserRepository> getUserRepo { get; set; }
        public Func<UserRoleRepository> getUserRoleRepo { get; set; }
        public Func<UserInfoRepository> getUserInfoRepo { get; set; }

        public Func<ParameterRepository> getParameterRepo { get; set; }
        #endregion

        #region Fields
        private IEnumerable<Function> listFuncs;
        private bool isInitTreeViewCheckBox;

        private ObservableCollection<Role> _AllRoles;
        #endregion

        #region ViewModels
        private FunctionViewModel functionVM;
        private UserViewModel userVM;
        private RoleFunctionViewModel roleFunctionVM;
        private RoleViewModel roleVM;
        private UserRoleViewModel userRoleVM;
        private UserInfoViewModel userInfoVM;
        #endregion

        #region Mapping
        private FunctionMap functionMap;
        private RoleMap roleMap;
        private UserMap userMap;
        #endregion

        #region Fillter-Variable
        private ObservableCollection<Role> listFill;
        #endregion

        #region Properties
        private ObservableCollection<FunctionDto> _Functions;
        public ObservableCollection<FunctionDto> Functions
        {
            get { return _Functions; }
            set
            {
                _Functions = value;
                OnPropertyChanged();
            }
        }
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

        public ucRoleFunctionManagement()
        {
            InitializeComponent();

            #region Allocations
            listFill = new ObservableCollection<Role>();

            roleFunctionVM = UnitOfViewModel.Instance.RoleFunctionViewModel;
            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
            roleVM = UnitOfViewModel.Instance.RoleViewModel;
            userRoleVM = UnitOfViewModel.Instance.UserRoleViewModel;
            userInfoVM = UnitOfViewModel.Instance.UserInfoViewModel;
            userVM = UnitOfViewModel.Instance.UserViewModel;

            functionMap = UnitOfMap.Instance.FunctionMap;
            roleMap = UnitOfMap.Instance.RoleMap;
            userMap = UnitOfMap.Instance.UserMap;
            #endregion

            txtSearch.TextChanged += TxtSearch_TextChanged;
            ucRolesTable.dgDatas.SelectionChanged += DgDatas_SelectionChanged;

            this.Loaded += ucRoleFunctionManagement_Loaded;
            btnSaveFunc.Click += BtnSaveFunc_Click;
            btnResetFunc.Click += BtnResetFunc_Click;
            btnCancelFunc.Click += BtnCancelFunc_Click;
            this.DataContext = this;
        }

        private void ucRoleFunctionManagement_Loaded(object sender, RoutedEventArgs e)
        {
            isInitTreeViewCheckBox = true;
            bool? statusValue = true;
            bool isAdminValue = false;

            ucRolesTable.getRoleFunctionRepo = getRoleFunctionRepo;
            ucRolesTable.getItem_Status = () => null;

            var listFunc = getFunctionRepo().Gets();
            var listFuncDto = functionMap.ConvertToDto(listFunc);


            Functions = functionVM.FillParent(listFuncDto, statusValue).ToObservableCollection();
            Functions = functionVM.FillAdminFunction(Functions, isAdminValue, statusValue).ToObservableCollection();
            functionVM.FillChildsAdminInParent(Functions, isAdminValue, statusValue);

            foreach (FunctionDto parent in Functions)
            {
                foreach (FunctionChildDto child in parent.Childs)
                {
                    if (Utilitys.FindInList(Constants.importantFunction, child.Id) != null)
                    {
                        child.IsSeal = true;
                    }
                }
            }

            foreach (FunctionDto parent in Functions)
            {
                foreach (FunctionChildDto child in parent.Childs)
                {
                    child.SetValue(ItemHelper.ParentProperty, parent);
                }
            }
            _AllRoles = new ObservableCollection<Role>(getRoleRepo().Gets());
            _AllRoles.RemoveAt(0);

            AddToListFill(_AllRoles);
            AddItemsToDataGrid(listFill);

            gdListFeature.IsEnabled = false;
        }

        #region Fillter-Methods
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fillter();
        }

        private void Fillter()
        {
            var results = FillByTextSearch(_AllRoles);

            AddToListFill(results);
            AddItemsToDataGrid(results);
        }

        private ObservableCollection<Role> FillByTextSearch(ObservableCollection<Role> source)
        {
            string textSearch = txtSearch.Text.ToLower();

            ObservableCollection<Role> results = new ObservableCollection<Role>();
            foreach (Role item in source)
            {
                var itemDto = roleMap.ConvertToDto(item);
                bool isCheck = itemDto.Name.ContainsCorrectly(textSearch, true);
                if (isCheck)
                {
                    results.Add(item);
                }
            }
            return results;
        }
        #endregion

        private void AddToListFill(ObservableCollection<Role> items)
        {
            listFill.Clear();
            listFill.AddRange(items);
        }

        private void AddItemsToDataGrid(ObservableCollection<Role> items)
        {
            ucRolesTable.getRoles = () => items;
            ucRolesTable.ModifiedPagination();
        }

        private void DgDatas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isInitTreeViewCheckBox = true;
            gdListFeature.IsEnabled = true;
            ClearTreeViewFunctions();

            RoleDto roleDto = ucRolesTable.SelectedRoleDto;
            if (roleDto == null)
                return;
            Role roleSelect = roleVM.CreateByDto(roleDto);
            
            IEnumerable<RoleFunction> roleFuncs = roleFunctionVM.GetListFunctionByRole(roleSelect.Id);
            listFuncs = functionVM.getFunctionsByListId(roleFuncs.Select(i => i.IdFunction));

            listFuncs = functionVM.FillByStatus(listFuncs.ToObservableCollection(), true);
            if (listFuncs.Count() == 0)
            {
            }

            // Set CheckBox Functions in role is True
            bool? updateCheck = true;
            foreach (Function func in listFuncs)
            {
                if (!Utilitys.IsCheckEmptyString(func.IdParent))
                {
                    Function getParent = functionVM.FindById(func.IdParent);

                    FunctionDto findfuncParent = null;
                    foreach (FunctionDto funcParentItem in Functions)
                    {
                        if (funcParentItem.Id == getParent.Id)
                        {
                            findfuncParent = funcParentItem;
                            break;
                        }
                    }

                    FunctionChildDto findfuncChild = null;
                    foreach (FunctionChildDto member in findfuncParent.Childs)
                    {
                        if (member.Id == func.Id)
                        {
                            findfuncChild = member;
                            break;
                        }
                    }
                    ItemHelper.SetIsChecked(findfuncChild, updateCheck);
                }
            }
            isInitTreeViewCheckBox = false;
        }

        private void BtnSaveFunc_Click(object sender, RoutedEventArgs e)
        {
            SettingSelectRoleFunctionState(true);

            foreach (FunctionDto parent in this.Functions)
            {
                bool? isCheckParent = ItemHelper.GetIsChecked(parent);
                if (isCheckParent == true || isCheckParent == null)
                {
                    Function findFunction = functionVM.FindById(listFuncs.ToObservableCollection(), parent.Id);
                    if (findFunction == null)
                    {
                        AddNewRoleFunction(parent.Id);
                    }
                }
                else if (isCheckParent == false)
                {
                    Function findFunction = functionVM.FindById(listFuncs.ToObservableCollection(), parent.Id);
                    if (findFunction != null)
                    {
                        DeleteRoleFunction(parent.Id);
                    }
                }

                foreach (FunctionChildDto funcChild in parent.Childs)
                {
                    bool? isCheckChild = ItemHelper.GetIsChecked(funcChild);

                    if (isCheckChild == true)
                    {
                        Function findFunction = functionVM.FindById(listFuncs.ToObservableCollection(), funcChild.Id);
                        if (findFunction == null)
                        {
                            AddNewRoleFunction(funcChild.Id);
                        }
                    }
                    else if (isCheckChild == false)
                    {
                        Function findFunction = functionVM.FindById(listFuncs.ToObservableCollection(), funcChild.Id);
                        if (findFunction != null)
                        {
                            DeleteRoleFunction(funcChild.Id);
                        }
                    }
                }
            }
            Utilitys.ShowMessageBox1(Utilitys.NotifyAddSuccessfully("role functions"));
        }

        private void ClearTreeViewFunctions()
        {
            bool updateCheck = false;
            foreach (FunctionDto funcParent in Functions)
            {
                ItemHelper.SetIsChecked(funcParent, updateCheck);
                foreach (FunctionChildDto member in funcParent.Childs)
                {
                    ItemHelper.SetIsChecked(member, updateCheck);
                }
            }
        }

        private void BtnResetFunc_Click(object sender, RoutedEventArgs e)
        {
            DgDatas_SelectionChanged(null, null);
        }

        private void BtnCancelFunc_Click(object sender, RoutedEventArgs e)
        {
            BtnResetFunc_Click(btnResetFunc, null);
            SettingSelectRoleFunctionState(true);
        }

        private void AddNewRoleFunction(string idNewFunc)
        {
            RoleFunction newRoleFunc = new RoleFunction();
            newRoleFunc.Id = roleFunctionVM.GetId();
            newRoleFunc.IdRole = ucRolesTable.SelectedRoleDto.Id;
            newRoleFunc.IdFunction = idNewFunc;

            getRoleFunctionRepo().Add(newRoleFunc);
            getRoleFunctionRepo().WriteAdd(newRoleFunc);
        }

        private void DeleteRoleFunction(string idFunction)
        {
            var getListFill = roleFunctionVM.FillByIdRole(ucRolesTable.SelectedRoleDto.Id);
            RoleFunction findedItem = roleFunctionVM.FindByIdFunction(getListFill, idFunction);

            getRoleFunctionRepo().Remove(findedItem);
            getRoleFunctionRepo().WriteDelete(findedItem);
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!isInitTreeViewCheckBox)
                SettingSelectRoleFunctionState(false);

            CheckBox chkCheck = sender as CheckBox;
            string idFunc = chkCheck.Tag.ToString();

            #region Is-Parent-Func
            Function func = functionVM.FindById(idFunc);
            if (func.IdParent == null)
                return;
            #endregion

            FunctionChildDto childFuncFound = GetChildFunction(idFunc);

            #region Set-IsCheck-To-Seal-Func
            if (childFuncFound.IsSeal == true)
                return;

            FunctionDto funcParentDto = ItemHelper.GetParent(childFuncFound) as FunctionDto;

            if (ItemHelper.GetIsChecked(childFuncFound) == true)
            {
                SetIsCheckForSealFunction(funcParentDto);
            }
            else // false
            {
                bool isAllChildFunction_unCheck = IsAllChildFunction_unCheck(funcParentDto);
                if (isAllChildFunction_unCheck)
                {
                    ItemHelper.SetIsChecked(funcParentDto, false);
                    SetIsCheckForSealFunction(funcParentDto);
                }
            }
            #endregion
        }

        private void SettingSelectRoleFunctionState(bool status)
        {
            var ucLibrarianDashBoard = MainWindow.UnitOfForm.UcLibrarianDashBoard(false);
            var ucRoleManagement = MainWindow.UnitOfForm.UcRoleManagement(false);

            var list = new List<TabItem>();
            int count = ucRoleManagement.tabMenu.Items.Count;

            foreach (TabItem tabItem in ucRoleManagement.tabMenu.Items)
            {
                if (tabItem == (ucRoleManagement.tabMenu.SelectedItem as TabItem))
                    continue;
                tabItem.IsEnabled = status;
            }
            ucLibrarianDashBoard.IsEnabled = status;
            ucRolesTable.IsEnabled = status;
        }

        private void SetIsCheckForSealFunction(FunctionDto funcParentDto)
        {
            bool? temp = ItemHelper.GetIsChecked(funcParentDto);
            bool parentIsCheck = (temp == null || temp == true) ? true : false;

            foreach (FunctionChildDto child in funcParentDto.Childs)
            {
                if (child.IsSeal)
                {
                    ItemHelper.SetIsChecked(child, parentIsCheck);
                }
            }
        }


        private FunctionChildDto GetChildFunction(string idFunc_Child)
        {
            FunctionChildDto childFuncFound = null;
            foreach (FunctionDto parentFunc in tvFunctions.ItemsSource)
            {
                foreach (FunctionChildDto childFunc in parentFunc.Childs)
                {
                    if (childFunc.Id == idFunc_Child)
                    {
                        childFuncFound = childFunc;
                        break;
                    }
                }
            }
            return childFuncFound;
        }

        private bool IsAllChildFunction_unCheck(FunctionDto funcParentDto)
        {
            foreach (FunctionChildDto child in funcParentDto.Childs)
            {
                if (!child.IsSeal)
                {
                    if (ItemHelper.GetIsChecked(child) == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}
