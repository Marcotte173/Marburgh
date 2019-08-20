using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


public class SpecialEvent
{
    public static void Event1(Dungeon d, Creature p, Event e)
    {
        Console.Clear();
        bool key = false;
        for (int i = 0; i < p.Drops.Count; i++)
        {
            if (p.Drops[i].name == "Chest Key" && p.Drops[i].amount > 0) key = true;
        }
        Console.WriteLine(e.flavor);
        Utilities.EmbedColourText(Colour.DAMAGE,"You could ", "bash"," the lock, but risk destroying the contents");
        Utilities.EmbedColourText(Colour.SP,"You could"," pick"," the lock, but in the time it takes, more monsters may find you");
        Utilities.EmbedColourText(Colour.NAME,"If only you had a"," key","!\n\n");
        Utilities.EmbedColourText(Colour.DAMAGE, Colour.SP, Colour.NAME, "","[B]","ash the lock        ","[P]","ick the lock             ","[K]","ey            [R]eturn");
        string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
        if (choice == "b")
        {
            Console.Clear();
            Utilities.ColourText(Colour.DAMAGE, "BLAM!");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            int bashRoll = Utilities.rand.Next(1, 101);
            if (bashRoll <= e.success + e.effect)
            {
                Console.WriteLine("Success!\n\n");
                Thread.Sleep(300);
                Console.WriteLine("Inside you find a bunch of treasure, to be described later!");
            }
            else
            {
                Console.WriteLine("Failure!");
                Thread.Sleep(300);
                Console.WriteLine("It looks like the valuables inside were fragile indeed. Oh well, maybe next time");
            }
        }
        else if (choice == "p")
        {
            Console.Clear();
            Utilities.ColourText(Colour.SP, "CHICK CHICK!");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            int pickRoll = Utilities.rand.Next(1, 101);
            if (pickRoll <= e.success)
            {
                Console.WriteLine("Success!\n\n");
                Thread.Sleep(300);
                Console.WriteLine("Inside you find a bunch of treasure, to be described later!");
                Utilities.Keypress();
            }
            else if(pickRoll >e.success && pickRoll <= e.success + e.effect)
            {
                Console.WriteLine("You got in!\n\n");
                Thread.Sleep(300);
                Console.WriteLine("That took a while tho, it looks like someone found you!");
                p.force = true;
                Utilities.Keypress();
                Console.Clear();
                Explore.MonsterSummon(Dungeon.RoomList[11], d, p);
            }
            else
            {
                Console.WriteLine("Failure!");
                Thread.Sleep(300);
                Console.WriteLine("Not only could you not get in, you took so long that someone found you!");
                p.force = true;
                Utilities.Keypress();
                Console.Clear();
                Explore.MonsterSummon(Dungeon.RoomList[11], d, p);
            }
        }
        else if (choice == "k" && key == true)
        {
            Console.Clear();
            Utilities.ColourText(Colour.NAME, "CLICK!");
            Utilities.DotDotDot();
            Console.WriteLine("\n\n\n\n\n\n\n");
            Console.WriteLine("Success!\n\n");
            Thread.Sleep(300);
            Console.WriteLine("Inside you find a bunch of treasure, to be described later!");
            Utilities.Keypress();
        }
        else if (choice == "k" && key == false)
        {
            Utilities.EmbedColourText(Colour.NAME, "\n\nYou don't have a ", "key", "!");
            Utilities.Keypress();
            Event1(d, p, e);
        }
        else if (choice == "r") Explore.GameDungeon(d, p);
        else if(choice == "x")
        {
            p.Drops.Add(new Drop("Chest Key", 1, 100, 1));
            Event1(d, p, e);
        }
        else Event1(d, p, e);
    }

    public static void Event2(Dungeon d, Creature p, Event e)
    {
        
    }

    public static void Event3(Dungeon d, Creature p, Event e)
    {
        
    }

    public static void Event4(Dungeon d, Creature p, Event e)
    {
        
    }
}

