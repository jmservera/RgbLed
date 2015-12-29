using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.Unloaded += MainPage_Unloaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var pwmProvider = PwmSoftware.PwmProviderSoftware.GetPwmProvider();
            var controller=(await Windows.Devices.Pwm.PwmController.GetControllersAsync(pwmProvider)).FirstOrDefault();
            if(controller!= null)
            {
                var pinR = controller.OpenPin(5);
                var pinG = controller.OpenPin(6);
                var pinB = controller.OpenPin(7);
                var led = new RgbLed(pinR, pinG, pinB);
                led.On();
                led.Color = Colors.LimeGreen;
            }
        }
        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
