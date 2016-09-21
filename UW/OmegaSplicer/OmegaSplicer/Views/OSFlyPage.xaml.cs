using OmegaSplicer.ViewModels;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OmegaSplicer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OSFlyPage : Page
    {
        MenuDrawer menu;
        public OSFlyPage()
        {
            this.InitializeComponent();

            menu = MenuDrawer.GetInstance();
            if (menu != null)
                menu.HideMenu();
            Settings settings = ((Settings)ControlPanel.DataContext);
            ControlGyroscope.Enable = settings.SetGyroscope;
            ControlJoystick.Enable = settings.SetPad;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            base.OnNavigatedTo(e);
        }

    }
}
