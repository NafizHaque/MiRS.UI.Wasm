namespace MiRs.Domain.Configurations
{
    /// <summary>
    /// Class that represents app setting from Azure config.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or Sets the ApiBaseUrl.
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets the ApiTimeoutSeconds.
        /// </summary>
        public int TimeoutSeconds { get; set; }

        /// <summary>
        /// Gets or Sets the ApiTimeoutSeconds.
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// Gets or Sets the AdminOic.
        /// </summary>
        public string AdminOid { get; set; } = "697b1b0b-d2fa-4a0d-9f12-548912609bfd";

    }
}
