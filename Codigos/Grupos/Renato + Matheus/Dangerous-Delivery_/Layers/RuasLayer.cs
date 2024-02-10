using System.IO;
using System.Linq;

public class  RuasLayer: Layer
{
    public RuasLayer(float velocidade) : base(velocidade)
    {
        this.Planos.AddRange(
            Directory.GetFiles("./Image/Street")
            .Select(path => new City(path, 720 , 1000, 300))
        );
    }
}