   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      1    +AnhQuoc_C5_Assignment.ucBookTitleManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      �� 
�������.+�5   
   ��    lblTitle#��[  6   $��Book Title Management��5      � 5   
   $��30 0 0 0q�6   $
��Left=�5        � ColumnDefinitions  5      �� 5      $
��auto �6#   .+�5      � $��0��6   $��30 0 0 0q�5      $
��Left=�5        5      �� 5      $
��auto �6'   �� 5      $
��auto �6'   .+�5      �� $��0��6     bdStyle1#��[ 5      ._�5      � .+�5       �� -   5        $��	txtSearch��5    "     txtStyleSearch#��[ 5!   "   $��0q�5"   "   .��66   5#      �� 5$      $��Search by name here...��6$   ��5%      �� 5&   "    �� 
TargetType" ��6(     tblPlaceHolder �� BasedOn# [ 6?   .�5'   &   ��  �� Property � y� �� Value$ Hidden=�6C    �� Triggers 5(   &   �� 5)   *    �� Binding 66   ��  �� ElementName$ 	txtSearch�� �� Path$
 Text�	 �� Value	  6k   .a�5*   .   ��  � y�$ Visible=�6K   5+   +   5-   #   5/      50      51      �� 54      -   5        $��stkFeatures��55      $��1��54        stkWrapButton#��[ 56      $��30 0 0 0q�57      .+�58      �� -   5        $��borderBtnAdd��58      #��[ 59      $y�Visible=�5:      ._�5;      �� -   5        $��btnAdd��5;   !    
 btnAdd#��[ 5<   !   $��0q�5=   !   $��Add new book title��5>   !   5?      5@      5A      5B      -   'AnhQuoc_C5_Assignment.ucBookTitlesTable  5E   
   -   5        
  Name$
 ucBookTitlesTable��5E   "     ucStyleVertical#��[ 5F   "   5G      5H      