using System.Collections.Generic;

namespace CSharpMiscLibrary.Classes.APIs
{
    /// <summary>
    /// Kapow default response value details.
    /// </summary>
    public class KapowValueResponse
    {
        /// <summary>
        /// Type value response name from response property (needs to start with undercase).
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// Kapow attribute response object from response property (needs to start with undercase).
        /// </summary>
        public List<KapowAttributeResponse> attribute { get; set; }
    }
}
