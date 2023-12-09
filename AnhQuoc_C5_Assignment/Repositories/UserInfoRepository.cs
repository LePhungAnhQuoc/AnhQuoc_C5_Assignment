using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class UserInfoRepository : Repository<UserInfo>
    {
        public UserInfoRepository(): base()
        {
        }

        public UserInfoRepository(ObservableCollection<UserInfo> items): base(items)
        {
        }
        
        public override void WriteUpdate(UserInfo updated)
        {
            var UserInfoViewModel = UnitOfViewModel.Instance.UserInfoViewModel;

            UserInfo itemSource = dbSource.UserInfoes.FirstOrDefault(sourceItem => sourceItem.IdUser == updated.IdUser);
            UserInfoViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}
