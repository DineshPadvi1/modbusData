using System.Drawing;
using System.Windows.Forms;

public class VerticalLabel : Label
{
    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.TranslateTransform(0, this.Height);
        e.Graphics.RotateTransform(-90);

        using (SolidBrush brush = new SolidBrush(this.ForeColor))
        {
            e.Graphics.DrawString(this.Text, this.Font, brush, 0, 0);
        }
    }
}
