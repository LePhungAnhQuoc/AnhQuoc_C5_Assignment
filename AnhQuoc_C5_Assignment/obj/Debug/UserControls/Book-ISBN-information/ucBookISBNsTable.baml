   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      ,    &AnhQuoc_C5_Assignment.ucBookISBNsTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter3   -AnhQuoc_C5_Assignment.BookISBNStatusConverter  bookISBNStatusConverter��%:   &	        &	        5
   
   6+     5   
   6(   5      .��5      &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5      ��5      �� *Books+ �� SelectedItem 5      �� *SelectedDto+   Columns 5   
   0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6!      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� 	 �� 	Converter#	 [    5      $
 ISBN��6!   $
 auto 6/    6<   �� *ISBN+  5      $ 
Book Title��6!   $ * 65    6?   �� *BookTitle.Name+  5      $ Author��6!   $
 auto 61    6>   �� *Author.Name+  5      $ Publish Date��6!   $
 auto 67    6D   �� *PublishDate+
 �� StringFormat$
 {0:dd/MM/yyyy}��  5      $ Language��6!   $
 auto 63    6@   �� *
Language+  5      $ Books Quantity��6!   $
 auto 69    6F   �� *Books.Count+  5      $ Status��6!   $
 auto 61     dtgcellStatus   	CellStyle# [ 5   !    5   !   �� *Status+#	 [ 5   !   4  .System.Windows.Controls.DataGridTemplateColumn  5      -   5        
dtgBtnInfo��   CellTemplate 5       �� 5!      .R�5"      �� $��btnInfo��6!   -   5          btnInfo#��[ 5#   !   $��5q�5$   !   5&      5'      5)        5*      -   5        dtgBtnDelete��   
Visibility$ 	Collapsed=�5+   %    5,      �� 5-      .R�5.      �� $��	btnDelete��6!   -   5          	btnDelete#��[ 5/   !   $��5q�51   !   {�50   !   �� *Status+53      54      56      58      59      