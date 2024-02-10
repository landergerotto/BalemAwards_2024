using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Menu : Form
{
    private PictureBox pbBackground;
    private Button playButton,
        opcoesButton,
        exitButton,
        rankingButton;

    public Menu()
    {
        WindowState = FormWindowState.Maximized;
        Text = "Dangerous Delivery";

        pbBackground = new PictureBox
        {
            Dock = DockStyle.Fill,
            Image = Image.FromFile("./Image/Menu/Menu.png"),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        playButton = CriarBotao("./Image/Menu/jogarButton.png");
        exitButton = CriarBotao("./Image/Menu/sairButton.png");
        rankingButton = CriarBotao("./Image/Menu/RANK.png");
        playButton.Click += PlayButtonClick;

        exitButton.Click += ExitButtonClick;
        rankingButton.Click += RankingButtonClick;

        this.Controls.Add(pbBackground);

        pbBackground.Controls.Add(playButton);
        pbBackground.Controls.Add(rankingButton);
        pbBackground.Controls.Add(exitButton);

        this.Load += (o, e) =>
        {
            AjustarLayout();
        };

        this.SizeChanged += (o, e) =>
        {
            AjustarLayout();
        };
    }

    private Button CriarBotao(string imagemPath)
    {
        var botao = new Button
        {
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            TextAlign = ContentAlignment.MiddleCenter
        };

        var imagem = Image.FromFile(imagemPath);
        botao.BackgroundImage = new Bitmap(imagem);
        botao.BackgroundImageLayout = ImageLayout.Stretch;

        botao.FlatAppearance.MouseOverBackColor = Color.Transparent;
        botao.FlatAppearance.MouseDownBackColor = Color.Transparent;

        return botao;
    }

    private void AjustarLayout()
    {
        pbBackground.Size = this.ClientSize;
        int larguraDisponivel = this.ClientSize.Width / 3;
        double proporcaoEspacamento = -0.15;
        int espacamentoEntreBotoes = (int)(larguraDisponivel * proporcaoEspacamento);

        AjustarTamanhoBotao(playButton, larguraDisponivel, 400);
        AjustarTamanhoBotao(exitButton, larguraDisponivel, 400);
        AjustarTamanhoBotao(rankingButton, larguraDisponivel, 400);

        CentralizarBotao(playButton, -larguraDisponivel / 2 - espacamentoEntreBotoes);
        CentralizarBotao(rankingButton, 0);
        CentralizarBotao(exitButton, larguraDisponivel / 2 + espacamentoEntreBotoes);
    }

    private void AjustarTamanhoBotao(Button botao, int larguraDisponivel, int tamanhoInicial)
    {
        double proporcaoImagem = (double)botao.BackgroundImage.Width / botao.BackgroundImage.Height;
        botao.Width = tamanhoInicial;
        botao.Height = (int)(tamanhoInicial / proporcaoImagem);

        if (larguraDisponivel < tamanhoInicial)
        {
            botao.Width = larguraDisponivel;
            botao.Height = (int)(larguraDisponivel / proporcaoImagem);
        }
    }

    private void CentralizarBotao(Button botao, float posicaoVertical)
    {
        float x = (float)((float)pbBackground.ClientSize.Width - (float)botao.Width) / 1.25f;
        int y = (int)(
            ((float)pbBackground.ClientSize.Height - (float)botao.Height) / 2 + posicaoVertical
        );

        botao.Location = new Point((int)x, y);

    }

    private void PlayButtonClick(object sender, EventArgs e)
    {
        using (PlayerNameForm playerNameForm = new PlayerNameForm())
        {
            if (playerNameForm.ShowDialog() == DialogResult.OK)
            {
                Game game = new Game(playerNameForm.PlayerName);
                this.Hide();
                game.Show();
            }
        }
    }

    private void OpcoesButtonClick(object sender, EventArgs e)
    {
        Opcoes opcoesForm = new Opcoes();
        opcoesForm.Mostrar();
    }

    private void ExitButtonClick(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void RankingButtonClick(object sender, EventArgs e)
    {
        GameHUD gameHUD = new GameHUD(this);
        gameHUD.LoadRankingsFromFile("rankings.json");
        List<PlayerScore> sortedRankings = gameHUD.GetSortedRankings();
        RankingForm rankingForm = new RankingForm(sortedRankings);
        rankingForm.ShowDialog();
    }
}
