namespace CSharpMiscLibrary.Classes.APIs
{
    /// <summary>
    /// Kapow default response detailed attributes.
    /// </summary>
    public class KapowAttributeResponse
    {
        /// <summary>
        /// Type from response property (needs to start with undercase).
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Name from response property (needs to start with undercase).
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Value from response property (needs to start with undercase).
        /// </summary>
        public string value { get; set; }
    }
}
