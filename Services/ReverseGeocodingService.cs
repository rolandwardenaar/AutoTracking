// use context7
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AutoTracking.Services
{
    public class ReverseGeocodingResult
    {
        public string? road { get; set; }
        public string? city { get; set; }
        public string? town { get; set; }
        public string? village { get; set; }
        public string? suburb { get; set; }
        public string? state { get; set; }
        public string? postcode { get; set; }
        public string? country { get; set; }
    }

    public class ReverseGeocodingService
    {
        private readonly HttpClient _http;
        public ReverseGeocodingService(HttpClient http)
        {
            _http = http;
        }

        public async Task<(string? street, string? city)> GetAddressAsync(double lat, double lon)
        {
            var url = $"https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat={lat}&lon={lon}&addressdetails=1";
            var response = await _http.GetFromJsonAsync<NominatimResponse>(url);
            var address = response?.address;
            string? street = address?.road ?? address?.suburb;
            string? city = address?.city ?? address?.town ?? address?.village ?? address?.state;
            return (street, city);
        }

        private class NominatimResponse
        {
            public ReverseGeocodingResult? address { get; set; }
        }
    }
}
