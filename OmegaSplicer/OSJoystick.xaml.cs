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

        int     direction = 360;
        int     lastDirection = 0;
        double  distance = 0;
        bool    moveJoystick = false;

        public static DependencyProperty _direction = DependencyProperty.Register("Direction", typeof(string), typeof(Joystick), null);
        public static DependencyProperty _percent = DependencyProperty.Register("Percent", typeof(int), typeof(Joystick), null);

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

        public int Percent
        {
            get { return (int)GetValue(_percent); }
            set
            {
                int tmp = (int)GetValue(_percent);
                if (tmp != value)
                {
                    SetValueDp(_percent, value);
                }
            }
        }

        public DependencyProperty GetDirectionDependency()
        {
            return _direction;
        }

        public DependencyProperty GetPercentDependency()
        {
            return _percent;
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

        public Joystick()
        {
            InitializeComponent();
        }

        public void Joystick_Start()
        {
            distance = ellipseMain.Width / 2;
        }

        private void ellipseSense_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e.PointerDeviceType == PointerDeviceType.Touch && sender == ellipseSense)
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
            if (!Contains(new Point(delta_x, delta_y)))
                return;

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                double x = delta_x;
                double y = delta_y;

                this.transform.TranslateX = x;
                this.transform.TranslateY = y;
                 
                this.JoystickX = x;
                this.JoystickY = y;

                this.SetDirection();

                //Update UI X,Y,Z values
                this.TextBoxX.Text = String.Format("{0,5:0.00}", x);
                this.TextBoxY.Text = String.Format("{0,5:0.00}", y);
            });
        }

        // Set the direction and the persent who will be soustract
        private void SetDirection()
        {
            if (this.JoystickX > 0)
                this.Direction = "RIGHT";
            else
                this.Direction = "LEFT";

            this.Percent = (int)(this.JoystickX * 100 / distance);
        }

        private void ellipseSense_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if (e.PointerDeviceType == PointerDeviceType.Touch)
            {
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
                MoveJoystick(0, 0);
                moveJoystick = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void SetValueDp(DependencyProperty property, object value,
            [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public bool Contains(Point location)
        {
            if (Math.Sqrt((Math.Pow(location.X, 2) + Math.Pow(location.Y, 2))) > distance)
                return false;
            return true;
        }
        
    }
}
