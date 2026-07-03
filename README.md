# Console Enum Alternative

![NET](https://img.shields.io/badge/NET-10-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2026](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)

# Projekt 
Unter C# gibt es bis heute keine Möglichkeit, Enums Partiell zu unterstützen. Ich kann also Enums nicht auf verschiedene Dateien oder Assemblies verteilen. Mit diesem Projekt soll eine Alternative zu Enums gezeigt werden, die es ermöglicht, die Vorteile von Enums zu nutzen, ohne die Einschränkungen von Enums zu haben.

## Was nicht geht
- Enums können nicht partiell sein.

```csharp
// Datei A
public enum Status
{
    Neu,
    InBearbeitung
}

// Datei B
public enum Status
{
    Abgeschlossen,
    Storniert
}
```

Je nachdem, was man erreichen möchtest, gibt es verschiedene Ansätze.

## Hinweis
Der Source ist soll auch einfache Art und Weise die Funktionen eines Features zeigen. Der Source ist so geschrieben, das so wenig wie möglich zusätzliche NuGet-Pakete benötigt werden.

# Beispielsource

## Statische Klasse mit Konstanten
Eine Alternative wäre eine statische Klasse mit Konstanten: Nicht schön, aber möglich.

```csharp
public static partial class Status
{
    public const string Neu = nameof(Neu);
    public const string InBearbeitung = nameof(InBearbeitung);
}

public static partial class Status
{
    public const string Abgeschlossen = nameof(Abgeschlossen);
    public const string Storniert = nameof(Storniert);
}
```

## Typsichere "Smart Enum"
Dieses Muster wird häufig als Smart Enum, Enumeration Class oder Type-safe Enum bezeichnet. Es ist wesentlich flexibler als ein echtes Enum.

```csharp
public sealed record MenuItemId(string Name);
```

```csharp
public static class PersonMenu
{
    public static readonly MenuItemId Personen = new("Personen");
}

public static class AdressMenu
{
    public static readonly MenuItemId Adressen = new("Adressen");
}
```

Das ganze läßt sich weiterentwicklen, z.B. mit einer Basisklasse, die die weitere Funktionalitäten bereitstellt.
```csharp
```

```csharp
```

```csharp
```

```xml
```

```json
```

# Versionshistorie
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)
- Erste Version
