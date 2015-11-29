using System;
using System.Windows;
using System.Collections.ObjectModel;
using Windows.Storage;
using OmegaSplicer.Model;
// using System.IO.IsolatedStorage;

namespace OmegaSplicer.ViewModelNamespace
{
    public class ViewModel
    {
        private ObservableCollection<Module> _modules;

        private Module _selectedModule;

        private JoyStick _joystick = new JoyStick();

        private Gyroscope _gyro = new Gyroscope();

        public Gyroscope SelectedGyro
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

        public JoyStick SelectedJoystick
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

        public Module SelectedModule
        {
            get { return this._selectedModule; }
            set
            {
                if (this._selectedModule == value)
                    return;
                this._selectedModule = value;
                // Do logic on selection change.
            }
        }

        public ObservableCollection<Module> Modules
        {
            get { return this._modules; }
            set { this._modules = value; }
        }

        public ViewModel() 
        { this.GetModules(); }

        public void GetModules()
        {
            if (ApplicationData.Current.LocalSettings.Values.Count > 0)
            {
                this.GetSavedModules();
            }
            else
            {
                this.GetDefaultModules();
            }
        }

        public void GetDefaultModules()
        {
            ObservableCollection<Module> a = new ObservableCollection<Module>();

            // Items to collect
            a.Add(new Module() { Name = "OmegaSplicer1", Power = 100, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });
            a.Add(new Module() { Name = "OmegaSplicer2", Power = 50, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });
            a.Add(new Module() { Name = "OmegaSplicer3", Power = 25, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });
            a.Add(new Module() { Name = "OmegaSplicer4", Power = 75, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });
            a.Add(new Module() { Name = "OmegaSplicer5", Power = 0, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });
            a.Add(new Module() { Name = "OmegaSplicer6", Power = 80, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });
            a.Add(new Module() { Name = "OmegaSplicer7", Power = 60, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });
            a.Add(new Module() { Name = "OmegaSplicer8", Power = 40, Coor = 0, Motor = 0, Image = "Assets/Paper-Plane.png" });

            this._modules = a;
            //MessageBox.Show("Got modules from default");
        }


        public void GetSavedModules()
        {
            ObservableCollection<Module> a = new ObservableCollection<Module>();

            foreach (Object o in ApplicationData.Current.LocalSettings.Values)
            {
                a.Add((Module)o);
            }

            this._modules = a;
            //MessageBox.Show("Got modules from storage");
        }
    }
}

