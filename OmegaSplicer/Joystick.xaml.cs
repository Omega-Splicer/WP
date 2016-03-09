using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.Devices.Input;
using Windows.UI.Core;
using System.ComponentModel;
using OmegaSplicer.ViewModelNamespace;


// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page http://go.microsoft.com/fwlink/?LinkId=234236

namespace OmegaSplicer
{
    public partial class Joystick : UserControl
    {
        public static readonly DependencyProperty TimerMilliSecondsProperty =
               DependencyProperty.Register("TimerMilliSeconds", typeof(double), typeof(Joystick),
                   new PropertyMetadata(Convert.ToDouble(250), new PropertyChangedCallback(Joystick.OnTimerMilliSecondsChanged)));

        DispatcherTimer timer;

        int direction = 360;
        int speed = 0;
        int lastDirection = 0;
        int lastSpeed = 0;

        public static DependencyProperty _direction = DependencyProperty.Register("Direction", typeof(string), typeof(Joystick), null);

        public string Direction
        {
            get { return (string)GetValue(_direction); }
            set
            {
                string tmp = (string)GetValue(_direction);
                if (tmp != value)
                {
                    SetValueDp(_direction, value);
                }
            }
        }

        public DependencyProperty GetDependency()
        {
            return _direction;
        }

        private double _joytickX;
        public double JoystickX
        {
            get { return this._joytickX; }
            set
            {
                value = Math.Round(value, 2);

                if (this._joytickX != value)
                {
                    this._joytickX = value;
                }
            }
        }

        private double _joystickY;
        public double JoystickY
        {
            get { return this._joystickY; }
            set
            {
                value = Math.Round(value, 2);

                if (this._joystickY != value)
                {
                    this._joystickY = value;
                }
            }
        }

        double lastX = 0;
        double lastY = 0;
        double newX = 0;
        double newY = 0;

        bool moveJoystick = false;

        public Joystick()
        {
            InitializeComponent();
        }

        public void Joystick_Start()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(TimerMilliSeconds);
            timer.Tick += timer_Tick;  
        }

        public void Joystick_Stop()
        {
            timer.Tick -= timer_Tick;
        }

        public double TimerMilliSeconds
        {
            get
            {
                return Convert.ToDouble(GetValue(TimerMilliSecondsProperty));
            }
            set
            {
                SetValue(TimerMilliSecondsProperty, value);
            }
        }

        private static void OnTimerMilliSecondsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Joystick).timer.Interval = TimeSpan.FromMilliseconds((d as Joystick).TimerMilliSeconds);
        }

        private void ellipseSense_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.PointerDeviceType == PointerDeviceType.Touch)
            {
                MoveJoystick(this.transform.TranslateX + e.Delta.Translation.X, this.transform.TranslateY + e.Delta.Translation.Y);
            }
            else
            {
                e.Handled = true;
            }
        }

        private async void MoveJoystick(double delta_x, double delta_y)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                double x = delta_x;
                double y = delta_y;

                this.transform.TranslateX = x;
                this.transform.TranslateY = y;

                this.JoystickX = x;
                this.JoystickY = y;

                this.Direction = "RIGHT";

                //Update UI X,Y,Z values
                this.TextBoxX.Text = String.Format("{0,5:0.00}", x);
                this.TextBoxY.Text = String.Format("{0,5:0.00}", y);
            });
        }

