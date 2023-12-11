using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AnhQuoc_C5_Assignment
{
    public class ServerNameViewModel : ViewModelBase<ServerName>
    {
        public ServerNameViewModel()
        {
            prefix = Constants.prefixServerName;
            Item = new ServerName(string.Empty);
            Repo = new ServerNameRepository();
            numberPrefix = 1;
        }

        public string GetId()
        {
            var propId = Item.GetType().GetProperty(nameof(Item.Id));
            int index = base.getMaxIndexId(propId);
            return base.GetId(index);
        }

        public void ClearFile()
        {
            XmlProvider.Instance.Open(Constants.fServerNames);

            while (XmlProvider.Instance.nodeRoot.HasChildNodes)
            {
                var firstChild = XmlProvider.Instance.nodeRoot.FirstChild;
                XmlProvider.Instance.nodeRoot.RemoveChild(firstChild);
            }
            XmlProvider.Instance.Close();
        }

        public ServerName FindTrue()
        {
            foreach (ServerName server in Repo)
            {
                if (server.Status == true)
                {
                    return server;
                }
            }
            return null;
        }

        public string GetServerName()
        {
            var result = System.Environment.MachineName;// + @"\SQLEXPRESS";
            return result;
        }

        public string GetServerName2()
        {
            var result = System.Environment.MachineName + @"\SQLEXPRESS";
            return result;
        }
    }
}
