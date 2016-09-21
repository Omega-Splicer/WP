using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OmegaSplicer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OSSettings : Page
    {
        MenuDrawer menu;
        public OSSettings()
        {
            this.InitializeComponent();
            menu = MenuDrawer.GetInstance();
            if (menu != null)
            {
                menu.Title("Settings");
                menu.ShowMenu();
                menu.SelectItem((int)MainPage.Page.SETTINGS);
            }
        }        

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

        private void ControlButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == Pad)
            {
                if (Gyroscope != null)
                    Gyroscope.IsChecked = false;
            }
            else if (sender == Gyroscope)
            {
                if (Pad != null)
                    Pad.IsChecked = false;
            }
        }

        private void UnitButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == UnitKMPH)
            {
                if (UnitMPS != null)
                    UnitMPS.IsChecked = false;
                if (UnitMPH != null)
                    UnitMPH.IsChecked = false;
            }
            else if (sender == UnitMPS)
            {
                if (UnitKMPH != null)
                    UnitKMPH.IsChecked = false;
                if (UnitMPH != null)
                    UnitMPH.IsChecked = false;
            }
            else if (sender == UnitMPH)
            {
                if (UnitKMPH != null)
                    UnitKMPH.IsChecked = false;
                if (UnitMPS != null)
                    UnitMPS.IsChecked = false;
            }
        }
    }
}
