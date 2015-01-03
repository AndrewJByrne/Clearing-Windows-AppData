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
            await FileIO.WriteTextAsync(newTextFile, String.Format("File created at: {0}", DateTime.Now.ToString()));
            LastStatus.Text = String.Format("Created {0}", newTextFile.Path);
        }

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
