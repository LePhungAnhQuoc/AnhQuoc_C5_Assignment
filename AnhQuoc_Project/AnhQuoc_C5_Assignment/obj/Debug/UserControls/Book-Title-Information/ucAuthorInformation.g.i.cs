﻿#pragma checksum "..\..\..\..\UserControls\Book-Title-Information\ucAuthorInformation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EBA6A0B1C8224B663B5C87C0D145BAF86EC45CA48DA3F0E7366A722CF8D66046"
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
    /// ucAuthorInformation
    /// </summary>
    public partial class ucAuthorInformation : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\UserControls\Book-Title-Information\ucAuthorInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainContent;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\..\UserControls\Book-Title-Information\ucAuthorInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkWrapButton;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\UserControls\Book-Title-Information\ucAuthorInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
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
            System.Uri resourceLocater = new System.Uri("/AnhQuoc_C5_Assignment;component/usercontrols/book-title-information/ucauthorinfo" +
                    "rmation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\Book-Title-Information\ucAuthorInformation.xaml"
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
            this.mainContent = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 62 "..\..\..\..\UserControls\Book-Title-Information\ucAuthorInformation.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 81 "..\..\..\..\UserControls\Book-Title-Information\ucAuthorInformation.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.stkWrapButton = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
