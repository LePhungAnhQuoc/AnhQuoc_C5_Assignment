   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5   	   .    (AnhQuoc_C5_Assignment.frmInputServerName   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:AnhQuoc_C5_Assignment     5   	     � Title$  Input Data Source Form��5   	      WSL � WindowStartupLocation# [  5	   	     wdStyleGeneral#��[ 5
   	    � 	Resources 5      :   4AnhQuoc_C5_Assignment.CalculatePercentValueConverter " calculatePercentValueConverter��%   &	         5   
   6/   5      .��5      �� 
�������.+�5   
   ��   lblTitle#��[ 6   $��	0 20 0 10q�5      $��Data Source Information��5      �� 5   
   $
��20 0q�6   .+�5      �� -   5        $��stkInputInformation��5       5       �� 
TargetType  
lblGeneral �� BasedOn  
txtGeneral  	cbGeneral(  "System.Windows.Controls.DatePicker  
dateStyle1  gdDisplayInputForm��%�   '��       7  '�� "     7  '�� D     7  '  f     7  '� �     7  �� 5      " ��6   8   6/   �� 5      " ��6   8   61   �� 5      " ��6   8   62   �� 5      "  6   8   64   �� 5      " �6   8   6.   5      .+�5      �  � ColumnDefinitions 5      �� 5       $	��120 �6+   �� 5!      $��* �6+   .+�5#      �� $��Server name: ��6   � 5$      $��1��6   ��5%      Z� .+�5&      �� -   5        $��cbServerName��5&   #    	 cbStyle2#��[	 5'   #   $	��250��5(   #    �� DisplayMemberPath$
 Name��5+   #    �� SelectedValuePath$
 Name��5,   #   ��5)   #   �� *ServerNames+ �� SelectedItem 5*   #   �� *
ServerName+5,   #   �� 5-      -   5        $��txtServerName��5-   "   $	��220��5.   "   $
��Left=�5/   "    
 txtErrorTemplate1	 =� ErrorTemplate#	 [
 50   "   ��51   "   �� *ServerName.Name+
 �� Mode$
 OneWay=� �� UpdateSourceTrigger$ PropertyChanged=�52      53      � 54       55      �� 56      $	��120 �6+   �� 57      $��* �6+   .+�5:      �� $��Database name: ��6   � 5;      $��1��6   ��5<      Z� .+�5=      �� -   5        $��cbDatabaseName��5=   #   #��[	 5>   #   $	��250��5?   #   $
 Name��5B   #   $
 Name��5C   #   ��5@   #   �� *DatabaseNames+ 5A   #   �� *DatabaseName+5C   #   �� 5D      -   5        $��txtDatabaseName��5D   "   $	��220��5E   "   $
��Left=�5F   "   #	 [
 5G   "   ��5H   "   �� *DatabaseName.Name+$
 OneWay=�$ PropertyChanged=�5I      5J      5K      �� 5L      -   5        $��chkRememberMe��5L        
chkGeneral#��[ 5M      $��
10 10 0 10q�5N      $��Remember Me��5O       d� 	IsChecked 5R      �� *IsRememberMe+5S      �� 5V   
     stkWrapButton#��[ 6   $��	0 10 0 20q�5W      .+�5X      �� -   5        $��
btnConfirm��5X      $��Confirm��6'   $��80��69     
btnConfirm#��[ 5Y      �� 5Z      -   5        $��btnExit��5Z      $
��Exit��6$   $��10 0 0 0q�63   $��80��6E     btnExit#��[ 5[      5\      5]      5^      