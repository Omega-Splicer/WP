using OmegaSplicer.Models;
using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace OmegaSplicer
{
    public partial class MenuDrawer : UserControl
    {
        List<MenuDrawerItem> itemList = new List<MenuDrawerItem>();
        static MenuDrawer instance;

        public MenuDrawer()
        {
            this.InitializeComponent();
            MenuContent.ItemsSource = itemList;
            PageContent.Navigated += OnNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += MenuBackRequested;
        }

        public static MenuDrawer GetInstance()
        {
            if (instance == null)
                instance = new MenuDrawer();
            return instance;
        }

        public void Title(string title)
        {
            this.TitlePage.Text = title;
        }

        public bool AddItem(string icon, string name)
        {
            this.itemList.Add(new MenuDrawerItem(icon, name));
            return true;
        }

        private void HamburgerButton_Click(object Sender, RoutedEventArgs e)
        {
            SplitViewMenu.IsPaneOpen = !SplitViewMenu.IsPaneOpen;
        }

        public void NavigateTo(Type page)
        {
            //this.TitlePage.Text = ((MenuDrawerItem)MenuContent.SelectedItem).Name;
            SplitViewMenu.IsPaneOpen = false;
            PageContent.Navigate(page);
        }

        public ListBox GetList()
        {
            return MenuContent;
        }

        public void HideMenu()
        {
            TitleBar.Height = 0;
            SplitViewMenu.CompactPaneLength = 0;
        }

        public void ShowMenu()
        {
            TitleBar.Height = 50;
            SplitViewMenu.CompactPaneLength = 50;
        }

        public void SelectItem(int index)
        {
            if (itemList[index] != null)
                MenuContent.SelectedItem = itemList[index];
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Each time a navigation event occurs, update the Back button's visibility
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }

        private void MenuBackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            if (PageContent == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (PageContent.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                PageContent.GoBack();
            }
        }
    }
}
