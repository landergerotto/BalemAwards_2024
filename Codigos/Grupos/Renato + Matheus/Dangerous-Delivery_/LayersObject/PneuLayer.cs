using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System;

public class PneuLayer : ObjectCollision
{
    public PneuLayer(float velocidade, Game game, GameHUD gameHUD)
        : base(velocidade, game, gameHUD)
    {
        List<Image> frames = new List<Image>();
        string[] imagePaths = Directory.GetFiles("./Image/Pneu");

        foreach (string imagePath in imagePaths)
        {
            frames.Add(Image.FromFile(imagePath));
        }
        Object obj = new Object(frames, 650, "Pneu");
        Objects.Add(obj);
    }
}
