using Api.Utilities.CheckProperty;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Api.Utilities
{
    public static class Utilitys
    {
        #region String-Uti
        // Check is null, is empty, is str only have WhiteSpace characters
        public static bool IsCheckEmptyString(string value)
        {
            bool result = string.IsNullOrEmpty(value)
                || string.IsNullOrWhiteSpace(value);
            return result;
        }
        #endregion

        #region Property-Uti
        // check if it's a generic List<T> or IEnumerable<T>, ...
        public static Type GetGenericDataType(Type type)
        {
            return type.GetGenericTypeDefinition();
        }

        public static bool IsListType(Type type)
        {
            return GetGenericDataType(type) == typeof(List<>);
        }

        public static bool IsGeneric(Type type)
        {
            return type.IsGenericType;
        }

        public static bool IsGeneric(object obj)
        {
            return IsGeneric(obj.GetType());
        }

        public static Type GetItemDataTypeInGenericList(IEnumerable list)
        {
            Type type = list.GetType();
            Type itemType = type.GetGenericArguments().Single();
            return itemType;
        }


        public static Type GetLeftDataTypeOfVariable<T>(T x) => typeof(T);

        public static List<PropertyInfo> getPropsFromType(Type type)
        {
            return type.GetProperties().OrderBy(item => item.Name).ToList();
        }

        public static List<PropertyInfo> getPropsFromType(object obj)
        {
            return getPropsFromType(obj.GetType());
        }

        public static List<PropertyInfo> getBasePropsFromType(Type type)
        {
            return getPropsFromType(type.BaseType);
        }

        public static List<PropertyInfo> getBasePropsFromType(object obj)
        {
            return getBasePropsFromType(obj.GetType());
        }

        public static List<PropertyInfo> getDerivePropsFromType(Type type)
        {
            var props = getPropsFromType(type);
            return props.Where(p => p.DeclaringType.FullName != type.BaseType.FullName).ToList();
        }

        public static List<PropertyInfo> getDerivePropsFromType(object obj)
        {
            return getDerivePropsFromType(obj.GetType());
        }

        public static void SetValueToProperty(string propName, object item, object value)
        {
            PropertyInfo prop = item.GetType().GetProperty(propName);
            SetValueToProperty(prop, item, value);
        }

        public static void SetValueToProperty(PropertyInfo prop, object item, object value)
        {
            prop.SetValue(item, value);
        }

        public static object getValueFromProperty(string propName, object item)
        {
            PropertyInfo prop = item.GetType().GetProperty(propName);
            return getValueFromProperty(prop, item);
        }

        public static object getValueFromProperty(PropertyInfo prop, object item)
        {
            return prop.GetValue(item, null);
        }

        public static object ConvertFromString(string tempValue, Type getType)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(getType);

            if (IsCheckEmptyString(tempValue))
                return null;
            object result = typeConverter.ConvertFromString(tempValue);
            return result;
        }

        public static string[] GetPrimaryKeys(SqlConnection connection, string tableName)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select * from [{0}]", tableName), connection))
            using (DataTable table = new DataTable(tableName))
            {
                return adapter
                    .FillSchema(table, SchemaType.Mapped)
                    .PrimaryKey
                    .Select(c => c.ColumnName)
                    .ToArray();
            }
        }

        public static List<PropertyInfo> FillPropertiesByName(List<PropertyInfo> properties, params string[] propsName)
        {
            List<PropertyInfo> result = new List<PropertyInfo>();
            foreach (string prop in propsName)
            {
                PropertyInfo property = FindPropertyByName(properties, prop);
                result.Add(property);
            }
            return result;
        }

        public static PropertyInfo FindPropertyByName(List<PropertyInfo> properties, string propertyName)
        {
            foreach (PropertyInfo item in properties)
            {
                if (string.Compare(item.Name, propertyName) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        public static string FindPropertyByName(List<string> properties, string propertyName)
        {
            foreach (string item in properties)
            {
                if (string.Compare(item, propertyName) == 0)
                {
                    return item;
                }
            }
            return null;
        }
        #endregion


        public static void Copy(object dest, object source)
        {
            IsCheckProperties isCheckProperties = new IsCheckProperties(CheckPropertyType.Except);
            Copy(dest, source, isCheckProperties);
        }

        public static void Copy(object dest, object source, IsCheckProperties isCheckProperties)
        {
            const string propSelector = "Name";
            bool isAllProperties = false;
            if (isCheckProperties.ListProperties.Count == 0 && isCheckProperties.Type == CheckPropertyType.Except)
            {
                isAllProperties = true;
            }

            var dest_props = getPropsFromType(dest);
            var source_props = getPropsFromType(source);

            var propsString = Utilitys.InnerJoin(dest_props, source_props, propSelector).ToList();
            dest_props = FillPropertiesByName(dest_props, propsString.ToArray());
            source_props = FillPropertiesByName(source_props, propsString.ToArray());

            for (int i = 0; i < dest_props.Count(); i++)
            {
                PropertyInfo dest_prop = dest_props[i];
                PropertyInfo source_prop = source_props[i];

                if (!isAllProperties)
                {
                    if (isCheckProperties.Type == CheckPropertyType.Include)
                    {
                        var itemCheck = FindPropertyByName(isCheckProperties.ListProperties, dest_prop.Name);
                        if (itemCheck == null)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        var itemCheck = FindPropertyByName(isCheckProperties.ListProperties, dest_prop.Name);
                        if (itemCheck != null)
                        {
                            continue;
                        }
                    }
                }
                object value2 = getValueFromProperty(source_prop, source);

                if (value2 == null)
                {
                    dest_prop.SetValue(dest, null);
                }
                else
                {
                    if (!IsGeneric(value2.GetType()))
                    {
                        if (dest_prop.PropertyType.Name == source_prop.PropertyType.Name)
                        {
                            dest_prop.SetValue(dest, value2);
                        }
                    }
                }
            }
        }

        public static IEnumerable<string> InnerJoin<T>(IEnumerable<T> bigSource, IEnumerable<T> smallSource, string propSelector)
        {
            Func<T, string> outerKeySelector = bigItem => getValueFromProperty(bigItem.GetType().GetProperty(propSelector), bigItem).ToString();
            Func<T, string> innerKeySelector = smallItem => getValueFromProperty(smallItem.GetType().GetProperty(propSelector), smallItem).ToString();

            IEnumerable<string> result = bigSource.Join(smallSource, outerKeySelector, innerKeySelector, (bigItem, smallItem) => getValueFromProperty(bigItem.GetType().GetProperty(propSelector), bigItem).ToString());
            return result;
        }

    }
}
