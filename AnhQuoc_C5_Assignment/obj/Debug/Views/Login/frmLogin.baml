   M S B A M L   `   `   `  ����P  LAnhQuoc_C5_Assignment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AnhQuoc_C5_AssignmentAnhQuoc_C5_Assignment  5   	   $    AnhQuoc_C5_Assignment.frmLogin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:AnhQuoc_C5_Assignment     5   	   -   5          � WindowStyle$  SingleBorderWindow=�5	   	      wdStyleGeneral#��[  5
   	     WSL � WindowStartupLocation# [ 5   	   .��5      �� 
���   �$��1q�5      ���#d4ebff._�5   
   �� 
���   � �� CornerRadius$ 0��5      $��0q�5      $��Center=�5      $��Center=�5      ._�5      �� ���#e6f3ff$��20 30q�5      .+�5      �� $��Center=�6   $
��Left=�5      $
��40 0q�5      $��50(�5      $
��0 10q�5      $��-1��5      
���   �.��5   +   	Sign in64   �� 5        	lblStyle1#��[ 6   $��
40 10 40 0q�5      $��0q�6,   $��Username��5       $��20(�5!      $
��Left=�5"      �� 5#      -   5        $��txtUsername��5#      $��
40 10 40 0q�5$      $��10q�6/   $��50��5%      $	��300��6&   $��20(�5&        txtErrorTemplate1 =� ErrorTemplate# [ 5'      $��0��5(      .��6'   ��5)      �� 5*       �� Path$ Account.Username�6"    �� UpdateSourceTrigger$ PropertyChanged=�6:    �� ValidationRules 5+      5-      .��5.   $   5/      �� 50      -   5        $��
lbPassword��50      #��[ 51      $
��Left=�52      $��
40 20 40 0q�53      $��0q�54      $��20(�55      $��Password��56      G� 57      -   5        $��boxPassword��57      $��
40 10 40 0q�58      $��50��59      $��10q�5:      $��1��5;      $��20(�5<      �� 5=       �� Orientation$ Vertical=�6   $��Center=�5>      $��60 30q�5?      .+�5@      �� -   5        $��	btnSignIn��5@        
btnConfirm#��[ 5A      $��20(�5B      $
��20 8q�5C      $��2��5D      $��Stretch=�5E      .��6;   	Sign in6D   �� 5F      -   5        $��btnOpenConnectform��5F      #��[ 5G      $$��Login to the connection system��5H      $��17(�5I      $��Italic$�5J      $��0 10 0 0q�5K      $
��20 8q�6/   $��2��5L      $��Center=�5M      5N      5O      5P      5Q      5R      