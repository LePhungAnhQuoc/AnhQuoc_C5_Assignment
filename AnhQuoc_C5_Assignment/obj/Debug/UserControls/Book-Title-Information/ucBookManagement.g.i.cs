﻿#pragma checksum "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9FFFE648B71C49EBB49DB8B3C3DD9EC6A4401977"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AnhQuoc_C3_B1;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AnhQuoc_C3_B1 {
    
    
    /// <summary>
    /// ucBookManagement
    /// </summary>
    public partial class ucBookManagement : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbBookISBNs;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtInput;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkFeatures;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border borderBtnAdd;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AnhQuoc_C3_B1.ucAddBook ucAddBook;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AnhQuoc_C3_B1.ucBooksTable ucBooksTable;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AnhQuoc_C3_B1;component/usercontrols/book-title-information/ucbookmanagement.xam" +
                    "l", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\Book-Title-Information\ucBookManagement.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cbBookISBNs = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.txtInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.stkFeatures = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.borderBtnAdd = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.ucAddBook = ((AnhQuoc_C3_B1.ucAddBook)(target));
            return;
            case 7:
            this.ucBooksTable = ((AnhQuoc_C3_B1.ucBooksTable)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

