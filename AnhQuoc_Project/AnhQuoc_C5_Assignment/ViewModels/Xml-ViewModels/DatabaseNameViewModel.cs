using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class DatabaseNameViewModel: BaseViewModel<DatabaseName>
    {
        public DatabaseNameViewModel()
        {
            prefix = Constants.prefixDatabaseName;
            Item = new DatabaseName(string.Empty);
            Repo = new DatabaseNameRepository(new APIProvider<DatabaseName>(nameof(DatabaseName)));
            numberPrefix = 1;
        }

        public string GetId()
        {
            int index = base.getMaxIndexId(nameof(Item.Id));
            return base.GetId(index);
        }

        public void ClearFile()
        {
            XmlProvider.Instance.Open(Constants.fDatabaseNames);

            while (XmlProvider.Instance.nodeRoot.HasChildNodes)
            {
                var firstChild = XmlProvider.Instance.nodeRoot.FirstChild;
                XmlProvider.Instance.nodeRoot.RemoveChild(firstChild);
            }
            XmlProvider.Instance.Close();
        }

        public DatabaseName FindTrue()
        {
            foreach (DatabaseName database in Repo)
            {
                if (database.Status == true)
                {
                    return database;
                }
            }
            return null;
        }
    }
}
