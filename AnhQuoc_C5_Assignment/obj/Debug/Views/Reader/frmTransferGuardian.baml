   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5   	   /    )AnhQuoc_C5_Assignment.frmTransferGuardian   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:AnhQuoc_C5_Assignment     5   	     � Title$  Transfer Guardian Form��5   	      wdStyleGeneral#��[  5	   	     WSL � WindowStartupLocation# [ 5
   	   .��5      �� $	��400��6   .+�5   
   �� -   5        $��lblTitle��5        lblTitle#��[ 5      $!��Transfer Guardian For Child��5      .   (AnhQuoc_C5_Assignment.ucChildInformation  5   
   -   5          Name$ ucChildInformation��5   #   ��5   #   Z� �� 5   
   $
��20 0q�6    � 	Resources 5       �� 
TargetType  
lblGeneral �� BasedOn  
txtGeneral  	cbGeneral(  "System.Windows.Controls.DatePicker  
dateStyle1  gdDisplayInputForm��%�   '��       7  '�� "     7  '�� D     7  '  f     7  '� �     7  �� 5      " ��6   8   6+   �� 5      " ��6   8   6-   �� 5      " ��6   8   6.   �� 5      "  6   8   60   �� 5      " �6   8   6*   5      .+�5      �  � ColumnDefinitions 5      �� 5      $	��100 �6'   �� 5      $��* �6'   .+�5       �� $��
Guardian: ��6   �� 5!      -   5        $��cbGuardians��5"      $��1��5!        txtErrorTemplate1 =� ErrorTemplate# [ 5#       �� DisplayMemberPath$ FullName��5%      ��5$      �� *	Guardians+	 �� SelectedItem	 5&      �� 5'      
 �� Path$
 SelectedGuardian�6"    �� UpdateSourceTrigger$ PropertyChanged=�6:    �� ValidationRules 5(      ,   &AnhQuoc_C5_Assignment.SelectedItemRule  5)   "   5+      5-      5.      50      �� 52   
   -   5        $��	stkFooter��52       	 stkWrapButton#��[	 53      .+�54      �� -   5        $��
btnConfirm��54      $��Confirm��6'    
 
btnConfirm#��[
 55      $y�	Collapsed=�56      $��80��57      58      �� 59      -   5        $��stkUpdateButton��59      #��[	 5:      $��0q�5;      $y�Visible=�5<      .+�5=      �� -   5        $��	btnUpdate��5=      $��Update��6*     	btnUpdate#��[ 5>      $��80��5?      �� 5@      -   5        $��btnReset��5@      $��Reset��6)     btnReset#��[ 5A      $��10 0 0 0q�5B      $��80��5C      5D      �� 5E      -	   5        $��	btnCancel��5E      $��Cancel��6&     	btnCancel#��[ 5F      $��10 0 0 0q�5G      $��80��5H      5I      5J      5K      