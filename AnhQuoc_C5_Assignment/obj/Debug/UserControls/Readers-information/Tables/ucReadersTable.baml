   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      *    $AnhQuoc_C5_Assignment.ucReadersTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter1   +AnhQuoc_C5_Assignment.ReaderStatusConverter  readerStatusConverter0   *AnhQuoc_C5_Assignment.ColorStatusConverter  colorStatusConverter3   -AnhQuoc_C5_Assignment.ChildsQuantityConverter  childsQuantityConverter-   'AnhQuoc_C5_Assignment.boFToAgeConverter  boFToAgeConverter��%�   &	        &	      &	 &     &	 9     &	 L       5
   
   6+     5   
   6&     5   
   6%     5   
   6(     5   
   6"   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��	dgReaders��5      $��0��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5       �� SelectedItem 5      �� *SelectedReaderDto+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6%      Width5	  /System.Windows.Controls.DataGridLengthConverter$
 auto	 64   1
  +System.Windows.Controls.DataGridBoundColumn	 
  Binding	 6A   �� 
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� 	Converter# [    5      $ Id��6%   $
 auto	 61   	 6>   �� *Id+  5          
Visibility$ 	Collapsed=�6%   $ 	Last Name��5!   %   $
 auto	 68   	 6E   �� *LName+  5"      $ 	Collapsed=�6%   $ 
First Name��5#   %   $
 auto	 69   	 6F   �� *FName+  5%      $
 Name��6%   $
 auto	 63   	 6@   �� *
FullName+  5&      $	 BOF��6%   $ 	Collapsed=�62   $
 auto	 5'   %   	 62   �� *boF+ �� StringFormat$ {0:dd/MM/yyyy}��  5(      $	 Age��6%   $
 auto	 62   	 6?   �� *boF+# [   5)      $
 Type��6%   $
 auto	 63   	 6@   �� *
ReaderType+  5*      $ Status��6%   $
 auto	 65    	 dtgcellStatus   	CellStyle# [	 5,   %   	 5+   %   �� *Status+# [ 5,   %     5-      $ 	CreatedAt��6%   $ 	Collapsed=�68   $
 auto	 5.   %   	 62   �� *	CreatedAt+$ {0:dd/MM/yyyy}��  5/      $ 
ModifiedAt��6%   $ 	Collapsed=�69   $
 auto	 50   %   	 62   �� *
ModifiedAt+$ {0:dd/MM/yyyy}��  52      $ 
Childs Qty��6%   $
 auto	 69   	 6F   �� *ChildsQuantity+# [ 4  .System.Windows.Controls.DataGridTemplateColumn  54      -   5        
dtgbtnInfo��   CellTemplate 55      �� 56      .R�57      �� $��btnInfo��6%   -   5         
 btnInfo#��[
 58   %   $��5q�59   %   5;      5<      5>        5?      -   5        dtgbtnUpdate�� 5@      �� 5A      .R�5B      �� $��	btnUpdate��6%   -   5          	btnUpdate#��[ 5C   %   $��5q�5E   %   {�5D   %   �� *Status+5G      5H      5J        5K      -   5        dtgbtnDelete�� 5L      �� 5M      .R�5N      �� $��	btnDelete��6%   -   5          	btnDelete#��[ 5O   %   $��5q�5Q   %   {�5P   %   �� *Status+5S      5T      5V        5W      -   5        dtgbtnRestore�� 5X      �� 5Y      .R�5Z      �� $��
btnRestore��6%   -	   5        $��5q�5[   %   ��5]   "   �� 5^   &    �� 
TargetType" ��6,     
btnRestore �� BasedOn# [ 6I    �� Triggers 5_   *   �� 5`   .    �� Binding 6:   �� *Status+ �� Value True6U   .a�5a   2   ��  �� Property � {� �� Value .  6N   5b   /   �� 5c   .    6:   �� *Status+	 False6U   .a�5d   2   ��  � {� . 6N   5e   /   5g   '   5i      5j      5l        5m      -
   5        dtgbtnTransferGuardian�� 5n      �� 5o      .R�5p      �� $��btnTransferGuardian��6%   -   5        $��5q�5q   %   $��Transfer guardian��5r   %     btnTransferGuardian#��[ 5t   %   5u      5v      5x      5z      (   "AnhQuoc_C5_Assignment.ucPagination  5{   
   -   5          Name$ ucPagination��5|      $��1��5{      5|      5}      5~      