using OmegaSplicer.Models;
using System.Collections.Generic;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OmegaSplicer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MenuDrawer menuDrawer;
        List<MenuDrawerItem> itemList = new List<MenuDrawerItem>();

        public enum Page
        {
            HOME,
            PAIR,
            NEWS,
            SETTINGS,
            FLY
        }

        public MainPage()
        {
            this.InitializeComponent();
            menuDrawer = MenuDrawer.GetInstance();
            this.InitializeMenuDrawer();
            menuDrawer.SelectItem((int)Page.HOME);
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {

                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = Colors.Black;
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }

        private void InitializeMenuDrawer()
        {
            MainContent.Children.Add(menuDrawer);

            menuDrawer.Title("Home");
            menuDrawer.AddItem("\xE10F", "Home");
            menuDrawer.AddItem("\xE724", "Pair");
            menuDrawer.AddItem("\xE734", "News");
            menuDrawer.AddItem("\xE115", "Settings");
            menuDrawer.GetList().SelectionChanged += (o, e) =>
            {
                ListBox list = menuDrawer.GetList();
                int selected = list.SelectedIndex;
                switch ((Page)list.SelectedIndex)
                {
                    case Page.HOME:
                        menuDrawer.NavigateTo(typeof(Views.OSHomePage));
                        break;
                    case Page.PAIR:
                        menuDrawer.NavigateTo(typeof(Views.OSPairPage));
                        break;
                    case Page.NEWS:
                        break;
                    case Page.SETTINGS:
                        menuDrawer.NavigateTo(typeof(Views.OSSettings));
                        break;
                    case Page.FLY:
                        menuDrawer.NavigateTo(typeof(Views.OSFlyPage));
                        break;
                }
            };       
        }
    }
}