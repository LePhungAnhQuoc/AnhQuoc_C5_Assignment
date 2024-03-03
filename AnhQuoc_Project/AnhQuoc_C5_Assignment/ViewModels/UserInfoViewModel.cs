using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserInfoViewModel : BaseViewModel<UserInfo>
    {
        public UserInfoViewModel()
        {
            Item = new UserInfo();
            Repo = new UserInfoRepository();
            prefix = Constants.prefixUserInfo;
            numberPrefix = 0;
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
            Utilities.Copy(dest, source);
        }
    }
}
