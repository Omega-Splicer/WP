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
            get { return this._accelX; }
            set 
            {
                value = Math.Round(value, 2);

                if (this._accelX != value)
                {
                    this._accelX = value;
                    this.RaisePropertyChanged("AccelX");
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
                    this.RaisePropertyChanged("AccelY");
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
                    this.RaisePropertyChanged("AccelZ");
                }
            }
        }

        Accelerometer _accelerometer = Accelerometer.GetDefault();

        public Gyroscope()
        {
            this._accelerometer.ReadingChanged += this.Accelerometer_ReadingChanged;

            this._accelX = 0;
            this._accelY = 0;
            this._accelZ = 0;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerReadingChangedEventArgs e)
        {
            this.AccelX = e.Reading.AccelerationX;
            this.AccelY = e.Reading.AccelerationY;
            this.AccelZ = e.Reading.AccelerationZ;
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
