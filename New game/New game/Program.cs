using System;
using System.Collections.Generic;
using System.Threading;

namespace New_game
{
    class Program
    {
        public static int attack; 
        public static string pName;
        public static Creature p;
        public static Monster mon;
        public static Shop shop;
        public static Equipment[] WeaponList = new Equipment[] { new Equipment("None", 0, 0), new Equipment("Dagger", 2, 50), new Equipment("Short Sword", 5, 400), new Equipment("Arming Sword", 8, 800) };
        public static Equipment[] ArmorList = new Equipment[] { new Equipment("None", 0, 0), new Equipment("Cloth Armor", 2, 50), new Equipment("Old Leather Armor", 5, 400), new Equipment("Leather Armor", 8, 800) };
        public static pClass[] MonsterClassList = new pClass[] { new pClass("Orc", 9, 4, 0, 0), new pClass("Goblin", 6, 3, 0, 0), new pClass("Kobald", 7, 2, 0, 0), new pClass("Skeleton", 8, 3, 0, 0) };
        public static pClass Boss = new pClass("Boss", 40, 10, 0, 0);
        public static pClass Warrior = new pClass("Warrior", 12, 4, 0, 0);
        public static pClass Rogue = new pClass("Rogue", 10, 5, 1, 1);
        public static pClass Mage = new pClass("Mage", 8, 2, 3, 3);
        public static Random rand = new Random();
        public static Shop WeaponShop = new Shop("Billford's weapon emporium.", "Billford", "troll", "Greetings, What can I do for you", WeaponList);
        public static Shop ArmorShop = new Shop("Alya's armor shop.", "Alya", "elf", "Hey there! Looking to buy some armor?", ArmorList);

        //Game Info Functions
        static void Main(string[] args)
        {
            CharacterCreate();
        }

        private static void AttackMonster()
        {
            Console.WriteLine($"You hit the {mon.name} for {attack} damage");
            mon.health -= attack;
        }

        private static void AttackPlayer()
        {
            Console.WriteLine($"The {mon.name} hits you for {mon.damage} damage");
            p.health -= mon.damage;
        }

        private static void AttackSelect()
        {
            Console.WriteLine("\n\nWhat would you like to do?");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "a")
            {
                attack = p.damage + p.Weapon.effect;
                return;
            }
            else if (choice == "b" && p.pClass.cName == "Rogue" && p.energy > 0)
            {
                attack = p.damage + p.Weapon.effect * 3;
                return;
            }
            else if (choice == "f" && p.pClass.cName == "Mage" && p.energy > 0)
            {
                attack = p.damage + p.magic * 4;
                return;
            }
            else if (choice == "b" && p.pClass.cName == "Rogue" && p.energy < 1 || choice == "f" && p.pClass.cName == "Mage" && p.energy < 1)
            {
                Console.WriteLine("You don't have enough energy!");
                Keypress();
            }
            else if (choice == "c")
                Character();
            else if (choice == "h")
                Heal();
            else if (choice == "r")
                GameDungeon();
            GameCombatMenu();
        }

        private static void Character()
        {
            Console.Clear();
            Console.WriteLine($"Name: {p.pName}");
            Console.WriteLine($"");
            Console.WriteLine($"Class: {p.pClass.cName}");
            if (p.level == 5) Console.WriteLine("YOU ARE MAX LEVEL");
            else Console.WriteLine($"Level: {p.level}");
            Console.WriteLine($"Gold: {p.gold}");
            if (p.xp >= p.xpRequired) Console.WriteLine("YOU ARE ELIGIBLE FOR A LEVEL RAISE");
            else Console.WriteLine($"Experience: {p.xp}/{p.xpRequired}");
            Console.WriteLine($"");
            Console.WriteLine($"Health: {p.health}/{p.maxHealth}");
            Console.WriteLine($"Energy: {p.energy}/{p.maxEnergy}");
            Console.WriteLine($"Spellpower: {p.magic}");
            Console.WriteLine($"");
            Console.WriteLine($"Weapon: {p.Weapon.name}");
            Console.WriteLine($"Armor: {p.Armor.name}");
            Console.WriteLine($"");
            Console.WriteLine($"Damage: {p.damage + p.Weapon.effect}");
            Console.WriteLine($"Mitigation: {p.Armor.effect}");
            Console.WriteLine($"");
            Console.WriteLine($"Potions: {p.potions}");
            Keypress();
        }

