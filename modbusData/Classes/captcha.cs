using System;
using System.Drawing;
using System.Linq;

namespace Uniproject.Classes
{
    public class captcha
    {
        private string captchaText;

        public void Captcha(int length = 6)
        {
            captchaText = GenerateRandomString(length);
        }

        public string CaptchaText => captchaText;


        //--------------------------------------------------------------------------
        public Bitmap GenerateCaptchaImage(int width, int height)
        {
            var image = new Bitmap(width, height);
            using (var g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);

                // Generate captcha text
                string captchaTxt = captchaText; GenerateRandomText();

                var font = new Font("Arial", 15);

                var brush = new SolidBrush(Color.Blue);

                // Measure the size of the text
                SizeF textSize = g.MeasureString(captchaTxt, font);

                // Calculate the position to center the text
                float x = (width - textSize.Width) / 2;
                float y = (height - textSize.Height) / 2;

                // Draw the text at the calculated position
                g.DrawString(captchaTxt, font, brush, new PointF(x, y));
            }
            return image;
        }

        // Generate random captcha text
        private string GenerateRandomText()
        {
            // Your logic to generate random text for captcha
            return "CaptchaText"; // Replace this with your actual captcha text generation logic
        }


        //--------------------------------------------------------------------------
        public Bitmap GenerateCaptchaImage_(int width, int height)
        {
            var image = new Bitmap(width, height);
            using (var g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);
                var font = new Font("Arial", 15);
                
                var brush = new SolidBrush(Color.Black);
                g.DrawString(captchaText, font, brush, new PointF(5, 10));
            }
            return image;
        }

        //--------------------------------------------------------------------------
        public bool ValidateCaptcha(string userInput)
        {
            return userInput.Equals(captchaText, StringComparison.OrdinalIgnoreCase);
        }

        private string GenerateRandomString(int length)
        {
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string chars = "ABCDEFGHJKLMNOPRSTUVWXYZabcdefhikmnorstuvwxz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
