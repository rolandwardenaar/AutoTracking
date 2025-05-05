// Download bestand vanuit Blazor (CSV export)
window.blazorDownloadFile = function (fileName, base64) {
    const link = document.createElement('a');
    link.download = fileName;
    link.href = "data:text/csv;base64," + base64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
// Haal frequentie uit localStorage (voor Blazor interop)
window.blazorGetFrequency = function() {
    const val = window.localStorage.getItem('settings_frequency');
    if (val) return parseInt(val);
    return 5;
}

// Initialiseer een OpenStreetMap kaart met Leaflet.js en beheer route/positie
window.initMap = () => {
    if (window.L) {
        if (window._leafletMap) {
            window._leafletMap.remove();
        }
        window._leafletMap = L.map('map').setView([52.3702, 4.8952], 13); // Amsterdam als startpunt
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '© OpenStreetMap contributors'
        }).addTo(window._leafletMap);
        window._routeLine = L.polyline([], { color: 'blue' }).addTo(window._leafletMap);
        window._positionMarker = null;
    }
};

window.addRoutePoint = (lat, lng) => {
    if (window._leafletMap && window._routeLine) {
        const latlng = [lat, lng];
        window._routeLine.addLatLng(latlng);
        if (window._positionMarker) {
            window._positionMarker.setLatLng(latlng);
        } else {
            window._positionMarker = L.marker(latlng, {
                icon: L.divIcon({
                    className: 'custom-triangle',
                    html: '<svg width="24" height="24"><polygon points="12,2 22,22 2,22" style="fill:red;stroke:black;stroke-width:1" /></svg>'
                })
            }).addTo(window._leafletMap);
        }
        window._leafletMap.panTo(latlng);
    }
};

window.clearRoute = () => {
    if (window._routeLine) {
        window._routeLine.setLatLngs([]);
    }
    if (window._positionMarker) {
        window._leafletMap.removeLayer(window._positionMarker);
        window._positionMarker = null;
    }
};

window.getCurrentPosition = () => {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject('Geolocatie niet ondersteund');
        } else {
            navigator.geolocation.getCurrentPosition(
                pos => resolve({ latitude: pos.coords.latitude, longitude: pos.coords.longitude }),
                err => reject(err)
            );
        }
    });
};
