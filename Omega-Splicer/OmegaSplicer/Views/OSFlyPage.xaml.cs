﻿using OmegaSplicer.Common;
using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OmegaSplicer.ViewModelNamespace;
using OmegaSplicer.Model;
using Windows.Storage;
using System.ComponentModel;

// Pour en savoir plus sur le modèle d'élément Page de base, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace OmegaSplicer
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FlyPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private UserControl control;

        public FlyPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;

            Binding binding = new Binding();
            binding.ElementName = "Percent";
            binding.Source = ((OSDevicesViewModel)this.DataContext);
            binding.Path = new PropertyPath("SelectedDevice.Percent");
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            if (settings.Values["RadioGyroControl"] == null || (bool)settings.Values["RadioGyroControl"] == false)
            {
                Joystick my_joystick = new Joystick();
//                DependencyProperty dp = DependencyProperty.RegisterAttached("Direction", typeof(string), typeof(Joystick), null);
                my_joystick.SetBinding(my_joystick.GetPercentDependency(), binding);
                my_joystick.Joystick_Start();
                this.joystick.Children.Add(my_joystick);

                control = my_joystick;
//                this.gyroscope.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
//                this.joystick.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                MyGyroscope my_gyroscope = new MyGyroscope();
//                DependencyProperty dp = DependencyProperty.RegisterAttached("Direction", typeof(string), typeof(MyGyroscope), null);
                my_gyroscope.SetBinding(my_gyroscope.GetPercentDependency(), binding);
                my_gyroscope.Gyroscope_Start();
                this.gyroscope.Children.Add(my_gyroscope);

                control = my_gyroscope;
//                this.joystick.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
//                this.gyroscope.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        #region NavigationHelper
        /// <summary>
        /// Obtient le <see cref="NavigationHelper"/> associé à ce <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Obtient le modèle d'affichage pour ce <see cref="Page"/>.
        /// Cela peut être remplacé par un modèle d'affichage fortement typé.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Remplit la page à l'aide du contenu passé lors de la navigation. Tout état enregistré est également
        /// fourni lorsqu'une page est recréée à partir d'une session antérieure.
        /// </summary>
        /// <param name="sender">
        /// La source de l'événement ; en général <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Données d'événement qui fournissent le paramètre de navigation transmis à
        /// <see cref="Frame.Navigate(Type, Object)"/> lors de la requête initiale de cette page et
        /// un dictionnaire d'état conservé par cette page durant une session
        /// antérieure.  L'état n'aura pas la valeur Null lors de la première visite de la page.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Conserve l'état associé à cette page en cas de suspension de l'application ou de
        /// suppression de la page du cache de navigation.  Les valeurs doivent être conformes aux
        /// exigences en matière de sérialisation de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">La source de l'événement ; en général <see cref="NavigationHelper"/></param>
        /// <param name="e">Données d'événement qui fournissent un dictionnaire vide à remplir à l'aide de l'
        /// état sérialisable.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async void HideStatusBar()
        {
           await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
        }

        #region Inscription de NavigationHelper

        /// <summary>
        /// Les méthodes fournies dans cette section sont utilisées simplement pour permettre
        /// NavigationHelper pour répondre aux méthodes de navigation de la page.
        /// <para>
        /// La logique spécifique à la page doit être placée dans les gestionnaires d'événements pour  
        /// <see cref="NavigationHelper.LoadState"/>
        /// et <see cref="NavigationHelper.SaveState"/>.
        /// Le paramètre de navigation est disponible dans la méthode LoadState 
        /// en plus de l'état de page conservé durant une session antérieure.
        /// </para>
        /// </summary>
        /// <param name="e">Fournit des données pour les méthodes de navigation et
        /// les gestionnaires d'événements qui ne peuvent pas annuler la requête de navigation.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.HideStatusBar();

            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.HideStatusBar();

            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void MainPage(object sender, RoutedEventArgs e)
        {
            if (control.GetType() == typeof(Joystick))
                control.SetBinding(((Joystick)control).GetPercentDependency(), new Binding());

            if (control.GetType() == typeof(MyGyroscope))
                control.SetBinding(((MyGyroscope)control).GetPercentDependency(), new Binding());

            ((OSDevicesViewModel)this.DataContext).SelectedDevice.Reset();
            if (this.navigationHelper.CanGoBack())
                this.navigationHelper.GoBack();
        }
        #endregion

        #endregion
    }
}
