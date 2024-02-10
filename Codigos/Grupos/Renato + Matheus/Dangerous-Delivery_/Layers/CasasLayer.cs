using System;
using System.IO;
using System.Linq;

public class CasasLayer : Layer
{
    public CasasLayer(float velocidade) : base(velocidade)
    {
        this.Planos.AddRange(
            Directory.GetFiles("./Image/Casas")
            .Select(path => 
            {
                int height = Random.Shared.Next(370, 500 + 1);
                int y = (400 - height) + 266;
                return new Casa(path, y, 330, height);
            })
        );
    }
}
