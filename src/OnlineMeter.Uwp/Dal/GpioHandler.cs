// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Windows.Devices.Gpio;
using GalaSoft.MvvmLight.Messaging;
using VDsoft.OnlineMeter.Uwp.ViewModel;
using VDsoft.OnlineMeter.Uwp.Model;

namespace VDsoft.OnlineMeter.Uwp.Dal
{
    /// <summary>
    /// Handles all GPIO requests.
    /// </summary>
    public class GpioHandler
    {
        /// <summary>
        /// Pin id of the red led.
        /// </summary>
        private readonly int ledPinRed = 6;

        /// <summary>
        /// Pin id of the green led.
        /// </summary>
        private readonly int ledPinGreen = 13;

        /// <summary>
        /// Instance for the red pin.
        /// </summary>
        private GpioPin pinRed = null;

        /// <summary>
        /// Instance for the green pin.
        /// </summary>
        private GpioPin pinGreen = null;

        /// <summary>
		/// Initializes a new instance of the <see cref="GpioHandler"/> class.
		/// </summary>
		public GpioHandler()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                throw new Exception("Initializing the gpio failed.");
            }

            this.InitializePins(gpio);

            Messenger.Default.Register<ConnectionResult>(this, ViewModelLocator.StatusUpdateToken, this.UpdateStatus);
        }

        /// <summary>
        /// Initializes all used pins.
        /// </summary>
        /// <param name="gpio"><see cref="GpioController"/> to initialize pins.</param>
        private void InitializePins(GpioController gpio)
        {
            this.pinRed = gpio.OpenPin(this.ledPinRed);
            this.pinGreen = gpio.OpenPin(this.ledPinGreen);
        }

        /// <summary>
        /// Updates the status of the leds.
        /// </summary>
        /// <param name="result"><see cref="ConnectionResult"/> with the current status.</param>
        private void UpdateStatus(ConnectionResult result)
        {
            if (result.Online)
            {
                this.pinGreen.Write(GpioPinValue.High);
                this.pinRed.Write(GpioPinValue.Low);
            }
            else
            {
                this.pinGreen.Write(GpioPinValue.Low);
                this.pinRed.Write(GpioPinValue.High);
            }

            this.pinGreen.SetDriveMode(GpioPinDriveMode.Output);
            this.pinRed.SetDriveMode(GpioPinDriveMode.Output);
        }
    }
}
