// use context7
// Initialiseer een OpenStreetMap kaart met Leaflet.js
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
    }
};
