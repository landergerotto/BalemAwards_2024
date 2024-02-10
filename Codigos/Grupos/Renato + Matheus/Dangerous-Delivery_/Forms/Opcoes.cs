using System.Drawing;
using System.Windows.Forms;

public class Opcoes
{
    private Form opcoesForm;

    public Opcoes()
    {
        opcoesForm = new Form
        {
            Text = "Opções",
            Size = new Size(500, 300),
            StartPosition = FormStartPosition.CenterScreen,
            FormBorderStyle = FormBorderStyle.FixedSingle
        };

        var label = new Label
        {
            Text = "Opções de game",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.TopCenter,
            Font = new Font("Arial", 16, FontStyle.Bold),
        };

        opcoesForm.Controls.Add(label);
        opcoesForm.KeyPreview = true;
        opcoesForm.KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                opcoesForm.Close();
            }
        };
    }

    public void Mostrar()
    {
        opcoesForm.ShowDialog();
    }
}