        private static void CharacterCreate()
        {
            NameSelect();
            CharacterSelect();
            GameTown();
        }

        private static void CharacterSelectConfirm(Creature p)
        {
            Console.WriteLine($"\n\n\nSo {p.pName} the {p.pClass.cName}?\n\n[Y]es      [N]o\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y") return;
            CharacterSelect();
        }

        private static void CharacterSelect()
        {
            Console.Clear();
            Console.WriteLine("Please select a class\n\n[W]arrior     [R]ogue         [M]age");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "w") p = new Hero(pName, Warrior, WeaponList[0], ArmorList[1]);
            else if (choice == "r") p = new Hero(pName, Rogue, WeaponList[1], ArmorList[0]);
            else if (choice == "m") p = new Hero(pName, Mage, WeaponList[0], ArmorList[0]);
            else CharacterSelect();
            CharacterSelectConfirm(p);
        }

        private static void Death()
        {
            Console.Clear();
            Console.WriteLine("YOU DIED!");
            Console.WriteLine("\n\nYou tried.\nYou failed but you tried.\nAnd in the end, is that not the real victory?\nThe answer is no.\n\nGoodbye!");
            Keypress();
            Environment.Exit(0);
        }

        private static void DungeonSearch()
        {
            Console.WriteLine("\n\nYou find...");
            int dungeonRoll = rand.Next(1, 6);
            if (dungeonRoll < 5) MonsterSummon();
            else ItemFind();
        }

        private static void Heal()
        {
            if (p.health == p.maxHealth)
                Console.WriteLine("You don't need healing!");
            else if (p.potions < 1)
                Console.WriteLine("You don't have enough potions!");
            else
            {
                p.health = p.maxHealth;
                p.potions -= 1;
                Console.WriteLine("You heal to full health");
            }
            Keypress();
        }

        private static void Help()
        {
            Console.WriteLine("\n\nThis is a very simple dungeon crawler. Descend the dungeon to the final level and defeat the boss to win!");
            Console.WriteLine("WEAPONS,ARMOR,ITEMS\nUse the shops to outfit your character with better gear and healing potions");
            Keypress();
        }
                
        private static void ItemFind()
        {
            int rewardRoll;
            Thread.Sleep(300);
            int itemRoll = rand.Next(1, 8);
            if (itemRoll == 1 || itemRoll == 2)
            {
                rewardRoll = rand.Next(45, 89);
                Console.WriteLine($"Gold! You gain {200 * p.level + rewardRoll} gold!");
                p.gold += 200 * p.level + rewardRoll;
            }
            else if (itemRoll == 3 || itemRoll == 4)
            {
                rewardRoll = rand.Next(5, 9);
                Console.WriteLine($"A book! Reading it gives you {rewardRoll * p.level} experience!");
                p.xp += rewardRoll * p.level;
            }
            else if (itemRoll == 5 || itemRoll == 6)
            {
                Console.WriteLine($"A potion! You gain 1 potion!");
                p.potions++;
            }
            else if (itemRoll == 7)
            {
                int equipRoll = rand.Next(1, 3);
                if (equipRoll == 1)
                {
                    if (p.Weapon.effect >= WeaponList[p.level / 2].effect)
                        Console.WriteLine($"An old broken weapon.\nCursing your almost good luck, you move on");
                    else
                    {
                        Console.WriteLine($"A {WeaponList[p.level / 2].name}!\nExcited, you equip it and move on");
                        p.Weapon = WeaponList[p.level / 2];
                    }
                }
                else
                {
                    if (p.Armor.effect >= ArmorList[p.level / 2].effect)
                        Console.WriteLine($"An old broken set of armor.\nCursing your almost good luck, you move on");
                    else
                    {
                        Console.WriteLine($"A {ArmorList[p.level / 2].name}!\nExcited, you equip it and move on");
                        p.Armor = ArmorList[p.level / 2];
                    }
                }
            }
            Keypress();
        }

        private static void ItemShop()
        {
            Console.Clear();
            Console.WriteLine("You eneter a dingy shop. A fat little man walks up to you quickly.\n'Hello there! Are you here to buy potions?\n\n[Y]es      [N]o\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm =="y")
            {
                int buyChoice;
                do
                {
                    Console.WriteLine($"'Excellent! how many would you like to buy? They are 100 gold apiece'\n\nYou can afford {p.gold/100} potions");
                } while (!int.TryParse(Console.ReadLine(), out buyChoice));
                if (p.gold< buyChoice *100) Console.WriteLine("'I'm sorry, it doesn't look like you can afford that'");
                else
                {
                    Console.WriteLine("'A pleasure doing business with you!'");
                    Console.WriteLine($"You give the man {buyChoice *100} gold and receive {buyChoice} potions");
                    p.gold -= buyChoice * 100;
                    p.potions += buyChoice;
                }
                Keypress();
            }
        }

        private static void Keypress()
        {
            Console.WriteLine("\n\nPress any key to continue");
            Console.ReadKey(true);
        }

        private static void LevelMaster()
        {
            if (p.level == 5) Console.WriteLine("YOU ARE MAX LEVEL");
            else
            {
                if (p.xp < p.xpRequired)
                    Console.WriteLine("Come back when you are more experienced");
                else
                {
                    Console.WriteLine("Congrats! You have gained a level!");
                    p.maxEnergy += p.pClass.startingEnergy;
                    p.maxHealth += p.pClass.startingHealth;
                    p.health = p.maxHealth;
                    p.energy = p.maxEnergy;
                    p.xp -= p.xpRequired;
                    p.level += 1;
                    p.damage += p.pClass.startingDamage;
                }
            }
            Keypress();
        }

        private static void Reward()
        {
            if (mon.name == "Marcotte") Win();
            Console.WriteLine($"\n\nYou kill the {mon.name}!");
            Console.WriteLine($"\nYou find {mon.gold} gold!");
            Console.WriteLine($"You earn {mon.xp} experience!");
            p.gold += mon.gold;
            p.xp += mon.xp;
            Keypress();
            GameDungeon();
        }        

        private static void ShopMenu()
        {
            Console.Clear();
            Console.WriteLine($"You walk into {shop.name}");
            Console.WriteLine($"{shop.shopkeepName} the {shop.shopkeepRace} comes over to greet you");
            Console.WriteLine($"'{shop.shopkeepGreeting}'\n\n");
            Console.WriteLine("[1] Buy      [2] Sell");
            Console.WriteLine("[C]haracter  [R]eturn\n\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "1")
            {
                for (int i = 1; i < shop.ItemList.Length; i++)
                {
                    Console.WriteLine($"[{i}] {shop.ItemList[i].name}  {shop.ItemList[i].price}");
                }
                int buyChoice;
                do
                {

                } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out buyChoice));
                if (buyChoice > 0 && buyChoice < shop.ItemList.Length)
                {
                    if (p.gold < shop.ItemList[buyChoice].price) Console.WriteLine("\n\n'Sorry, you don't Have enough Gold'");
                    else
                    {
                        Console.WriteLine($"\n\nWould you like to buy {shop.ItemList[buyChoice].name}?\n\n[Y]es      [N]o\n\n");
                        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                        if (confirm == "y")
                        {
                            if (p.Weapon.name != "None" && shop.ItemList[1].name == "Dagger")
                                ShopBuy(p.Weapon, shop.ItemList[buyChoice]);
                            else if (p.Armor.name != "None" && shop.ItemList[1].name == "Cloth Armor")
                                ShopBuy(p.Armor, shop.ItemList[buyChoice]);
                            else
                            {
                                Console.WriteLine($"\n\nSmiling, {shop.shopkeepName} takes your money and gives you your {shop.ItemList[buyChoice].name}");
                                p.gold -= shop.ItemList[buyChoice].price;
                                if (shop.shopkeepName == "Billford") p.Weapon = shop.ItemList[buyChoice];
                                else if (shop.shopkeepName == "Alya") p.Armor = shop.ItemList[buyChoice];
                            }
                        }
                    }
                }
                Keypress();
            }
            else if (choice == "2")
            {
                if (p.Weapon.name == "None" && p.Armor.name == "None") Console.WriteLine("You have nothing to Sell!");
                else
                {
                    Console.WriteLine($"\n\nWhat would you like to Sell?\n\n");
                    List<Equipment> EquipmentList = new List<Equipment> { };
                    if (p.Weapon.name != "None")
                        EquipmentList.Add(p.Weapon);
                    if (p.Armor.name != "None")
                        EquipmentList.Add(p.Armor);
                    for (int i = 0; i < EquipmentList.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {EquipmentList[i].name}  {EquipmentList[i].price / 2}");
                    }
                    int sellChoice;
                    do
                    {

                    } while (!int.TryParse(Console.ReadKey(true).KeyChar.ToString().ToLower(), out sellChoice));
                    if (sellChoice > 0 && sellChoice <= shop.ItemList.Length)
                    {
                        Console.WriteLine($"\n\nWould you Like to sell {EquipmentList[sellChoice - 1].name}? I'll give you {EquipmentList[sellChoice - 1].price / 2} for it\n\n[Y]es      [N]o\n\n");
                        string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
                        if (confirm == "y")
                        {
                            Console.WriteLine($"\n\n'Great!' {shop.shopkeepName} takes your {EquipmentList[sellChoice - 1].name} and gives you {EquipmentList[sellChoice - 1].price / 2} gold");
                            p.gold = EquipmentList[sellChoice - 1].price / 2;
                            if (p.Weapon == EquipmentList[sellChoice - 1]) p.Weapon = WeaponList[0];
                            if (p.Armor == EquipmentList[sellChoice - 1]) p.Armor = ArmorList[0];
                        }
                    }
                }
                Keypress();
            }
            else if (choice == "c") Character();
            else if (choice == "r") GameTown();
            ShopMenu();
        }

        private static void ShopBuy(Equipment equip, Equipment playerEquip)
        {
            Console.WriteLine($"I see you have a {equip.name}. Would you like to sell it?\n\n[Y]es      [N]o\n");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y")
            {
                Console.WriteLine($"{ shop.shopkeepName} takes your {equip.name} and gives you {equip.price / 2} gold");
                p.gold += equip.price / 2;
                Console.WriteLine($"Smiling, {shop.shopkeepName} takes your money and gives you your {playerEquip.name }");
                p.gold -= playerEquip.price;
                if (shop.shopkeepName == "Billford") p.Weapon = playerEquip;
                else if (shop.shopkeepName == "Alya") p.Armor = playerEquip;
            }
        }

        private static void MonsterSummon()
        {
            Thread.Sleep(300);
            Monster[] MonsterList = new Monster[] { new Monster("Orc", "You're dead!", MonsterClassList[0], 49, 5,"n Orc"), new Monster("Goblin", "Scatter!", MonsterClassList[1], 45, 3," Goblin"), new Monster("Kobald", "Leave my candles alone!", MonsterClassList[2], 35, 3," Kobald"), new Monster("Skeleton", "*Creak*", MonsterClassList[3], 40, 4, " Skeleton") };
            int monsterChoice = rand.Next(0, 4);
            mon = MonsterList[monsterChoice];
            Console.WriteLine($"A{mon.summonName}!");
            mon.health *= p.level;
            mon.health -= p.level;
            if (p.level > 1)
            {
                mon.damage *= p.level / 2 + p.level;
            }
            Keypress();
            GameCombatMenu();            
        }

        private static void NameSelect()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Marcotte's first game, remastered.\n\nWhat is your name?");
            pName = Console.ReadLine();
            Console.WriteLine($"\n\n\n{pName}, is that correct?");
            string confirm = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (confirm == "y") return;
            else NameSelect();
        }        

        private static void Quit()
        {
            Console.WriteLine("Are you sure you want to quit?\n\n[Y]es      [N]o\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "y")
                Environment.Exit(0);
        }

        private static void Win()
        {
            Console.WriteLine("Thanks for playing my game!\nThat's all there is for no but if there's any interest whatsoever i'd love to add more to it.\nMonsters, bosses, dungeons, I've got a lot of ideas");
            Keypress(); 
        }

        //GAME FUNCTIONS

        private static void GameCombatMenu()
        {
            Console.Clear();
            int playerHitRoll = rand.Next(1, 101);
            int monsterHitRoll = rand.Next(1, 101);
            int damageRoll = rand.Next(1 * (p.level / 2), 3 * (p.level / 2));
            if (p.level == 5) Console.WriteLine($"You are facing {mon.name}");
            else Console.WriteLine($"You are facing a {mon.name}");
            Console.Write($"'{mon.taunt}'");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"You                                 {mon.name}");
            Console.WriteLine($"Health: {p.health}/{p.maxHealth}    Health: {mon.health}");
            Console.WriteLine($"Energy: {p.energy}/{p.maxEnergy}    Energy: {mon.energy}");
            Console.WriteLine("");
            Console.WriteLine("[A]ttack     [H]eal      [R]un");
            if (p.pClass.cName == "Mage")
                Console.WriteLine("[F]ireball");
            else if (p.pClass.cName == "Mage")
                Console.WriteLine("[B]ackstab");
            AttackSelect();            
            attack += damageRoll;
            Console.WriteLine("");
            if (playerHitRoll < 81) AttackMonster();            
            else Console.WriteLine($"You miss the {mon.name}!");
            if (mon.health < 1) Reward();
            Thread.Sleep(400);
            if (monsterHitRoll < 81) AttackPlayer();
            else Console.WriteLine($"The {mon.name} misses you!");
            Keypress();
            if (p.health < 1) Death();
            if (p.health > 0 && mon.health > 0) GameCombatMenu();
        }

        private static void GameDungeon()
        {
            Console.Clear();
            if (p.level == 5)
            {
                Console.WriteLine($"You are on the lowest dungeon level. It looks... different. There is treasure everywhere!\nBut there is also Marcotte");
                Console.WriteLine($"Are you ready for your final showdown?");
                string confirm = Console.ReadKey(true).KeyChar.ToString();
                if (confirm == "y")
                {
                    mon = new Monster("Marcotte", "End of the line", Boss, 0, 0, " Marcotte");
                    GameCombatMenu();
                }
                else GameTown();
            }
            Console.WriteLine($"You are in dungeon level {p.level}");
            Console.WriteLine($"\n[L]ook for a monster    [H]eal");
            Console.WriteLine($"[C]haracter             [R]eturn to town");
            Console.WriteLine($"\nWhat would you like to do?");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "l")
                DungeonSearch();
            else if (choice == "c")
                Character();
            else if (choice == "h")
                Heal();
            else if (choice == "r")
                GameTown();
            GameDungeon();
        }               
        
        private static void GameTown()
        {
            Console.Clear();
            Console.WriteLine("You are in a town. You see a several places to go\n\n" +
                              "[W]eapon shop      [A}rmor shop            [I]tem shop");
            Console.WriteLine("[D]ungeon          [V]isit level master    [H]eal");
            Console.WriteLine("[C]haracter        [Q]uit                  [?]Help");
            Console.WriteLine("\n\nWhat would you like to do?\n\n");
            string choice = Console.ReadKey(true).KeyChar.ToString().ToLower();
            if (choice == "w")
            {
                shop = WeaponShop;
                ShopMenu();
            }                
            else if (choice == "a")
            {
                shop = ArmorShop;
                ShopMenu();
            }
            else if (choice == "d")
                GameDungeon();
            else if (choice == "h")
                Heal();
            else if (choice == "i")
                ItemShop();
            else if (choice == "?")
                Help();
            else if (choice == "c")
                Character();
            else if (choice == "v")
                LevelMaster();
            else if (choice == "q")
                Quit();
            else if (choice == "x")
                p.level = 5;
            GameTown();
        }        
    }       
}