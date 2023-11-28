using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;

namespace AnhQuoc_C5_Assignment
{
    public class MyVirtualizingStackPanel : VirtualizingStackPanel
    {
        /// <summary>
        /// Publically expose BringIndexIntoView.
        /// </summary>
        public void BringIntoView(int index)
        {

            this.BringIndexIntoView(index);
        }
    }

    public class Utilities
    {
        #region Notify
        public static MessageBoxResult ShowMessageBox1(string message, string caption = "", MessageBoxImage msbImg = MessageBoxImage.Information)
        {
            return MessageBox.Show(message, caption, MessageBoxButton.OK, msbImg);
        }

        public static MessageBoxResult ShowMessageBox2(string message)
        {
            return MessageBox.Show(message, string.Empty, MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
        }

        public static string NotifyNotHaveRole()
        {
            return string.Format("This user doesn't have role!");
        }

        public static string NotifyRoleLock()
        {
            return string.Format("This role for this user is currently locked. Contact admin for help!");
        }

        public static string NotifyNotHaveFunctions(string item)
        {
            return string.Format("This {0} doesn't have any function!", item);
        }

        public static string NotifyItemExistInfo(string item)
        {
            return $"This {item} is has the same information in the list";
        }

        public static string NotifyItemExistInTheList(string item, bool isExist = true)
        {
            string result = string.Empty;
            if (isExist)
                result = string.Format("This {0} already exists in the list", item);
            else
                result = string.Format("This {0} is currently not in the list", item);
            return result;
        }
        
        public static string NotifyListEmptyMessage(string item, string lstName = "List")
        {
            return $"There are no {item} in the {lstName}.";
        }

        public static string NotifyBookTitleExistInfo()
        {
            return "This book title is has the same information in the list";
        }

        public static string NotifyBookISBNExistInfo()
        {
            return "This book ISBN is has the same information in the list";
        }


        public static string NotifyPleaseSelect(string item)
        {
            return string.Format("Please select {0}", item);
        }

        public static string NotifyInValidFormInput()
        {
            return "Please enter correct information!";
        }

        public static string NotifySureToDelete()
        {
            return "Are you sure you want to delete this item?";
        }

        public static string NotifySureToRestore()
        {
            return "Are you sure you want to restore this item?";
        }

        public static string NotifyFeatureNotDevelop()
        {
            return "The feature is currently under development. We're sorry";
        }

        public static string NotifyAddSuccessfully(string item)
        {
            var message = string.Format("Add new {0} successfully!", item);
            return message;
        }

        public static string NotifyDeleteSuccessfully(string item)
        {
            var message = string.Format("Delete {0} successfully!", item);
            return message;
        }

        public static string NotifyRestoreSuccessfully(string item)
        {
            var message = string.Format("Restore {0} successfully!", item);
            return message;
        }

        public static string NotifyUpdateSuccessfully(string item)
        {
            var message = string.Format("Update {0} successfully!", item);
            return message;
        }

        public static string NotifyDeleteSuccessfullyAdultReader(bool isHasChild)
        {
            string message = string.Empty;
            if (!isHasChild)
            {
                message = string.Format("Delete {0} successfully!", "Adult");
            }
            else
            {
                message = string.Format("Delete {0} and all childs successfully!", "Adult");
            }
            return message;
        }

        public static string NotifyDeleteSuccessfullyParentFunction(bool isHasChild)
        {
            string message = string.Empty;
            if (!isHasChild)
            {
                message = string.Format("Delete {0} successfully!", "Function");
            }
            else
            {
                message = string.Format("Delete {0} and all childs successfully!", "Parent function");
            }
            return message;
        }

        public static string NotifyRestoreSuccessfullyParentFunction(bool isHasParent)
        {
            string message = string.Empty;
            if (!isHasParent)
            {
                message = string.Format("Restore {0} successfully!", "Function");
            }
            else
            {
                message = string.Format("Restore {0} function and child function successfully!", "Parent");
            }
            return message;
        }

        public static string NotifyRestoreSuccessfullyAdultReader(bool isHasAdult)
        {
            string message = string.Empty;
            if (!isHasAdult)
            {
                message = string.Format("Restore {0} successfully!", "child reader");
            }
            else
            {
                message = string.Format("Restore {0} reader and child reader successfully!", "adult");
            }
            return message;
        }

        #endregion

        #region Validation
        // Validation notify
        public static string ValidateNoteFormNotEmptyRule()
        {
            return "This form can not empty!";
        }
        public static string ValidateNoteInputNumberRule()
        {
            return "Please input number only!";
        }
        public static string ValidateNoteInputNumberRangeRule(int? min, int? max)
        {
            string message = string.Empty;

            if (max == null)
            {
                if (min == null)
                {
                    return string.Empty;
                }
                else
                {
                    message = $"Please enter an number greater than {min}.";
                }
            }
            else
            {
                if (min == null)
                {
                    message = $"Please enter an number less than {max}.";
                }
                else
                {
                    message = $"Please enter an number in range: {min} - {max}.";
                }
            }
            return message;
        }
        public static string ValidateNoteInputNumberLengthRangeRule(int? min, int? max)
        {
            string message = string.Empty;

            if (max == null)
            {
                if (min == null)
                {
                    return string.Empty;
                }
                else
                {
                    message = $"Please enter an number in range greater than {min}.";
                }
            }
            else
            {
                if (min == null)
                {
                    message = $"Please enter an number in range less than {max}.";
                }
                else
                {
                    message = $"Please enter an number in range: {min} - {max}.";
                }
            }
            return message;
        }
        public static string ValidateNoteSelectedItemRule()
        {
            return $"Please select item";
        }
        public static string ValidateNoteFixLengthRule(int fixLength)
        {
            return $"Please enter an number with {fixLength} digits.";
        }
        public static string ValidateNoteDateTimeRangeRule(DateTime min)
        {
            return $"Please select a date between {min.ToShortDateString()} and today.";
        }
        public static string ValidateNoteInputUnicodeTextRule()
        {
            return "Please input text only!";
        }
        public static string ValidateNoteInputUnicodeTextNumberRule()
        {
            return "Please input text without special characters!";
        }
        public static string ValidateNoteInputParagraphRule()
        {
            return "Please input text only!";
        }
        public static string ValidateNoteQuantityEqual0()
        {
            return "Please input quantity greater than 0";
        }

        // Validation checkings
        public static bool FormNotEmptyRule(object value)
        {
            string getValue = (string)value;
            if (value != null && !IsCheckEmptyString(getValue))
            {
                return true;
            }
            return false;
        }

        public static bool InputNumberRule(string value)
        {
            foreach (char c in value)
            {
                if (char.IsDigit(c) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool InputNumberRule(string value, char KeyChar)
        {
            if (!char.IsControl(KeyChar) && !char.IsDigit(KeyChar))
            {
                return false;
            }
            return true;
        }

        public static bool InputDecimalRule(string value, char KeyChar)
        {
            if (!char.IsControl(KeyChar) && !char.IsDigit(KeyChar)
                && (KeyChar != '.'))
            {
                return false;
            }

            // only allow one decimal point
            if ((KeyChar == '.') && (value.IndexOf('.') > -1))
            {
                return false;
            }
            return true;
        }

        public static bool FixLengthRule(string value, int fixLength)
        {
            if (value.Length != fixLength)
            {
                return false;
            }
            return true;
        }

        public static bool DateTimeRangeRule(DateTime value, DateTime min, DateTime max)
        {
            if (value >= min && value <= max)
            {
                return true;
            }
            return false;
        }

        public static bool InputUnicodeTextRule(string value, bool isAllowSpace)
        {
            foreach (char c in value)
            {
                if (!InputUnicodeTextRule(c, isAllowSpace))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool InputUnicodeTextNumberRule(string value, bool isAllowSpace)
        {
            foreach (char c in value)
            {
                if (char.IsDigit(c))
                {
                    continue;
                }
                if (!InputUnicodeTextRule(c, isAllowSpace))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool InputUnicodeTextRule(char c, bool isAllowSpace)
        {
            if (isAllowSpace == true && c == ' ')
            {
                return true;
            }
            if (char.IsLetter(c))
            {
                return true;
            }
            return false;
        }

        public static bool InputParagraphRule(string value, bool isAllowSpace, List<char> allowCharacters)
        {
            bool result = value.All((c) =>
            {
                if (allowCharacters.Contains(c))
                {
                    return true;
                }
                if (char.IsDigit(c))
                {
                    return true;
                }
                if (InputUnicodeTextRule(c, isAllowSpace))
                {
                    return true;
                }
                return false;
            });
            return result;
        }
        #endregion

        #region String-Uti

        public static string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        // Check is null, is empty, is str only have WhiteSpace characters
        public static bool IsCheckEmptyString(string value)
        {
            bool result = string.IsNullOrEmpty(value)
                || string.IsNullOrWhiteSpace(value);
            return result;
        }

        public static string ToCapitalizeEachWord(string str)
        {
            // Creates a TextInfo based on the "en-US" culture.
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            string result = textInfo.ToTitleCase(str);
            return result;
        }

        public static string ToCapitalizeInParagragh(string str)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string lipsum2lower = textInfo.ToLower(str);

            string[] lipsum2split = lipsum2lower.Split(' ');

            bool first = true;

            string result = string.Empty;
            foreach (string s in lipsum2split)
            {
                if (first)
                {
                    result += textInfo.ToTitleCase(s);
                    first = false;
                }
                else
                {
                    result += s;
                }
            }
            return result;
        }
        
        #endregion

        #region Control-Layouts
        public static FrameworkElement FindChildInRoot(string name, FrameworkElement root)
        {
            Stack<FrameworkElement> tree = new Stack<FrameworkElement>();
            tree.Push(root);

            while (tree.Count > 0)
            {
                FrameworkElement current = tree.Pop();
                if (current.Name == name)
                    return current;

                int count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(current, i);
                    if (child is FrameworkElement)
                        tree.Push((FrameworkElement)child);
                }
            }

            return null;
        }

        public static TreeViewItem GetTreeViewItem(ItemsControl container, object item)
        {
            if (container != null)
            {
                if (container.DataContext == item)
                {
                    return container as TreeViewItem;
                }

                // Expand the current container
                if (container is TreeViewItem && !((TreeViewItem)container).IsExpanded)
                {
                    container.SetValue(TreeViewItem.IsExpandedProperty, true);
                }

                // Try to generate the ItemsPresenter and the ItemsPanel.
                // by calling ApplyTemplate.  Note that in the
                // virtualizing case even if the item is marked
                // expanded we still need to do this step in order to
                // regenerate the visuals because they may have been virtualized away.

                container.ApplyTemplate();
                ItemsPresenter itemsPresenter =
                    (ItemsPresenter)container.Template.FindName("ItemsHost", container);
                if (itemsPresenter != null)
                {
                    itemsPresenter.ApplyTemplate();
                }
                else
                {
                    // The Tree template has not named the ItemsPresenter,
                    // so walk the descendents and find the child.
                    itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                    if (itemsPresenter == null)
                    {
                        container.UpdateLayout();

                        itemsPresenter = FindVisualChild<ItemsPresenter>(container);
                    }
                }

                Panel itemsHostPanel = (Panel)VisualTreeHelper.GetChild(itemsPresenter, 0);

                // Ensure that the generator for this panel has been created.
                UIElementCollection children = itemsHostPanel.Children;

                MyVirtualizingStackPanel virtualizingPanel =
                    itemsHostPanel as MyVirtualizingStackPanel;

                for (int i = 0, count = container.Items.Count; i < count; i++)
                {
                    TreeViewItem subContainer;
                    if (virtualizingPanel != null)
                    {
                        // Bring the item into view so
                        // that the container will be generated.
                        virtualizingPanel.BringIntoView(i);

                        subContainer =
                            (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);
                    }
                    else
                    {
                        subContainer =
                            (TreeViewItem)container.ItemContainerGenerator.
                            ContainerFromIndex(i);

                        // Bring the item into view to maintain the
                        // same behavior as with a virtualizing panel.
                        subContainer.BringIntoView();
                    }

                    if (subContainer != null)
                    {
                        // Search the next level for the object.
                        TreeViewItem resultContainer = GetTreeViewItem(subContainer, item);
                        if (resultContainer != null)
                        {
                            return resultContainer;
                        }
                        else
                        {
                            // The object is not under this TreeViewItem
                            // so collapse it.
                            subContainer.IsExpanded = false;
                        }
                    }
                }
            }

            return null;
        }

        public static T FindVisualChild<T>(Visual visual) where T : Visual
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual child = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (child != null)
                {
                    T correctlyTyped = child as T;
                    if (correctlyTyped != null)
                    {
                        return correctlyTyped;
                    }

                    T descendent = FindVisualChild<T>(child);
                    if (descendent != null)
                    {
                        return descendent;
                    }
                }
            }

            return null;
        }


        public static void GetLogicalChildCollection<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetLogicalChildCollection(depChild, logicalCollection);
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T) yield return ithChild as T;
            foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
        }
    }

    #endregion

        #region OtherMethods
        public static Brush GetColorFromCode(string colorCode)
        {
            return (new BrushConverter()).ConvertFrom(colorCode) as Brush;
        }

        public static ObservableCollection<T> FillByStatus<T>(ObservableCollection<T> items, bool? statusValue)
            {
                ObservableCollection<T> result = new ObservableCollection<T>();
                foreach (T item in items)
                {
                    PropertyInfo statusProp = item.GetType().GetProperty("Status");
                    if (statusValue == null || (bool)statusProp.GetValue(item) == statusValue)
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
        
        public static T FindInList<T>(IEnumerable<T> source, T findItem)
            {
                return source.FirstOrDefault(item => item.Equals(findItem));
            }

        public static bool Copy<T>(T item1, T item2, params string[] checkProperties)
        {
            bool isAllProperties = false;
            if (checkProperties.Length == 0)
            {
                isAllProperties = true;
            }

            var item1_props = getPropsFromType(item1);
            var item2_props = getPropsFromType(item2);
            for (int i = 0; i < item1_props.Count(); i++)
            {
                PropertyInfo item1_prop = item1_props[i];
                PropertyInfo item2_prop = item2_props[i];

                if (!isAllProperties)
                {
                    var itemCheck = FindPropertyByName(checkProperties.ToList(), item1_prop.Name);
                    if (itemCheck == null)
                    {
                        continue;
                    }
                }
                object value2 = getValueFromProperty(item2_prop, item2);

                if (value2 == null)
                {
                    item1_prop.SetValue(item1, null);
                }
                else
                {
                    item1_prop.SetValue(item1, value2);
                }
            }
            return true;
        }

        public static void MoveComboBox(ComboBox comboBox, KeyEventArgs e)
            {
                if (e.Key == Key.Up)
                {
                    if (comboBox.SelectedIndex > -1)
                    {
                        comboBox.SelectedIndex--;
                    }
                }
                else if (e.Key == Key.Down)
                {
                    if (comboBox.SelectedIndex < comboBox.Items.Count - 1)
                    {
                        comboBox.SelectedIndex++;
                    }
                }
            }

        public static bool IsCheckEnter(TextCompositionEventArgs e)
        {
            char enterKey = Convert.ToChar(13);
            if (e.Text == enterKey.ToString())
            {
                return true;
            }
            return false;
        }

        public static IEnumerable<T> OrderBy<T>(IEnumerable<T> source, PropertyInfo Tkey)
        {
            return source.OrderBy(item =>
            {
                return Tkey.GetValue(item);
            });
        }

        public static object ConvertFromString(string tempValue, Type getType)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(getType);

            object result = typeConverter.ConvertFromString(tempValue);
            return result;
        }

        public static List<int> GetIndex(List<string> first, List<string> second)
        {
            int[] result = new int[first.Count];

            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count; j++)
                {
                    if (first[i] == second[j])
                    {
                        result[j] = i;
                    }
                }
            }
            return result.ToList();
        }

