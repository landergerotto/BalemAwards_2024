using System;
using System.IO;
using System.Linq;

public class  CloudLayer: Layer
{
    public CloudLayer(float velocidade) : base(velocidade)
    {
        this.Planos.AddRange(
            Directory.GetFiles("./Image/Cloud")
            .Select(path => 
            {
                int y = Random.Shared.Next(30, 70 + 1);
                int height = (50 - y) + 400;
                return new Sky(path, y, 800, height);
            })
        );
    }
}