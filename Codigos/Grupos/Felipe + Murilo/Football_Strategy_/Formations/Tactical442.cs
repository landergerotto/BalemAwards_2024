using System.Drawing;
using System.Windows.Forms;

namespace Views;

public class Tactical442 : Formation 
{
    public Tactical442()
    {
        AddEmptyPosition(Position.GoalKeeper, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.271f, Screen.PrimaryScreen.Bounds.Height*0.74f)
        ); //GL
        AddEmptyPosition(Position.LeftBack, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.128f, Screen.PrimaryScreen.Bounds.Height*0.592f)
        ); //LE
        AddEmptyPosition(Position.Defender, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.219f, Screen.PrimaryScreen.Bounds.Height*0.629f)
        ); //ZC
        AddEmptyPosition(Position.Defender, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.323f, Screen.PrimaryScreen.Bounds.Height*0.629f)
        ); //ZC
        AddEmptyPosition(Position.RightBack, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.416f, Screen.PrimaryScreen.Bounds.Height*0.592f)
        ); //LD
        AddEmptyPosition(Position.Midfield, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.219f, Screen.PrimaryScreen.Bounds.Height*0.407f)
        ); //MC
        AddEmptyPosition(Position.Midfield, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.323f, Screen.PrimaryScreen.Bounds.Height*0.407f)
        ); //MCD
        AddEmptyPosition(Position.Midfield, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.136f, Screen.PrimaryScreen.Bounds.Height*0.370f)
        ); //ME
        AddEmptyPosition(Position.Midfield, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.396f, Screen.PrimaryScreen.Bounds.Height*0.370f)
        ); //MD
        AddEmptyPosition(Position.Striker, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.323f, Screen.PrimaryScreen.Bounds.Height*0.138f)
        ); //ATA
        AddEmptyPosition(Position.Striker, 
            new PointF(Screen.PrimaryScreen.Bounds.Width*0.219f, Screen.PrimaryScreen.Bounds.Height*0.138f)
        ); //ATA
    }
}