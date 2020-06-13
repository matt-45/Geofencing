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
    }
}