   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      -    'AnhQuoc_C5_Assignment.ucBookTitlesTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter��%   &	          5
   
   6+   5      .��5      &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5      ��5      �� *
BookTitles+   Columns 5   
   0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6!      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   ��  �� RelativeSource �� *FindAncestor+	 �� AncestorType)  #System.Windows.Controls.DataGridRow"	  
 �� 	Converter#
 [    5      $ Id��6!   $
 auto 6-    6:   �� *Id+  5      $ Category��6!   $
 auto 63    6@   �� *Category.Name+  5      $
 Name��6!   $ * 6/    69   �� *Name+  5      $ Author��6!   $
 auto 61    6>   �� *Author.Name+  5      $ Summary��6!      MinWidth$	 150Q�62      MaxWidth$	 200Q�6A    6P   �� *	Summary+  5      $ Book ISBN Quantity��6!   $
 auto 6=    6J   �� *BookISBNs.Count+5      5      