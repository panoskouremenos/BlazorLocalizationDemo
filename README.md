# Blazor Localization Demo (.NET 8)

This is a small demo project that demonstrates how to implement localization in a Blazor Server application using resource (.resx) files and culture cookies.

The application allows switching between multiple languages and dynamically updates UI text based on the selected culture.

---

## Features

- Blazor Server (.NET 8)
- Localization using `.resx` resource files
- Language switching via culture cookie
- Supports multiple UI cultures:
  - English (en)
  - Spanish (es)
  - German (de)
- Uses `IStringLocalizer` with shared resource files

---

## Tech Stack

- ASP.NET Core 8
- Blazor Server (Interactive Server Components)
- C#
- Localization Middleware
- Resource Files (.resx)

---

## Files Related to Localization Setup

- Program.cs
  - Registers localization services
  - Defines supported cultures
  - Configures RequestLocalizationMiddleware
  - Provides an endpoint to change culture and store it in a cookie

- Resources/SharedResources.resx
  - Default (English) UI strings
- Resources/SharedResources.es.resx
  - Spanish translations
- Resources/SharedResources.de.resx
  - German translations

- SharedResources.cs
  - Marker class used by IStringLocalizer<SharedResources> to locate shared resource files

- Components/Pages/Home.razor
  - Uses IStringLocalizer to display localized UI text
  - Language selection from dropdown menu
  - Redirects to culture endpoint to updeate the culture cookie

---
