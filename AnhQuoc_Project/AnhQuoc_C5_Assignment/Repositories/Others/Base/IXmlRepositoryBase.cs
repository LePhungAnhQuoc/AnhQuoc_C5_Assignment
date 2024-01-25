using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AnhQuoc_C5_Assignment
{
    public interface IXmlRepositoryBase<T>: IEnumerable where T : class
    {
        T LoadItem(XmlNode nodeData);
        void LoadItems(XmlNodeList lstNode);
        XmlNode WriteItems(XmlNode parentNode);
        XmlNode WriteItem(T newItem);
    }
}
