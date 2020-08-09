using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SampleSignalRMobile.Services;
using SampleSignalRMobile.Views;
using Xamarin.Essentials;

namespace SampleSignalRMobile
{
    public partial class App : Application
    {

        public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5001" : "http://localhost:5001";
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
