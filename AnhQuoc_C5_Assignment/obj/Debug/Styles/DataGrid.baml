   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0894   .AnhQuoc_C5_Assignment.IsLoanOutOfDateConverter   isLoanOutOfDateConverter  
dtgGeneral  �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value �� Triggers P� Property P� Value  dtgColHeaderGeneral=  7System.Windows.Controls.Primitives.DataGridColumnHeader  dtgCellGeneral*  $System.Windows.Controls.DataGridCell �� 
TargetType �� Padding J� SnapsToDevicePixels  dtgcellStatus	 �� BasedOn
 �� Binding �� Value  dtgRowGeneral)  #System.Windows.Controls.DataGridRow  dtgRowExpireDateLoan�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      -local#clr-namespace:AnhQuoc_C5_Assignment  5      1System&clr-namespace:System;assembly=mscorlib  5      %
  &	        &	      &	 �    &	 �    &	 �    7  &	 �    &	 r	    7     5      6%   �� 5      "   6   .�5   
   ��  �@ HeadersVisibility$ Column=�6.   �� 5	   
    �@ AutoGenerateColumns .  60   �� 5
   
    �@ 
IsReadOnly . 6'   �� 5   
    � �� �#CCCCCC6(   �� 5   
    � ��$ 1q�6,   �� 5   
    �@ ColumnHeaderHeight$ 40Q�6/   �� 5   
    � ��$ 14(�6%   �� 5   
    � �� �#F5F5F56'   �� 5   
    � ��
 �   �6'   �� 5   
    �@ RowBackground
 �����6*   �� 5   
     �@ AlternatingRowBackground �#F2F2F265   �� 5   
     �@ HorizontalGridLinesBrush �#CCCCCC65   �� 5   
    �@ VerticalGridLinesBrush �#CCCCCC63   �� 5   
    � �� . 6:   �� 5   
    �@ 
IsReadOnly . 6'   �� 5   
    �@ AutoGenerateColumns .  60   �� 5   
    �@ CanUserResizeColumns . 61   �� 5   
    �@ CanUserResizeRows . 6.   �� 5   
    �@ SelectionMode$ Single=�6*   �� 5    
    �@ CanUserDeleteRows .  6.   �� 5!   
    �@ CanUserAddRows .  6+   �� 5"   
    �@ CanUserSortColumns .  6/   �� 5#   
    �@ CanUserReorderColumns .  62   �� 5%   
    � ��$ Stretch=�60    5&   
   P� 5'       �@J�IsMouseOver . . �5(      ��  � �� �#EFEFEF6/   5)      P� 5*       � {� .  . �5+      ��  �@J�Opacity$	 0.5Q�6,   5,      5.      �� 50      "   6(   51      �� 53      "   6#   .�54   
   ��  � ��$ 5q�6$   �� 55   
    � ��
 �����6(   �� 56   
    � ��$ 1q�6,   �� 58   
    � �� 59      �� 5:      "  6"   .R�5;      �� # z��6   #��z��5<      #��z��5=      #��z��5>       . ._�5@      �� # z 5A   !   5B      5C      5E       5G   
   P� 5H       �@ 
IsSelected . . �5I      ��  � �� �#86D1FD6/   5J      5L      �� 5N      "   6"   8	    5O   
   �� 5P      
 6   �� *Status+ True65   .a�5Q      ��  � ��
 � � �6/   5R      �� 5S      
 6   �� *Status+	 False65   .a�5T      ��  � ��
 �  ��6/   5U      5W      �� 5Y      "   6"   .�5Z   
   ��  � ��$ 35��6&   �� 5[   
    � ��$ Center=�6.   �� 5\   
    � ��$	 2,1q�6#   �� 5]   
    � ��$ 14(�6%   �� 5^   
    � ��$
 10,5q�6$   5_      �� 5a      "   6)   8	   .�5i   
   ��  � ��
 �  ��5j      5k      5m      