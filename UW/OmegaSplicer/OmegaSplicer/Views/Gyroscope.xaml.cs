using System;
using System.ComponentModel;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OmegaSplicer.Views
{
    public sealed partial class Gyroscope : UserControl
    {
        public static DependencyProperty direction = DependencyProperty.Register("Direction", typeof(int), typeof(Gyroscope), null);
        public static DependencyProperty enable = DependencyProperty.Register("Enable", typeof(bool), typeof(Gyroscope), null);
        private int maxAngle = 75;
        private int sensitivity = 10;

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

        private double accelX;
        public double AccelX
        {
            get { return this.accelX; }
            set
            {
                value = Math.Round(value, 2);

                if (this.accelX != value)
                {
                    this.accelX = value;
                }
            }
        }

        private double accelY;
        public double AccelY
        {
            get { return this.accelY; }
            set
            {
                value = Math.Round(value, 2);

                if (this.accelY != value)
                {
                    this.accelY = value;
                }
            }
        }

        private double accelZ;
        public double AccelZ
        {
            get { return this.accelZ; }
            set
            {
                value = Math.Round(value, 2);

                if (this.accelZ != value)
                {
                    this.accelZ = value;
                }
            }
        }

        Accelerometer accelerometer = Accelerometer.GetDefault();
        TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs> update_func;

        public Gyroscope()
        {
            this.InitializeComponent();

            this.update_func = new TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs>(ReadingChanged);

            this.accelX = 0;
            this.accelY = 0;
            this.accelZ = 0;
        }

        public void Gyroscope_Start()
        {
            if (accelerometer != null && IsEnabled == true)
            {
                // Select a report interval that is both suitable for the purposes of the app and supported by the sensor.
                // This value will be used later to activate the sensor.
                uint minReportInterval = accelerometer.MinimumReportInterval;
                uint desiredReportInterval = minReportInterval > 16 ? minReportInterval : 16;
                accelerometer.ReportInterval = desiredReportInterval;
                //add event for accelerometer readings
                accelerometer.ReadingChanged += this.update_func;
            }
        }

        public void Gyroscope_Stop()
        {
            if (accelerometer != null && IsEnabled == true)
            {
                accelerometer.ReadingChanged -= this.update_func;
            }
        }

        private async void ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                AccelerometerReading reading = args.Reading;

                //Update class X,Y,Z values
                this.AccelX = reading.AccelerationX;
                this.AccelY = reading.AccelerationY;
                this.AccelZ = reading.AccelerationZ;

                //Update UI X,Y,Z values and Direction
                this.SetDirection();
                /*
                this.TextBoxX.Text = String.Format("{0,5:0.00}", reading.AccelerationX);
                this.TextBoxY.Text = String.Format("{0,5:0.00}", reading.AccelerationY);
                this.TextBoxZ.Text = String.Format("{0,5:0.00}", reading.AccelerationZ);
                 */
            });
        }

        // Set the direction, the persent who will be soustract and change the UI(ellipses)
        private void SetDirection()
        {
            double angleY;
            double angleX;
            double valueY;
            double valueX;

            //calul the angle
            angleY = Math.Asin(this.accelY) * 180 / Math.PI;
            angleX = Math.Asin(this.accelX) * 180 / Math.PI;

            //major the angle
            angleY = angleY > this.maxAngle ? this.maxAngle : angleY;
            angleY = angleY < -this.maxAngle ? -this.maxAngle : angleY;

            angleX = angleX > this.maxAngle ? this.maxAngle : angleX;
            angleX = angleX < -this.maxAngle ? -this.maxAngle : angleX;

            //calcul the percent
            this.Direction = (int)angleY * 100 / this.maxAngle;

            //false 3d rotation of ellipseX and Y
            valueY = (angleY * 250 / this.maxAngle);
            valueX = (angleX * 250 / this.maxAngle);

            valueY = valueY > 0 ? valueY : valueY < 0 ? valueY * -1 : 1;
            valueX = valueX > 0 ? valueX : valueX < 0 ? valueX * -1 : 1;

            this.ellipseY.Width = valueY > this.ellipseY.Width + this.sensitivity || valueY < this.ellipseY.Width - this.sensitivity ? valueY : this.ellipseY.Width;
            this.ellipseX.Height = valueX > this.ellipseX.Height + this.sensitivity || valueX < this.ellipseX.Height - this.sensitivity ? valueX : this.ellipseX.Height;
        }

        private void SetVisibility(bool value)
        {
            if (value)
            {
                Visibility = Visibility.Visible;
                this.Gyroscope_Start();
            }
            else
            {
                Visibility = Visibility.Collapsed;
                this.Gyroscope_Stop();
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
    }
}
