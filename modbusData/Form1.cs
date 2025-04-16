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
            comboBox1.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "UniPro Modbus Packets")
            {
                ModBusDecode();
            }
            if(comboBox1.Text == "Float - Big Endian (ABCD)")
            {
                Float_Big_Endian_ABCD();
            }
            else
            {
                // Redirect Console Output
                Console.SetOut(new MultiTextWriter(Console.Out, consoleOutputWriter));

                string fullInput = textBox1.Text;
                Console.WriteLine("Captured Input:\n");
                Console.WriteLine(fullInput);

                Dictionary<int, ushort> modbusRegisters = new Dictionary<int, ushort>();
                Regex regex = new Regex(@"Register\s+(\d+)\s+\(UINT16\):\s+(\d+)", RegexOptions.Compiled);

                foreach (Match match in regex.Matches(fullInput))
                {
                    int registerNumber = int.Parse(match.Groups[1].Value);
                    ushort registerValue = ushort.Parse(match.Groups[2].Value);
                    modbusRegisters[registerNumber] = registerValue;
                }

                Console.WriteLine("\nDecoded FLOAT values:\n");
                for (int i = 0; i < modbusRegisters.Count - 1; i += 2)
                {
                    if (!modbusRegisters.ContainsKey(i) || !modbusRegisters.ContainsKey(i + 1))
                        continue;

                    ushort[] pair = new ushort[] { modbusRegisters[i], modbusRegisters[i + 1] };
                    float floatValue = ConvertToFloat(pair);
                    Console.WriteLine($"Registers {i:D2}-{i + 1:D2} (FLOAT): {floatValue}");
                }

                Console.WriteLine("\nDecoded UINT32 values:\n");
                for (int i = 0; i < modbusRegisters.Count - 1; i += 2)
                {
                    if (!modbusRegisters.ContainsKey(i) || !modbusRegisters.ContainsKey(i + 1))
                        continue;

                    ushort[] pair = new ushort[] { modbusRegisters[i], modbusRegisters[i + 1] };
                    uint uintValue = ConvertToUInt32(pair);
                    Console.WriteLine($"Registers {i:D2}-{i + 1:D2} (UINT32): {uintValue}");
                }

                Console.WriteLine("\nDecoded INT32 values:\n");
                for (int i = 0; i < modbusRegisters.Count - 1; i += 2)
                {
                    if (!modbusRegisters.ContainsKey(i) || !modbusRegisters.ContainsKey(i + 1))
                        continue;

                    ushort[] pair = new ushort[] { modbusRegisters[i], modbusRegisters[i + 1] };
                    int intValue = ConvertToInt32(pair);
                    Console.WriteLine($"Registers {i:D2}-{i + 1:D2} (INT32): {intValue}");
                }

                Console.WriteLine("\nDecoded DOUBLE values:\n");
                for (int i = 0; i < modbusRegisters.Count - 3; i += 4)
                {
                    if (!modbusRegisters.ContainsKey(i) || !modbusRegisters.ContainsKey(i + 1) ||
                        !modbusRegisters.ContainsKey(i + 2) || !modbusRegisters.ContainsKey(i + 3))
                        continue;

                    ushort[] quad = new ushort[]
                    {
            modbusRegisters[i],
            modbusRegisters[i + 1],
            modbusRegisters[i + 2],
            modbusRegisters[i + 3]
                    };
                    double doubleValue = ConvertToDouble(quad);
                    Console.WriteLine($"Registers {i:D2}-{i + 3:D2} (DOUBLE): {doubleValue}");
                }

                //Console.WriteLine("\nDecoded INT16 (Big Endian - AB) values:\n");
                //foreach (var kvp in modbusRegisters.OrderBy(k => k.Key))
                //{
                //    int address = kvp.Key;
                //    ushort rawValue = kvp.Value;

                //    short int16Value = ConvertToInt16BigEndian(rawValue);
                //    Console.WriteLine($"Register {address:D2} (INT16 - Big Endian): {int16Value}");
                //}

                // Save the output to a file
                WriteConsoleOutputToFile("ModbusDecodedOutput.txt");
                Console.WriteLine("\nConsole output written to 'ModbusDecodedOutput.txt'");
                consoleOutputWriter = null; // Reset the StringWriter
                consoleOutputWriter = new StringWriter(); // Create a new StringWriter for future use
                MessageBox.Show("File generated!");
            }
        }

        public void Float_Big_Endian_ABCD()
        {
            string[] hexBytes = textBox1.Text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

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


        private void ModBusDecode()
        {
            // Get the string from TextBox1
            string inputText = textBox1.Text;

            // Regular expression to match "Register <number>: <value>"
            var registerPattern = new Regex(@"Register (\d+): (\d+)");

            // Find all matches for the registers and values
            var matches = registerPattern.Matches(inputText);

            // List to store decoded float values
            var decodedFloats = new System.Collections.Generic.List<float>();

            // Process each match and decode to float
            for (int i = 0; i < matches.Count - 1; i += 2)
            {
                // Get the first and second register values
                int register1 = int.Parse(matches[i].Groups[2].Value);
                int register2 = int.Parse(matches[i + 1].Groups[2].Value);

                // Combine two 16-bit values into a byte array (Little Endian format)
                byte[] bytes = new byte[4];
                bytes[0] = (byte)(register1 & 0xFF);
                bytes[1] = (byte)((register1 >> 8) & 0xFF);
                bytes[2] = (byte)(register2 & 0xFF);
                bytes[3] = (byte)((register2 >> 8) & 0xFF);

                // Convert to float and add to the list
                float value = BitConverter.ToSingle(bytes, 0);
                decodedFloats.Add(value);
            }

            // Display the decoded float values
            string result = "Decoded FLOAT values:\n";
            foreach (var decodedValue in decodedFloats)
            {
                result += decodedValue + "\n";
            }

            // Show the result in a message box or update a label
            MessageBox.Show(result);

            // Generate log with DateTime in the file name
            string logFileName = $"UniPro_ModbusLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string logFilePath = Path.Combine(Application.StartupPath, logFileName);

            // Log header with timestamp
            string logHeader = $"Log generated at {DateTime.Now}\n\n";

            // Prepare log content with decoded float values
            string logContent = logHeader + "Decoded FLOAT values:\n";
            foreach (var decodedValue in decodedFloats)
            {
                logContent += decodedValue + "\n";
            }

            // Write the log content to the file
            try
            {
                File.WriteAllText(logFilePath, logContent);
                MessageBox.Show($"Log file saved: {logFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing log file: {ex.Message}");
            }
        }



        static float ConvertToFloat(ushort[] registers)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(registers[0] >> 8);
            bytes[1] = (byte)(registers[0] & 0xFF);
            bytes[2] = (byte)(registers[1] >> 8);
            bytes[3] = (byte)(registers[1] & 0xFF);
            return BitConverter.ToSingle(bytes, 0);
        }
        static short ConvertToInt16BigEndian(ushort register)
        {
            byte highByte = (byte)(register >> 8);
            byte lowByte = (byte)(register & 0xFF);
            byte[] bytes = new byte[] { highByte, lowByte };
            return BitConverter.ToInt16(bytes.Reverse().ToArray(), 0); // BitConverter expects little endian by default
        }


        static uint ConvertToUInt32(ushort[] registers)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(registers[0] >> 8);
            bytes[1] = (byte)(registers[0] & 0xFF);
            bytes[2] = (byte)(registers[1] >> 8);
            bytes[3] = (byte)(registers[1] & 0xFF);
            return BitConverter.ToUInt32(bytes, 0);
        }

        static int ConvertToInt32(ushort[] registers)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(registers[0] >> 8);
            bytes[1] = (byte)(registers[0] & 0xFF);
            bytes[2] = (byte)(registers[1] >> 8);
            bytes[3] = (byte)(registers[1] & 0xFF);
            return BitConverter.ToInt32(bytes, 0);
        }

        static double ConvertToDouble(ushort[] registers)
        {
            byte[] bytes = new byte[8];
            bytes[0] = (byte)(registers[0] >> 8);
            bytes[1] = (byte)(registers[0] & 0xFF);
            bytes[2] = (byte)(registers[1] >> 8);
            bytes[3] = (byte)(registers[1] & 0xFF);
            bytes[4] = (byte)(registers[2] >> 8);
            bytes[5] = (byte)(registers[2] & 0xFF);
            bytes[6] = (byte)(registers[3] >> 8);
            bytes[7] = (byte)(registers[3] & 0xFF);
            return BitConverter.ToDouble(bytes, 0);
        }

        static void WriteConsoleOutputToFile(string path)
        {
            string dateTimeHeader = $"Log Created On: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\n\r\n";
            string content = dateTimeHeader + consoleOutputWriter.ToString();
            File.WriteAllText(path, content);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath;  // Open in app folder
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";         // Show only .txt files
                openFileDialog.Title = "Select a .txt file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    // Optional: open the selected file
                    System.Diagnostics.Process.Start("notepad.exe", selectedFilePath);
                }
            }
        }
        private void button2_Click_(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "ModbusDecodedOutput.txt");

            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start("notepad.exe", filePath);
            }
            else
            {
                MessageBox.Show("The file 'ModbusDecodedOutput.txt' was not found.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //string pattern = @"\b(?:[A-Fa-f0-9]{2}\s+)+\b";
            //var matches = Regex.Matches(textBox1.Text, pattern);

            //foreach (Match match in matches)
            //{
            //    Console.WriteLine("Hex Block Found:");
            //    //Console.WriteLine(match.Value);
            //}


            //if (textBox1.Text.Contains("HOLDING Modbus Data Received"))
            //{
            //    comboBox1.SelectedIndex = 1;
            //}
            //else
            //{
            //    comboBox1.SelectedIndex = 0;

            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