        public static int? GetMax(int a, int b)
        {
            if (a > b)
                return a;
            if (a < b)
                return b;
            return null;
        }

        public static bool IsCheckRange(int value, int? min, int? max)
        {
            bool isCheck = false;

            if (max == null)
            {
                if (min == null)
                {
                    isCheck = true;
                }
                else
                {
                    isCheck = value >= min;
                }
            }
            else
            {
                if (min == null)
                {
                    isCheck = value <= max;
                }
                else
                {
                    isCheck = value >= min && value <= max;
                }
            }
            return isCheck;
        }

        public static void CatchExceptionError()
        {
            MessageBox.Show("Unknown error in the program.", "Error Program", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void SetExceptPropertiesForDataGrid(DataGrid dataGrid, List<PropertyInfo> ExceptProperties)
        {
            foreach (PropertyInfo propItem in ExceptProperties)
            {
                foreach (var col in dataGrid.Columns)
                {
                    if (col is DataGridTextColumn)
                    {
                        var colText = col as DataGridTextColumn;

                        Binding colBinding = colText.Binding as Binding;
                        if (colBinding.Path == null)
                            continue;
                        if (string.Compare(colBinding.Path.Path, propItem.Name) == 0)
                        {
                            col.Visibility = Visibility.Collapsed;
                        }
                    }
                }
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

        public static ObservableCollection<string> GetListAllLanguages()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();

            CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);

            foreach (CultureInfo cul in cinfo)
            {
                result.Add(cul.DisplayName);
            }
            return result;
        }

        public static bool IsExistInformation<T>(ObservableCollection<T> items, T itemCheck, bool igNoreCase = true, params string[] checkProperties)
        {
            foreach (T item in items)
            {
                bool isEqual = IsExistInformation(item, itemCheck, igNoreCase, checkProperties);
                if (isEqual)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsExistInformation<T>(T item, T itemCheck, bool igNoreCase = true, params string[] checkProperties)
        {
            return IsEqual(item, itemCheck, igNoreCase, checkProperties);
        }

        public static bool IsEqual<T>(T item1, T item2, bool igNoreCase = true, params string[] checkProperties)
        {
            var item1_props = getPropsFromType(item1);
            var item2_props = getPropsFromType(item2);
            for (int i = 0; i < item1_props.Count(); i++)
            {
                PropertyInfo item1_prop = item1_props[i];
                PropertyInfo item2_prop = item2_props[i];

                var itemCheck = FindPropertyByName(checkProperties.ToList(), item1_prop.Name);
                if (itemCheck == null)
                {
                    continue;
                }
                object value1 = getValueFromProperty(item1_prop, item1);
                object value2 = getValueFromProperty(item2_prop, item2);

                if (value1 == null && value2 != null)
                {
                    return false;
                }
                if (value1 != null && value2 == null)
                {
                    return false;
                }
               

                if (value1 != null && value2 != null)
                {
                    if (value1 is string && value2 is string)
                    {
                        var tempValue1 = (value1 as string);
                        var tempValue2 = (value2 as string);
                        if (igNoreCase)
                        {
                            tempValue1 = tempValue1.ToLower();
                            tempValue2 = tempValue2.ToLower();
                        }

                        if (!tempValue1.Equals(tempValue2))
                        {
                            return false;
                        }
                        continue;
                    }
                    if (!value1.Equals(value2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static List<PropertyInfo> getPropsFromType(Type type)
            {
                return type.GetProperties().OrderBy(item => item.Name).ToList();
            }

        public static List<PropertyInfo> getPropsFromType(object obj)
        {
            return getPropsFromType(obj.GetType());
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

        public static void SetValueFromProperty(string propName, object item, object value)
        {
            PropertyInfo prop = item.GetType().GetProperty(propName);
            SetValueFromProperty(prop, item, value);
        }

        public static void SetValueFromProperty(PropertyInfo prop, object item, object value)
        {
            prop.SetValue(item, value);
        }

        public static TreeViewItem FindTreeViewItemByName(ItemCollection lst, string name)
        {
            foreach (var item in lst)
            {
                TreeViewItem itemTreeView = item as TreeViewItem;
                if (string.Compare(itemTreeView.Name, name, false) == 0)
                {
                    return itemTreeView;
                }
            }
            return null;
        }

        public static FrameworkElementFactory FindComboBoxItemByName(ItemCollection lst, string name)
        {
            foreach (var item in lst)
            {
                TreeViewItem itemTreeView = item as TreeViewItem;
                if (string.Compare(itemTreeView.Name, name, false) == 0)
                {
                    var dataTemplate = itemTreeView.HeaderTemplate;

                    FrameworkElementFactory gdHeader = dataTemplate.VisualTree as FrameworkElementFactory;
                    var child = gdHeader.FirstChild;
                    for (int i = 0; i < 2; i++)
                    {
                        child = child.NextSibling;
                    }
                    var comboBox = child;
                    return comboBox;
                }
            }
            return null;
        }

        public static bool? IsCheckComboBoxItem(ItemCollection lst)
        {
            foreach (var item in lst)
            {
                TreeViewItem itemTreeView = item as TreeViewItem;

                var dataTemplate = itemTreeView.HeaderTemplate;

                FrameworkElementFactory gdHeader = dataTemplate.VisualTree as FrameworkElementFactory;
                var child = gdHeader.FirstChild;
                for (int i = 0; i < 2; i++)
                {
                    child = child.NextSibling;
                }
                var comboBox = child;

                // comboBox.get

                return null;
                
            }
            return null;
        }

        public static void GetTreeView(TreeView treeView, ObservableCollection<Function> lst, MouseButtonEventHandler implementMethod)
        {
            foreach (Function function in lst)
            {
                TreeViewItem newTreeView = new TreeViewItem();
                newTreeView.Name = function.Id;
                newTreeView.Header = function.Name;
                newTreeView.Tag = function.UrlImage;

                if (IsCheckEmptyString(function.IdParent))
                {
                    newTreeView.MouseLeftButtonUp += implementMethod;
                    treeView.Items.Add(newTreeView);
                    continue;
                }
                else
                {
                    continue;
                    TreeViewItem findedItem = Utilities.FindTreeViewItemByName(treeView.Items, function.IdParent);

                    newTreeView.MouseLeftButtonUp += implementMethod;
                    findedItem.Items.Add(newTreeView);
                }
            }
        }

        public static void GetTreeViewHeaderTemplate(TreeView treeView, ObservableCollection<Function> lst, RoutedEventHandler implementMethod)
        {
            foreach (Function function in lst)
            {
                TreeViewItem newTreeView = new TreeViewItem();
                newTreeView.Name = function.Id;

                DataTemplate dataTemplate = new DataTemplate();

                FrameworkElementFactory gdHeader = new FrameworkElementFactory(typeof(Grid));
                FrameworkElementFactory colDef = null;

                colDef = new FrameworkElementFactory(typeof(ColumnDefinition));
                colDef.SetValue(ColumnDefinition.WidthProperty, new GridLength(0, GridUnitType.Auto));
                gdHeader.AppendChild(colDef);

                colDef = new FrameworkElementFactory(typeof(ColumnDefinition));
                colDef.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
                gdHeader.AppendChild(colDef);

                FrameworkElementFactory chk = new FrameworkElementFactory(typeof(CheckBox));

                Binding newBind = new Binding("IsSelected");
                newBind.Source = newTreeView;
                newBind.Mode = BindingMode.TwoWay;
                newBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                chk.SetBinding(CheckBox.IsCheckedProperty, newBind);

                //chk.SetValue(CheckBox.NameProperty, function.Id);
                chk.SetValue(CheckBox.NameProperty, "myCBox");

                chk.AddHandler(CheckBox.ClickEvent, implementMethod);

                chk.SetValue(Grid.ColumnProperty, 0);


                FrameworkElementFactory tbl = new FrameworkElementFactory(typeof(TextBlock));
                tbl.SetValue(TextBlock.MarginProperty, new Thickness(10, 0, 0, 0));
                tbl.SetValue(TextBlock.TextProperty, function.Name);

                tbl.SetValue(Grid.ColumnProperty, 1);

                gdHeader.AppendChild(chk);
                gdHeader.AppendChild(tbl);

                dataTemplate.VisualTree = gdHeader;
                newTreeView.HeaderTemplate = dataTemplate;

                if (IsCheckEmptyString(function.IdParent))
                {
                    treeView.Items.Add(newTreeView);
                    continue;
                }
                else
                {
                    TreeViewItem findedItem = FindTreeViewItemByName(treeView.Items, function.IdParent);
                    findedItem.Items.Add(newTreeView);
                }
            }
        }

        public static int ExtractNumberFromAString(string str)
        {
            string temp = string.Empty;
            int result = -1;

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    temp += str[i];
            }

            if (temp.Length > 0)
                result = int.Parse(temp);
            return result;
        }
        #endregion

        #region Enum-Uti
        public static IEnumerable<T> GetListFromEnum<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        #endregion

        #region UserControl-Form-Utilities
        public static bool OnClickButtonReaderInfo(ucReadersTable ucReadersTable, bool? adult_Childs_Status, ReaderViewModel readerVM, AdultViewModel adultVM, ChildViewModel childVM, AdultMap adultMap, ChildMap childMap)
        {
            if (ucReadersTable.dgReaders.SelectedIndex == -1)
            {
                ShowMessageBox1(NotifyPleaseSelect("reader"));
                return false;
            }
            ReaderDto readerDtoSelect = ucReadersTable.SelectedReaderDto;

            return Utilities.OnClickButtonReaderInfo(readerDtoSelect, adult_Childs_Status, readerVM, adultVM, childVM, adultMap, childMap);
        }
        public static bool OnClickButtonReaderInfo(ReaderDto readerDtoSelect, bool? adult_Childs_Status, ReaderViewModel readerVM, AdultViewModel adultVM, ChildViewModel childVM, AdultMap adultMap, ChildMap childMap)
        {
            bool? adultStatusValue = null;
            bool? childStatusValue = null;
           
            Reader readerSelect = readerVM.FindById(readerDtoSelect.Id);

            if (readerSelect.ReaderType.ConvertValue() == ReaderType.Adult)
            {
                Adult adultFinded = adultVM.FindByIdReader(readerSelect.Id, adultStatusValue);
                if (adultFinded == null)
                {
                    CatchExceptionError();
                    return false;
                }

                AdultDto adultDtoFinded = adultMap.ConvertToDto(adultFinded);

                frmAdultReaderInformation frmAdultReaderInformation = MainWindow.UnitOfForm.FrmAdultReaderInformation(true);
                frmAdultReaderInformation.getItem = () => adultDtoFinded;
                frmAdultReaderInformation.getReader = () => readerSelect;
                frmAdultReaderInformation.getChilds_Status = () => adult_Childs_Status;

                frmAdultReaderInformation.Show();
            }
            else if (readerSelect.ReaderType.ConvertValue() == ReaderType.Child)
            {
                Child childFinded = childVM.FindByIdReader(readerSelect.Id, childStatusValue);
                if (childFinded == null) return false;

                ChildDto childDtoFinded = childMap.ConvertToDto(childFinded);

                frmChildReaderInformation frmChildInformation = MainWindow.UnitOfForm.FrmChildReaderInformation(true);
                frmChildInformation.getItem = () => childDtoFinded;
                frmChildInformation.getReader = () => readerSelect;

                frmChildInformation.Show();
            }
            return true;
        }

        public static Window CreateDefaultForm()
        {
            Window frmDefault = new Window();
            frmDefault.Style = Application.Current.FindResource(Constants.styleWDGeneral) as Style;
            frmDefault.WindowStartupLocation = Constants.WDLocation;
            return frmDefault;
        }
        #endregion

        #region Xml-Uti
        public static void RemoveAllChilds(XmlNode parentNode)
        {
            XmlNodeList lstChild = parentNode.ChildNodes;
            while (lstChild.Count > 0)
            {
                parentNode.RemoveChild(lstChild[0]);
            }
        }

        private static int getIndexDirectory(string filePath)
        {
            if (!filePath.Contains(".xml"))
            {
                return -1;
            }
            int index = filePath.Length - 1;
            foreach (char c in filePath.Reverse())
            {
                if (c.ToString() == "\\" || c.ToString() == "/")
                {
                    break;
                }
                index--;
            }
            return index;
        }

        public static void CreateXML(string filePath, string rootName)
        {
            string temp_filePath = filePath;
            int indexOf = getIndexDirectory(filePath);
            if (indexOf == -1)
            {
                return;
            }
            string directory = filePath.Remove(indexOf);
            CreateDirectory(directory);

            filePath = temp_filePath;
            if (!File.Exists(filePath))
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                XmlNode root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement element1 = doc.CreateElement(string.Empty, rootName, string.Empty);
                doc.AppendChild(element1);
                try { doc.Save(filePath); }
                catch { }
            }
        }
        #endregion

        #region Directory-Uti
        private static string GetDebugDirectory()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }

        private static string GetProjectDirectory()
        {
            string startupPath = GetDebugDirectory();

            startupPath = Directory.GetParent(startupPath).FullName;
            startupPath = Directory.GetParent(startupPath).FullName;

            return startupPath;
        }

        public static string GetDirectoryImage()
        {
            string startupPath = GetProjectDirectory();
            string appPath = startupPath + Constants.StartUrlImage;
            Utilities.CreateDirectory(appPath);
            
            return appPath;
        }

        public static void SaveFile(string source, string dest, string fileName)
        {
            File.Copy(source, dest + fileName);
        }

        public static string SaveImage(OpenFileDialog openFile)
        {
            string source = openFile.FileName;
            string dest = Utilities.GetDirectoryImage();
            string fileName = openFile.SafeFileName;

            if (!source.Contains(dest))
                SaveFile(source, dest, fileName);

            return Constants.StartUrlImage + fileName;
        }

        public static void RemoveImage(string urlImage)
        {
            if (!urlImage.Contains(Constants.StartUrlImage))
                return;

            string filePath = GetDirectoryImage().Replace(Constants.StartUrlImage, string.Empty) + urlImage;
            DeleteFile(filePath);
        }

        #region Diretory-File
        public static void CreateDirectory(string folderPath)
        {
            // If directory does not exist, create it
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public static void RenameDirectory(string oldName, string newName)
        {
            // If directory does not exist, create it
            if (Directory.Exists(oldName))
            {
                Directory.Move(oldName, newName);
            }
        }

        public static void DeleteDirectory(string folderPath)
        {
            // If directory does not exist, create it
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }

        public static void RenameFile(string oldName, string newName)
        {
            // If directory does not exist, create it
            if (File.Exists(oldName))
            {
                File.Move(oldName, newName);
            }
        }

        public static void DeleteFile(string filePath)
        {
            // If directory does not exist, create it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        #endregion

        #endregion
    }
}