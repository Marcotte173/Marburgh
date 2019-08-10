using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class House
{
    public static string[] magPrefix = new string[] { "", "Deadly", "Vibrant", "Affluent", "Knowledgeable" };
    public static string[] magSuffix = new string[] { "", "of death", "of life", "of wealth", "of wisdom" };

    public static void YourHouse(Creature p)
    {
        Console.Clear();
        Utilities.ColourText(Colour.SPEAK, "You are in your house. It's not big, but it's clean and cozy. In the corner you see your bed.");
        if (p.craft == true) Utilities.EmbedColourText(Colour.SPEAK, Colour.RAREDROP, Colour.SPEAK, "\n", "In the center of the main room you have set up your ", "", "crafting machine", "", ". \nNow you just have to figure out how it works", "");
        Console.Write("\n\n(B)ed");
        if (p.craft == true) Utilities.ColourText(Colour.RAREDROP, "                  (C)rafting machine");
        Console.WriteLine("\n(R)eturn to town\n");
        Utilities.EmbedColourText(Colour.ENERGY, Colour.ENERGY, Colour.ENERGY, Colour.ENERGY, "It is day ", $"{Time.day}", ", the ", $"{Time.weeks[Time.week]}", " week of ", $"{Time.months[Time.month]}", ", ", $"{Time.year}", "\n\n");
        Console.WriteLine("\n\nWhat would you like to do?\n\n");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "r")
        {
            Marburgh.Program.GameTown();
        }
        else if (choice == "b")
        {
            Console.Clear();
            Console.WriteLine("You sleep until morning.");
            Time.DayChange(1, p);
        }
        if (choice == "c")
        {
            if (p.craft == true)
            {
                Craft();
            }
        }
        YourHouse(p);
    }

    public static void Craft()
    {
        throw new NotImplementedException();
    }
}