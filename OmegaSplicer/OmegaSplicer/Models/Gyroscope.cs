using System;
using System.ComponentModel;
using OmegaSplicer;
using Windows.Devices.Sensors;

namespace OmegaSplicer.Model
{
    public class Gyroscope : INotifyPropertyChanged
    {
        private double _accelX;
        public double AccelX
        {
            get { return _accelX; }
            set 
            {
                value = Math.Round(value, 2);

                if (AccelX != value)
                {
                    _accelX = value;
                    RaisePropertyChanged("AccelX");
                }
            }
        }

        private double _accelY;
        public double AccelY
        {
            get { return _accelY; }
            set
            {
                value = Math.Round(value, 2);

                if (AccelY != value)
                {
                    _accelY = value;
                    RaisePropertyChanged("AccelY");
                }
            }
        }

        private double _accelZ;
        public double AccelZ
        {
            get { return _accelZ; }
            set
            {
                value = Math.Round(value, 2);

                if (AccelZ != value)
                {
                    _accelZ = value;
                    RaisePropertyChanged("AccelZ");
                }
            }
        }

        Accelerometer _accelerometer = Accelerometer.GetDefault();

        public Gyroscope()
        {
            _accelerometer.ReadingChanged += Accelerometer_ReadingChanged;

            _accelX = 0;
            _accelY = 0;
            _accelZ = 0;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerReadingChangedEventArgs e)
        {
            AccelX = e.Reading.AccelerationX;
            AccelY = e.Reading.AccelerationY;
            AccelZ = e.Reading.AccelerationZ;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                await Windows.ApplicationModel.Core.CoreApplication


                .MainView.CoreWindow.Dispatcher


                .RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                });
            }
        }

        // Create a copy of an accomplishment to save.
        // If your object is databound, this copy is not databound.
        public Gyroscope GetCopy()
        {
            Gyroscope copy = (Gyroscope)this.MemberwiseClone();
            return copy;
        }
    }
}
