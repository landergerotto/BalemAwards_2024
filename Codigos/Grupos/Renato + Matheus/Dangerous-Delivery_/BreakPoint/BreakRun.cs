using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Media;

public class BreakRun
{
    public List<BreakPoint> BreakPoints { get; set; } = new List<BreakPoint>();
    private Queue<BreakPoint> nextQueue = new();
    private Queue<BreakPoint> queue = new();
    private DateTime lastFrame = DateTime.Now;
    private Player player;
    private DateTime nextSpawnTime = DateTime.Now.AddSeconds(1);
    private SoundPlayer deliverySoundPlayer = new SoundPlayer("./Music\\entrega.wav");

    public BreakRun(GameHUD hud, Food food)
    {
        player = new Player(hud, food);
        deliverySoundPlayer.Load();
    }

    public void Draw(Graphics g)
    {
        Queue<BreakPoint> newQueue = new Queue<BreakPoint>();

        if (!newQueue.Any())
            refillQueue();

        if (queue.Any())
        {
            foreach (var breakP in queue)
            {
                if (queue.Any())
                {
                    breakP.Draw(g);
                    breakP.X -= 180 * deltaTime();

                    if (Collision.Current.CheckCollisions(breakP) && player.DecrementHUDCounter(breakP))
                    {
                        breakP.X = 2000;
                        deliverySoundPlayer.Play();
                    }

                    else if (breakP.X + breakP.Width < 0)
                    {
                        breakP.X = 2000;
                    }
                    else
                    {
                        newQueue.Enqueue(breakP);
                    }
                }
            }
            queue = newQueue;
            SetNextSpawnTime();
        }
    }

    private float deltaTime()
    {
        var newFrame = DateTime.Now;
        var time = newFrame - lastFrame;
        lastFrame = newFrame;

        return (float)time.TotalSeconds;
    }

    private void refillQueue()
    {
        if (DateTime.Now >= nextSpawnTime)
        {
            if (nextQueue.Count == 0)
            {
                genNextQueue();
            }
            else
            {
                while (queue.Count < Random.Shared.Next(1, 2))
                {
                    if (nextQueue.Any())
                    {
                        var next = nextQueue.Dequeue();
                        queue.Enqueue(next);
                    }
                }
            }
        }
    }

    private void SetNextSpawnTime()
    {
        int seconds = Random.Shared.Next(0, 2);
        nextSpawnTime = DateTime.Now.AddSeconds(seconds);
    }

    private void genNextQueue()
    {
        int initialX = 2200;

        foreach (var breakP in BreakPoints.OrderBy(p => Random.Shared.Next()))
        {
            breakP.X = initialX;
            nextQueue.Enqueue(breakP);
            initialX += Random.Shared.Next(300, 600);
        }
    }

    public void AddFood(BreakPoint breakP) => this.BreakPoints.Add(breakP);
}
