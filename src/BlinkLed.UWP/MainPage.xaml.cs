using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlinkLed.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LEDPinRed = 5;
        private const int LedPinYellow = 6;
        private const int LedPinGreen = 13;

        private GpioPin pinRed = null;
        private GpioPin pinYellow = null;
        private GpioPin pinGreen = null;

        private GpioPinValue pinRedValue;
        private GpioPinValue pinYellowValue;
        private GpioPinValue pinGreenValue;

        private DispatcherTimer timer;
        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush yellowBrush = new SolidColorBrush(Windows.UI.Colors.Yellow);
        private SolidColorBrush greenBrush = new SolidColorBrush(Windows.UI.Colors.Green);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);

        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            InitGPIO();
            if (pinRed != null)
            {
                timer.Start();
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pinRed = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            this.InitializePins(gpio);

            ////this.pinRed = gpio.OpenPin(LEDPinRed);

            ////this.pinRed.Write(pinRedValue);
            ////this.pinRed.SetDriveMode(GpioPinDriveMode.Output);

            GpioStatus.Text = "GPIO pin initialized correctly.";

        }

        private void InitializePins(GpioController gpio)
        {
            this.pinRed = gpio.OpenPin(LEDPinRed);
            this.pinYellow = gpio.OpenPin(LedPinYellow);
            this.pinGreen = gpio.OpenPin(LedPinGreen);

            this.pinRedValue = GpioPinValue.High;
            this.pinYellowValue = GpioPinValue.High;
            this.pinGreenValue = GpioPinValue.High;

        }

        private void Timer_Tick(object sender, object e)
        {
            if (pinRedValue == GpioPinValue.High)
            {
                this.pinRedValue = GpioPinValue.Low;
                this.pinYellowValue = GpioPinValue.Low;
                this.pinGreenValue = GpioPinValue.Low;
                this.LEDRed.Fill = redBrush;
                this.LEDYellow.Fill = this.yellowBrush;
                this.LEDGreen.Fill = this.greenBrush;

            }
            else
            {
                this.pinRedValue = GpioPinValue.High;
                this.pinYellowValue = GpioPinValue.High;
                this.pinGreenValue = GpioPinValue.High;
                this.LEDRed.Fill = grayBrush;
                this.LEDYellow.Fill = this.grayBrush;
                this.LEDGreen.Fill = this.grayBrush;
            }

            this.pinRed.Write(this.pinRedValue);
            this.pinYellow.Write(this.pinYellowValue);
            this.pinGreen.Write(this.pinGreenValue);

            this.pinRed.SetDriveMode(GpioPinDriveMode.Output);
            this.pinYellow.SetDriveMode(GpioPinDriveMode.Output);
            this.pinGreen.SetDriveMode(GpioPinDriveMode.Output);
        }
    }
}
