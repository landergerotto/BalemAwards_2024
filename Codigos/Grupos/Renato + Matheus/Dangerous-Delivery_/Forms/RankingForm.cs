using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

public class RankingForm : Form
{
    private List<Label> rankingLabels = new List<Label>();
    private PictureBox pbBackground;

    public RankingForm(List<PlayerScore> rankings)
    {
        InitializeComponent();
        LoadRankings(rankings);
        this.KeyPreview = true;
        this.KeyDown += new KeyEventHandler(Form_KeyDown);
    }

    private void Form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            this.Close();
        }
    }

    private void InitializeComponent()
    {
        this.pbBackground = new PictureBox
        {
            Dock = DockStyle.Fill,
            ImageLocation = "./Image/Menu/ran.png",
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        this.Controls.Add(this.pbBackground);

        this.Size = new Size(458, 533);
        this.Text = "Rankings";
        this.StartPosition = FormStartPosition.CenterScreen;
        TransparencyKey = Color.FromArgb(0, 0, 1);
        BackColor = Color.FromArgb(0, 0, 1);
        this.FormBorderStyle = FormBorderStyle.None;
    }

    private void LoadRankings(List<PlayerScore> rankings)
    {
        var pFontCollection = new PrivateFontCollection();
        pFontCollection.AddFontFile("Fonts/BrokenConsole.ttf");
        FontFamily family = pFontCollection.Families[0];
        int startY = 108;
        int spaceBetween = 42;

        var topRankings = rankings.OrderByDescending(r => r.Score).Take(5);

        foreach (var score in topRankings)
        {
            Label rankingLabel = new Label
            {
                Text = $"{score.Name}",
                Location = new Point(110, startY),
                Size = new Size(180, 30),
                Font = new Font(family, 18, FontStyle.Bold),
                ForeColor = Color.Orange,
                BackColor = Color.Transparent,
                Parent = pbBackground,
            };
            pbBackground.Controls.Add(rankingLabel);

            Label rankingLabelScore = new Label
            {
                Text = $"{score.Score}",
                Location = new Point(300, startY),
                Size = new Size(100, 30),
                Font = new Font(family, 18, FontStyle.Bold),
                ForeColor = Color.Orange,
                BackColor = Color.Transparent,
                Parent = pbBackground,
            };
            pbBackground.Controls.Add(rankingLabelScore);

            startY += rankingLabel.Height + spaceBetween;
        }
    }
}
