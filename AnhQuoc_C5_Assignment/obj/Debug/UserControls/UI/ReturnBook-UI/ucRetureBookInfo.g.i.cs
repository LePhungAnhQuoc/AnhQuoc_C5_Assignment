﻿#pragma checksum "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D6834E74D7208E85745F986E076E8ACB1D152566"
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
    /// ucRetureBookInfo
    /// </summary>
    public partial class ucRetureBookInfo : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gdInputLoanHistory;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkBookDetailInformation;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtIdReader;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtIdLoan;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gdInputReason;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbTxtReason;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtReason;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNote;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFineAmount;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFinish;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReturnBook;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel wrapUnPaidBooksTable;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel wrapPaidBooksTable;
        
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
            System.Uri resourceLocater = new System.Uri("/AnhQuoc_C5_Assignment;component/usercontrols/ui/returnbook-ui/ucreturebookinfo.x" +
                    "aml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UserControls\UI\ReturnBook-UI\ucRetureBookInfo.xaml"
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
            this.gdInputLoanHistory = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.stkBookDetailInformation = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.txtIdReader = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtIdLoan = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.gdInputReason = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.cbTxtReason = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.txtReason = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtNote = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtFineAmount = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.btnFinish = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.btnReturnBook = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.wrapUnPaidBooksTable = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 13:
            this.wrapPaidBooksTable = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

