using OmegaSplicer.ViewModels;
using Windows.UI.Xaml.Controls;

namespace OmegaSplicer.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OSPairPage : Page
    {
        MenuDrawer menu;
        public OSPairPage()
        {
            this.InitializeComponent();
            menu = MenuDrawer.GetInstance();
            if (menu != null)
            {
                menu.Title("Pair");
                menu.ShowMenu();
                menu.SelectItem((int)MainPage.Page.PAIR);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (menu != null)
            {
                ((OSDevicesViewModel)this.DataContext).SelectedItem = (string)DevicesList.SelectedValue;
                menu.NavigateTo(typeof(Views.OSFlyPage));
            }
        }
    }
}
