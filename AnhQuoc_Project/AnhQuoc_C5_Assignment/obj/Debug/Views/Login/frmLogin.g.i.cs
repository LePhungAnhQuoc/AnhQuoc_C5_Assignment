﻿#pragma checksum "..\..\..\..\Views\Login\frmLogin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "42F66F08B00781DCA3390B8C381ED8C882B3439FDA4C84BC0B47BB0989EAE84D"
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
    /// frmLogin
    /// </summary>
    public partial class frmLogin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\Views\Login\frmLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainContent;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Views\Login\frmLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUsername;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\Views\Login\frmLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox boxPassword;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\Views\Login\frmLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid subContent;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\Views\Login\frmLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblForgetPass;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\Views\Login\frmLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel footer;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\..\Views\Login\frmLogin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSignIn;
        
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
            System.Uri resourceLocater = new System.Uri("/AnhQuoc_C5_Assignment;component/views/login/frmlogin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Login\frmLogin.xaml"
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
            
            #line 9 "..\..\..\..\Views\Login\frmLogin.xaml"
            ((AnhQuoc_C5_Assignment.frmLogin)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainContent = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.txtUsername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.boxPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.subContent = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.tblForgetPass = ((System.Windows.Controls.TextBlock)(target));
            
            #line 107 "..\..\..\..\Views\Login\frmLogin.xaml"
            this.tblForgetPass.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.tblForgetPass_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.footer = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.btnSignIn = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            
            #line 129 "..\..\..\..\Views\Login\frmLogin.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TblOpenConnectForm_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

