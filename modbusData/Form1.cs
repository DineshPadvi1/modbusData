using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;

namespace modbusData
{
    public partial class Form1 : Form
    {
        static StringWriter consoleOutputWriter = new StringWriter(); // Captures everything printed to console
        public Form1()
        {
            InitializeComponent();
            //comboBox1.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            Float_Big_Endian_ABCD();
        }

        public void Float_Big_Endian_ABCD()
        {
            string[] hexBytes = null;// textBox1.Text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            byte[] bytes = new byte[hexBytes.Length];
            for (int i = 0; i < hexBytes.Length; i++)
            {
                bytes[i] = byte.Parse(hexBytes[i], NumberStyles.HexNumber);
            }

            // Define file path (you can change the path as needed)
            string filePath = Path.Combine(Application.StartupPath, "Float_Big_Endian_ABCD_FloatOutput.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Converted Float Values (Big Endian):\n");

                for (int i = 0; i < bytes.Length; i += 4)
                {
                    if (i + 3 >= bytes.Length) break;

                    byte[] floatBytes = new byte[4];
                    floatBytes[0] = bytes[i];
                    floatBytes[1] = bytes[i + 1];
                    floatBytes[2] = bytes[i + 2];
                    floatBytes[3] = bytes[i + 3];

                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(floatBytes);

                    float value = BitConverter.ToSingle(floatBytes, 0);
                    writer.WriteLine($"{i / 4:D2}: {value}");
                }
            }

            MessageBox.Show("Float values written to file:\n" + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

 
    }

    // Helper class to write to both console and memory
    public class MultiTextWriter : TextWriter
    {
        private readonly TextWriter _console;
        private readonly TextWriter _memory;

        public MultiTextWriter(TextWriter console, TextWriter memory)
        {
            _console = console;
            _memory = memory;
        }

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value)
        {
            _console.Write(value);
            _memory.Write(value);
        }

        public override void Write(string value)
        {
            _console.Write(value);
            _memory.Write(value);
        }

        public override void WriteLine(string value)
        {
            _console.WriteLine(value);
            _memory.WriteLine(value);
        }
    }
}
