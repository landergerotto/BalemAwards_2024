using System;
using System.IO;
using System.Linq;

public class  FoodLanche: Food
{
    public FoodLanche(GameHUD hud) : base(hud)
    {
        this.Lanches.AddRange(
            Directory.GetFiles("./Image/Food")
            .Select(path => 
            {
                int y = Random.Shared.Next(735, 950);
                Lanche lanche = new Lanche(path, y);
                lanche.Type = ExtractFoodTypeFromPath(path);
                return lanche;
            })
        );
    }

    private string ExtractFoodTypeFromPath(string path)
    {
        string fileName = Path.GetFileNameWithoutExtension(path).ToLower();
        if (fileName.Contains("1")) return "pizza";
        if (fileName.Contains("2")) return "sushi";
        if (fileName.Contains("3")) return "frango";
        if (fileName.Contains("4")) return "sorvete";
        if (fileName.Contains("5")) return "bolo";
        if (fileName.Contains("6")) return "macarrao";
        return null;
    }
}