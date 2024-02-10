using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

public class Layer : BaseLayer
{
    public List<IPlano> Planos { get; set; } = new List<IPlano>();
    private Queue<IPlano> nextQueue = new();
    private Queue<IPlano> queue = new();
    private DateTime lastFrame = DateTime.Now;
    private DrawPlanoParameters parameters = new DrawPlanoParameters { X = 0 };
    public float Velocidade { get; set; }

    public Layer(float velocidade) => this.Velocidade = velocidade;

    public override void Draw(Graphics g)
    {
        refillQueue();

        parameters.X -= Velocidade * deltaTime();
        float currentX = parameters.X;

        foreach (var plano in queue)
        {
            plano.Draw(g, new DrawPlanoParameters { X = currentX});
            currentX += plano.Width - 1;
        }

        if (parameters.X + queue.Peek().Width < 0)
        {
            parameters.X += queue.Peek().Width;
            queue.Dequeue();
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
        int planosCount = Planos.Count;
        while (queue.Count < planosCount)
        {
            if (nextQueue.Count == 0)
                genNextQueue();

            var next = nextQueue.Dequeue();
            queue.Enqueue(next);
        }
    }

    private void genNextQueue()
    {
        foreach (var plano in Planos.OrderBy(p => Random.Shared.Next()))
            nextQueue.Enqueue(plano);
    }

    public void AddPlano(IPlano plano) => this.Planos.Add(plano);
}
