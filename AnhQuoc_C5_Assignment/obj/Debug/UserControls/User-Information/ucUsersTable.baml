   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      (    "AnhQuoc_C5_Assignment.ucUsersTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      -   5          >�Name$  userControl��5	       � 	Resources 5
      5  /AnhQuoc_C5_Assignment.BoolToVisibilityConverter   boolToVisibilityConverter6   0AnhQuoc_C5_Assignment.IndexDataGridItemConverter  indexDataGridItemConverter/   )AnhQuoc_C5_Assignment.UserStatusConverter  userStatusConverter0   *AnhQuoc_C5_Assignment.ColorStatusConverter  colorStatusConverter3   -AnhQuoc_C5_Assignment.ChildsQuantityConverter  childsQuantityConverter.  (AnhQuoc_C5_Assignment.AdminUserConverter  adminUserConverter-   'AnhQuoc_C5_Assignment.boFToAgeConverter  boFToAgeConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r       5   
   6*     5   
   6+     5   
   6$     5   
   6%     5   
   6(     5   
   6#     5   
   6"   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgDatas��5      $��0��5        
dtgGeneral#��[ 5        dtgColHeaderGeneral   ColumnHeaderStyle# [ 5       	 dtgCellGeneral   	CellStyle# [	 5       
 dtgRowGeneral   RowStyle# [
 5       �� SelectedItem 5      �� *SelectedUserDto+   Columns 5      0	  *System.Windows.Controls.DataGridTextColumn	  5       ,
  &System.Windows.Controls.DataGridColumn 
  Header$ Index��6%   	 
  Width5  /System.Windows.Controls.DataGridLengthConverter$
	 auto 64   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6A   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType)  #System.Windows.Controls.DataGridRow"   �� 	Converter# [ 	  5!      $ Id��6%   $
	 auto 61   
 6>   �� *Id+	  5"      $ Username��6%   $	 * 67   
 6A   �� *
Username+	  5#      $ Password��6%   $	 * 67   
 6A   �� *
Password+	  5%      $ Status��6%   $
	 auto 65     dtgcellStatus 
  	CellStyle# [ 5'   %   
 5&   %   �� *Status+# [ 5'   %   	  5(      $ 	CreatedAt��6%   $
	 auto 68   
 6E   �� *	CreatedAt+ �� StringFormat$ {0:dd/MM/yyyy}��	  5)      $ 
ModifiedAt��6%   $
	 auto 69   
 6F   �� *
ModifiedAt+$ {0:dd/MM/yyyy}��4  .System.Windows.Controls.DataGridTemplateColumn  5+      -   5        
dtgbtnInfo��   CellTemplate 5,      �� 5-      .R�5.      �� $��btnInfo��6%   -   5          btnInfo#��[ 5/   %   $��5q�50   %   52      53      55        57      -   5        dtgbtnUpdate�� 58      �� 59      .R�5:      �� $��	btnUpdate��6%   -   5          	btnUpdate#��[ 5;   %   $��5q�5=   %   {�5<   %   �� *Status+5?      5@      5B        5D      -   5        dtgbtnDelete�� 5E      �� 5F      .R�5G      �� $��	btnDelete��6%   -   5          	btnDelete#��[ 5J   %   $��5q�5K   %   {�5H   %   �� *Status+y�5I   %   �� *Id+# [ 5M      5N      5P        5R      -	   5        dtgbtnRestore�� 5S      �� 5T      .R�5U      �� $��
btnRestore��6%   -
   5        $��5q�5V   %   ��5X   "   �� 5Y   &    �� 
TargetType" ��6,     
btnRestore �� BasedOn# [ 6I    �� Triggers 5Z   *   �� 5[   .    �� Binding 6:   �� *Status+ �� Value True6U   .a�5\   2   ��  �� Property � {� �� Value .  6N   5]   /   �� 5^   .    6:   �� *Status+	 False6U   .a�5_   2   ��  � {� . 6N   5`   /   5b   '   5d      5e      5g      5j      (   "AnhQuoc_C5_Assignment.ucPagination  5k   
   -   5          Name$ ucPagination��5l      $��1��5k      5l      5m      5n      