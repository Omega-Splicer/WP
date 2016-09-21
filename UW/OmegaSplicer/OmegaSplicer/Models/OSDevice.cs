using System;
using System.ComponentModel;

namespace OmegaSplicer.Models
{
    public class OSDevice : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private double battery = 0;

        public double Battery
        {
            get { return this.battery; }
            set
            {
                if (this.battery != value)
                {
                    this.battery = value;
                    if (this.battery <= 25)
                        this.BatteryIcon = "/Assets/battery_25.png";
                    else if (this.battery <= 50)
                        this.BatteryIcon = "/Assets/battery_50.png";
                    else if (this.battery <= 75)
                        this.BatteryIcon = "/Assets/battery_75.png";
                    else
                        this.BatteryIcon = "/Assets/battery_full.png";
                    this.RaisePropertyChanged("Battery");
                }
            }
        }

        private string batteryIcon;

        public string BatteryIcon
        {
            get { return this.batteryIcon; }
            set { this.batteryIcon = value; }
        }

        private double motorLeft = 0;

        public double MotorLeft
        {
            get { return this.motorLeft; }
            set { this.motorLeft = value; }
        }

        private double motorRight = 0;

        public double MotorRight
        {
            get { return this.motorRight; }
            set { this.motorRight = value; }
        }

        private double power = 0;

        public double Power
        {
            get { return this.power; }
            set
            {
                if (this.power != value)
                {
                    this.power = value;
                    this.SetMotorsPower();
                    this.RaisePropertyChanged("Power");
                }
            }
        }

        private double signal = 0;

        public double Signal
        {
            get { return this.signal; }
            set { this.signal = value; }
        }

        private double direction = 0;

        public double Direction
        {
            get { return this.direction; }
            set
            {
                if (this.direction != value)
                {
                    this.direction = value;
                    if (this.power != 0)
                        SetMotorsPower();
                    this.RaisePropertyChanged("Direction");
                }
            }
        }

        public OSDevice() { }

        // Create a copy of an accomplishment to save.
        // If your object is databound, this copy is not databound.
        public OSDevice GetCopy()
        {
            OSDevice copy = (OSDevice)this.MemberwiseClone();
            return copy;
        }

        // Reset the power add coordonate of the module
        public void Reset()
        {
            this.power = 0;
        }

        //Change motors power depending of the percent receive
        private void SetMotorsPower()
        {
            this.motorRight = this.power;
            this.motorLeft = this.power;

            if (this.direction > 5)
                this.MotorLeft = this.power - this.power * this.direction / 100;
            else if (this.direction < -5)
                this.MotorRight = this.power - this.power * -this.direction / 100;
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
    }
}
