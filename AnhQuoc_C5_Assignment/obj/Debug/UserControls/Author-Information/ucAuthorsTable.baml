   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      *    $AnhQuoc_C5_Assignment.ucAuthorsTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter1   +AnhQuoc_C5_Assignment.AuthorStatusConverter  authorStatusConverter��%:   &	        &	        5
   
   6+     5   
   6&   5      .��5      &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5      ��5      �� *	Authors+ �� SelectedItem 5      �� *SelectedDto+   Columns 5   
   0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6!      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� 	 �� RelativeSource	 �� *FindAncestor+
 �� AncestorType)  #System.Windows.Controls.DataGridRow"
   �� 	Converter# [    5      $ Id��6!   $
 auto 6-    6:   �� *Id+  5      $
 Name��6!   $ * 6/    69   �� *Name+  5      $ Description��6!      MinWidth$	 150Q�66      MaxWidth$	 200Q�6E    6T   �� *Description+  5      $	 boF��6!   $
 auto 6.    6;   �� *boF+ �� StringFormat$ {0:dd/MM/yyyy}��  5      $ Summary��6!   $	 150Q�62   $	 200Q�6A    6P   �� *	Summary+  5      $ Status��6!   $
 auto 61      
Visibility$ 	Collapsed=�6�    6>   �� *Status+# [ 6�     5      $ 
Created At��6!   $
 auto 65   $ 	Collapsed=�6`    6B   �� *	CreatedAt+6`     5      $ Modified At��6!   $
 auto 66   $ 	Collapsed=�6b    6C   �� *
ModifiedAt+6b   5      5       