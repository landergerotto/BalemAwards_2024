using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using NAudio.Wave;

public class Game : Form
{
    private Bitmap bmp;
    private Graphics g;
    private Parallax parallax;
    private ObjectsLayers objectsLayers;
    private FoodLanche foodLanche;
    private BreakImg breakImg;
    private Player player;
    private Pause pause;
    private RainLayer rain;
    private bool moveLeft,
        moveRight,
        moveUp,
        moveDown;
    private bool isPaused = false;
    public string PlayerName { get; }
    private AudioManager audioManager;
    private GameHUD gameHUD;

    public Game(string playerName)
    {
        PlayerName = playerName;

        this.WindowState = FormWindowState.Maximized;
        this.Text = "Dangerous Delivery";
        this.KeyPreview = true;
        var pb = new PictureBox { Dock = DockStyle.Fill };
        var timer = new Timer { Interval = 1000 / 60, };

        audioManager = new AudioManager();
        audioManager.PlayMusic("Music/1.mp3");

        gameHUD = new GameHUD(this);
        gameHUD.LoadRankingsFromFile("rankings.json");
        foodLanche = new FoodLanche(gameHUD);
        player = new Player(gameHUD, foodLanche);
        parallax = new Parallax();
        objectsLayers = new ObjectsLayers();
        breakImg = new BreakImg(gameHUD, foodLanche);
        pause = new Pause();
        rain = new RainLayer(5000);
        pause.ResumeGame += (sender, e) => ResumeGame();

        parallax.Layers.Add(new SkyLayer());
        parallax.Layers.Add(new CloudLayer(40));
        parallax.Layers.Add(new CityLayer(70));
        parallax.Layers.Add(new SlumLayer(110));
        parallax.Layers.Add(new CasasLayer(150));
        parallax.Layers.Add(new RuasLayer(320));
        parallax.Layers.Add(new CalcadasLayer(180));

        objectsLayers.Objects.Add(new OleoLayer(320, this, gameHUD));
        objectsLayers.Objects.Add(new PneuLayer(400, this, gameHUD));
        objectsLayers.Objects.Add(new PneuLayer(200, this, gameHUD));
        objectsLayers.Objects.Add(new OleoLayer(210, this, gameHUD));

        gameHUD.Player(playerName);

        this.Load += (sender, e) =>
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.CompositingQuality = CompositingQuality.AssumeLinear;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            g.Clear(Color.Black);
            pb.Image = bmp;
            timer.Start();
        };

        timer.Tick += (sender, e) =>
        {
            if (!isPaused)
            {
                UpdatePlayerMovement();

                parallax.Draw(g);
                foodLanche.Draw(g);
                breakImg.Draw(g);
                objectsLayers.Draw(g);
                player.Draw(g);
                gameHUD.Draw(g);
                rain.Draw(g);
                rain.Update();
                pb.Refresh();
            }
        };

        this.KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
            else if (!isPaused && e.KeyCode == Keys.P)
            {
                TogglePause();
            }
            if (e.KeyCode == Keys.G)
            {
                if (player.UsingGrauImages && player.isGrauLoopActive)
                {
                    player.ReverseAnimation = !player.ReverseAnimation;
                }
                else
                {
                    player.frameAtual = 0;
                    player.UsingGrauImages = !player.UsingGrauImages;
                }
            }
            else if (!isPaused)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        moveLeft = true;
                        break;
                    case Keys.D:
                        moveRight = true;
                        break;
                    case Keys.W:
                        moveUp = true;
                        break;
                    case Keys.S:
                        moveDown = true;
                        break;
                }
            }
        };

        this.KeyUp += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    moveLeft = false;
                    break;
                case Keys.D:
                    moveRight = false;
                    break;
                case Keys.W:
                    moveUp = false;
                    break;
                case Keys.S:
                    moveDown = false;
                    break;
            }
        };

        FormClosed += delegate
        {
            Application.Exit();
        };

        this.Controls.Add(pb);
    }

    private void UpdatePlayerMovement()
    {
        if (moveLeft)
            player.MoveLeft();
        if (moveRight)
            player.MoveRight();
        if (moveUp)
            player.MoveUp();
        if (moveDown)
            player.MoveDown();
    }

    private void ResumeGame()
    {
        isPaused = false;
        pause.HidePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pause.ShowPause();
        }
        else
        {
            ResumeGame();
        }
    }

    public void GG()
    {
        GameOver gameOver = new GameOver(this);
        isPaused = true;
        gameOver.ShowPause();
    }
    public void EndGame()
    {
        this.Close();
    }

}
