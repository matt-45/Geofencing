using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Geofencing
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        // 36.0967124, -80.0728266 Coder Foundry
        private MainPageViewModel _viewModel;
        private double _fenceDistance = 15;
        Location coderFoundry = new Location(36.0967124, -80.0728266);
        Location classRoomA = new Location(36.0966796, -80.0719821);
        Location homeFromDoor = new Location(35.9459782 ,-80.11415);
        private Location myLocation;
        private Location selectedLocation;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            selectedLocation = coderFoundry;
            StartListening();
            BindingContext = _viewModel;
            MainStackLayout.BackgroundColor = Color.Yellow;
        }

        async Task StartListening()
        {
            /*await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 10, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(2),
                ListenForSignificantChanges = true,
            });
            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;*/

            
            if (CrossGeolocator.IsSupported &&
                CrossGeolocator.Current.IsGeolocationAvailable &&
                CrossGeolocator.Current.IsGeolocationEnabled)
            {
                CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1), 10);
            }
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var test = e.Position;
                listenLabel.Text = "Full: Lat: " + test.Latitude.ToString() + " Long: " + test.Longitude.ToString();
                listenLabel.Text += "\n" + $"Time: {test.Timestamp.ToString()}";
                listenLabel.Text += "\n" + $"Heading: {test.Heading.ToString()}";
                listenLabel.Text += "\n" + $"Speed: {test.Speed.ToString()}";
                listenLabel.Text += "\n" + $"Accuracy: {test.Accuracy.ToString()}";
                listenLabel.Text += "\n" + $"Altitude: {test.Altitude.ToString()}";
                listenLabel.Text += "\n" + $"AltitudeAccuracy: {test.AltitudeAccuracy.ToString()}";

                myLocation = new Location(e.Position.Latitude, e.Position.Longitude);
                var distance = Location.CalculateDistance(myLocation, selectedLocation, DistanceUnits.Kilometers);
                _viewModel.Distance = distance;
                DistanceLabel.Text = $"Distance from Center: {distance * 1000} Meters";
                CurrentLocation.Text = $"Current Location: {myLocation.Latitude}, {myLocation.Longitude}";
                FenceDistance.Text = $"Fence Distance: {_fenceDistance} Meters";
                
                if (distance * 1000 < _fenceDistance)
                {
                    Result.Text = "You are in the fence!";
                    MainStackLayout.BackgroundColor = Color.Green;
                }
                else
                {
                    Result.Text = "You are not in the fence.";
                    MainStackLayout.BackgroundColor = Color.Red;
                }
                
            });
        }

        private void CoderFoundry_OnClicked(object sender, EventArgs e)
        {
            CFButton.BackgroundColor = Color.Green;
            ClassroomButton.BackgroundColor = Color.Red;
            selectedLocation = coderFoundry;
        }
        
        private void ClassroomA_OnClicked(object sender, EventArgs e)
        {
            CFButton.BackgroundColor = Color.Red;
            ClassroomButton.BackgroundColor = Color.Green;
            selectedLocation = homeFromDoor;
        }
    }
}
