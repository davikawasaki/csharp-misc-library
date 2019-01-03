namespace CSharpMiscLibrary.Classes.Excel
{
    /// <summary>
    /// Excel pair match between two cells positions.
    /// Inherits ExcelHeaderBinding to store cell column position and value.
    /// If binding is set, it needs to store also the comparison module and method to be called.
    /// </summary>
    public class ExcelPairMatch : ExcelHeaderBinding
    {
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public ExcelPairMatch() : base() { }

        /// <summary>
        /// Constructor to set pair value-pos of cell and the binding cell to be matched. If there's a binding cell, sets the comparison module and method.
        /// </summary>
        /// <param name="ColPos">Column position on Excel sheet.</param>
        /// <param name="ColValue">Column value on Excel sheet.</param>
        /// <param name="ColBind">Column binding to be matched on Excel sheet.</param>
        /// <param name="ComparisonModule">Module to be used in the comparison.</param>
        /// <param name="ComparisonMethod">Method to be used in the comparison.</param>
        public ExcelPairMatch(int ColPos, string ColValue, int ColBind = -1, string ComparisonModule = null, string ComparisonMethod = null) : base(ColValue, ColPos)
        {
            this.ColBind = ColBind;
            this.ComparisonModule = ComparisonModule;
            this.ComparisonMethod = ComparisonMethod;
        }

        /// <summary>
        /// Column binding to be matched on Excel sheet.
        /// </summary>
        public int ColBind { get; set; }

        /// <summary>
        /// Module to be used in the comparison.
        /// </summary>
        public string ComparisonModule { get; set; }

        /// <summary>
        /// Method to be used in the comparison.
        /// </summary>
        public string ComparisonMethod { get; set; }
    }
}
