using System;
using System.ComponentModel;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OmegaSplicer.Views
{
    public partial class Joystick : UserControl
    {
        double distance;

        public static DependencyProperty direction = DependencyProperty.Register("Direction", typeof(int), typeof(Joystick), null);
        public static DependencyProperty enable = DependencyProperty.Register("Enable", typeof(bool), typeof(Joystick), null);

        public bool Enable
        {
            get { return (bool)GetValue(enable); }
            set
            {
                bool tmp = (bool)GetValue(enable);
                SetVisibility(value);
                if (tmp != value)
                {
                    IsEnabled = value;
                    SetValueDp(enable, value);
                }
            }
        }

        public int Direction
        {
            get { return (int)GetValue(direction); }
            set
            {
                int tmp = (int)GetValue(direction);
                if (tmp != value)
                {
                    SetValueDp(direction, value);
                }
            }
        }

        private double joytickX;
        public double JoystickX
        {
            get { return this.joytickX; }
            set
            {
                value = Math.Round(value, 2);

                if (this.joytickX != value)
                {
                    this.joytickX = value;
                }
            }
        }

        private double joystickY;
        public double JoystickY
        {
            get { return this.joystickY; }
            set
            {
                value = Math.Round(value, 2);

                if (this.joystickY != value)
                {
                    this.joystickY = value;
                }
            }
        }

        public Joystick()
        {
            InitializeComponent();

            Joystick_Start();
        }

        public void Joystick_Start()
        {
            distance = ellipseMain.Width / 2;
        }

        private void ellipseSense_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (sender == ellipseSense)
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
            });
        }

        // Set the direction and the persent who will be soustract
        private void SetDirection()
        {
            this.Direction = (int)(this.JoystickX * 100 / distance);
        }

        private void ellipseSense_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
        }

        private void ellipseSense_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            MoveJoystick(0, 0);
        }

        private void SetVisibility(bool value)
        {
            if (value)
                Visibility = Visibility.Visible;
            else
                Visibility = Visibility.Collapsed;
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
