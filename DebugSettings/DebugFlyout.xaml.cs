using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace DebugSettings
{
    public sealed partial class DebugFlyout : SettingsFlyout
    {
        public DebugFlyout()
        {
            this.InitializeComponent();
        }


        private async void ClearData_Click(object sender, RoutedEventArgs e)
        {
            if (ClearLocal.IsChecked.HasValue && ClearLocal.IsChecked.Value)
            {
                Debug.WriteLine("Clearing local app data");
                await ApplicationData.Current.ClearAsync(ApplicationDataLocality.Local);

            }

            if (ClearRoaming.IsChecked.HasValue && ClearRoaming.IsChecked.Value)
            {
                Debug.WriteLine("Clearing roaming app data");
                await ApplicationData.Current.ClearAsync(ApplicationDataLocality.Roaming);
            }

            if (ClearTemporary.IsChecked.HasValue && ClearTemporary.IsChecked.Value)
            {
                Debug.WriteLine("Clearing temporary app data");
                await ApplicationData.Current.ClearAsync(ApplicationDataLocality.Temporary);
            }

            if (ClearLocalCache.IsChecked.HasValue && ClearLocalCache.IsChecked.Value)
            {
                Debug.WriteLine("Clearing local cache app data");
                await ApplicationData.Current.ClearAsync(ApplicationDataLocality.LocalCache);
            }
        }

        private void CopyLocal_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(Windows.Storage.ApplicationData.Current.LocalFolder.Path);
            Clipboard.SetContent(dp);
        }

        private void CopyLocalCache_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();

            // All other app data folders are accessed through a property on Windows.Storage.ApplicationData.Current. 
            // I have to create the local cache path based on its relative location to all other folders myself. 
            dp.SetText(Path.Combine(Path.GetDirectoryName(Windows.Storage.ApplicationData.Current.RoamingFolder.Path),"LocalCache"));
            Clipboard.SetContent(dp);
        }

        private void CopyTemporary_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(Windows.Storage.ApplicationData.Current.TemporaryFolder.Path);
            Clipboard.SetContent(dp);
        }

        private void CopyRoaming_Click(object sender, RoutedEventArgs e)
        {
            DataPackage dp = new DataPackage();
            dp.SetText(Windows.Storage.ApplicationData.Current.RoamingFolder.Path);
            Clipboard.SetContent(dp);
        }
    }
}
