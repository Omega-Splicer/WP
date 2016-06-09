using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand;
using InTheHand.Devices;
using InTheHand.Devices.Enumeration;
using System.IO;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using OmegaSplicer.Common;

namespace OmegaSplicer
{
    class OSBluetoothManager
    {
        BluetoothLEDevice   currentDevice;
        Dictionary<string, Guid>              serviceList = new Dictionary<string, Guid>();
        Dictionary<string, BluetoothLEDevice>   mapDevices = new Dictionary<string,BluetoothLEDevice>();
        /*
        BluetoothClient bluetoothClient;
        BluetoothComponent bluetoothComponent;
        Dictionary<string, BluetoothDeviceInfo> mapDevices;
        private String DEVICE_PIN = "4303";
        */
        
        OSBluetoothManager()
        {
            /*
            // client is used to manage connections
            bluetoothClient = new BluetoothClient();
            // component is used to manage device discovery
            bluetoothComponent = new BluetoothComponent(bluetoothClient);
            // async methods, can be done synchronously too

            bluetoothComponent.DiscoverDevicesAsync(255, true, true, true, true, null);
            bluetoothComponent.DiscoverDevicesProgress += new EventHandler<DiscoverDevicesEventArgs>(discoverDevicesProgress);
            bluetoothComponent.DiscoverDevicesComplete += new EventHandler<DiscoverDevicesEventArgs>(discoverDevicesComplete);
             * */

            discoverDevices();
        }

        private async void discoverDevices()
        {
            foreach (DeviceInformation di in await DeviceInformation.FindAllAsync(BluetoothLEDevice.GetDeviceSelector()))
            {
                BluetoothLEDevice bleDevice = await BluetoothLEDevice.FromIdAsync(di.Id);
                this.mapDevices.Add(bleDevice.Name, bleDevice);
            }
            Notification.UpdateTile(this.mapDevices.Count);
        }

        /*
        private void discoverDevicesProgress(object sender, DiscoverDevicesEventArgs e)
        {
            // save all found devices
            for (int i = 0; i < e.Devices.Length; i++)
            {
                String name = "OmegaSplicer" + i.ToString();
                if (e.Devices[i].Remembered)
                {
                }
                else
                {
                }
                this.mapDevices.Add(name, e.Devices[i]);
            }
        }

        private void discoverDevicesComplete(object sender, DiscoverDevicesEventArgs e)
        {
            // log some stuff udpdate ViewModel
        }
        */

        public bool pairageDevice(String name, out BluetoothLEDevice device)
        {
            bool ret = mapDevices.TryGetValue(name, out this.currentDevice);

            device = this.currentDevice;
            initializeServiceList();

            return ret;
        }

        /*
        public void connectDevice(String name, Action<IAsyncResult> callback)
        {
            BluetoothDeviceInfo device;
            bool ret = mapDevices.TryGetValue(name, out device);
            // check if device is paired
            if (device.Authenticated)
            {
                // set pin of device to connect with
                bluetoothClient.SetPin(DEVICE_PIN);
                // async connection method
                bluetoothClient.BeginConnect(device.DeviceAddress, BluetoothService.SerialPort, new AsyncCallback(callback), device);           
            }
        }

        // callback
        private void Connect(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                // client is connected now
            }
        }
        */

        private void initializeServiceList()
        {
            if (this.currentDevice == null)
                return;

            string  serviceStrUuid;
            string  serviceName;
            serviceList.Clear();

            foreach (GattDeviceService service in this.currentDevice.GattServices)
            {
                serviceStrUuid = service.Uuid.ToString();
                switch (serviceStrUuid)
                {
                    case "00001811-0000-1000-8000-00805f9b34fb":
                        serviceName = "AlertNotification";
                        break;
                    case "0000180f-0000-1000-8000-00805f9b34fb":
                        serviceName = "Battery";
                        break;
                    case "0000180a-0000-1000-8000-00805f9b34fb":
                        serviceName = "DeviceInformation";
                        break;
                    case "00001802-0000-1000-8000-00805f9b34fb":
                        serviceName = "ImmediateAlert";
                        break;
                    case "00001819-0000-1000-8000-00805f9b34fb":
                        serviceName = "LocationAndNavigation";
                        break;
                    default:
                        serviceName = serviceStrUuid;
                        break;
                }
                serviceList.Add(serviceName, service.Uuid);
            }
        }



    }
}
