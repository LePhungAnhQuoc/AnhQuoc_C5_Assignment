   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      0    *AnhQuoc_C5_Assignment.ucBookISBNManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      �� 
�������.+�5   
   ��    lblTitle#��[  6   $��Book ISBN Management��5      � 5   
   $��30 0 0 0q�6   $
��Left=�5        � ColumnDefinitions  5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      � $��0��6   $��10 0 0 0q�5      .+�5      �� -   5        $��cbBookTitles��5        cbStyle2#��[ 5       �� DisplayMemberPath$
 Name��5      ��5      �� *
BookTitles+ �� SelectedItem 5      �� *SelectedBookTitle+5      �� 5      $��Select Book Title��6   ��5      �� 5       �� 
TargetType" ��6      tblPlaceHolderComboBox �� BasedOn# [ 67   .�5       ��  �� Property � y� �� Value$ Hidden=�6;    �� Triggers 5!      �� 5"   "    �� Binding 6.   �� 	 �� ElementName$	 cbBookTitles��
 �� Path$
 SelectedIndex� �� Value -16o   .a�5#   &   ��  � y�$ Visible=�6C   5$   #   5&      5(      �� 5)      -   5        $��btnClearComboBox��5)        btnClearComboBox#��[ 5*      5+      �� 5,      $��1��6    �� Orientation$ 
Horizontal=�5-      $��30 0 0 0q�5.      .+�5/      �� -   5        $��btnAdd��5/       
 btnAdd#��[ 50      $��New Book ISBN��51      52      53      ,   &AnhQuoc_C5_Assignment.ucBookISBNsTable  56   
   -   5          Name$ ucBookISBNsTable��56   !     ucStyleVertical#��[ 57   !   58      59      