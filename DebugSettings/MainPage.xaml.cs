using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            var roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
            var newTextFile = await roamingFolder.CreateFileAsync("MyRoamingFile.txt", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
            await FileIO.WriteTextAsync(newTextFile, String.Format("File created at: {0}", DateTime.Now.ToString()));
            LastStatus.Text = String.Format("Create {0}", newTextFile.Path);
        }

        private async void Add_Local_File_Click(object sender, RoutedEventArgs e)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var newTextFile = await localFolder.CreateFileAsync("MyLocalFile.txt", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
            await FileIO.WriteTextAsync(newTextFile, String.Format("File created at: {0}", DateTime.Now.ToString()));
            LastStatus.Text = String.Format("Create {0}", newTextFile.Path);
        }

        private async void Add_Temp_File_Click(object sender, RoutedEventArgs e)
        {
            var tempFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            var newTextFile = await tempFolder.CreateFileAsync("MyTempFile.txt", Windows.Storage.CreationCollisionOption.GenerateUniqueName);
            await FileIO.WriteTextAsync(newTextFile, String.Format("File created at: {0}", DateTime.Now.ToString()));
            LastStatus.Text = String.Format("Create {0}", newTextFile.Path);
        }
    }
}
