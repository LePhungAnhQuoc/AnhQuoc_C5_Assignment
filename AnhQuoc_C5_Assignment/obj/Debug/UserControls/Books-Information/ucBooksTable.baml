   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      (    "AnhQuoc_C5_Assignment.ucBooksTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter/   )AnhQuoc_C5_Assignment.BookStatusConverter  bookStatusConverter��%:   &	        &	        5
   
   6+     5   
   6$   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgBooks��5      $��0��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5       �� SelectedItem 5      �� *SelectedDto+��5      �� *Books+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 64   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6A   �� 
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� 	Converter# [    5      $ Id��6%   $
 auto 61   	 6>   �� *Id+  5      $
 ISBN��6%   $
 auto 63   	 6@   �� *ISBN+  5      $ Title��6%   $
 auto 64   	 6A   �� *Title+  5       $ Category��6%   $
 auto 67   	 6D   �� *
Category+  5!      $ Author��6%   $
 auto 65   	 6B   �� *Author+  5"      $ 
Translator��6%   $
 auto 69   	 6F   �� *
Translator+  5#      $ Language��6%   $
 auto 67   	 6D   �� *
Language+  5$      $ 	Publisher��6%   $
 auto 68   	 6E   �� *Publisher.Name+  5%      $ Publish Date��6%   $
 auto 6;   	 6H   �� *PublishDate+ �� StringFormat$ {0:dd/MM/yyyy}��  5&      $ Price��6%   $
 auto 64   	 6A   �� *Price+$ {0:#,#.000} đ��  5'      $ PriceCurrent��6%   $
 auto 6;   	 6H   �� *PriceCurrent+$ {0:#,#.000} đ��  5)      $ Status��6%   $
 auto 65     dtgcellStatus   	CellStyle# [ 5+   %   	 5*   %   �� *Status+# [ 5+   %     5-      $ 	CreatedAt��6%   $
 auto 68   	 6E   �� *	CreatedAt+$ {0:dd/MM/yyyy}��  5.      $ 
ModifiedAt��6%   $
 auto 69   	 6F   �� *
ModifiedAt+$ {0:dd/MM/yyyy}��4	  .System.Windows.Controls.DataGridTemplateColumn	  50         
Visibility$ 	Collapsed=�6)    	  CellTemplate 51      �� 52      .R�53      �� $��	btnDelete��6%   -   5          	btnDelete#��[ 54   %   $��5q�56   %   {�55   %   �� *Status+58      59      5;      5=      (
   "AnhQuoc_C5_Assignment.ucPagination
  5>   
   -   5         
 Name$ ucPagination��5?      $��1��5>      5?      5@      5A      