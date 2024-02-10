using System.Drawing;

public interface IPlano
{
    public float Width { get; set; }
    void Draw(Graphics g, DrawPlanoParameters parameters);
}