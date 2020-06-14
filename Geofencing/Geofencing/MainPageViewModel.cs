using Xamarin.Forms;

namespace Geofencing
{
    public class MainPageViewModel
    {
        public double Distance
        {
            set
            {
                DistanceText = $"Distance in Miles: {value}";
            }
        }

        public string DistanceText { get; set; }

        public Color BackgroundColor
        {
            get
            {
                return Color.FromHex(BackgroundColorHex);
            }
        }

        public string BackgroundColorHex { get; set; }
    }
}