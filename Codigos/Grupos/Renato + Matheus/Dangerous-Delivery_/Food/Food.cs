using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Media;

public class Food
{
    public List<Lanche> Lanches { get; set; } = new List<Lanche>();
    private Queue<Lanche> nextQueue = new();
    private Queue<Lanche> queue = new();
    private Player player;
    public bool notGrau = false;
    private DateTime nextSpawnTime = DateTime.Now.AddSeconds(1);
    private SoundPlayer collectSoundPlayer = new SoundPlayer("./Music\\captura.wav");


    public Food(GameHUD hud)
    {
        player = new Player(hud, this);
        collectSoundPlayer.Load();
    }
    public void Draw(Graphics g)
    {
        Queue<Lanche> newQueue = new Queue<Lanche>();

        if (!newQueue.Any())
            refillQueue();

        if (queue.Any())
        {
            foreach (var lanche in queue)
            {
                lanche.Draw(g);
                lanche.X -= 15;

                if (Collision.Current.CheckCollisions(lanche) && player.IncrementHUDCounter(lanche) && !notGrau)
                {
                    lanche.X = 2000;
                    collectSoundPlayer.Play();
                }
                else if (lanche.X + lanche.Width < 0)
                {
                    lanche.X = 2000;
                }
                else
                {
                    newQueue.Enqueue(lanche);
                }
            }
            queue = newQueue;
            SetNextSpawnTime();
        }
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
                while (queue.Count < Random.Shared.Next(1, 3))
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

    public void Grau()
        => notGrau = true;
    
    public void NotFood()
        => notGrau = false;

    private void SetNextSpawnTime()
    {
        int seconds = Random.Shared.Next(1, 3);
        nextSpawnTime = DateTime.Now.AddSeconds(seconds);
    }

    private void genNextQueue()
    {
        int initialX = 2000;

        foreach (var lanche in Lanches.OrderBy(p => Random.Shared.Next()))
        {
            lanche.X = initialX;
            nextQueue.Enqueue(lanche);
            initialX += Random.Shared.Next(300, 600);
        }
    }

    public void AddFood(Lanche lanche) => this.Lanches.Add(lanche);
}
