   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5      -    'AnhQuoc_C5_Assignment.ucStatisticalCard   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:AnhQuoc_C5_Assignment     5      -   5        $��YourUserControl��5
      
�������  � 	Resources  5         MouseEnterStoryboard g� Duration �� TargetProperty	 W� To  MouseLeaveStoryboard��%8  &	        &	 �     �� 5   
   .��5      W� $ 	0:0:0.300D�6   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�63   $	 -10X�6�   5      5      �� 5   
   .��5      W� $ 	0:0:0.800D�6   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�63   $ 0X�6�   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ UIElement.MouseEnter��6    :� 
SourceName$ YourUserControl��6:   .[�5      �� #��[  6   5      :� 5   
   $ UIElement.MouseLeave��6   $ YourUserControl��6:   .[�5      �� #��[ 6   5      z�5       T� 5!   
   .��5%      ��   	styleCard#��[ 6   .��5&   
   �  � RowDefinitions 5'      �� 5(      $
��auto �6    �� 5)      $��* �6    .+�5+      �� $��15(�5,      $��0q�5-      ��5+      �� *Header+ �� ElementName$ YourUserControl��	 �� UpdateSourceTrigger$	 PropertyChanged=�5-   "   	� 5.      $��1��6   
 	� 	GroupName$
 Parent��5/        radioBtnStatistical#��[ 50      ��51      �� *TextFontSize+$ YourUserControl��$	 PropertyChanged=�
 � Tag 52      �� *Icon+$ YourUserControl��$	 PropertyChanged=���53      �� *	Content+$ YourUserControl��$	 PropertyChanged=�54      55      57      