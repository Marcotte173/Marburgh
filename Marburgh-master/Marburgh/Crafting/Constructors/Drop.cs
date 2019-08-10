public class Drop
{
    //Variables, self explanatory
    public string name;
    public int amount;
    public bool isTracked;
    public int dropChance;

    //Constructor
    public Drop(string name, int amount, int dropChance, bool isTracked)
    {
        this.name = name;
        this.amount = amount;
        this.isTracked = isTracked;
        this.dropChance = dropChance;
    }
}