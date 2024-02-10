using System;
using System.Drawing;
using System.Windows.Forms;

public class Pause
{
    public event EventHandler ResumeGame;

    private Form pauseForm;

    public Pause()
    {
        pauseForm = new Form
        {
            Text = "Paused",
            Size = new Size(200, 100),
            StartPosition = FormStartPosition.CenterScreen,
            ControlBox = false,
            FormBorderStyle = FormBorderStyle.FixedSingle
        };

        var label = new Label
        {
            Text = "Game Paused",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 16, FontStyle.Bold),
        };

        pauseForm.Controls.Add(label);
        pauseForm.KeyPreview = true;
        pauseForm.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.P)
            {
                OnResumeGame();
            }
        };
    }

    protected virtual void OnResumeGame()
    {
        ResumeGame?.Invoke(this, EventArgs.Empty);
    }

    public void ShowPause()
    {
        pauseForm.ShowDialog();
    }

    public void HidePause()
    {
        pauseForm.Hide();
    }
}
