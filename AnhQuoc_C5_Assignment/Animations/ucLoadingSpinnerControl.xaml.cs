using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucLoadingSpinnerControl.xaml
    /// </summary>
    public partial class ucLoadingSpinnerControl : UserControl
    {
        public ucLoadingSpinnerControl()
        {
            InitializeComponent();
        }
        
        ////private void StartRotationAnimation()
        ////{
        ////    double centerX = SpinnerCanvas.Width / 2;
        ////    double centerY = SpinnerCanvas.Height / 2;

        ////    RotateTransform rotateTransform1 = new RotateTransform();
        ////    Circle1.RenderTransform = rotateTransform1;

        ////    RotateTransform rotateTransform2 = new RotateTransform();
        ////    Circle2.RenderTransform = rotateTransform2;

        ////    RotateTransform rotateTransform3 = new RotateTransform();
        ////    Circle3.RenderTransform = rotateTransform3;

        ////    DoubleAnimation rotateAnimation = new DoubleAnimation
        ////    {
        ////        From = 0,
        ////        To = 360,
        ////        Duration = new Duration(TimeSpan.FromSeconds(1)),
        ////        RepeatBehavior = RepeatBehavior.Forever
        ////    };

        ////    rotateTransform1.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        ////    rotateTransform2.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        ////    rotateTransform3.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

        ////    // Add more rotations for additional circles
        ////}
        //private void StartRotationAnimation()
        //{
        //    double centerX = SpinnerCanvas.Width / 2;
        //    double centerY = SpinnerCanvas.Height / 2;

        //    RotateTransform rotateTransform1 = new RotateTransform();
        //    Circle1.RenderTransform = rotateTransform1;
        //    SetCirclePosition(Circle1, 0, centerX, centerY);

        //    RotateTransform rotateTransform2 = new RotateTransform();
        //    Circle2.RenderTransform = rotateTransform2;
        //    SetCirclePosition(Circle2, 120, centerX, centerY);

        //    RotateTransform rotateTransform3 = new RotateTransform();
        //    Circle3.RenderTransform = rotateTransform3;
        //    SetCirclePosition(Circle3, 240, centerX, centerY);

        //    DoubleAnimation rotateAnimation = new DoubleAnimation
        //    {
        //        From = 0,
        //        To = 360,
        //        Duration = new Duration(TimeSpan.FromSeconds(1)),
        //        RepeatBehavior = RepeatBehavior.Forever
        //    };

        //    rotateTransform1.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        //    rotateTransform2.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        //    rotateTransform3.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

        //    // Add more rotations for additional circles

        //    // Add more rotations for additional circles
        //    // Set positions accordingly
        //}

        //private void SetCirclePosition(Ellipse circle, double angleDegrees, double centerX, double centerY)
        //{
        //    double angleRadians = angleDegrees * (Math.PI / 180);
        //    double radius = SpinnerCanvas.Width / 2 - circle.Width / 2;

        //    double circleX = centerX + radius * Math.Cos(angleRadians);
        //    double circleY = centerY + radius * Math.Sin(angleRadians);

        //    Canvas.SetLeft(circle, circleX - circle.Width / 2);
        //    Canvas.SetTop(circle, circleY - circle.Height / 2);
        //}
    }
}
