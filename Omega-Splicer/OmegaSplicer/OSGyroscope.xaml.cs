using OmegaSplicer.ViewModelNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page http://go.microsoft.com/fwlink/?LinkId=234236

namespace OmegaSplicer
{
    public sealed partial class MyGyroscope : UserControl
    {
        public static DependencyProperty _direction = DependencyProperty.Register("Direction", typeof(string), typeof(MyGyroscope), null);
        public static DependencyProperty _percent = DependencyProperty.Register("Percent", typeof(int), typeof(MyGyroscope), null);
        private int _maxAngle = 75;
        private int sensitivity = 10;

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

        private double _accelX;
        public double AccelX
        {
            get { return this._accelX; }
            set 
            {
                value = Math.Round(value, 2);

                if (this._accelX != value)
                {
                    this._accelX = value;
                }
            }
        }

        private double _accelY;
        public double AccelY
        {
            get { return this._accelY; }
            set
            {
                value = Math.Round(value, 2);

                if (this._accelY != value)
                {
                    this._accelY = value;
                }
            }
        }

        private double _accelZ;
        public double AccelZ
        {
            get { return this._accelZ; }
            set
            {
                value = Math.Round(value, 2);

                if (this._accelZ != value)
                {
                    this._accelZ = value;
                }
            }
        }

        Accelerometer _accelerometer = Accelerometer.GetDefault();
        TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs> _update_func;

        public MyGyroscope()
        {
            this.InitializeComponent();

            this._update_func = new TypedEventHandler<Accelerometer, AccelerometerReadingChangedEventArgs>(ReadingChanged);

            this._accelX = 0;
            this._accelY = 0;
            this._accelZ = 0;
        }

        public void Gyroscope_Start()
        {
            if (_accelerometer != null)
            {
                // Select a report interval that is both suitable for the purposes of the app and supported by the sensor.
                // This value will be used later to activate the sensor.
                uint minReportInterval = _accelerometer.MinimumReportInterval;
                uint desiredReportInterval = minReportInterval > 16 ? minReportInterval : 16;
                _accelerometer.ReportInterval = desiredReportInterval;
                //add event for accelerometer readings
                _accelerometer.ReadingChanged += this._update_func;
            }
        }

        public void Gyroscope_Stop()
        {
            if (_accelerometer != null)
            {
                _accelerometer.ReadingChanged -= this._update_func;
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
            angleY = Math.Asin(this._accelY) * 180 / Math.PI;
            angleX= Math.Asin(this._accelX) * 180 / Math.PI;

            if (angleY > 5)
                this.Direction = "LEFT";
            else if (angleY < -5)
                this.Direction = "RIGHT";
            else
                this.Direction = "CENTER";

            //major the angle
            angleY = angleY > this._maxAngle ? this._maxAngle : angleY;
            angleY = angleY < -this._maxAngle ? -this._maxAngle : angleY;

            angleX = angleX > this._maxAngle ? this._maxAngle : angleX;
            angleX = angleX < -this._maxAngle ? -this._maxAngle : angleX;

            //calcul the percent
            this.Percent = (int)angleY * 100 / this._maxAngle;

            //false 3d rotation of ellipseX and Y
            valueY = (angleY * 350 / this._maxAngle);
            valueX = (angleX * 350 / this._maxAngle);

            valueY = valueY > 0 ? valueY : valueY < 0 ? valueY * -1 : 1;
            valueX = valueX > 0 ? valueX : valueX < 0 ? valueX * -1 : 1;

            this.ellipseY.Width =  valueY > this.ellipseY.Width + this.sensitivity || valueY < this.ellipseY.Width - this.sensitivity ? valueY : this.ellipseY.Width;
            this.ellipseX.Height = valueX > this.ellipseX.Height + this.sensitivity || valueX < this.ellipseX.Height - this.sensitivity ? valueX : this.ellipseX.Height;
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
