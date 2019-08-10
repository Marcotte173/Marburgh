public class Equipment
{
    public string name;
    public int effect;
    public int price;
    public int goldEffect;
    public int xpEffect;
    public int healthEffect;

    public Equipment(string name, int effect, int price, int goldEffect, int xpEffect, int healthEffect)
    {
        this.name = name;
        this.effect = effect;
        this.price = price;
        this.goldEffect = goldEffect;
    }
}