
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace AutoTracking.Services
{
    public class TrackingPoint
    {
        public int RouteNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public double DistanceKm { get; set; }
    }

    public class TrackingService
    {
        private readonly ILocalStorageService _localStorage;
        private const string StorageKey = "tracking_routes";
        public int CurrentRouteNumber { get; private set; } = 1;
        public bool IsTracking { get; private set; }
        public List<TrackingPoint> CurrentRoute { get; private set; } = new();
        public event Action? OnTrackingChanged;

        public TrackingService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task StartTrackingAsync()
        {
            if (!IsTracking)
            {
                IsTracking = true;
                OnTrackingChanged?.Invoke();
            }
        }

        public void PauseTracking()
        {
            if (IsTracking)
            {
                IsTracking = false;
                OnTrackingChanged?.Invoke();
            }
        }

        public async Task EndRouteAsync()
        {
            if (CurrentRoute.Count > 0)
            {
                var allRoutes = await _localStorage.GetItemAsync<List<List<TrackingPoint>>>(StorageKey) ?? new List<List<TrackingPoint>>();
                allRoutes.Add(new List<TrackingPoint>(CurrentRoute));
                await _localStorage.SetItemAsync(StorageKey, allRoutes);
            }
            CurrentRoute.Clear();
            IsTracking = false;
            OnTrackingChanged?.Invoke();
        }

        public void AddPoint(TrackingPoint point)
        {
            if (IsTracking)
            {
                if (CurrentRoute.Count > 0)
                {
                    var last = CurrentRoute[^1];
                    point.DistanceKm = last.DistanceKm + CalculateDistance(last.Latitude, last.Longitude, point.Latitude, point.Longitude);
                }
                else
                {
                    point.DistanceKm = 0;
                }
                CurrentRoute.Add(point);
                OnTrackingChanged?.Invoke();
            }
        }

        public void NewRoute()
        {
            CurrentRouteNumber++;
            CurrentRoute.Clear();
            IsTracking = false;
            OnTrackingChanged?.Invoke();
        }

        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Haversine formula
            var R = 6371.0; // km
            var dLat = (lat2 - lat1) * Math.PI / 180.0;
            var dLon = (lon2 - lon1) * Math.PI / 180.0;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * Math.PI / 180.0) * Math.Cos(lat2 * Math.PI / 180.0) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
    }
}
