// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

using GalaSoft.MvvmLight.Messaging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VDsoft.OnlineMeter.Uwp.Model;

namespace VDsoft.OnlineMeter.Uwp.Dal
{
    /// <summary>
    /// Monitors the internet connection.
    /// </summary>
    public class InternetMonitor
    {
        /// <summary>
        /// Url for the test.
        /// </summary>
        private readonly string testUrl = "http://www.google.at";

        /// <summary>
        /// Starts to check the connection periodically.
        /// </summary>
        /// <returns>A Task when the operation is finished.</returns>
        public async Task StartConnectionCheck()
        {
            while (true)
            {
                var result = this.CheckConnection();

                Messenger.Default.Send<ConnectionResult>(result.Result, ViewModel.ViewModelLocator.StatusUpdateToken);

                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        /// <summary>
        /// Checks the connection.
        /// </summary>
        /// <returns><see cref="ConnectionResult"/> with the result of the check.</returns>
        public async Task<ConnectionResult> CheckConnection()
        {
            ConnectionResult connection = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var result = await client.GetAsync(this.testUrl);
                    string body = await result.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(body))
                    {
                        connection = new ConnectionResult(false);
                    }
                    else
                    {
                        connection = new ConnectionResult(true);
                    }
                }
            }
            catch (Exception)
            {
                connection = new ConnectionResult(false);
            }

            return connection;
        }
    }
}
