using System.Collections.Generic;

public class Dungeon
{
    //Variables, self explanatory
    public string name;
    public Boss boss;
    public int rooms;
    public Room[] roomOptions;
    public bool dungeonAvailable;
    public Monster[] bestiary;
    public string flavor;
    public int lootTier;
    public int monsterSummon;
    public double diminishingReturns;
    public bool roomExplored;

    public static Drop[] drop = new Drop[]
        {
            new Drop("Nothing",0,0,0),
            new Drop("Goblin Tooth",1,15,0),
            new Drop("Candle",1,25,0),
            new Drop("Green Goo",1,25,0)
        };
    public static pClass[] MonsterClassList = new pClass[]
    {
            new pClass("Slime", 9, 5, 0, 0),
            new pClass("Goblin", 8, 6, 0, 0),
            new pClass("Kobald", 10, 5, 0, 0),
            new pClass("Skeleton", 28, 18, 0, 0),
            new pClass("Orc", 38, 26, 0, 0),
            new pClass("Minotaur", 55, 44, 0, 0),
            new pClass("Ogre", 60, 48, 0, 0)
    };
    public static Monster[] MonsterList = new Monster[]
    {
            new Monster("Slime", "*Squish*", MonsterClassList[0], 4, 27, 0, 0, drop[0]),
            new Monster("Goblin", "Time to die!", MonsterClassList[1], 4, 45, 0, 0, drop[1]),
            new Monster("Kobold", "I'll get you, bucko", MonsterClassList[2], 5, 45, 0, 0, drop[1])
    };
    public static Event[] SearchList = new Event[]
    {
            new Event("Gold",  "Some" + Colour.GOLD + " gold" + Colour.RESET,        0 ,     150,           50),
            new Event("Potion","A "+ Colour.HEALTH +"potion" + Colour.RESET,  0,      1,             40),
            new Event("XP", "An old " + Colour.XP + "book",         0,      15,            30),
            //More Regular events to be made when required
            new Event("XP", "An old book",         0,      15,            30),
            new Event("XP", "An old book",         0,      15,            30),
            new Event("XP", "An old book",         0,      15,            30),
            //
            new Event("Plundered Loot", "A large chest of goods with a sturdy lock sits here. " +
                "\n\nThe goblins haven't managed to bypass the lock yet, but an old goblin dagger sticks out of the top of the chest" +
                "\nwhere they attempted to create a new opening.\n",         1,      20,            40),
    };
    public static Room[] RoomList = new Room[]
    {
            new Room("Small room with Gold", "a small, squalid room", 5, new Event[] { SearchList[0] }),
            new Room("Small library with book","a small squalid library", 5, new Event[]{SearchList[0],SearchList[2]}),
            new Room("Small room with potion/Gold", "a smallish, squalid room", 5, new Event[]{ SearchList[0], SearchList[1]}),
            new Room("Regular Room with Gold", "a medium sized, regular room", 10,new Event[]{ SearchList[0] }),
            new Room("Regular Room with Gold, equipment, potion, ", "a medium sized, regular room", 10,new Event[]{ SearchList[0], SearchList[1], SearchList[2]}),
            //More regular rooms to be made when required
            new Room("Small room with Gold", "a small, squalid room", 5, new Event[] { SearchList[0] }),
            new Room("Small room with Gold", "a small, squalid room", 5, new Event[] { SearchList[0] }),
            new Room("Small room with Gold", "a small, squalid room", 5, new Event[] { SearchList[0] }),
            new Room("Small room with Gold", "a small, squalid room", 5, new Event[] { SearchList[0] }),
            new Room("Small room with Gold", "a small, squalid room", 5, new Event[] { SearchList[0] }),
            new Room("Small room with Gold", "a small, squalid room", 5, new Event[] { SearchList[0] }),
            //DUNGEON 1 SPECIAL ROOMS
            new Room("Plundered loot","a room with a treasure chest!",0,new Event[] {SearchList[6] })
    };
    public static Drop[] BossDrop = new Drop[] {
        new Drop("Savage Orc Claw", 1, 100,1)
    };
    public static Boss[] BossList = new Boss[] {
        new Boss("Savage Orc", "*GRAAAAH!*", MonsterClassList[5], 340, 15, 15, 6, BossDrop[0], true)
    };
    public static Dungeon d;
    public static List<Dungeon> DungeonList = new List<Dungeon>()
    {
        new Dungeon("Starter dungeon", "YOU ARE IN DUNGEON 1", 5, 60, new Room[] { RoomList[0], RoomList[1], RoomList[2], RoomList[11] }, BossList[0], true, new Monster[] { MonsterList[0], MonsterList[1], MonsterList[2] }, 1, 1, false)
    };

    //Constructor
    public Dungeon(string name, string flavor, int howManyRooms, int monsterSummon, Room[] roomOptions, Boss boss, 
        bool dungeonAvailable, Monster[] bestiary, int loot, double diminishingReturns, bool roomExplored)
    {
        this.name = name;
        this.boss = boss;
        rooms = howManyRooms;
        this.dungeonAvailable = dungeonAvailable;
        this.bestiary = bestiary;
        this.roomOptions = roomOptions;
        this.flavor = flavor;
        lootTier = loot;
        this.monsterSummon = monsterSummon;
        this.diminishingReturns = diminishingReturns;
        this.roomExplored = roomExplored;
    }
}