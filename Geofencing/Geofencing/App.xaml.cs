using System;
using Geofencing;
using Geofencing.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Geofencing
{
    public partial class App : Application
    {
        public static ILocationUpdateService LocationUpdateService;

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
            //LocationUpdateService.LocationChanged += LocationUpdateService_LocationChanged; ;
        }

        private void LocationUpdateService_LocationChanged(object sender, ILocationEventArgs e)
        {
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}