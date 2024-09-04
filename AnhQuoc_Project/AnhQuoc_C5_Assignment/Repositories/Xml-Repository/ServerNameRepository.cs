using AnhQuoc_C5_Assignment.DTOs.Other_Dtos;
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
    public class ServerNameRepository : Repository<ServerName>, IXmlRepositoryBase<ServerName>
    {
        public ServerNameRepository(APIProvider<ServerName> ApiProvider) : base(ApiProvider)
        {
        }

        public override void LoadList()
        {
            XmlProvider.Instance.Open(Constants.fServerNames);
            LoadItems(XmlProvider.Instance.nodeRoot.ChildNodes);
            XmlProvider.Instance.Close();
        }

        public override void WriteAdd(ServerName newServerName)
        {
            XmlProvider.Instance.Open(Constants.fServerNames);

            XmlNode newNodeItem = WriteItem(newServerName);
            XmlNode parentNode = XmlProvider.Instance.nodeRoot;

            parentNode.AppendChild(newNodeItem);
            XmlProvider.Instance.Close();
        }

        public void WritePrepend(ServerName newServerName)
        {
            XmlProvider.Instance.Open(Constants.fServerNames);

            XmlNode newNodeItem = WriteItem(newServerName);
            XmlNode parentNode = XmlProvider.Instance.nodeRoot;

            parentNode.PrependChild(newNodeItem);
            XmlProvider.Instance.Close();
        }


        public ServerName LoadItem(XmlNode nodeData)
        {
            XmlNode nodeTemp = nodeData.FirstChild;

            ServerName newItem = new ServerName(nodeTemp.InnerText);

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

        public XmlNode WriteItem(ServerName newItem)
        {
            XmlNode newNode = XmlProvider.Instance.createNode(Constants.childServerName);
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
            foreach (ServerName item in _Items)
            {
                XmlNode newNode = WriteItem(item);
                parentNode.AppendChild(newNode);
            }
            int count = parentNode.ChildNodes.Count;
            parentNode.Attributes["Count"].Value = count.ToString();
            return parentNode;
        }

        public override void WriteDelete(ServerName item)
        {
            throw new NotImplementedException();
        }

        public override void WriteUpdate(ServerName updated)
        {
            var temp = new ServerName();
            string propIdName = nameof(temp.Id);
            string propName = nameof(temp.Status);

            XmlProvider.Instance.Open(Constants.fServerNames);
            XmlNode nodeFinded = XmlProvider.Instance.FindNode(Constants.childServerName, propIdName, updated.Id);
            
            XmlNode nodePropFinded = XmlProvider.Instance.FindAttributeNode(nodeFinded, propName);
            nodePropFinded.InnerText = updated.Status.ToString();
            XmlProvider.Instance.Close();
        }
    }
}
