   M S B A M L   `   `   `  ����  AnhQuoc_C5_Assignment3    -AnhQuoc_C5_Assignment.ucLoadingSpinnerControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5      �   � 	Resources  5	   
      RotationStoryboard �� 
TargetName �� TargetProperty W� From	 W� To g� Duration g� RepeatBehavior��%�  &	        �� 5      .��5      W� $ circle1��6"   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)�6B   $ 0X�5   "   $	 360X�6+   $ 0:0:1D�64   $ Forever��6E   W� 5      $ circle2��6"   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(RotateTransform.Angle)�6B   $ 0X�5   "   $
 -360X�6+   $ 0:0:1D�65   $ Forever��6F   5      5      .+�5   
   �� -   5        $��
lblContent��5        
lblGeneral#��[ 5      $��Center=�5      $��Center=�5      $��	Loading..��5      A� 5   
   -   5         A�Name$ circle1��5      $��50��6#   $��50��6.   
��� ���$��50q�6H   z�5      V� 5      .~�5      ��  �� Angle$ 0Q�6&   5      5       A� 5#   
   -   5        $ circle2��5#      $��50��6#   $��50��6.   
������$��50q�6L   z�5$      V� 5%      .~�5&      �� $ 0Q�6&   5'      5)      	 � Triggers	 5,   
   :� 5-      
 :� RoutedEvent$
 Loaded��6   .[�5.      �� #��[  6"   5/      51      52      