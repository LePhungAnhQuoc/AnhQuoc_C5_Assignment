   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      ,    &AnhQuoc_C5_Assignment.ucBookISBNsTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter3   -AnhQuoc_C5_Assignment.BookISBNStatusConverter  bookISBNStatusConverter��%:   &	        &	        5
   
   6+     5   
   6(   5      .��5      &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5      ��5      �� *Books+ �� SelectedItem 5      �� *SelectedDto+   Columns 5   
   0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6!      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� 	 �� 	Converter#	 [    5      $
 ISBN��6!   $
 auto 6/    6<   �� *ISBN+  5      -   5        dtgTitle��$ Title��5   3   $ * 6B    6L   �� *BookTitle.Name+  5      $ Author��6!   $
 auto 61    6>   �� *Author.Name+  5      $ Origin Language��6!   $
 auto 6:    6G   �� *OriginLanguage+  5      $ Books Quantity��6!   $
 auto 69    6F   �� *Books.Count+  5      $ Status��6!   $
 auto 61     dtgcellStatus
   	CellStyle#
 [ 5   !    5   !   �� *Status+#	 [ 5   !   4  .System.Windows.Controls.DataGridTemplateColumn  5       -   5        
dtgbtnInfo��   CellTemplate 5!      �� 5"      .R�5#      �� $��btnInfo��6!   -   5          btnInfo#��[ 5$   !   $��5q�5%   !   5'      5(      5*        5+      -   5        dtgbtnDelete��   
Visibility$ 	Collapsed=�5,   %    5-      �� 5.      .R�5/      �� $��	btnDelete��6!   -   5          	btnDelete#��[ 50   !   $��5q�52   !   {�51   !   �� *Status+54      55      57      59      5:      