﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucLoanSlipConfirm.xaml
    /// </summary>
    public partial class ucLoanSlipConfirm : UserControl
    {
        public ucLoanSlipConfirm()
        {
            InitializeComponent();
            this.DataContext = MainWindow.borrowBookContext;
        }
    }
}