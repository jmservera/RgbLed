using PwmSoftware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Pwm;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RgbLed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RgbLed _led;
        DispatcherTimer _timer;
        int _counter;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.Unloaded += MainPage_Unloaded;
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            switch (_counter++ % 4)
            {
                case 0:
                    _led.Color = Colors.Red;
                    break;
                case 1:
                    _led.Color = Colors.Yellow;
                    break;
                case 2:
                    _led.Color = Colors.Orange;
                    break;
                default:
                    _led.Color = Colors.Green;
                    break;
            }    
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var controller=(await PwmController.GetControllersAsync(PwmProviderSoftware.GetPwmProvider())).FirstOrDefault();
            if (controller!= null)
            {
                controller.SetDesiredFrequency(100);
                var pinR = controller.OpenPin(5);
                var pinG = controller.OpenPin(6);
                var pinB = controller.OpenPin(7);
                _led = new RgbLed(pinR, pinG, pinB);
                _led.Color = Colors.Green;
                _led.On();
            }
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if(_led!= null)
            {
                _led.Off();
                _led.Dispose();
                _led = null;
            }
        }
    }
}