using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
//using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestPressureSensor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<string> data = new List<string>();
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            var _bmp180 = new Bmp180Sensor();
            await _bmp180.InitializeAsync();

            

            var sensorData = await _bmp180.GetSensorDataAsync(Bmp180AccuracyMode.UltraLowPower);
            var temp = sensorData.Temperature.ToString("F1");
            var press = sensorData.Pressure.ToString("F2");

            data.Clear();
            while(data.Count < 100)
            {
                data.Add(press);
                press = sensorData.Pressure.ToString("F2");
            }
            StringBuilder str = new StringBuilder();
            for(int i = 0; i < 100; i++) { str.AppendLine(data[i]); }
            outputBox.Text = str.ToString();
        }
    }
}
