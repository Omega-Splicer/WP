using System;
using System.ComponentModel;


namespace OmegaSplicer.Model
{
    public class OSDevice : INotifyPropertyChanged
    {
        // The name of the module.
        public string Name { get; set; }

        // The Image of the module
        public string Image { get; set; }

        public string Battery { get; set; }

        // MaxPower of the motors.
        private int _motors;
        public int Motors 
        {
            get { return this._motors; }
            set
            {
                if (this._motors != value)
                {
                    this._motors = value;
                    if (this._percent != 0)
                        this.SetMotorsPower();
                    this.RaisePropertyChanged("Motors");
                }
            }
        }

        // Power of the left motor.
        private int _motorleft;
        public int MotorLeft
        {
            get { return this._motorleft; }
            set
            {
                if (this._motorleft != value)
                {
                    this._motorleft = value;
                    this.RaisePropertyChanged("MotorLeft");
                }
            }
        }

        // Power of the right motors.
        private int _motorright;
        public int MotorRight
        {
            get { return this._motorright; }
            set
            {
                if (this._motorright != value)
                {
                    this._motorright = value;
                    this.RaisePropertyChanged("MotorRight");
                }
            }
        }

        // The direction of the module
        private string _direction;
        public string Direction
        {
            get { return this._direction; }
            set
            {
                if (this._direction != value)
                {
                    this._direction = value;
                    this.RaisePropertyChanged("Direction");
                }
            }
        }

        // The direction of the module
        private int _percent = 0;
        public int Percent
        {
            get { return this._percent; }
            set
            {
                if (this._percent != value)
                {
                    this._percent = value;
                    if (this._motors != 0)
                        this.SetMotorsPower();
                    this.RaisePropertyChanged("Percent");
                }
            }
        }

        //Change motors power depending of the percent receive
        private void SetMotorsPower()
        {
            this.MotorRight = this._motors;
            this.MotorLeft = this._motors;

            if (this._percent > 5)
                this.MotorLeft -= this._motors * this._percent / 100;
            else if (this._percent < -5)
                this.MotorRight -= this._motors * -this._percent / 100;
        }

        // The coordonates of the module.
        public int Coor { get; set; }

        // The Power of the module.
        private int _power;

        public int Power
        {
            get { return this._power; } 
            set 
            {
                if (this._power != value)
                {
                    this._power = value;
                    if (this._power <= 25)
                        this.Battery = "/Assets/battery_25.png";
                    else if (this._power <= 50)
                        this.Battery = "/Assets/battery_50.png";
                    else if (this._power <= 75)
                        this.Battery = "/Assets/battery_75.png";
                    else
                        this.Battery = "/Assets/battery_full.png";
                    this.RaisePropertyChanged("Battery");
                }
            }
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

        public OSDevice() {}

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
            this._motors = 0;
            this.Coor = 0;
        }
    }
}
