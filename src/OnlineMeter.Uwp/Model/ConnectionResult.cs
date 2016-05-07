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
		/// Initializes a new instance of the <see cref="ConnectionResult"/> class.
		/// </summary>
		public ConnectionResult(bool online)
            : this(online, -1)
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
    }
}
