using System;
using System.Windows;
using System.Collections.ObjectModel;
using Windows.Storage;
using OmegaSplicer.Model;
using OmegaSplicer.Common;
using OmegaSplicer.Models;

namespace OmegaSplicer.ViewModelNamespace
{
    public class OSDevicesViewModel
    {
        private ObservableCollection<Item> _devices;

        private OSDevice _selectedDevice;

        public OSDevice SelectedDevice
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

        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return this._selectedItem; }
            set
            {
                if (this._selectedItem == value)
                    return;
                this._selectedItem = value;
                this.SelectedDevice = new OSDevice() { Name = this._selectedItem.Name, Power = 50, Motors = 0, Percent = 0 };
                // Do logic on selection change.
            }
        }

        public ObservableCollection<Item> Devices
        {
            get { return this._devices; }
            set { this._devices = value; }
        }

        public OSDevicesViewModel() 
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
            ObservableCollection<Item> a = new ObservableCollection<Item>();

            // Items to collect
            a.Add(new Item() { Name = "OmegaSplicer1", Image = "/Assets/Paper-Plane.png" });
            a.Add(new Item() { Name = "OmegaSplicer2", Image = "/Assets/Paper-Plane.png" });
            a.Add(new Item() { Name = "OmegaSplicer3", Image = "/Assets/Paper-Plane.png" });
            a.Add(new Item() { Name = "OmegaSplicer4", Image = "/Assets/Paper-Plane.png" });
            a.Add(new Item() { Name = "OmegaSplicer5", Image = "/Assets/Paper-Plane.png" });
            a.Add(new Item() { Name = "OmegaSplicer6", Image = "/Assets/Paper-Plane.png" });
            a.Add(new Item() { Name = "OmegaSplicer7", Image = "/Assets/Paper-Plane.png" });

            this._devices = a;
        }


        public void GetSavedDevices()
        {
            ObservableCollection<Item> a = new ObservableCollection<Item>();

            foreach (Object o in ApplicationData.Current.LocalSettings.Values)
            {
                a.Add((Item)o);
            }

            this._devices = a;
        }
    }
}

