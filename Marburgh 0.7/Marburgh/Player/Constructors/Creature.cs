using System.Collections.Generic;

public class Creature
{
    //Crafting Variables
    public bool craft;
    public List<Drop> Drops = new List<Drop> { };                     

    //Combat Variables
    public string[] statusText = new string[] { "", "", "", "" };
    public bool canAct;
    public int bleed;
    public int confused;
    public int casting;
    public int burning;
    public int shield;
    public int burnDam;
    public int bleedDam;
    public bool defending;
    public int special;
    public string declareAction = "";
    public bool force = false;
    //stun[0] = frozen, stun[1] = stunned
    public int[] stun = new int[] { 0, 0 };

    //Character Creation
    public Family family;
    public int maxPotions;
    public int health;
    public int maxHealth;
    public int maxMagic;
    public int magic;
    public int damage;
    public int level;
    public int gold;
    public int xp;
    public int potions;
    public int levelMax;
    public int bankGold;
    public int addDam;
    public int addHP;
    public pClass pClass;
    public int energy;
    public int maxEnergy;

    //Bank Variables
    public int hasInvestment;
    public int invested;
    
    //Equipment Variables
    public Equipment Weapon;
    public Equipment Armor;
    
    //Creature Constructor
    public Creature(pClass pClass) { }
}