   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      4    .AnhQuoc_C5_Assignment.ucRoleFunctionManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5        � 	Resources  5	      0   *AnhQuoc_C5_Assignment.InverseBoolConverter   inverseBoolConverter��%   &	          5
   
   6%   5      .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      ��   lblTitle#��[ 6   $��Role Functions Management��5      � 5      $��30 0 0 0q�6   $
��Left=�5       � ColumnDefinitions 5      �� 5      $
��auto �6'   �� 5      $
��auto �6'   .+�5      � $��0��6   .+�5      �� -   5        $��	txtSearch��5        txtStyleSearch#��[ 5       �� 5!      $��Search by name here...��6    ��5"      �� 5#       �� 
TargetType" ��6$     tblPlaceHolder �� BasedOn# [ 6;   .�5$   "   ��  �� Property � y� �� Value$ Hidden=�6?    �� Triggers 5%   "   �� 5&   &    �� Binding 62   �� 	 �� ElementName$	 	txtSearch��
 �� Path$

 Text� �� Value  6g   .a�5'   *   ��  � y�$ Visible=�6G   5(   '   5*      5,      5-      �� 5.      $��1��6    �� Orientation$ 
Horizontal=�5/      $��30 0 0 0q�50      51      52      53      � 56   
   $��1��6   $��0 30 0 0q�57       58      �� 59      $��* �6#   �� 5:      $��* �6#   .+�5=      (   "AnhQuoc_C5_Assignment.ucRolesTable  -   5          Name$ ucRolesTable��5>   !   $��0��5=   !     ucStyleVertical#��[ 5?   !   $��0q�5@   !   $
��auto��5A   !      AllowPagination .     NumberItems$ 7��5C   !   � 5D      -   5        $��gdListFeature��5D      $��1��5E      {�.  ��5F      Z�  5H      �� 5I      $��* �6$   �� 5J      $
��auto �6$   .+�5L      R� -   5         R�Name$ tvFunctions��5M      $��0��5L      ��5N      Z� ��5O      �� *	Functions+  5P       �� DataType'   !AnhQuoc_C5_Assignment.FunctionDto �� ItemsSource
 � Tag d� 	IsChecked �� Mode �� UpdateSourceTrigger,   &AnhQuoc_C5_Assignment.FunctionChildDto �� 	Converter��%9  (��       5Q   3   *, +)(�� �    5f   '   *, +)7   �� 5Q      "  63    5R   3   �� *Childs+.R�5S      �� -   5         5S   '   �� *Id+��5T   '   �� *Name+ 5U   '   �� $"
 (local:ItemHelper.IsChecked)�$ TwoWay=�$ PropertyChanged=���5Y   "   �� 5Z   &   " ��6,   .�5[   *   ��  � ��
 �   �6G   �� 5\   *    � y�$ Visible=�6G    5]   *   �� 5^   .    6:   �� $"
 (local:ItemHelper.IsChecked)�	 False6p   .a�5_   2   ��  � ��
 �����6O   5`   /   5b   '   5d      5e      �� 5f      "  6'   .R�5g      �� -   5         5g   '   �� *Id+��5h   '   �� *Name+{�5i   '   �� *IsSeal+8    5j   '   �� $"
 (local:ItemHelper.IsChecked)�$ TwoWay=�$ PropertyChanged=���5n   "   �� 5o   &   " ��6,   .�5p   *   ��  � ��
 �   �6G   �� 5q   *    � y�$ Visible=�6G    5r   *   �� 5s   .    6:   �� $"
 (local:ItemHelper.IsChecked)�	 False6p   .a�5t   2   ��  � ��
 �����6O   5u   /   5w   '   5y      5z      5{      ��5|      �� 5}      " Q�6    .�5~      ��  �@Q�
IsExpanded . 6;   5      5�      �� 5�      $��1��6     stkWrapButton#��[ 5�      .+�5�      �� -   5        $��btnSaveFunc��5�        
btnConfirm#��[ 5�      $��80��5�      �� 5�      -   5        $��btnResetFunc��5�        btnReset#��[ 5�      $��80��5�      $��10 0 0 0q�5�      �� 5�      -	   5        $��btnCancelFunc��5�        	btnCancel#��[ 5�      $��80��5�      $��10 0 0 0q�6(   5�      5�      5�      5�      5�      