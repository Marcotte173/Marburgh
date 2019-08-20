using System;
using System.Collections.Generic;

public class Combat
{
    public static List<bool> playerText = new List<bool> { };
    public static List<string> combatText = new List<string> { };
    public static int round;
    public static void GameCombat(Creature p, List<Monster> Monster)
    {
        CombatUpdate(p,Monster);
        CombatUI(p,Monster);
        if (p.canAct) CombatActionSelect(p,Monster);
        CombatUpdateText(p, Monster);
    }

    public static void CombatUpdate(Creature p, List<Monster> Monster)
    {
        //This is to give monsters statuses for testing
        foreach(Monster mon in Monster)
        {            
            int roll = Utilities.rand.Next(0, 3);
            int roll1 = Utilities.rand.Next(0, 3);
            int roll2 = Utilities.rand.Next(0, 3);
            int roll3 = Utilities.rand.Next(0, 3);
            mon.bleed = roll;
            mon.burning = roll1;
            mon.shield = roll2;
            mon.confused = roll3;
        }
        p.canAct = true;
        for (int i = 0; i < p.stun.Length; i++)
        {
            if (p.stun[i] > 0) p.canAct = false;
            p.stun[i]--;
        }
        if (p.bleed > 0) p.bleed--;
        if (p.casting > 0) p.casting--;
        if (p.burning > 0) p.burning--;
        if (p.shield > 0) p.shield--;

        foreach (Monster mon in Monster)
        {            
            mon.canAct = true;
            for (int i = 0; i < mon.stun.Length; i++)
            {
                if (mon.stun[i] > 0)
                {
                    mon.canAct = false;
                    mon.declareAction = Attack.creatureUpdateName[i];
                    mon.stun[i]--;
                }
            }
            if (mon.bleed > 0) mon.bleed--;
            if (mon.casting > 0) mon.casting--;
            if (mon.burning > 0) mon.burning--;
            if (mon.shield > 0) mon.shield--;
            if (mon.confused > 0) mon.confused--;
        }         
    }
    
                
    public static void CombatUI(Creature p, List<Monster> Monster)
    {
        DrawOpponent(p, Monster);
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        DrawPlayerOptions(p);        
    }

    public static void DrawPlayerOptions(Creature p)
    {
        if (p.canAct)
        {
            combatText = new List<string> { };
            Utilities.CombatText("[1]Attack", "[2]Defend", "[3]Special");
            Utilities.CombatText("[4]Special", "[5]Special", "[6]Run");
            Utilities.CombatText("[H]eal", "[C]haracter", "\n");
            string a = "", b = "", c = "", d = "";
            if (p.shield > 0) a = "SHIELDED\t";
            if (p.bleed > 0) b = "BLEEDING\t";
            if (p.burning > 0) c = "BURNING\t";
            if (p.defending == true) d = "DEFENDING";
            Console.Write(Colour.SHIELD + a + Colour.BLOOD + b + Colour.BURNING + c + Colour.MITIGATION + d + Colour.RESET);
            Console.SetCursorPosition(50, 18);
            Console.Write("SELECT AN ACTION");
            Console.ReadKey(true);
        }
        else
        {
            Console.WriteLine("");
            Utilities.CenterText("YOU ARE STUNNED");
            Utilities.CenterText("Press any key to continue");
            Console.ReadKey(true);
        }
        
    }

    public static void CombatUpdateText(Creature p, List<Monster> Monster)
    {        
        for (int i = 0; i < combatText.Count; i++)
        {
            Console.WriteLine(combatText[i]);
        }
    }

    public static void DrawOpponent(Creature p, List<Monster> Monster)
    {
        string ActionColourChoice = Colour.ACTION;
        foreach (Monster mon in Monster)
        {
            for (int i = 0; i < mon.statusText.Length; i++)
            {
                mon.statusText[i] = "";
            }
            if (mon.special > 0) ActionColourChoice = Colour.SPECIAL;
            if (mon.bleed > 0) mon.statusText[0] = "BLEEDING";
            if (mon.burning > 0) mon.statusText[1] = "BURNING";
            if (mon.shield > 0) mon.statusText[2] = "SHIELDED";
            if (mon.confused > 0) mon.statusText[3] = "CONFUSED";
        }
        Console.Clear();
        Console.WriteLine($"Combat round {round}");
        if (Monster.Count == 1)
        {
            Console.Write(Colour.MONSTER);
            Utilities.CenterText(Monster[0].name);
            Console.Write(Colour.HEALTH);
            Utilities.CenterText(Monster[0].health.ToString());
            Console.Write(ActionColourChoice);
            Utilities.CenterText(Monster[0].declareAction);
            Console.Write(Colour.BLOOD);
            Utilities.CenterText(Monster[0].statusText[0]);
            Console.Write(Colour.BURNING);
            Utilities.CenterText(Monster[0].statusText[1]);
            Console.Write(Colour.SHIELD);
            Utilities.CenterText(Monster[0].statusText[2]);
            Console.Write(Colour.STUNNED);
            Utilities.CenterText(Monster[0].statusText[3]);
            Console.Write(Colour.RESET);
        }
        else if (Monster.Count == 2)
        {
            Console.Write(Colour.MONSTER);
            Utilities.CenterText(Monster[0].name, Monster[1].name);
            Console.Write(Colour.HEALTH);
            Utilities.CenterText(Monster[0].health.ToString(), Monster[1].health.ToString());
            Console.Write(ActionColourChoice);
            Utilities.CenterText(Monster[0].declareAction, Monster[1].declareAction);
            Console.Write(Colour.BLOOD);
            Utilities.CenterText(Monster[0].statusText[0], Monster[1].statusText[0]);
            Console.Write(Colour.BURNING);
            Utilities.CenterText(Monster[0].statusText[1], Monster[1].statusText[1]);
            Console.Write(Colour.SHIELD);
            Utilities.CenterText(Monster[0].statusText[2], Monster[1].statusText[2]);
            Console.Write(Colour.STUNNED);
            Utilities.CenterText(Monster[0].statusText[3], Monster[1].statusText[3]);
            Console.Write(Colour.RESET);
        }
        else if (Monster.Count == 3)
        {
            Console.Write(Colour.MONSTER);
            Utilities.CenterText(Monster[0].name, Monster[1].name, Monster[2].name);
            Console.Write(Colour.HEALTH);
            Utilities.CenterText(Monster[0].health.ToString(), Monster[1].health.ToString(), Monster[2].health.ToString());
            Console.Write(ActionColourChoice);
            Utilities.CenterText(Monster[0].declareAction, Monster[1].declareAction, Monster[2].declareAction);
            Console.Write(Colour.STATUS);
            Console.Write(Colour.BLOOD);
            Utilities.CenterText(Monster[0].statusText[0], Monster[1].statusText[0], Monster[2].statusText[0]);
            Console.Write(Colour.BURNING);
            Utilities.CenterText(Monster[0].statusText[1], Monster[1].statusText[1], Monster[2].statusText[1]);
            Console.Write(Colour.SHIELD);
            Utilities.CenterText(Monster[0].statusText[2], Monster[1].statusText[2], Monster[2].statusText[2]);
            Console.Write(Colour.STUNNED);
            Utilities.CenterText(Monster[0].statusText[3], Monster[1].statusText[3], Monster[2].statusText[3]);
            Console.Write(Colour.RESET);
        }        
    }

    public static void CombatActionSelect(Creature p, List<Monster> Monster)
    {
        
    }

    

    
}