﻿#pragma checksum "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6296E33406F15A9B9ED118E4BDB1545C88D398BD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AnhQuoc_C5_Assignment;
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


namespace AnhQuoc_C5_Assignment {
    
    
    /// <summary>
    /// ucAddBookISBN
    /// </summary>
    public partial class ucAddBookISBN : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtISBN;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbBookTitle;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbAuthor;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePublishDate;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbLanguage;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConfirm;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/AnhQuoc_C5_Assignment;component/usercontrols/book-isbn-information/ucaddbookisbn" +
                    ".xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\Book-ISBN-information\ucAddBookISBN.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.txtISBN = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cbBookTitle = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.cbAuthor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.datePublishDate = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.cbLanguage = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.btnConfirm = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
