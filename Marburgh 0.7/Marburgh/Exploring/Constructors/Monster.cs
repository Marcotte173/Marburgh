public class Monster: Creature
{
    //Variables, self explanatory
    public string name;
    public string taunt;
    public int rewardGold;
    public int rewardXP;
    public string summonName;
    public Drop drop;
    public bool isBoss = false;

    //Constructor
    public Monster(string name, string taunt, pClass pClass, int xp, int gold, int addHP, int addDam, Drop drop)
    : base(pClass)
    {
        declareAction = Attack.creatureUpdateName[2];
        health = pClass.startingHealth + addHP;
        damage = pClass.startingDamage + addDam;
        energy = pClass.startingEnergy;
        magic = pClass.startingMagic;
        this.addDam = addDam;
        this.addHP = addHP;
        this.taunt = taunt;
        this.drop = drop;
        this.name = name;
        this.pClass = pClass;
        this.xp = xp;
        this.gold = gold;
        this.drop = drop;
        special = 0;
    }

    //Creates a copy of the monster, rather than the static version
    public Monster MonsterCopy()
    {
        return (Monster)MemberwiseClone();
    }
}