    using System;
    using Newtonsoft.Json;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.IO;

    public class GameHUD
    {
        private string playerName;
        public int pizzaCount = 0;
        public int sushiCount = 0;
        public bool notFood = false;
        public int sorveteCount = 0;
        public int score = 0;
        public int macarraoCount = 0;
        public int boloCount = 0;
        public int frangoCount = 0;
        private List<PlayerScore> rankings = new List<PlayerScore>();
        private Timer scoreTimer;
        private Timer grauTimer;
        private Font hudFont;
        private Image hudImage;
        private Image hudImage2;
        private static PrivateFontCollection pFontCollection = new PrivateFontCollection();

        public GameHUD(Control parent)
        {
            pFontCollection.AddFontFile("Fonts/BrokenConsole.ttf");
            FontFamily family = pFontCollection.Families[0];
            hudFont = new Font(family, 12f, FontStyle.Bold);
            // hudFont = new Font("Arial", 14, FontStyle.Bold);

            scoreTimer = new Timer();
            scoreTimer.Interval = 1000;
            scoreTimer.Tick += ScoreTimer_Tick;
            scoreTimer.Start();

            grauTimer = new Timer();
            grauTimer.Interval = 1000;
            grauTimer.Tick += GrauTimer_Tick;

            hudImage = Image.FromFile("./Image/Menu/hud.png");
            hudImage2 = Image.FromFile("./Image/Menu/score.png");
        }

        public void Draw(Graphics g)
        {
            Brush brush = Brushes.White;
            Brush brush2 = Brushes.Black;
            g.DrawImage(hudImage, 5, 5, 654, 182);
            g.DrawImage(hudImage2, 1550, 70, 340, 91);
            g.DrawString(playerName, hudFont, brush, 265, 155);
            g.DrawString("X" + pizzaCount, hudFont, brush2, 235, 115);
            g.DrawString("X" + macarraoCount, hudFont, brush2, 310, 115);
            g.DrawString("X" + boloCount, hudFont, brush2, 390, 115);
            g.DrawString("X" + sorveteCount, hudFont, brush2, 455, 115);
            g.DrawString("X" + frangoCount, hudFont, brush2, 533, 115);
            g.DrawString("X" + sushiCount, hudFont, brush2, 605, 115);
            g.DrawString("X" + score, hudFont, brush2, 1710, 95);
        }

        public void Player(string playerName)
        {
            this.playerName = playerName;
        }

        private void ScoreTimer_Tick(object sender, EventArgs e)
        {
            score++;
        }

        private void GrauTimer_Tick(object sender, EventArgs e)
        {
            score += 3;
        }

        public void SaveRankingsToFile(string filePath)
        {
            var jsonString = JsonConvert.SerializeObject(rankings, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }

        public void LoadRankingsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonString = File.ReadAllText(filePath);
                rankings = JsonConvert.DeserializeObject<List<PlayerScore>>(jsonString);
            }
        }

        public void UpdateRanking(string playerName, int score)
        {
            var player = rankings.Find(p => p.Name == playerName);
            if (player != null)
            {
                if (player.Score < score)
                {
                    player.Score = score;
                }
            }
            else
            {
                rankings.Add(new PlayerScore(playerName, score));
            }
        }

        public List<PlayerScore> GetSortedRankings()
        {
            rankings.Sort((x, y) => y.Score.CompareTo(x.Score));
            return rankings;
        }

        public void Save()
        {
            UpdateRanking(this.playerName, this.score);
            SaveRankingsToFile("rankings.json");
        }

        public void IncrementFoodScore() => score += 5;

        public void IncrementGrau() => grauTimer.Start();
        public void StopGrau() => grauTimer.Stop();
        public void IncrementPizzaCount() => pizzaCount++;

        public void IncrementSushiCount() => sushiCount++;

        public void IncrementSorveteCount() => sorveteCount++;

        public void IncrementMacarraoCount() => macarraoCount++;

        public void IncrementBoloCount() => boloCount++;

        public void IncrementFrangoCount() => frangoCount++;

        public void DecrementFoodScore() => score += 10;

        public void DecrementPizzaCount() => pizzaCount--;

        public void DecrementSushiCount() => sushiCount--;

        public void DecrementSorveteCount() => sorveteCount--;

        public void DecrementMacarraoCount() => macarraoCount--;

        public void DecrementBoloCount() => boloCount--;

        public void DecrementFrangoCount() => frangoCount--;
    }
