using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace color_carillon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serialPort;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                serialPort = new SerialPort("COM3", 9600);
                serialPort.Open();
            } catch (Exception ex)
            {
                Console.WriteLine("Whoops! Something went wrong.");
                Console.WriteLine(ex.Message);
            }
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort.BytesToRead > 0)
            {
                char cmd = (char)serialPort.ReadChar();
                if (cmd.Equals('C'))
                {
                    while (serialPort.BytesToRead < 3) ;
                    int r = serialPort.ReadByte();
                    int g = serialPort.ReadByte();
                    int b = serialPort.ReadByte();
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        tbR.Text = r.ToString();
                        tbG.Text = g.ToString();
                        tbB.Text = b.ToString();
                        RectangleColor.Fill = new SolidColorBrush(Color.FromRgb((byte)r, (byte)g, (byte)b));

                        Enums.Color detectedColor = Utils.Classify(r, g, b);
                        string detected = "";
                        switch (detectedColor)
                        {
                            case Enums.Color.None:
                                detected = "None";
                                break;
                            case Enums.Color.Black:
                                detected = "Black";
                                break;
                            case Enums.Color.White:
                                detected = "White";
                                break;
                            case Enums.Color.Gray:
                                detected = "Gray";
                                break;
                            case Enums.Color.Red:
                                detected = "Red";
                                break;
                            case Enums.Color.Yellow:
                                detected = "Yellow";
                                break;
                            case Enums.Color.Green:
                                detected = "Green";
                                break;
                            case Enums.Color.Cyan:
                                detected = "Cyan";
                                break;
                            case Enums.Color.Blue:
                                detected = "Blue";
                                break;
                            case Enums.Color.Magenta:
                                detected = "Magenta";
                                break;
                        }

                        tbDetected.Text = detected;
                    }));
                }
            }
        }
    }
}
