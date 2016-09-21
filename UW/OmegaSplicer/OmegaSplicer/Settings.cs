using OmegaSplicer.Common;
using Windows.Storage;

namespace OmegaSplicer
{
    public class Settings : ObservableSettings
    {
        private static Settings settings = new Settings();
        public static Settings Default
        {
            get { return settings; }
        }

        public Settings()
            : base(ApplicationData.Current.LocalSettings)
        {
        }

        [DefaultSettingValue(Value = true)]
        public bool SetGyroscope
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        [DefaultSettingValue(Value = false)]
        public bool SetPad
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        [DefaultSettingValue(Value = true)]
        public bool UnitKMPH
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        [DefaultSettingValue(Value = false)]
        public bool UnitMPS
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        [DefaultSettingValue(Value = false)]
        public bool UnitMPH
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }
    }
}
