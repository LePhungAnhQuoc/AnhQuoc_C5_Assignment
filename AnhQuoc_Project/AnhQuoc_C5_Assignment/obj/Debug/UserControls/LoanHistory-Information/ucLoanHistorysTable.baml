   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      /    )AnhQuoc_C5_Assignment.ucLoanHistorysTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter0   *AnhQuoc_C5_Assignment.PriceFormatConverter  PriceFormatConverter7   1AnhQuoc_C5_Assignment.IdReaderToFullNameConverter  IdReaderToFullNameConverter��%W   &	        &	      &	 &       5
   
   6+     5   
   6%     5   
   6,   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5      $��0��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5       �� SelectedItem 5      �� *SelectedDto+��5      �� *Books+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 64   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6A   �� 
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType)	  #System.Windows.Controls.DataGridRow" 	  �� 	Converter# [    5      $ Id��6%   $
 auto 61   	 6>   �� *Id+  5      $ Reader��6%   $ * 65   	 6?   �� *
IdReader+# [   5       $ Quantity��6%   $
 auto 67   	 6D   �� *
Quantity+  5!      $ 	Loan Date��6%   $
 auto 68   	 6E   �� *
LoanDate+ �� StringFormat$ {0:dd/MM/yyyy}��  5"      $ Expire Date��6%   $
 auto 6:   	 6G   �� *	ExpDate+$ {0:dd/MM/yyyy}��  5$      $ 	Loan Paid��6%   $
 auto 68   	 6E   �� *
LoanPaid+# [   5%      $ Deposit��6%   $
 auto 66   	 6C   �� *	Deposit+# [ 4
  .System.Windows.Controls.DataGridTemplateColumn
  5'      -   5        
dtgBtnInfo�� 
  CellTemplate 5(      �� 5)      .R�5*      �� $��btnInfo��6%   -   5          btnInfo#��[ 5+   !   $��5q�5,   !   5.      5/      51      
  53      -   5        dtgbtnDelete�� 54      �� 55      .R�56      �� $��	btnDelete��6%   -   5          	btnDelete#��[ 57   %   $��5q�59   %   {�58   %   �� *Status+5;      5<      5>      5@      (   "AnhQuoc_C5_Assignment.ucPagination  5A   
   -   5          Name$ ucPagination��5B      $��1��5A      5B      5C      5D      