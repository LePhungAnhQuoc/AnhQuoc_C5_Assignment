   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      ,    &AnhQuoc_C5_Assignment.ucFunctionsTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      -   5        $��userControl��5	        � 	Resources  5
      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter3   -AnhQuoc_C5_Assignment.FunctionStatusConverter  functionStatusConverter0   *AnhQuoc_C5_Assignment.ColorStatusConverter  colorStatusConverter3   -AnhQuoc_C5_Assignment.ChildsQuantityConverter  childsQuantityConverter-   'AnhQuoc_C5_Assignment.boFToAgeConverter  boFToAgeConverter��%�   &	        &	      &	 &     &	 9     &	 L       5   
   6+     5   
   6(     5   
   6%     5   
   6(     5   
   6"   5      .��5      � -   5        $��gdTable��5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5      $��0��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5       �� SelectedItem 5      �� *SelectedFunctionDto+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6%      Width5	  /System.Windows.Controls.DataGridLengthConverter$
 auto	 64   1
  +System.Windows.Controls.DataGridBoundColumn	 
  Binding	 6A   �� 
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� 	Converter# [    5      $ Id��6%   $
 auto	 61   	 6>   �� *Id+  5       $
 Name��6%   $
 auto	 63   	 6@   �� *Name+  5!      $ Description��6%      MinWidth$	 150Q�6:      MaxWidth$	 200Q�6I   	 6X   �� *Description+  5"      $ Parent��6%   $	 150Q�65   $	 200Q�6D   	 6S   �� *Parent.Name+4  .System.Windows.Controls.DataGridTemplateColumn  5#      $ UrlImage��6)   $
 auto	 6;      CellTemplate 5$      �� 5%      .R�5&      �� $��Center=�6(   $��5q�5'   )   $��30��5(   )   $��50��5)   )    �� Stretch$
 Fill=�5*   )   ��5+   "   �� 5,   &    �� ImageSource 61   �� *
UrlImage+5.      5/      51        53      $ Status��6%   $
 auto	 65    	 dtgcellStatus   	CellStyle# [	 55   %   	 54   %   �� *Status+# [ 55   %     56      -   5        dtgbtnUpdate�� 57      �� 58      .R�59      �� $��	btnUpdate��6%   -   5         
 	btnUpdate#��[
 5:   %   $��5q�5<   %   {�5;   %   �� *Status+5>      5?      5A        5B      -   5        dtgbtnDelete�� 5C      �� 5D      .R�5E      �� $��	btnDelete��6%   -   5        $��5q�5G   %   {�5F   %   �� *Status+��5I   "   �� 5J   &    �� 
TargetType" ��6,     	btnDelete �� BasedOn# [ 6I    �� Triggers 5K   *   �� 5L   .    �� Binding 6:   �� *	IsAdmin+ �� Value True6V   .a�5M   2   ��  �� Property � y� �� Value$ Hidden=�6O   5N   /   5P   '   5R      5S      5U        5V      -   5        dtgbtnRestore�� 5W      �� 5X      .R�5Y      �� $��
btnRestore��6%   -	   5        $��5q�5Z   %   ��5\   "   �� 5]   &   " ��6,     
btnRestore# [ 6I    5^   *   �� 5_   .    6:   �� *Status+ True6U   .a�5`   2   ��  � {� .  6N   5a   /   �� 5b   .    6:   �� *Status+	 False6U   .a�5c   2   ��  � {� . 6N   5d   /   5f   '   5h      5i      5k      5m      (   "AnhQuoc_C5_Assignment.ucPagination  5n   
   -
   5          Name$ ucPagination��5o      $��1��5n      5o      5p      5q      