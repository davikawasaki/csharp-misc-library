namespace CSharpMiscLibrary.Classes.Excel
{
    /// <summary>
    /// Class to store Excel header value and column position.
    /// </summary>
    public class ExcelHeaderBinding
    {
        /// <summary>
        /// Excel header column value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Excel header column position.
        /// </summary>
        public int Pos { get; set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ExcelHeaderBinding() { }

        /// <summary>
        /// Excel header binding constructor.
        /// </summary>
        /// <param name="Value">Excel header column value.</param>
        /// <param name="Pos">Excel header column position.</param>
        public ExcelHeaderBinding(string Value, int Pos)
        {
            this.Value = Value;
            this.Pos = Pos;
        }
    }
}
