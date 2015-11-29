using System;
using System.ComponentModel;

namespace OmegaSplicer.Model
{
    public class JoyStick : INotifyPropertyChanged
    {
        public string Background { get; set; }

        public string Stick { get; set; }

        public double CanvasX { get; set; }

        public double CanvasY { get; set; }

        public double JoystickX { get; set; }

        public double JoystickY { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public JoyStick()
        {
            this.CanvasX = 0;
            this.CanvasY = 0;
            this.JoystickX = 0;
            this.JoystickY = 0;
        }

        public void Holding()
        { }

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

        public JoyStick GetCopy()
        {
            JoyStick copy = (JoyStick)this.MemberwiseClone();
            return copy;
        }
    }
}
