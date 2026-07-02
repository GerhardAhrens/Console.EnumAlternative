//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2026
// </copyright>
// <Template>
// 	Version 3.0.2026.2, 15.04.2026
// </Template>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>02.07.2026 16:18:55</date>
//
// <summary>
// Konsolen Applikation mit Menü
// </summary>
//-----------------------------------------------------------------------

namespace Console.EnumAlternative
{
    /* Imports from NET Framework */
    using System;

    using Console.EnumAlternative.Features;

    using static System.Diagnostics.Activity;

    public class Program
    {
        public Program()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
        }

        private static void Main(string[] args)
        {
            CMenu mainMenu = new CMenu("Hauptmenü");
            mainMenu.AddItem("Smart Enum-Test", MenuPoint1);
            mainMenu.AddItem("Beenden", () => ApplicationExit());
            mainMenu.Show();
        }

        private static void ApplicationExit()
        {
            Environment.Exit(0);
        }

        private static void MenuPoint1()
        {
            Console.Clear();

            Console.Title("Zuweisen / Vergelichen");

            Buttons button = Buttons.Personen;

            if (button == Buttons.Personen)
            {
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

            Console.Wait();
        }
    }

    public sealed partial class Buttons : EnumerationEnum<Buttons>
    {
        private Buttons(int value, string name) : base(value, name) {}
    }

    public sealed partial class Buttons
    {
        public static readonly Buttons Personen = new(1, nameof(Personen));
    }

    public sealed partial class Buttons
    {
        public static readonly Buttons Adressen = new(2, nameof(Adressen));
    }

    public sealed partial class Buttons
    {
        public static readonly Buttons Rechnungen = new(3, nameof(Rechnungen));
    }
}
