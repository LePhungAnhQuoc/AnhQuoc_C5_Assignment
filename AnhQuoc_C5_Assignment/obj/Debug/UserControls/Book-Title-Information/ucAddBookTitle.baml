   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      *    $AnhQuoc_C5_Assignment.ucAddBookTitle   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5      �� 
�������$	��400��5	      .+�5   
   ��    lblTitle#��[  6   $��	0 20 0 10q�5      $��Book Title Information��5      �� 5   
   $��0��6   $
��20 0q�5        � 	Resources  5       �� 
TargetType  
lblGeneral �� BasedOn  
txtGeneral  	cbGeneral(  "System.Windows.Controls.DatePicker  
dateStyle1  gdDisplayInputForm��%�   '��       7  '�� "     7  '�� D     7  '  f     7  '� �     7  �� 5      " ��6   8   6+   �� 5      " ��6   8   6-   �� 5      " ��6   8   6.   �� 5      "  6   8   60   �� 5      " �6   8   6*   5      .+�5      �  � ColumnDefinitions 5      �� 5      $	��100 �6'   �� 5      $��* �6'   .+�5      �� $
��Id: ��6   �� 5       -   5        $��txtId��5!   !   $��1��5       {�.  ��5"   !   �� *	Item.Id+5#   !   5$      � 5%       5&      �� 5'      $	��100 �6'   �� 5(      $��* �6'   .+�5*      �� $��
Category: ��6   �� 5+      -   5        $��
cbCategory��5,      $��1��5+      $��Stretch=�5-       �� DisplayMemberPath$
 Name��5/        txtErrorTemplate1 =� ErrorTemplate# [ 50      ��5.      �� *
Categories+ �� SelectedItem 51      �� 52       �� Path$ SelectedCategory�6"    �� UpdateSourceTrigger$ PropertyChanged=�6:   	 �� ValidationRules	 53      ,   &AnhQuoc_C5_Assignment.SelectedItemRule  54   "   56      58      59      � 5:       5;      �� 5<      $	��100 �6'   �� 5=      $��* �6'   .+�5?      �� $��Author: ��6   �� 5@      -   5        $��cbAuthor��5A      $��1��5@      $��Stretch=�5B      $
 Name��5D      ��5C      �� *	Authors+ 5E      �� 5F      $ SelectedAuthor�6"   $ PropertyChanged=�68   	 5G        5H   "   5J      5L      5M      � 5N       5O      �� 5P      $	��100 �6'   �� 5Q      $��* �6'   .+�5S      �� $��Name: ��6   �� 5T      -   5        $��txtName��5T      $��1��5U      # [ 5V      .��6[   ��5W      �� 5X      $ 	Item.Name�6"   $ PropertyChanged=�63   	 5Y      ,   &AnhQuoc_C5_Assignment.FormNotEmptyRule  5Z   "   5\      .��5]   $   5^      5_      � 5`       5a      �� 5b      $	��100 �6'   �� 5c      $��* �6'   .+�5e      �� $��	Summary: ��6   �� 5f      -   5        $��
txtSummary��5g      $��1��5f        txtStyleInputBig#��[ 5h      # [ 5i      .��6[   ��5j      �� 5k      $ Item.Summary�6"   $ PropertyChanged=�66   	 5l      5n      .��5o   $   5p      5q      5r      �� 5v   
     stkWrapButton#��[ 6   .+�5w      �� -   5        $��
btnConfirm��5w      $��Confirm��6'   ��5x      �� 5y      " ��6    	 
btnConfirm# [	 60   .�5z      �� 
 �� Property
 � {� �� Value .  66    �� Triggers 5{      ^� 5|       ^� 
Conditions 5}   "   �� 5~   &    �� Binding 60   ��  �� ElementName$ txtName��$ (Validation.HasError)� �� Value	 False6t   0�5�   "   �� 5�   &   
 � {� . 6B   5�      5�      5�      �� 5�      -   5        $��	btnCancel��5�      $��Cancel��6&    
 	btnCancel#��[
 5�      $��10 0 0 0q�5�      5�      5�      5�      