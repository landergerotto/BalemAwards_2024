using System.IO;
using System.Linq;

public class  LandscapeLayer: Layer
{
    public LandscapeLayer(float velocidade) : base(velocidade)
    {
        this.Planos.AddRange(
            Directory.GetFiles("./Image/Landscape")
            .Select(path => new Landscape(path, 170 , 330, 500))
        );
    }
}