﻿#pragma checksum "..\..\..\..\Views\Login\frmInputServerName.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68BD63942CC734121159F5865047775336CCEC1A"
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
    /// frmInputServerName
    /// </summary>
    public partial class frmInputServerName : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\Views\Login\frmInputServerName.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkInputInformation;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Views\Login\frmInputServerName.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbServerName;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\Login\frmInputServerName.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtServerName;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\Login\frmInputServerName.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbDatabaseName;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\Views\Login\frmInputServerName.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDatabaseName;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\Views\Login\frmInputServerName.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkRememberMe;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Views\Login\frmInputServerName.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConfirm;
        
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
            System.Uri resourceLocater = new System.Uri("/AnhQuoc_C5_Assignment;component/views/login/frminputservername.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Login\frmInputServerName.xaml"
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
            this.stkInputInformation = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.cbServerName = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.txtServerName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cbDatabaseName = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.txtDatabaseName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.chkRememberMe = ((System.Windows.Controls.CheckBox)(target));
            
            #line 80 "..\..\..\..\Views\Login\frmInputServerName.xaml"
            this.chkRememberMe.Checked += new System.Windows.RoutedEventHandler(this.chkRememberMe_CheckedChanged);
            
            #line default
            #line hidden
            
            #line 81 "..\..\..\..\Views\Login\frmInputServerName.xaml"
            this.chkRememberMe.Unchecked += new System.Windows.RoutedEventHandler(this.chkRememberMe_CheckedChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnConfirm = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

