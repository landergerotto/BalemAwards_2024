using System.Drawing;

public abstract class ObjBox
{
    public abstract RectangleF Box { get; set; }

    public virtual void CreateHitbox(float x, float y, float width, float height) =>
        this.Box = new RectangleF(x, y, width, height);
}
