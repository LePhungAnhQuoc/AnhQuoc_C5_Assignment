   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      ,    &AnhQuoc_C5_Assignment.ucBookManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      � -   5        $��	gdContent��5	      
�������  � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      ��    lblTitle#��[  6   $��Books Management��5      � 5      $��30 0 0 0q�6   $
��Left=�5       � ColumnDefinitions 5      �� 5      $
��auto �6'   �� 5      $
��auto �6'   �� 5      $
��auto �6'   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�5       $��10 0 0 0q�5"      ��5!      Z� .+�5#      � -   5        $��gdTxtSearchByName��5#      .+�5$      �� -   5        $��txtSearchByName��5$   "     txtStyleSearch#��[ 5%   "   �� 5&      $��Search by name here...��6$   ��5'      �� 5(   "    �� 
TargetType" ��6(     tblPlaceHolder �� BasedOn# [ 6?   .�5)   &   ��  �� Property � y� �� Value$ Hidden=�6C    �� Triggers 5*   &   �� 5+   *    �� Binding 66   �� 	 �� ElementName$	 txtSearchByName��
 �� Path$

 Text� �� Value  6q   .a�5,   .   ��  � y�$ Visible=�6K   5-   +   5/   #   51      52      53      � 56      -   5        $��gdCbBookISBNs��57      $��1��56      .+�58      �� -   5        $��cbBookISBNs��58        cbStyle2#��[ 59       �� DisplayMemberPath$
 ISBN��5:      �� 5;      $��Select Book ISBN��6    ��5<      �� 5=      " ��6$     tblPlaceHolderComboBox# [ 6;   .�5>   "   ��  � y�$ Hidden=�6?    5?   "   �� 5@   &    62   �� $	 cbBookISBNs��$
 SelectedIndex� -16r   .a�5A   *   ��  � y�$ Visible=�6G   5B   '   5D      5F      �� 5G      -   5        $��btnClearComboBox��5G        btnClearComboBox#��[ 5H      5I      �� 5L      -   5        $��stkFeatures��5M      $��2��5L        stkWrapButton#��[ 5N      $��30 0 0 0q�5O      .+�5P      �� -   5        $��borderBtnAdd��5P        bdStyle1#��[ 5Q      $y�Visible=�5R      ._�5S      �� -	   5        $��btnAdd��5S   !    
 btnAdd#��[ 5T   !   $��0q�5U   !   $��Add more books��5V   !   5W      5X      5Y      5Z      (   "AnhQuoc_C5_Assignment.ucBooksTable  5]   
   -
   5          Name$ ucBooksTable��5^      $��1��5]       	 ucStyleVertical#��[	 5_      $
��auto��5`      5a      5b      