   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      .    (AnhQuoc_C5_Assignment.ucReaderManagement   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      .��5	      � 
�������  � RowDefinitions  5
   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      ��    lblTitle#��[  6   $��Readers Management��5      � 5      $��30 0 0 0q�6   $
��Left=�5       � ColumnDefinitions 5      �� 5      $
��auto �6'   �� 5      $
��auto �6'   �� 5      $
��auto �6'   �� 5      $
��auto �6'   .+�5      � -   5        $��gdTxtSearchByIdentify��5      $��0��5      .+�5      �� -   5        $��txtSearchByIdentify��5        txtStyleSearch#��[ 5       �� 5!      $ ��Search by identify here...��6    ��5"      �� 5#       �� 
TargetType" ��6$     tblPlaceHolder �� BasedOn# [ 6;   .�5$   "   ��  �� Property � y� �� Value$ Hidden=�6?    �� Triggers 5%   "   �� 5&   &    �� Binding 62   ��  �� ElementName$ txtSearchByIdentify��	 �� Path$
	 Text�
 �� Value
  6q   .a�5'   *   ��  � y�$ Visible=�6G   5(   '   5*      5,      5-      � 5.      -   5        $��gdTxtSearchByName��5/      $��1��5.      .+�50      �� -   5        $��txtSearchByName��50      #��[ 51      �� 52      $��Search by name here...��6    ��53      �� 54      " ��6$   # [ 6;   .�55   "   ��  � y�$ Hidden=�6?    56   "   �� 57   &    62   �� $ txtSearchByName��$
	 Text�
  6m   .a�58   *   ��  � y�$ Visible=�6G   59   '   5;      5=      5>      � 5?      $��2��6   $��10 0 0 0q�5@      .+�5A      � .+�5B      �� -   5        $��cbReaderTypes��5B   #     cbStyle2#��[ 5E   #   ��5C   #   �� *ReaderTypes+ �� SelectedValue 5D   #   �� *SelectedReaderType+5E   #   �� 5F      -   5        $��btnClearComboBox��5F   !     btnClearComboBox#��[ 5G   !   5H      �� 5I      $��Select Reader Type��6    ��5J      �� 5K      " ��6$     tblPlaceHolderComboBox# [ 6;   .�5L   "   ��  � y�$ Hidden=�6?    5M   "   �� 5N   &    62   �� $ cbReaderTypes��$	 SelectedIndex�
 -16t   .a�5O   *   ��  � y�$ Visible=�6G   5P   '   5R      5T      5U      �� 5V      $��3��6    �� Orientation$ 
Horizontal=�5W      $��30 0 0 0q�5X      .+�5Y      �� -   5        $��btnAdd��5Y       
 btnAdd#��[ 5Z      $��
New Reader��5[      5\      5]      5^      *   $AnhQuoc_C5_Assignment.ucReadersTable  5a   
   -   5          Name$ ucReadersTable��5b      $��1��5a        ucStyleVertical#��[ 5c      $
��auto��5d         NumberItems$ 7��5e         AllowPagination . 5f      5g      5h      