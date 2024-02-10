using System.Drawing;

public interface IObject
{
    public float Width { get; set; }
    void Draw(Graphics g, DrawPlanoParameters parameters);
    void UpdateAnimation();
}