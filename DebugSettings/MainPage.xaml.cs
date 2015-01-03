using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DebugSettings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Add_Roaming_File_Click(object sender, RoutedEventArgs e)
        {
            await CreateFileInFolderAsync(Windows.Storage.ApplicationData.Current.RoamingFolder, "MyRoamingFile.txt");
        }

        private async void Add_Local_File_Click(object sender, RoutedEventArgs e)
        {
            await CreateFileInFolderAsync(Windows.Storage.ApplicationData.Current.LocalFolder, "MyLocalFile.txt");
        }

        private async void Add_Temp_File_Click(object sender, RoutedEventArgs e)
        {
            await CreateFileInFolderAsync(Windows.Storage.ApplicationData.Current.TemporaryFolder, "MyTempFile.txt");
        }

        private async Task CreateFileInFolderAsync(IStorageFolder targetFolder, string suggestedFileName)
        {
            var newTextFile = await targetFolder.CreateFileAsync(suggestedFileName, Windows.Storage.CreationCollisionOption.GenerateUniqueName);

            // This is a really useful way to write text to a file. For demo purposes, I will just store a timestamp in a text file. 
            await FileIO.WriteTextAsync(newTextFile, String.Format("File created at: {0}", DateTime.Now.ToString()));
            LastStatus.Text = String.Format("Created {0}", newTextFile.Path);
        }

        // When you write to Roaming or Local settings, those values are actually stored in the registry. 
        // To learn how to read those entires in the registry, check out http://lunarfrog.com/blog/2012/09/13/inspect-app-settings/
        private void Add_Roaming_Setting_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAppSetting(ApplicationData.Current.RoamingSettings);
        }

        private void Add_Local_Setting_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAppSetting(ApplicationData.Current.LocalSettings);
        }

        private void CreateNewAppSetting(ApplicationDataContainer settingContainer)
        {
            var settingKey = Guid.NewGuid().ToString();
            var settingValue = DateTime.Now.ToString();
            settingContainer.Values[settingKey] = settingValue;
            LastStatus.Text = String.Format("Created new {0} setting Key: {1} Value: {2}", settingContainer.Locality.ToString(), settingKey, settingValue);
        }

        // Even though the user can access the settings from the Settings Charm, I give the user an alternative here by opening the 
        // SettingsPane programmatially when the user taps on the settings button in the UI. 
        private void Show_Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsPane.Show();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // When the user opens the settings pane, I want to clear the status on this page, since it is no longer relevant.
            SettingsPane.GetForCurrentView().CommandsRequested += (s, a) => { LastStatus.Text = string.Empty; };
        }
    }
}
