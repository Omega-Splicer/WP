using OmegaSplicer.Common;
using OmegaSplicer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;

namespace OmegaSplicer.ViewModels
{
    public class OSDevicesViewModel
    {
        private ObservableCollection<string> devices;

        private OSDevice selectedDevice;

        private BLEManager bleManager;

        public OSDevice SelectedDevice
        {
            get { return this.selectedDevice; }
            set
            {
                if (this.selectedDevice == value)
                    return;
                this.selectedDevice = value;
                // Do logic on selection change.
            }
        }

        private string selectedItem;

        public string SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem == value)
                    return;
                this.selectedItem = value;
                this.SelectDevice(value);
                // Do logic on selection change.
            }
        }

        private void SelectDevice(string devicename)
        {
            this.SelectedDevice = new OSDevice() { Name = this.selectedItem, Battery = 50, Power = 0, Direction = 0 };
            /*
            this.bleManager.selectDevice(devicename);
            this.bleManager.selectService(Parser.SERVICE_UUID);
            this.SelectedDevice.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == "Power" || e.PropertyName == "Direction")
                {
                    List<double> values = new List<double>();
                    values.Add(SelectedDevice.MotorLeft);
                    values.Add(SelectedDevice.MotorRight);
                    Write<double>(values);
                }
            };
            */
        }

        public async void Write<T>(List<T> values)
        {
            await bleManager.write(Parser.WRITE_UUID, Parser.WriteMessage<T>(Parser.Value.MOTORS, values));
        }

        public ObservableCollection<string> Devices
        {
            get { return this.devices; }
            set { this.devices = value; }
        }

        public OSDevicesViewModel()
        {
            this.bleManager = BLEManager.GetInstance();
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
            ObservableCollection<string> a = new ObservableCollection<string>();

            // Items to collect
            a.Add("OmegaSplicer1");
            a.Add("OmegaSplicer2");
            a.Add("OmegaSplicer3");
            a.Add("OmegaSplicer4");
            a.Add("OmegaSplicer5");
            a.Add("OmegaSplicer6");
            a.Add("OmegaSplicer7");
            a.Add("OmegaSplicer8");
            a.Add("OmegaSplicer9");

            this.devices = a;
        }

        public async void GetBLEDevices()
        {
            ObservableCollection<string> a = new ObservableCollection<string>();

            List<string> list = await bleManager.getDevices();

            foreach (string item in list)
                a.Add(item);

            this.devices = a;
        }

        public void GetSavedDevices()
        {
            ObservableCollection<string> a = new ObservableCollection<string>();

            foreach (Object o in ApplicationData.Current.LocalSettings.Values)
            {
                a.Add((string)o);
            }

            this.devices = a;
        }
    }
}
