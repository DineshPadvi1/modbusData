using System;
using System.Device.Location;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class LocationService
{

    private const string GoogleMapsApiKey = "AIzaSyDo48PIpf4fGFnBYJCmB3qqiaMHiZPChKU";    //"YOUR_GOOGLE_MAPS_API_KEY";

    //-----------------------------------------------------------------------------------------------------------

    public async Task<string> GetCurrentLocationAsync()
    {
        try
        {
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default)
            {
                MovementThreshold = 1.0
            };

            var tcs = new TaskCompletionSource<GeoCoordinate>();

            EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>> positionChangedHandler = null;
            positionChangedHandler = (s, e) =>
            {
                if (!e.Position.Location.IsUnknown)
                {
                    tcs.SetResult(e.Position.Location);
                    watcher.PositionChanged -= positionChangedHandler;
                }
            };

            watcher.PositionChanged += positionChangedHandler;
            watcher.Start();

            GeoCoordinate coord = await tcs.Task;

            if (!coord.IsUnknown)
            {
                double latitude = coord.Latitude;
                double longitude = coord.Longitude;
                return await GetAddressFromCoordinatesAsync(latitude, longitude);
            }
            else
            {
                return "Location unknown";
            }
        }
        catch (Exception ex)
        {
            return "Location unknown";
        }
    }

    //-----------------------------------------------------------------------------------------------------------

    private async Task<string> GetAddressFromCoordinatesAsync(double latitude, double longitude)
    {
        try
        {
            string requestUri = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={GoogleMapsApiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(json);
                    JToken addressToken = jsonObject["results"]?[0]?["formatted_address"];
                    return addressToken?.ToString() ?? "Address not found";
                }
                else
                {
                    return "Unable to retrieve address";
                }
            }
        }
        catch (Exception ex)
        {
            return "-";
        }
    }

    //-----------------------------------------------------------------------------------------------------------



}
