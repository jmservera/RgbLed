using GalaSoft.MvvmLight;
using PwmSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Pwm;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
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
        RgbLed led;
        DispatcherTimer timer;
        int counter;

        public ColorSource SelectedColor { get; set; }

        public MainPage()
        {
            this.SelectedColor = new ColorSource();
            this.SelectedColor.PropertyChanged += SelectedColor_PropertyChanged;
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.Unloaded += MainPage_Unloaded;
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void SelectedColor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(led!= null)
            {
                led.Color = SelectedColor.Color;
            }
        }

        Color[] colors = new Color[] { Colors.Red, Colors.Green, Colors.Blue , Colors.Yellow, Colors.Orange, Colors.Turquoise, Colors.White, Color.FromArgb(255, 120, 120, 120), Color.FromArgb(255,50,50,50), Colors.Black };

        private void Timer_Tick(object sender, object e)
        {
            var pos = counter++ % colors.Length;
            SelectedColor.Color = colors[pos];
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsApiContractPresent("Windows.Devices.DevicesLowLevelContract", 1))
            {
                try
                {
                    var gpio = await Windows.Devices.Gpio.GpioController.GetDefaultAsync();
                    if (gpio != null)
                    {
                        var provider = PwmProviderSoftware.GetPwmProvider();
                        if (provider != null)
                        {
                            var controllers = (await PwmController.GetControllersAsync(provider));
                            if (controllers != null)
                            {
                                var controller = controllers.FirstOrDefault();
                                if (controller != null)
                                {
                                    controller.SetDesiredFrequency(100);
                                    var pinR = controller.OpenPin(5);
                                    var pinB = controller.OpenPin(6);
                                    var pinG = controller.OpenPin(13);
                                    led = new RgbLed(pinR, pinG, pinB);
                                    led.On();
                                    led.Color = Colors.White;
                                    Task.Delay(50).Wait();
                                    led.Color = Colors.Black;

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                timer.Start();
            }
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (led != null)
            {
                led.Off();
                led.Dispose();
                led = null;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }

}