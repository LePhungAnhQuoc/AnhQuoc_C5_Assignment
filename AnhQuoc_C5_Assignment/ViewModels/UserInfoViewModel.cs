﻿using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserInfoViewModel : ViewModelBase<UserInfo>
    {
        public UserInfoViewModel()
        {
            Item = new UserInfo();
            Repo = new UserInfoRepository();
            prefix = Constants.prefixUserInfo;
        }
        
        public UserInfo FindByIdUser(string idUserValue)
        {
            return Repo.Gets().FirstOrDefault(item => string.Compare(item.IdUser, idUserValue, false) == 0);
        }

        public UserInfo CreateByDto(UserDto dto)
        {
            UserInfo result = new UserInfo();
            result.IdUser = dto.Id;
            Copy(result, dto);
            return result;
        }

        public void Copy(UserInfo dest, UserDto source)
        {
            dest.LName = source.LName;
            dest.FName = source.FName;

            dest.Phone = source.Phone;
            dest.Address = source.Address;
        }
    }
}
