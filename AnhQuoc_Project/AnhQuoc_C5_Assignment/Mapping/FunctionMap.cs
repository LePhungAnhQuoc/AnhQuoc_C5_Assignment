using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class FunctionMap : MapBase<Function, FunctionDto>
    {
        #region ViewModels
        private FunctionViewModel functionVM;
        #endregion

        public FunctionMap()
        {
            functionVM = UnitOfViewModel.Instance.FunctionViewModel;
        }

        public override FunctionDto ConvertToDto(Function sourceItem)
        {
            var functionVM = UnitOfViewModel.Instance.FunctionViewModel;

            FunctionDto newItem = new FunctionDto(sourceItem.Id);
            Utilitys.Copy(newItem, sourceItem);

            newItem.Parent = (Utilitys.IsCheckEmptyString(sourceItem.IdParent)) ? null : functionVM.FindById(sourceItem.IdParent, null);

            var allChildsFunc = functionVM.getChildsByIdParent(newItem.Id, null);
            foreach (Function childFunc in allChildsFunc)
            {
                FunctionChildDto newDto = new FunctionChildDto(childFunc.Id);
                functionVM.Copy(newDto, childFunc);
                newItem.Childs.Add(newDto);
            }
            
            return newItem;
        }
    }
}
