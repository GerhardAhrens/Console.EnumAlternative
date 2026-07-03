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
public abstract class EnumerationEnumBase<T> : IEquatable<T>, IComparable<T> where T : EnumerationEnumBase<T>
{
}
```

Beispiele 

Initalisieren eines SmartEnum `Buttons`. Die Klasse stellt den Typ `Buttons` bereit, der die Funktionalitäten von Enums bereitstellt. Die Einträge können in verschiedenen Dateien oder Assemblies liegen.
```csharp
public sealed partial class Buttons : EnumerationEnumBase<Buttons>
{
    private Buttons(int value, string name) : base(value, name) {}
}
```

Erstellen der jeweiligen Einträge in entweder einer C# Klasse oder in einer anderen Datei. Die Einträge können auch in verschiedenen Assemblies liegen.

```csharp
public sealed partial class Buttons
{
    [Category("Hauptmenü")]
    [Description("Personen verwalten")]
    public static readonly Buttons Personen = new(1, nameof(Personen));
}

public sealed partial class Buttons
{
    [Category("Hauptmenü")]
    [Description("Adressen verwalten")]
    public static readonly Buttons Adressen = new(2, nameof(Adressen));
}

public sealed partial class Buttons
{
    [Category("Hauptmenü")]
    [Description("Rechnungen verwalten")]
    public static readonly Buttons Rechnungen = new(3, nameof(Rechnungen));
}
```

Hier werden die verschidenen Möglichkeiten gezeigt:
- zuweisen
- vergleichen
- den Namen von einerm Wert zurückgen
- den Wert eine von einem Namen zurückgeben
- Wird ein Wert oder ein Name nicht gefunden, wird eine Exception geworfen.

```csharp
Buttons button = Buttons.Personen;

if (button == Buttons.Personen)
{
    Console.Success(Buttons.Personen.Category());
    Console.Success(Buttons.Personen.Description());
    Console.Success(button.Name);
    Console.Success(button.Value);
}

if (button.In(Buttons.Adressen,Buttons.Rechnungen) == true)
{
    Console.Title("In()");
    Console.Success(button.Name);
    Console.Success(button.Value);
}

if (button.NotIn(Buttons.Adressen, Buttons.Rechnungen) == true)
{
    Console.Title("NotIn()");
    Console.Success(button.Name);
    Console.Success(button.Value);
}

Console.WriteText("Wert oder Name zurückgeben");

var bv = Buttons.FromValue(2);
Console.Success(bv.Name);

var bn = Buttons.FromName("Rechnungen");
Console.Success(bn.Value);

Console.WriteText("Metadaten");

foreach (var buttons in Buttons.GetValues())
{
    Console.WriteLine($"{buttons.Value} - {buttons.Name}");
}

Console.WriteLine($"Anzahl : {Buttons.Count}");
```

# Versionshistorie
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)
- Erste Version