/*
        public void StartJoystick()
        {
            Touch.FrameReported += Touch_FrameReported;
        }

        public void StopJoystick()
        {
            Touch.FrameReported -= Touch_FrameReported;
        }

        void MovePointer(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                int pointsNumber = e.GetIntermediatePoints(ellipseSense).Count;
                IList<PointerPoint> pointCollection = e.GetIntermediatePoints(ellipseSense);


                for (int i = 0; i < pointsNumber; i++)
                {
                    if (pointCollection[i].Position.X > 0 && pointCollection[i].Position.X < ellipseSense.ActualWidth)
                    {
                        if (pointCollection[i].Position.Y > 0 && pointCollection[i].Position.Y < ellipseSense.ActualHeight)
                        {
                            // Update Shpero speed and direction
                            Point p = pointCollection[i].Position;
                            Point center = new Point(ellipseSense.ActualWidth / 2, ellipseSense.ActualHeight / 2);

                            double distance = Math.Sqrt(Math.Pow((p.X - center.X), 2) + Math.Pow((p.Y - center.Y), 2));

                            double distanceRel = distance * 255 / (ellipseSense.ActualWidth / 2);
                            if (distanceRel > 255)
                            {
                                distanceRel = 255;
                            }

                            double angle = Math.Atan2(p.Y - center.Y, p.X - center.X) * 180 / Math.PI;
                            if (angle > 0)
                            {
                                angle += 90;
                            }
                            else
                            {
                                angle = 270 + (180 + angle);
                                if (angle >= 360)
                                {
                                    angle -= 360;
                                }
                            }
                            direction = Convert.ToInt16(angle);
                            speed = Convert.ToInt16(distanceRel);

                            // Set Joystick Pos
                            newX = p.X - (ellipseSense.ActualWidth / 2);
                            newY = p.Y - (ellipseSense.ActualWidth / 2);
                            if (moveJoystick) MoveJoystick(newX, newY);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            try
            {
                int pointsNumber = e.GetTouchPoints(ellipseSense).Count;
                TouchPointCollection pointCollection = e.GetTouchPoints(ellipseSense);


                for (int i = 0; i < pointsNumber; i++)
                {
                    if (pointCollection[i].Position.X > 0 && pointCollection[i].Position.X < ellipseSense.ActualWidth)
                    {
                        if (pointCollection[i].Position.Y > 0 && pointCollection[i].Position.Y < ellipseSense.ActualHeight)
                        {
                            // Update Shpero speed and direction
                            Point p = pointCollection[i].Position;
                            Point center = new Point(ellipseSense.ActualWidth / 2, ellipseSense.ActualHeight / 2);

                            double distance = Math.Sqrt(Math.Pow((p.X - center.X), 2) + Math.Pow((p.Y - center.Y), 2));

                            double distanceRel = distance * 255 / (ellipseSense.ActualWidth / 2);
                            if (distanceRel > 255)
                            {
                                distanceRel = 255;
                            }

                            double angle = Math.Atan2(p.Y - center.Y, p.X - center.X) * 180 / Math.PI;
                            if (angle > 0)
                            {
                                angle += 90;
                            }
                            else
                            {
                                angle = 270 + (180 + angle);
                                if (angle >= 360)
                                {
                                    angle -= 360;
                                }
                            }
                            direction = Convert.ToInt16(angle);
                            speed = Convert.ToInt16(distanceRel);

                            // Set Joystick Pos
                            newX = p.X - (ellipseSense.ActualWidth / 2);
                            newY = p.Y - (ellipseSense.ActualWidth / 2);
                            if (moveJoystick) MoveJoystick(newX, newY);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void MoveJoystick(double moveX, double moveY)
        {
            Storyboard sb = new Storyboard();
            KeyTime ktStart = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0));
            KeyTime ktEnd = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));

            DoubleAnimationUsingKeyFrames animationFirstX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames animationFirstY = new DoubleAnimationUsingKeyFrames();

            ellipseButton.RenderTransform = new CompositeTransform();

            Storyboard.SetTargetProperty((Timeline)animationFirstX, new PropertyPath(CompositeTransform.TranslateXProperty.ToString()).ToString());
            Storyboard.SetTarget(animationFirstX, ellipseButton.RenderTransform);
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastX });
            animationFirstX.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveX });


            Storyboard.SetTargetProperty((Timeline)animationFirstY, new PropertyPath(CompositeTransform.TranslateYProperty.ToString()).ToString());
            Storyboard.SetTarget(animationFirstY, ellipseButton.RenderTransform);
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktStart, Value = lastY });
            animationFirstY.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = ktEnd, Value = moveY });

            sb.Children.Add(animationFirstX);
            sb.Children.Add(animationFirstY);
            sb.Begin();

            lastX = moveX;
            lastY = moveY;
        }
*/
        void timer_Tick(object sender, object o)
        {
            if (((direction - lastDirection) > 5 || (direction - lastDirection) < -5) || ((speed - lastSpeed) > 5 || (speed - lastSpeed) < -5))
            {
                lastDirection = direction;
                lastSpeed = speed;

                OnNewCoordinates();

                Debug.WriteLine("Event fired: " + speed + ", " + direction);
            }
        }

        private void ellipseSense_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (e.PointerDeviceType == PointerDeviceType.Touch)
            {
                timer.Start();
                moveJoystick = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ellipseSense_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.PointerDeviceType == PointerDeviceType.Touch)
            {
                timer.Stop();
                MoveJoystick(0, 0);
                moveJoystick = false;
            }
            else
            {
                e.Handled = true;
            }
            // Fire event
//            OnStop();

            // Move Joystick to Center
//            MoveJoystick(0, 0);
        }

        public event EventHandler NewCoordinates;
        protected void OnNewCoordinates()
        {
            var myCoordinates = new MyCoordinates();
            myCoordinates.Direction = direction;
            myCoordinates.Speed = speed;
            if (NewCoordinates != null)
                NewCoordinates(this, myCoordinates);
        }

        public event EventHandler Stop;
        protected void OnStop()
        {
            var myStop = new MyStop();
            myStop.Stopped = true;
            if (Stop != null)
                Stop(this, myStop);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void SetValueDp(DependencyProperty property, object value,
            [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }
    }

    public class MyCoordinates : EventArgs
    {
        public int Direction;
        public int Speed;
    }

    public class MyStop : EventArgs
    {
        public bool Stopped;
    }
}
