using System;
using System.ComponentModel;


namespace OmegaSplicer.Model
{
    public class Module : INotifyPropertyChanged
    {
        // The name of the module.
        public string Name { get; set; }

        // The Image of the module
        public string Image { get; set; }

        public string Battery { get; set; }

        // The Motor's Power of the motor.
        public int Motor { get; set; }

        // The coordonates of the module.
        public int Coor { get; set; }

        // The Power of the module.
        private int _power;
        public int Power
        {
            get { return _power; } 
            set 
            {
            if (_power != value)
                {
                    _power = value;
                    if (_power <= 25)
                        Battery = "Assets/battery_25.png";
                    else if (_power <= 50)
                        Battery = "Assets/battery_50.png";
                    else if (_power <= 75)
                        Battery = "Assets/battery_75.png";
                    else
                        Battery = "Assets/battery_full.png";
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


        // Create a copy of an accomplishment to save.
        // If your object is databound, this copy is not databound.
        public Module GetCopy()
        {
            Module copy = (Module)this.MemberwiseClone();
            return copy;
        }

        // Reset the power add coordonate of the module
        public void Reset()
        {
            Motor = 0;
            Coor = 0;
        }
    }
}
