   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      0    *AnhQuoc_C5_Assignment.ucUserRoleManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      � 
�������  � RowDefinitions  5
   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      ��    lblTitle#��[  6   $��User Roles Management��5      � 5      $��30 0 0 0q�6   $
��Left=�5       � ColumnDefinitions 5      �� 5      $
��auto �6'   �� 5      $
��auto �6'   .+�5      � $��0��6   .+�5      �� -   5        $��	txtSearch��5        txtStyleSearch#��[ 5      �� 5      $��Search by name here...��6    ��5      �� 5       �� 
TargetType" ��6$     tblPlaceHolder �� BasedOn# [ 6;   .�5    "   ��  �� Property � y� �� Value$ Hidden=�6?    �� Triggers 5!   "   �� 5"   &    �� Binding 62   ��  �� ElementName$ 	txtSearch��	 �� Path$
	 Text�
 �� Value
  6g   .a�5#   *   ��  � y�$ Visible=�6G   5$   '   5&      5(      5)      �� 5*      $��1��6    �� Orientation$ 
Horizontal=�5+      $��30 0 0 0q�5,      .+�5-      �� -   5        $��btnAdd��5-       
 btnAdd#��[ 5.      $��New User Role��5/      50      51      52      ,   &AnhQuoc_C5_Assignment.ucUserRolesTable  55   
   -   5          Name$ ucUserRolesTable��56   !   $��1��55   !     ucStyleVertical#��[ 57   !   $
��auto��58   !      AllowPagination .    NumberItems$ 7��5:   !   5;      5<      