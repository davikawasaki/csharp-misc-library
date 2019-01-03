using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSharpMiscLibrary.Classes.APIs
{
    /// <summary>
    /// Kapow default HTTP response.
    /// </summary>
    public class KapowResponse
    {
        /// <summary>
        /// Execution time response property (needs to start with undercase).
        /// </summary>
        public string executionTime { get; set; }

        /// <summary>
        /// Robot error object from response property (needs to start with undercase).
        /// </summary>
        [JsonProperty(PropertyName = "robot-error")]
        public RobotError robotError { get; set; }

        /// <summary>
        /// List of returned values from response property (needs to start with undercase).
        /// </summary>
        public List<KapowValueResponse> values { get; set; }
    }
}
