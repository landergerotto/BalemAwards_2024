using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class Player : ObjBox
{
    private const int TamanhoJogador = 210;
    private const int LimiteSuperiorY = 550;
    private const int LimiteInferiorY = 770;
    private const int LimiteDireitaX = 1450;
    private const int LimiteEsquerdaX = 100;
    private GameHUD gameHUD;
    private List<Image> playerImages = new List<Image>();
    private List<Image> playerImagesGrau = new List<Image>();
    private Timer animationTimer;
    private int posX = 500;
    private int posY = 650;
    private float velocidadeMax = 17f;
    private float aceleracao = 2f;
    private float desaceleracao = 1.8f;
    private float velocidadeX = 0;
    private Food food;
    private float centerScreen;
    private float velocidadeY = 0;
    public bool isGrauLoopActive { get; set; } = false;
    public bool UsingGrauImages { get; set; } = false;
    public bool ReverseAnimation { get; set; } = false;
    public int frameAtual { get; set; } = 0;

    public override RectangleF Box { get; set; }

    public Player(GameHUD hud, Food food)
    {
        foreach (var filePath in Directory.GetFiles("Image/Entregador"))
        {
            playerImages.Add(Image.FromFile(filePath));
        }
        foreach (var filePath in Directory.GetFiles("Image/EntregadorGrau"))
        {
            playerImagesGrau.Add(Image.FromFile(filePath));
        }
        gameHUD = hud;
        this.food = food;
        centerScreen = Screen.PrimaryScreen.Bounds.Width / 2f;
        Collision.Current.AddObjBox(this);
        animationTimer = new Timer { Interval = 100, Enabled = true };
        animationTimer.Tick += (sender, e) => Update();
    }

    public void MoveLeft()
    {
        velocidadeX = Math.Max(velocidadeX - aceleracao, -velocidadeMax);
    }

    public void MoveRight()
    {
        velocidadeX = Math.Min(velocidadeX + aceleracao, velocidadeMax);
    }

    public void MoveUp()
    {
        velocidadeY = Math.Max(velocidadeY - aceleracao, -velocidadeMax);
    }

    public void MoveDown()
    {
        velocidadeY = Math.Min(velocidadeY + aceleracao, velocidadeMax);
    }

    public void Update()
    {
        ApplyFriction();
        UpdatePosition();
        Animate();
    }

    private void ApplyFriction()
    {
        if (velocidadeX > 0)
            velocidadeX = Math.Max(0, velocidadeX - desaceleracao);
        else if (velocidadeX < 0)
            velocidadeX = Math.Min(0, velocidadeX + desaceleracao);

        if (velocidadeY > 0)
            velocidadeY = Math.Max(0, velocidadeY - desaceleracao);
        else if (velocidadeY < 0)
            velocidadeY = Math.Min(0, velocidadeY + desaceleracao);
    }

    private void UpdatePosition()
    {
        posX = Math.Clamp(posX + (int)velocidadeX, LimiteEsquerdaX, LimiteDireitaX);
        posY = Math.Clamp(posY + (int)velocidadeY, LimiteSuperiorY, LimiteInferiorY);
    }

    private void Animate()
    {
        if (UsingGrauImages)
        {
            if (!ReverseAnimation)
            {
                if (!isGrauLoopActive && frameAtual >= 7)
                {
                    isGrauLoopActive = true;
                }
                if (isGrauLoopActive)
                {
                    frameAtual++;
                    if (frameAtual >= playerImagesGrau.Count)
                    {
                        frameAtual = 8;
                    }
                }
                else
                {
                    frameAtual = (frameAtual + 1) % playerImagesGrau.Count;
                }
            }
            else
            {
                frameAtual--;
                if (frameAtual == 2)
                    frameAtual = 0;
                if (frameAtual < 0)
                {
                    frameAtual = 0;
                    ReverseAnimation = false;
                    UsingGrauImages = false;
                }
            }
        }
        else
        {
            frameAtual = (frameAtual + 1) % playerImages.Count;
            isGrauLoopActive = false;
            ReverseAnimation = false;
        }
    }

    public void Draw(Graphics g)
    {
        List<Image> images = UsingGrauImages ? playerImagesGrau : playerImages;
        if (UsingGrauImages)
        {
            gameHUD.IncrementGrau();
            food.Grau();
        }
        else
        {
            gameHUD.StopGrau();
            food.NotFood();
        }
        if (frameAtual == 1)
            frameAtual = 3;
        Image currentImage = images[frameAtual];
        float deltaY = -posY / 500f;
        float scaleFactor = 0.2f - deltaY * 0.7f;

        int adjustedSize = (int)(TamanhoJogador * scaleFactor);
        int adjustedX = posX + (TamanhoJogador - adjustedSize) / 2;
        int adjustedY = posY + (TamanhoJogador - adjustedSize) / 2;

        Rectangle destino = new Rectangle(adjustedX, adjustedY, adjustedSize, adjustedSize);
        if(food.notGrau == false)
            CreateHitbox(adjustedX+25, posY + 143, adjustedSize - 45, adjustedSize / 3f);
        else
            CreateHitbox(adjustedX+50, posY + 143, adjustedSize - 150, adjustedSize / 3f);
        // g.DrawRectangle(Pens.White, Box);
        g.DrawImage(currentImage, destino);
    }

    public bool IncrementHUDCounter(Lanche lanche)
    {
        if (food.notGrau == false)
        {
            switch (lanche.Type)
            {
                case "pizza":
                    if (gameHUD.pizzaCount < 5)
                    {
                        gameHUD.IncrementPizzaCount();
                        gameHUD.IncrementFoodScore();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "sushi":
                    if (gameHUD.sushiCount < 5)
                    {
                        gameHUD.IncrementSushiCount();
                        gameHUD.IncrementFoodScore();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "sorvete":
                    if (gameHUD.sorveteCount < 5)
                    {
                        gameHUD.IncrementSorveteCount();
                        gameHUD.IncrementFoodScore();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "macarrao":
                    if (gameHUD.macarraoCount < 5)
                    {
                        gameHUD.IncrementMacarraoCount();
                        gameHUD.IncrementFoodScore();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "frango":
                    if (gameHUD.frangoCount < 5)
                    {
                        gameHUD.IncrementFrangoCount();
                        gameHUD.IncrementFoodScore();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "bolo":
                    if (gameHUD.boloCount < 5)
                    {
                        gameHUD.IncrementBoloCount();
                        gameHUD.IncrementFoodScore();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }

        return false;
    }

    public bool DecrementHUDCounter(BreakPoint breakP)
    {
        switch (breakP.Type)
        {
            case "pizza":
                if (gameHUD.pizzaCount > 0)
                {
                    gameHUD.DecrementPizzaCount();
                    gameHUD.DecrementFoodScore();
                    return true;
                }
                else
                {
                    return false;
                }
            case "sushi":
                if (gameHUD.sushiCount > 0)
                {
                    gameHUD.DecrementSushiCount();
                    gameHUD.DecrementFoodScore();
                    return true;
                }
                else
                {
                    return false;
                }
            case "sorvete":
                if (gameHUD.sorveteCount > 0)
                {
                    gameHUD.DecrementFoodScore();
                    gameHUD.DecrementSorveteCount();
                    return true;
                }
                else
                {
                    return false;
                }
            case "macarrao":
                if (gameHUD.macarraoCount > 0)
                {
                    gameHUD.DecrementFoodScore();
                    gameHUD.DecrementMacarraoCount();
                    return true;
                }
                else
                {
                    return false;
                }
            case "frango":
                if (gameHUD.frangoCount > 0)
                {
                    gameHUD.DecrementFoodScore();
                    gameHUD.DecrementFrangoCount();
                    return true;
                }
                else
                {
                    return false;
                }
            case "bolo":
                if (gameHUD.boloCount > 0)
                {
                    gameHUD.DecrementFoodScore();
                    gameHUD.DecrementBoloCount();
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }
}
