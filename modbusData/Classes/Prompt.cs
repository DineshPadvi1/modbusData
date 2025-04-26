using System.Windows.Forms;

namespace Uniproject.Classes
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 280;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240, UseSystemPasswordChar = true };
            Button confirmation = new Button() { Text = "Ok", Left = 170, Width = 70, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            prompt.StartPosition = FormStartPosition.CenterParent;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
