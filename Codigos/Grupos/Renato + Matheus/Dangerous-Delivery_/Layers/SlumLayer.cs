using System;
using System.IO;
using System.Linq;

public class  SlumLayer: Layer
{
    public SlumLayer(float velocidade) : base(velocidade)
    {
        this.Planos.AddRange(
            Directory.GetFiles("./Image/Favelas")
            .Select(path => 
            {
                int height = Random.Shared.Next(270, 340 + 1);
                int y = (300 - height) + 373;
                return new Casa(path, y, 270, height);
            })
        );
    }
}