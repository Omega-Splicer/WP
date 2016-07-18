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
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.Storage.Streams;
using OmegaSplicer.Services;
using Windows.ApplicationModel.Background;

namespace OmegaSplicer
{
    class OSBluetoothManager : IBluetoothManager
    {
        BluetoothLEDevice   currentDevice;
        Dictionary<string, Guid>              serviceList = new Dictionary<string, Guid>();
        Dictionary<string, BluetoothLEDevice>   mapDevices = new Dictionary<string,BluetoothLEDevice>();
        
        OSBluetoothManager()
        {
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

        public bool pairageDevice(String name, out BluetoothLEDevice device)
        {
            bool ret = mapDevices.TryGetValue(name, out this.currentDevice);

            if (ret)
            {
                device = this.currentDevice;
                initializeServiceList();
            }
            else
                device = null;

            return ret;
        }

        public bool pairageDevice(String name)
        {
            bool ret = mapDevices.TryGetValue(name, out this.currentDevice);

            return ret;
        }

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
                //GattCharacteristicNotificationTrigger;
                //DeviceChangeTrigger
                //DeviceUpdateTrigger

                serviceList.Add(serviceName, service.Uuid);
            }
        }

        public async Task<byte[]> GetPropery(Guid gattCharacteristicUuids)
        {
            try
            {
                var gattDeviceService = await GattDeviceService.FromIdAsync(currentDevice.DeviceId);
                if (gattDeviceService != null)
                {
                    var characteristics = gattDeviceService.GetCharacteristics(gattCharacteristicUuids).First();

                    //If the characteristic supports Notify then tell it to notify us.
                    try
                    {
                        if (characteristics.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Notify))
                        {
                            characteristics.ValueChanged += characteristics_ValueChanged;
                            await characteristics.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                        }
                    }
                    catch { }

                    //Read
                    if (characteristics.CharacteristicProperties.HasFlag(GattCharacteristicProperties.Read))
                    {
                        var result = await characteristics.ReadValueAsync(BluetoothCacheMode.Uncached);

                        if (result.Status == GattCommunicationStatus.Success)
                        {
                            byte[] forceData = new byte[result.Value.Length];
                            DataReader.FromBuffer(result.Value).ReadBytes(forceData);
                            return forceData;
                        }
                        else
                        {
                            await new MessageDialog(result.Status.ToString()).ShowAsync();
                        }
                    }
                }
                else
                {
                    await new MessageDialog("Access to the device has been denied =(").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        void characteristics_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            byte[] data = new byte[args.CharacteristicValue.Length];
            Windows.Storage.Streams.DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(data);

            //Update properties
            if (sender.Uuid == GattCharacteristicUuids.BatteryLevel)
            {
                //BatteryLevel = Convert.ToInt32(data[0]);
            }
        }
    }
}
