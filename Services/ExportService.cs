// use context7
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Blazored.LocalStorage;

namespace AutoTracking.Services
{
    public class ExportService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "tracking_routes";
        private const string ExportedKey = "exported_routes";

        public ExportService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> ExportUnexportedRoutesAsCsvAsync()
        {
            var allRoutes = await _localStorage.GetItemAsync<List<List<TrackingPoint>>>(StorageKey) ?? new List<List<TrackingPoint>>();
            var exportedIndexes = await _localStorage.GetItemAsync<HashSet<int>>(ExportedKey) ?? new HashSet<int>();
            var sb = new StringBuilder();
            sb.AppendLine("Route,Lat,Lon,DatumTijd,Straat,Plaats,AfstandKm");
            for (int i = 0; i < allRoutes.Count; i++)
            {
                if (exportedIndexes.Contains(i)) continue;
                foreach (var p in allRoutes[i])
                {
                    sb.AppendLine($"{p.RouteNumber},{p.Latitude},{p.Longitude},{p.Timestamp:O},\"{p.Street}\",\"{p.City}\",{p.DistanceKm:0.000}");
                }
                exportedIndexes.Add(i);
            }
            await _localStorage.SetItemAsync(ExportedKey, exportedIndexes);
            return sb.ToString();
        }
    }
}
