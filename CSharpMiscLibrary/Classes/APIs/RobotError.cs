namespace CSharpMiscLibrary.Classes.APIs
{
    /// <summary>
    /// Kapow default error response object.
    /// </summary>
    public class RobotError
    {
        /// <summary>
        /// Date from response property (needs to start with undercase).
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// Robot URL from response property (needs to start with undercase).
        /// </summary>
        public string robotUrl { get; set; }

        /// <summary>
        /// Error message from response property (needs to start with undercase).
        /// </summary>
        public string errorMessage { get; set; }

        /// <summary>
        /// Error location from response property (needs to start with undercase).
        /// </summary>
        public string errorLocation { get; set; }

        /// <summary>
        /// Error location code from response property (needs to start with undercase).
        /// </summary>
        public string errorLocationCode { get; set; }
    }
}
