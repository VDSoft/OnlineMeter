// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System.Threading.Tasks;
using VDsoft.OnlineMeter.Uwp.Dal;
using Windows.UI.Xaml.Media;
using GalaSoft.MvvmLight.Messaging;
using VDsoft.OnlineMeter.Uwp.Model;

namespace VDsoft.OnlineMeter.Uwp.ViewModel
{
    /// <summary>
    /// View model for the <see cref="Meter"/> view.
    /// </summary>
    public class MeterViewModel : ViewModelBase
    {
        /// <summary>
        /// Message when system is online.
        /// </summary>
        private readonly string onlineMessage = "You're online and ready to go.";

        /// <summary>
        /// Message when system is offline.
        /// </summary>
        private readonly string offlineMessage = "Woow, you're offline. Better check your connection.";

        /// <summary>
        /// Green color.
        /// </summary>
        private readonly Windows.UI.Color green = Windows.UI.Colors.Green;

        /// <summary>
        /// Red color.
        /// </summary>
        private readonly Windows.UI.Color red = Windows.UI.Colors.Red;

        /// <summary>
        /// Brush for the red led.
        /// </summary>
        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);

        /// <summary>
        /// Brush for the green led.
        /// </summary>
        private SolidColorBrush greenBrush = new SolidColorBrush(Windows.UI.Colors.Green);

        /// <summary>
        /// Brush for the gray default.
        /// </summary>
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.Gray);

        /// <summary>
        /// Status message.
        /// </summary>
        private string statusMessage = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterViewModel"/> class.
        /// </summary>
        public MeterViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.RedBrush = this.grayBrush;
                this.StatusMessage = this.onlineMessage;
            }
            else
            {
                this.GreenBrush = this.grayBrush;
                this.RedBrush = this.grayBrush;
            }

            // register to receive the status updates.
            Messenger.Default.Register<ConnectionResult>(this, ViewModelLocator.StatusUpdateToken, this.UpdateStatus);
            Task.Run(() => 
            {
                InternetMonitor monitor = new InternetMonitor();
                monitor.StartConnectionCheck();
            });
        }

        /// <summary>
        /// Gets or sets the brush for the red led.
        /// </summary>
        public SolidColorBrush RedBrush
        {
            get
            {
                return this.redBrush;
            }
            set
            {
                if (value == this.redBrush)
                {
                    return;
                }

                this.redBrush = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the brush for the green led.
        /// </summary>
        public SolidColorBrush GreenBrush
        {
            get
            {
                return this.greenBrush;
            }
            set
            {
                if (value == this.greenBrush)
                {
                    return;
                }

                this.greenBrush = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }
            set
            {
                if (value == this.statusMessage)
                {
                    return;
                }

                this.statusMessage = value;
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Updates the current status depending on the provided <see cref="ConnectionResult"/>.
        /// </summary>
        /// <param name="result"><see cref="ConnectionResult"/> of the check.</param>
        private void UpdateStatus(ConnectionResult result)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (result.Online)
                {
                    this.StatusMessage = this.onlineMessage;

                    this.GreenBrush = new SolidColorBrush(this.green);
                    this.RedBrush = this.grayBrush;
                }
                else
                {
                    this.StatusMessage = this.offlineMessage;

                    this.RedBrush = new SolidColorBrush(this.red);
                    this.GreenBrush = this.grayBrush;
                }

            });
        }
    }
}
