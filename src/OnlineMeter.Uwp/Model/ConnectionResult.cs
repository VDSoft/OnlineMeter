// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------


namespace VDsoft.OnlineMeter.Uwp.Model
{
    /// <summary>
    /// Contains the result of a ping.
    /// </summary>
    public class ConnectionResult
    {
        /// <summary>
        /// Value indicating whether the system is online or not.
        /// </summary>
        private bool online;

        /// <summary>
        /// Delay of the ping.
        /// </summary>
        private int delay;

        /// <summary>
		/// Initializes a new instance of the <see cref="ConnectionResult"/> class.
		/// </summary>
        /// <param name="online">Value indicating whether the system is online or not.</param>
        /// <param name="delay">Delay of the ping.</param>
		public ConnectionResult(bool online, int delay)
        {
            this.Online = online;
            this.Delay = delay;
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="ConnectionResult"/> class.
		/// </summary>
		public ConnectionResult(bool online)
            : this(online, -1)
        {
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="ConnectionResult"/> class.
		/// </summary>
		public ConnectionResult(int delay)
            :this(false, delay)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the system is online or not.
        /// </summary>
        public bool Online
        {
            get
            {
                return this.online;
            }

            set
            {
                if (value == this.online)
                {
                    return;
                }

                this.online = value;
            }
        }

        /// <summary>
        /// Gets or sets the delay of the ping.
        /// </summary>
        public int Delay
        {
            get
            {
                return this.delay;
            }
            set
            {
                if (value == this.delay)
                {
                    return;
                }

                this.delay = value;
            }
        }
    }
}
