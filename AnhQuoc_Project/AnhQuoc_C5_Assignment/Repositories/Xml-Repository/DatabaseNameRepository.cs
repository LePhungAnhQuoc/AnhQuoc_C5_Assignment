using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_C5_Assignment
{
    public class DatabaseNameRepository : Repository<DatabaseName>, IXmlRepositoryBase<DatabaseName>
    {
        public DatabaseNameRepository(): base()
        {
        }
        public DatabaseNameRepository(ObservableCollection<DatabaseName> items): base(items)
        {
        }


        public override void LoadList()
        {
            XmlProvider.Instance.Open(Constants.fDatabaseNames);
            LoadItems(XmlProvider.Instance.nodeRoot.ChildNodes);
            XmlProvider.Instance.Close();
        }

        public override void WriteAdd(DatabaseName newDatabaseName)
        {
            XmlProvider.Instance.Open(Constants.fDatabaseNames);

            XmlNode newNodeItem = WriteItem(newDatabaseName);
            XmlNode parentNode = XmlProvider.Instance.nodeRoot;

            parentNode.AppendChild(newNodeItem);
            XmlProvider.Instance.Close();
        }

        public void WritePrepend(DatabaseName newDatabaseName)
        {
            XmlProvider.Instance.Open(Constants.fDatabaseNames);

            XmlNode newNodeItem = WriteItem(newDatabaseName);
            XmlNode parentNode = XmlProvider.Instance.nodeRoot;

            parentNode.PrependChild(newNodeItem);
            XmlProvider.Instance.Close();
        }


        public DatabaseName LoadItem(XmlNode nodeData)
        {
            XmlNode nodeTemp = nodeData.FirstChild;

            DatabaseName newItem = new DatabaseName(nodeTemp.InnerText);

            nodeTemp = nodeTemp.NextSibling;
            newItem.Name = nodeTemp.InnerText;

            nodeTemp = nodeTemp.NextSibling;
            newItem.Status = Convert.ToBoolean(nodeTemp.InnerText);
            return newItem;
        }

        public void LoadItems(XmlNodeList lstNode)
        {
            _Items.Clear();
            foreach (XmlNode nodeData in lstNode)
            {
                _Items.Add(LoadItem(nodeData));
            }
        }

        public XmlNode WriteItem(DatabaseName newItem)
        {
            XmlNode newNode = XmlProvider.Instance.createNode(Constants.childDatabaseName);
            XmlNode newAttr = null;

            newAttr = XmlProvider.Instance.createNode("Id");
            newAttr.InnerText = newItem.Id;
            newNode.AppendChild(newAttr);

            newAttr = XmlProvider.Instance.createNode("Name");
            newAttr.InnerText = newItem.Name;
            newNode.AppendChild(newAttr);

            newAttr = XmlProvider.Instance.createNode("Status");
            newAttr.InnerText = newItem.Status.ToString();
            newNode.AppendChild(newAttr);
            return newNode;
        }

        public XmlNode WriteItems(XmlNode parentNode)
        {
            foreach (DatabaseName item in _Items)
            {
                XmlNode newNode = WriteItem(item);
                parentNode.AppendChild(newNode);
            }
            int count = parentNode.ChildNodes.Count;
            parentNode.Attributes["Count"].Value = count.ToString();
            return parentNode;
        }

        public override void WriteDelete(DatabaseName item)
        {
            throw new NotImplementedException();
        }

        public override void WriteUpdate(DatabaseName updated)
        {
            var temp = new DatabaseName();
            string propIdName = nameof(temp.Id);
            string propName = nameof(temp.Status);

            XmlProvider.Instance.Open(Constants.fDatabaseNames);
            XmlNode nodeFinded = XmlProvider.Instance.FindNode(Constants.childDatabaseName, propIdName, updated.Id);

            XmlNode nodePropFinded = XmlProvider.Instance.FindAttributeNode(nodeFinded, propName);
            nodePropFinded.InnerText =  updated.Status.ToString();
            XmlProvider.Instance.Close();
        }
    }
}
