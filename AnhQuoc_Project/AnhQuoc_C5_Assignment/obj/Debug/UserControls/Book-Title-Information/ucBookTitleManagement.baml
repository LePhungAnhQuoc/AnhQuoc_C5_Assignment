   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      1    +AnhQuoc_C5_Assignment.ucBookTitleManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      �� .+�5   
   ��    lblTitle#��[  6   $��Book Title Management��5      � 5   
   $��30 0 0 0q�6   $
��Left=�5        � ColumnDefinitions  5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�5      .+�5      � .+�5      �� -   5        $��	txtSearch��5   "     txtLightBorder#��[ 5   "   .��6J   5      �� 5      $��Search by name here...��6$   ��5      �� 5   "    �� 
TargetType" ��6(     tblPlaceHolder �� BasedOn# [ 6?   .�5   &   ��  �� Property � y� �� Value$ Hidden=�6C    �� Triggers 5    &   �� 5!   *    �� Binding 66   ��  �� ElementName$ 	txtSearch��	 �� Path$
	 Text�
 �� Value
  6k   .a�5"   .   ��  � y�$ Visible=�6K   5#   +   5%   #   5'      5(      5)      �� 5+      -   5        $��stkFeatures��5,      $��1��5+        stkWrapButton#��[ 5-      $��30 0 0 0q�5.      .+�5/      �� -   5        $��btnAdd��5/       
 btnAdd#��[ 50      $��0q�51      $��Add new book title��52      53      54      -   'AnhQuoc_C5_Assignment.ucBookTitlesTable  57   
   -   5          Name$ ucBookTitlesTable��57   "     ucStyleVertical#��[ 58   "   59      5:      