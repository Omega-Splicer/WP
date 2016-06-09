using System;
using System.Windows;
using System.Collections.ObjectModel;
using Windows.Storage;
using OmegaSplicer.Model;
using OmegaSplicer.Common;
// using Windows.UI.Xaml.Controls;
// using System.IO.IsolatedStorage;

namespace OmegaSplicer.ViewModelNamespace
{
    public class OSViewModel
    {
        private ObservableCollection<OSDevice> _devices;

        private OSDevice _selectedDevice;

/*
        private UserControl _selectedControl;

        private Joystick _joystick = new Joystick();

        private Gyroscope _gyro = new Gyroscope();

        public Gyroscope Gyroscope
        {
            get { return this._gyro; }
            set
            {
                if (this._gyro == value)
                    return;
                this._gyro = value;
                // Do logic on selection change.
            }
        }

        public Joystick Joystick
        {
            get { return this._joystick; }
            set
            {
                if (this._joystick == value)
                    return;
                this._joystick = value;
                // Do logic on selection change.
            }   
        }

        public UserControl SelectedControl
        {
            get { return this._selectedControl; }
            set
            {
                if (this._selectedControl == value)
                    return;
                this._selectedControl = value;
                // Do logic on selection change.
            }
        }
*/
        public OSDevice SelectedModule
        {
            get { return this._selectedDevice; }
            set
            {
                if (this._selectedDevice == value)
                    return;
                this._selectedDevice = value;
                // Do logic on selection change.
            }
        }

        public ObservableCollection<OSDevice> Modules
        {
            get { return this._devices; }
            set { this._devices = value; }
        }

        public OSViewModel() 
        { 
            this.GetDevices();
        }

        public void GetDevices()
        {
            /*
            if (ApplicationData.Current.LocalSettings.Values.Count > 0)
            {
                this.GetSavedDevices();
            }
            else
            {
                this.GetDefaultDevices();
            }
             */
            this.GetDefaultDevices();
        }

        public void GetDefaultDevices()
        {
            ObservableCollection<OSDevice> a = new ObservableCollection<OSDevice>();

            // Items to collect
            a.Add(new OSDevice() { Name = "OmegaSplicer1", Power = 100, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });
            a.Add(new OSDevice() { Name = "OmegaSplicer2", Power = 50, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });
            a.Add(new OSDevice() { Name = "OmegaSplicer3", Power = 25, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });
            a.Add(new OSDevice() { Name = "OmegaSplicer4", Power = 75, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });
            a.Add(new OSDevice() { Name = "OmegaSplicer5", Power = 0, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });
            a.Add(new OSDevice() { Name = "OmegaSplicer6", Power = 80, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });
            a.Add(new OSDevice() { Name = "OmegaSplicer7", Power = 60, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });
            a.Add(new OSDevice() { Name = "OmegaSplicer8", Power = 40, Coor = 0, Motors = 0, Image = "/Assets/Paper-Plane.png", Direction = "CENTER" });

            this._devices = a;
            //MessageBox.Show("Got modules from default");
        }


        public void GetSavedDevices()
        {
            ObservableCollection<OSDevice> a = new ObservableCollection<OSDevice>();

            foreach (Object o in ApplicationData.Current.LocalSettings.Values)
            {
                a.Add((OSDevice)o);
            }

            this._devices = a;
            //MessageBox.Show("Got devices from storage");
        }
    }
}

