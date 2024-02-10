using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class RainLayer : BaseLayer
{
    private Rectangle[] rainRectangles;
    private int velocidade = 5;
    private Timer chuvaTimer;
    private bool chuvaAtiva;
    private bool chuvaReiniciada;

    public RainLayer(int numRectangles)
    {
        rainRectangles = new Rectangle[numRectangles];
        InitializeRainRectangles();

        chuvaTimer = new Timer();
        chuvaTimer.Interval = 20000;
        chuvaTimer.Tick += ChuvaTimer_Tick;
        chuvaTimer.Start();
    }

    private int GetRandomInterval()
    {
        Random random = new Random();
        return random.Next(80000, 110000);
    }

    private void InitializeRainRectangles()
    {
        Random random = new Random();

        for (int i = 0; i < rainRectangles.Length; i++)
        {
            int x = random.Next(0, 3000);
            int y = random.Next(-5000, 0);
            int width = 2;
            int height = random.Next(5, 15);
            rainRectangles[i] = new Rectangle(x, y, width, height);
        }

        chuvaReiniciada = true;
    }

    public override void Draw(Graphics g)
    {
        foreach (var rectangle in rainRectangles)
        {
            g.FillRectangle(Brushes.DodgerBlue, rectangle);
        }
    }

    private void ChuvaTimer_Tick(object sender, EventArgs e)
    {
        chuvaAtiva = !chuvaAtiva;

        if (chuvaAtiva && chuvaReiniciada)
        {
            chuvaTimer.Interval = GetRandomInterval() - 10;
            chuvaReiniciada = false;
        }
        else
        {
            chuvaTimer.Interval = GetRandomInterval();
            if (!chuvaAtiva)
            {
                chuvaReiniciada = true;
                InitializeRainRectangles();
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < rainRectangles.Length; i++)
        {
            rainRectangles[i] = new Rectangle(
                rainRectangles[i].X - velocidade,
                rainRectangles[i].Y + velocidade,
                rainRectangles[i].Width,
                rainRectangles[i].Height
            );

            if (!chuvaAtiva && (rainRectangles[i].X + rainRectangles[i].Width < 0 || rainRectangles[i].Y > 2000))
            {
                rainRectangles[i] = new Rectangle(
                    3000,
                    new Random().Next(-5000, 0),
                    rainRectangles[i].Width,
                    rainRectangles[i].Height
                );
            }
        }

        if (!chuvaAtiva && rainRectangles.All(rectangle => rectangle.Y > 2000))
        {
            InitializeRainRectangles();
        }
    }
}
