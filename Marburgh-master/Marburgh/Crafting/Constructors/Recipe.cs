public class Recipe
{
    public string name;
    public int ingredient1Amount;
    public int ingredient2Amount;
    public int ingredient3Amount;
    public int ingredient4Amount;
    public Drop ingredient1;
    public Drop ingredient2;
    public Drop ingredient3;
    public Drop ingredient4;
    public int succesChance;
    public Equipment ItemCreated;

    public Recipe(string name, Drop ingredient1, int ingredient1Amount, Drop ingredient2, int ingredient2Amount,Drop ingredient3, int ingredient3Amount,Drop ingredient4, int ingredient4Amount,int succesChance, Equipment ItemCreated)
    {
     this.name = name;
     this.ingredient1Amount =ingredient1Amount;
     this.ingredient2Amount =ingredient2Amount;
     this.ingredient3Amount =ingredient3Amount;
     this.ingredient4Amount =ingredient4Amount;
     this.ingredient1 =  ingredient1;
     this.ingredient2 =  ingredient2;
     this.ingredient3 =  ingredient3;
     this.ingredient4 =  ingredient4;
     this.succesChance =  succesChance;
     this.ItemCreated =  ItemCreated;
    }
}

