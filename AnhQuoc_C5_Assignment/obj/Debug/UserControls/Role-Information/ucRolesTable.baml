   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      (    "AnhQuoc_C5_Assignment.ucRolesTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter   indexDataGridItemConverter/  )AnhQuoc_C5_Assignment.RoleStatusConverter  roleStatusConverter0   *AnhQuoc_C5_Assignment.ColorStatusConverter  colorStatusConverter3   -AnhQuoc_C5_Assignment.ChildsQuantityConverter  childsQuantityConverter-   'AnhQuoc_C5_Assignment.boFToAgeConverter  boFToAgeConverter��%�   &	        &	      &	 &     &	 9     &	 L       5
   
   6+     5   
   6$     5   
   6%     5   
   6(     5   
   6"   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5      $��0��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5        dtgCellGeneral   	CellStyle# [ 5        dtgRowGeneral   RowStyle# [ 5       �� SelectedItem 5      �� *SelectedRoleDto+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Index��6%      Width5	  /System.Windows.Controls.DataGridLengthConverter$
 auto	 64   1
  +System.Windows.Controls.DataGridBoundColumn	 
  Binding	 6A   �� 
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� 	Converter# [    5      $ Id��6%   $
 auto	 61   	 6>   �� *Id+  5      $
 Name��6%   $ *	 63   	 6=   �� *Name+  5       $ Group��6%   $
 auto	 64   	 6A   �� *Group+  5!      $ Status��6%   $
 auto	 65    	 dtgcellStatus   	CellStyle# [	 5#   %   	 5"   %   �� *Status+# [ 5#   %   4  .System.Windows.Controls.DataGridTemplateColumn  5$      -   5        dtgbtnDelete��   CellTemplate 5%      �� 5&      .R�5'      �� $��	btnDelete��6%   -   5        $��5q�5)   %   {�5(   %   �� *Status+��5+   "   �� 5,   &    �� 
TargetType" ��6,    
 	btnDelete �� BasedOn# [
 6@    �� Triggers 5-   *   �� 5.   .    �� Binding 6:   �� *Id+ �� Value R16Q   .a�5/   2   ��  �� Property � y� �� Value$ Hidden=�6O   50   /   52   '   54      55      57        58      -   5        dtgbtnUpdate�� 59      �� 5:      .R�5;      �� $��	btnUpdate��6%   -   5        $��5q�5=   %   {�5<   %   �� *Status+��5?   "   �� 5@   &   " ��6,     	btnUpdate# [ 6@    5A   *   �� 5B   .    6:   �� *Id+ R16Q   .a�5C   2   ��  � y�$ Hidden=�6O   5D   /   5F   '   5H      5I      5K      5M      (   "AnhQuoc_C5_Assignment.ucPagination  5O   
   -   5          Name$ ucPagination��5P      $��1��5O      5P      5Q      5R      