   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      )    #AnhQuoc_C5_Assignment.ucChildsTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter0   *AnhQuoc_C5_Assignment.ChildStatusConverter  childStatusConverter��%:   &	        &	        5
   
   6+     5   
   6%   5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5      ��5      �� *	ChildDtos+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 64   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   ��  �� RelativeSource �� *FindAncestor+	 �� AncestorType)  #System.Windows.Controls.DataGridRow"	  
 �� 	Converter#
 [    5      $ 	Reader Id��6%   $
 auto 68    6E   �� *
IdReader+  5      $
 Name��6%   $ * 63    6=   �� *
FullName+  5      $ Adult��6%   $ * 64    6>   �� *	AdultName+  5      $ Status��6%   $
 auto 65     dtgcellStatus   	CellStyle# [ 5   %    5   %   �� *Status+#
 [ 5   %     5      $ 	CreatedAt��6%   $
 auto 68    6E   �� *	CreatedAt+ �� StringFormat$ {0:dd/MM/yyyy}��  5      $ 
ModifiedAt��6%   $
 auto 69    6F   �� *
ModifiedAt+$ {0:dd/MM/yyyy}��4	  .System.Windows.Controls.DataGridTemplateColumn	  5          
Visibility$ 	Collapsed=�6)    	  CellTemplate 5!      �� 5"      .R�5#      �� $��	btnDelete��6%   -   5          	btnDelete#��[ 5$   %   $��5q�5&   %   {�5%   %   �� *Status+5(      5)      5+      	  5,      $ 	Collapsed=�6)    5-      �� 5.      .R�5/      �� $��btnInfo��6%   -   5          btnInfo#��[ 50   %   $��5q�51   %   53      54      56      58      59      5:      