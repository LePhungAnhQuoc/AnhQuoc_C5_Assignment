   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      %    AnhQuoc_C5_Assignment.ucAddBook   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      ��   �� Orientation$  
Horizontal=�6   .+�5
   
   �� $  
Horizontal=�6   .+�5      � .+�5      �� -   5        $��cbBookISBNs��5         cbStyle2#��[  5       �� DisplayMemberPath$
 ISBN��5        txtErrorTemplate1 =� ErrorTemplate# [ 5      ��5      �� *	BookISBNs+ �� SelectedItem 5      �� 5       �� Path$ SelectedBookISBN�6"    �� UpdateSourceTrigger$ PropertyChanged=�6:    �� ValidationRules 5      ,   &AnhQuoc_C5_Assignment.SelectedItemRule  5   "   5      5      �� 5      $��Select Book ISBN��6   ��5      �� 5       �� 
TargetType" ��6      tblPlaceHolderComboBox �� BasedOn# [ 67   .�5      �� 	 �� Property	 � y�
 �� Value$
 Hidden=�6;    �� Triggers 5      �� 5   "    �� Binding 6.   ��  �� ElementName$ cbBookISBNs��$ SelectedIndex� �� Value -16n   .a�5    &   �� 	 � y�$
 Visible=�6C   5!   #   5#      5%      5&      �� 5'        bdStyle1#��[ 6   ._�5(      � .+�5)      �� -   5        $��txtInputQuantity��5)        
txtGeneral#��[ 5*      $	��100��5+      $��0q�5,      # [ 5-      .��6\   ��5.      �� 5/      $ QuantityValue�6&   $ PropertyChanged=�6;    50   "   ,   &AnhQuoc_C5_Assignment.FormNotEmptyRule  51   &   +   %AnhQuoc_C5_Assignment.InputNumberRule  52   &   0   *AnhQuoc_C5_Assignment.InputNumberRangeRule  53   &   
   Min$ 1X�6A   55      .��56   (   57      �� 58      $��Input quantity��6    ��59      �� 5:      " ��6$     tblPlaceHolder# [ 6;   .�5;   "   �� 	 � y�$
 Hidden=�6?    5<   "   �� 5=   &    62   �� $ txtInputQuantity��$
 Text�  6n   .a�5>   *   �� 	 � y�$
 Visible=�6G   5?   '   5A      5C      5D      5E      5F      �� 5H   
   -   5        $��stkWrapButton��5H        stkWrapButton#��[ 5I      $��10 0 0 0q�5J      .+�5K      �� #��[ 6   ._�5L      �� -   5        $��btnAdd��5L      $��0q�5M      ��5N      �� 5O      " ��6     
 btnAdd# [ 64   .�5P      �� 	 � {�
 .  6:    5Q      ^� 5R   "    ^� 
Conditions 5S   &   �� 5T   *    �� Binding 64   �� $ cbBookISBNs��$ (Validation.HasError)� �� Value	 False6|   �� 5U   *    64   �� $ txtInputQuantity��$ (Validation.HasError)�	 False6�   0�5W   &   �� 5X   *   	 � {�
 . 6F   5Z   #   5\      5^      5_      �� 5`      #��[ 6   $��10 0 0 0q�5a      ._�5b      �� -   5        $��	btnCancel��5b        	btnCancel#��[ 5c      $��0q�5d      5e      5f      5g      5h      