   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      0    *AnhQuoc_C5_Assignment.ucBookISBNManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      �� .+�5   
   ��    lblTitle#��[  6   $��Book ISBN Management��5      � 5   
   $��30 0 0 0q�6   $
��Left=�5        � ColumnDefinitions  5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�5       � 	Resources 5       �� 
TargetType  cbLightBorder �� BasedOn �� Property �� Value��%�   '��       7  '� "     �� 5      " ��6   8   62   �� 5      " �6   .�5      ��  � ��$
 10 0q�63   5      5      .+�5      � .+�5      �� -   5        $��cbBookTitles��5       �� DisplayMemberPath$
 Name��5"      ��5       �� *
BookTitles+ �� SelectedItem 5!      �� *SelectedBookTitle+5"      �� 5#      $��Select Book Title��6    ��5$      �� 5%      " ��6$     tblPlaceHolderComboBox# [ 6;   .�5&   "   ��  � y�$ Hidden=�6?   	 �� Triggers	 5'   "   �� 5(   &   
 �� Binding
 62   ��  �� ElementName$ cbBookTitles�� �� Path$ SelectedIndex� �� Value -16s   .a�5)   *   ��  � y�$ Visible=�6G   5*   '   5,      5.      �� 5/      -   5        $��btnClearCbBookTitles��5/        btnClearComboBox#��[ 50      51      � 52      .+�53      �� -   5        $��	cbAuthors��53      $
 Name��56       �� SelectedIndex$ 0��57      ��54      �� *	Authors+ 55      �� *SelectedAuthor+57      �� 58      $��Select Author��6    ��59      �� 5:      " ��6$   # [ 6;   .�5;   "   ��  � y�$ Hidden=�6?   	 5<   "   �� 5=   &   
 62   �� $ 	cbAuthors��$ SelectedIndex� -16p   .a�5>   *   ��  � y�$ Visible=�6G   5?   '   5A      5C      �� 5D      -   5        $��btnClearCbAuthors��5D      #��[ 5E      5F      5G      �� 5I      $��1��6   $ 
Horizontal=�5J      $��30 0 0 0q�5K      .+�5L      �� -   5        $��btnAdd��5L       
 btnAdd#��[ 5M      $��New Book ISBN��5N      5O      5P      ,   &AnhQuoc_C5_Assignment.ucBookISBNsTable  5S   
   -   5          Name$ ucBookISBNsTable��5S   !     ucStyleVertical#��[ 5T   !   5U      5V      