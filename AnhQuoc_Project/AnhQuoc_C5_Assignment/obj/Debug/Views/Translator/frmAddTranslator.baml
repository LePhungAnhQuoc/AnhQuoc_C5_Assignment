   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	   ,    &AnhQuoc_C5_Assignment.frmAddTranslator   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:AnhQuoc_C5_Assignment     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   -   5          �Name$  myUserControl��5   	    � Title$ Add New Translator Form��5	   	      WSL � WindowStartupLocation# [  5
   	     wdStyleGeneral#��[ 5   	   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *	LoadedCmd+   CommandParameter 5   $   ��  �� ElementName$ myUserControl��5        5   
   $ Closing��6   . 5         6$   �� *
ClosingCmd+ 5      �� $ myUserControl��5      	 � 	Resources	 5      7   1AnhQuoc_C5_Assignment.DateMinToNullValueConverter  dateMinToNullValueConverter-   'AnhQuoc_C5_Assignment.boFToAgeConverter  boFToAgeConverter��%:   &	       &	        5   
   6,     5   
   6"   5      .��5      �� $	��800��6   .+�5   
   �� -   5        $��lblTitle��5        lblTitle#��[ 5      � 5!   
   -   5        $��mainContent��5#      $
��auto��5!      $��
20 0 20 10q�5"      
 � ColumnDefinitions
 5$      �� 5%      $��* �6#   �� 5&      $��* �6#   	 5(      ��%    5)      .+�5*      �� $��0��6   $��0 0 20 0q�5+      	 5,       �� 
TargetType  
lblGeneral �� BasedOn  
txtGeneral  	cbGeneral(  "System.Windows.Controls.DatePicker  
dateStyle1 	 gdDisplayInputForm��%�   '��       7  '�� "     7  '�� D     7  '  f     7  '� �     7 	 �� 5-      " ��6   8   6/   �� 5.      " ��6   8   61   �� 5/      " ��6   8   62   �� 50      "  6   8   64   �� 51      " �6   8   6.   52      .+�53      � 
 54      �� 55      $	��100 �6+   �� 56      $��* �6+   .+�58      �� $
��Id: ��6   �� 59      $��1��6   {�.  ��5:   !   �� *	Item.Id+5;   !   5<      � 5=      
 5>      �� 5?      $	��100 �6+   �� 5@      $��* �6+   .+�5B      �� $��Name: ��6   �� 5C      -   5        $��txtName��5C      $��1��5D       
 txtErrorTemplate1 =� ErrorTemplate# [
 5E      .��6[   ��5F      �� 5G       �� Path$ 	Item.Name�6&    �� UpdateSourceTrigger$ PropertyChanged=�67    �� ValidationRules 5H   "   ,   &AnhQuoc_C5_Assignment.FormNotEmptyRule  5I   &   0	   *AnhQuoc_C5_Assignment.InputUnicodeTextRule	  5J   &   5L      .��5M   (   5N      5O      � 5P      
 5Q      �� 5R      $	��100 �6+   �� 5S      $��* �6+   .+�5U      �� $��Description: ��6   � 5V      $��1��6   ��5W      Z� .+�5X      �� -   5        $��txtDescription��5Y   "   $��1��5X   "   # [
 5Z   "   .��6`   ��5[      �� 5\   "   $ Item.Description�6*   $ PropertyChanged=�6B    5]   &   5`   #   .��5a   ,   5b      �� 5c      -   5          btnExpandMore#��[ 5d   !   5e      5f      5g      � 5h      $��0 15 0 0q�6   
 5i      �� 5j      $	��100 �6+   �� 5k      $��* �6+   .+�5m      �� $��Date of birth: ��6     5n      -   5        $��dateboF��5o   !   $��1��5n   !   # [
 5p   !      SelectedDate 5q      �� 5r      $ Item.boF�6&   $ PropertyChanged=�66    �� 	Converter# [ 6\    5s   "   ,
   &AnhQuoc_C5_Assignment.SelectedItemRule
  5t   &   (   "AnhQuoc_C5_Assignment.InputBoFRule  5u   &   5w      5y      �� 5z      -   5        $��tblAge��5{       $��1��5z         tblPlaceHolder#��[ 5|       
���   �$��	100 0 0 0q�5       ��5}       �� *SelectedDate+$ dateboF��# [  �� StringFormat$ Age: {0}��5       5�      5�      �� 5�      $��1��6   $��20 0 0 0q�5�      	 5�      ��%�   '��       7  '�� "     7  '�� D     7  '  f     7  '� �     7 	 �� 5�      " ��6   8   6/   �� 5�      " ��6   8   61   �� 5�      " ��6   8   62   �� 5�      "  6   8   64   �� 5�      " �6   8   6.   5�      .+�5�      � 
 5�      �� 5�      $	��100 �6+   �� 5�      $��* �6+   .+�5�      �� $��	Summary: ��6   � 5�      $��1��6   ��5�      Z� .+�5�      �� -	   5        $��
txtSummary��5�   "   $��1��5�      # [
 5�      .��6[   ��5�      �� 5�   "   $ Item.Summary�6*   $ PropertyChanged=�6>    5�   &   5�   #   .��5�   ,   5�      �� 5�      -
   5        #��[ 5�   !   5�      5�      5�      � 5�      
 5�      �� 5�      $	��100 �6+   �� 5�      $��* �6+   .+�5�      �� $��Created At: ��6   �� 5�      $��1��6   {�.  ��5�   !   �� *Item.CreatedAt+5�   !   5�      � 5�      $��0 15 0 0q�6   
 5�      �� 5�      $	��100 �6+   �� 5�      $��* �6+   .+�5�      �� $��Modified At: ��6   �� 5�      $��1��6   {�.  ��5�   !   �� *Item.ModifiedAt+5�   !   5�      5�      5�      �� 5�   
     stkWrapButton#��[ 6   .+�5�      �� -   5        $��
btnConfirm��5�      $��Confirm��6'     
btnConfirm#��[ 5�      $y�Visible=�5�      $��80��5�      ��5�      �� *btnConfirmClickCmd+5�      �� 5�      -   5        $��stkUpdateButton��5�      #��[ 5�      $��0q�5�      $y�	Collapsed=�5�      .+�5�      �� -   5        $��	btnUpdate��5�      $��Update��6*     	btnUpdate#��[ 5�      $��80��5�      ��5�      �� *btnUpdateClickCmd+�� 5�      -   5        $��btnReset��5�      $��Reset��6)     btnReset#��[ 5�      $��10 0 0 0q�5�      $��80��5�      ��5�      �� *btnResetClickCmd+5�      �� 5�      -   5        $��	btnCancel��5�      $��Cancel��6&     	btnCancel#��[ 5�      $��10 0 0 0q�5�      $��80��5�      ��5�      �� *btnCancelClickCmd+5�      5�      5�      