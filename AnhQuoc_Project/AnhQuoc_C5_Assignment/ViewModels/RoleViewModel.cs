﻿using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class RoleViewModel : BaseViewModel<Role>
    {
        public RoleViewModel()
        {
            Item = new Role();
            Repo = new RoleRepository(new APIProvider<Role>(nameof(Role)));
            prefix = Constants.prefixRole;
            numberPrefix = 1;

        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public Role FindById(string idValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Id, idValue, false) == 0);
        }

        public Role FindByName(string nameValue, bool isIgnorecase)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.Name, nameValue, isIgnorecase) == 0);
        }

        public ObservableCollection<string> GetGroups()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            foreach (Role item in Repo)
            {
                string value = item.Group;
                if (!result.Contains(value))
                {
                    result.Add(value);
                }
            }
            return result;
        }



        public bool IsCheckEmptyItem(RoleDto item)
        {
            if (Utilitys.IsCheckEmptyString(item.Name))
            {
                return false;
            }
            if (Utilitys.IsCheckEmptyString(item.Group))
            {
                return false;
            }
            return true;
        }

        public Role CreateByDto(RoleDto dto)
        {
            Role role = new Role();
            role.Id = dto.Id;
            Copy(role, dto);
            return role;
        }

        public void Copy(Role dest, RoleDto source)
        {
            Utilitys.Copy(dest, source);
        }

        public void Copy(RoleDto dest, RoleDto source)
        {
            Utilitys.Copy(dest, source);
        }
    }
}
