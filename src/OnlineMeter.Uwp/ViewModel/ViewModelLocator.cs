﻿// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

using VDsoft.OnlineMeter.Uwp.Dal;

namespace VDsoft.OnlineMeter.Uwp.ViewModel
{
    /// <summary>
    /// Locator for all view models
    /// </summary>
    public class ViewModelLocator
    {
        private MeterViewModel meterViewModel = null;

        private GpioHandler gpio = null;

        /// <summary>
		/// Initializes a new instance of the <see cref="ViewModelLocator"/> class.
		/// </summary>
		public ViewModelLocator()
        {
            this.gpio = new GpioHandler();
        }

        #region Tokens

        /// <summary>
        /// Token for the status update.
        /// </summary>
        public static object StatusUpdateToken = new object();

        #endregion

        /// <summary>
        /// Gets the current instance of the <see cref="MeterViewModel"/> class.
        /// </summary>
        public MeterViewModel Meter
        {
            get
            {
                if (this.meterViewModel == null)
                {
                    this.meterViewModel = new MeterViewModel();
                }

                return this.meterViewModel;
            }
        }
    }
}