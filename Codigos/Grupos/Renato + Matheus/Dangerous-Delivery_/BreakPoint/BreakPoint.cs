using System.Drawing;

public class BreakPoint : ObjBox
{
    private Image Img { get; set; }
    public float Y { get; set; }
    public float X { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }
    public string Type { get; set; }
    public int Count { get; set; }
    public override RectangleF Box { get; set; }

    public BreakPoint(string imagePath, float y)
    {
        this.Img = Image.FromFile(imagePath);
        this.Y = y;
        this.Width = Img.Width;
        this.Height = Img.Height;
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(Img, X, Y, Width, Height);
        CreateHitbox(X, Y, this.Width, this.Height);
        // g.DrawRectangle(Pens.Red, Box);
        Collision.Current.AddObjBox(this);
    }
}
