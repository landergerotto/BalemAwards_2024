using System.Drawing;

public class City : IPlano
{
    private Image Img;
    private float Y;
    private float Height;

    public City(string imagePath, float y, float width, float height)
    {
        this.Img = Image.FromFile(imagePath)
            .GetThumbnailImage((int)width, (int)height, null, nint.Zero);
        this.Y = y;
        this.Width = (int)width;
        this.Height = (int)height;
    }

    public float Width { get; set; }

    public void Draw(Graphics g, DrawPlanoParameters parameters)
    {
        g.DrawImage(Img, parameters.X, Y);
    }
}
