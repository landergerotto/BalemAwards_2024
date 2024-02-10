using System;
using System.IO;
using System.Linq;

public class BreakImg: BreakRun
{
    public BreakImg(GameHUD hud, Food food) : base(hud, food)
    {
        this.BreakPoints.AddRange(
            Directory.GetFiles("./Image/BreakPoint")
            .Select(path => 
            {
                BreakPoint breakP = new BreakPoint(path, 560);
                breakP.Type = ExtractFoodTypeFromPath(path);
                return breakP;
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