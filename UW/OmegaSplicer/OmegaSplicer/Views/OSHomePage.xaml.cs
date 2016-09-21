using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OmegaSplicer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OSHomePage : Page
    {
        MenuDrawer menu;
        public OSHomePage()
        {
            this.InitializeComponent();
            menu = MenuDrawer.GetInstance();
            if (menu != null)
            {
                menu.Title("Home");
                menu.HideMenu();
                menu.SelectItem((int)MainPage.Page.HOME);
            }
        }

        private void FlyButton_Click(object sender, RoutedEventArgs e)
        {
            if (menu != null)
            {
                menu.SelectItem((int)MainPage.Page.PAIR);
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (menu != null)
            {
                menu.SelectItem((int)MainPage.Page.SETTINGS);
            }
        }

        private void NewsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
