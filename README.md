# AutoTracking

Een moderne, single page Blazor WebAssembly-applicatie voor het bijhouden en exporteren van routes op een OpenStreetMap-kaart.

## Features

- **OpenStreetMap integratie**: Kaart met huidige locatie en routeweergave.
- **Live tracking**: Start, pauzeer en beëindig route-tracking met duidelijke knoppen.
- **Routegegevens**: Routenumers, locatie, datum/tijd, straatnaam, plaats en afstand worden elke X seconden opgeslagen.
- **Database**: Blazored.LocalStorage als lokale opslag.
- **UI**: Modern en overzichtelijk met MudBlazor.
- **Export**: Exporteer niet-geëxporteerde routes als CSV naar het ingestelde e-mailadres.
- **Instellingen**: Frequentie en e-mailadres instelbaar via een aparte pagina.
- **MCP**: Gebruik altijd `use context7` bij codegeneratie.

## Stack

- **C#**
- **Blazor WebAssembly**
- **MudBlazor** (UI)
- **Blazored.LocalStorage** (database)
- **OpenStreetMap** (kaart)
- **Deployment**: GitHub Pages (branch: `gh-pages`)

## Installatie

1. **Vereisten**:  
   - .NET 8 SDK  
   - Node.js (voor build tooling)  
   - VS Code met C#-extensie

2. **Dependencies installeren**:
   ```powershell
   dotnet restore
   ```

3. **MCP instructie**:  
   Voeg bovenaan elk codebestand toe:  
   ```csharp
   
   ```

4. **Starten**:
   ```powershell
   dotnet run
   ```

## Deployment naar GitHub Pages

- Gebruik de workflow in `.github/workflows/deploy.yml` (zie hieronder).
- Publiceer naar de `gh-pages` branch van `roland.wardenaar/AutoTracking`.
