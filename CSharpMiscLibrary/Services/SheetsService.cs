using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using ExcelDataReader;
using CSharpMiscLibrary.Classes.Excel;

namespace CSharpMiscLibrary.Services
{
    /// <summary>
    /// Common service for Excel Sheets.
    /// </summary>
    public class SheetsService
    {
        /// <summary>
        /// Default namespace to be used in FindMatch comparisons.
        /// </summary>
        public const string DEFAULT_COMPARISON_NAMESPACE = "CSharpMiscLibrary";

        /// <summary>
        /// Default module to be used in FindMatch comparisons.
        /// </summary>
        public const string DEFAULT_COMPARISON_MODULE = DEFAULT_COMPARISON_NAMESPACE + ".Services.DataService";

        /// <summary>
        /// Default function/method to be used in FindMatch comparisons.
        /// </summary>
        public const string DEFAULT_COMPARISON_FN = "CheckEqual";

        /// <summary>
        /// Check between two numbers function/method to be used in FindMatch comparisons.
        /// </summary>
        public const string BETWEEN_COMPARISON_FN = "CheckBetween";

        /// <summary>
        /// Find multiple/single match from sheet.
        /// </summary>
        /// <param name="table">DataTable from Excel sheet file</param>
        /// <param name="inputObjs">Input cells to be compared</param>
        /// <param name="outputPositions">Output cell positions to be returned</param>
        /// <param name="headerRow">Position of the header row</param>
        /// <param name="moduleName">Delegate module of the comparison function</param>
        /// <param name="fnName">Delegate comparison function (proxy function which changes accordingly)</param>
        /// <param name="multipleMatch">Sets if the return output must be multiple matches or not</param>
        /// <returns>List of lists of found rows with output values</returns>
        public static List<List<ExcelPairMatch>> FindMatch(DataTable table, ExcelPairMatch[] inputObjs, int[] outputPositions,
            int headerRow, string moduleName = DEFAULT_COMPARISON_MODULE, string fnName = DEFAULT_COMPARISON_FN, bool multipleMatch = false)
        {
            List<List<ExcelPairMatch>> matches = new List<List<ExcelPairMatch>>();
            for (int i = headerRow + 1; i < table.Rows.Count; i++)
            {
                var found = true;
                foreach (var pairMatch in inputObjs)
                {
                    if (found)
                    {
                        // @Override: if pair match object has a ComparisonMethod, override the normal comparison module and method
                        string fnNameTemp = fnName;
                        string moduleNameTemp = moduleName;
                        if (pairMatch.ComparisonMethod != null) {
                            if (!DataService.CheckEqual(pairMatch.ComparisonMethod, fnName) && pairMatch.ColBind != -1)
                            {
                                moduleNameTemp = pairMatch.ComparisonModule;
                                fnNameTemp = pairMatch.ComparisonMethod;
                            }
                        }

                        var assemblyObjType = AppDomain.CurrentDomain.Load(DEFAULT_COMPARISON_NAMESPACE);
                        Type serviceType = assemblyObjType.GetType(moduleNameTemp);
                        MethodInfo ComparisonMethod = serviceType.GetMethod(fnNameTemp);

                        object[] compMethodParams;
                        switch (fnNameTemp)
                        {
                            case BETWEEN_COMPARISON_FN:
                                if (DataService.IsTextEmptyOrNull(pairMatch.Value)) found = false;
                                else if (!DataService.IsDigitsOnly(pairMatch.Value)) found = false;
                                else
                                {
                                    // @Warning: Pair match col pos normally begins at 1,
                                    // but ItemArray begins at 0. So index reducing is mandatory!
                                    compMethodParams = new object[] { Math.Round(Convert.ToDouble(pairMatch.Value), 2),
                                    Math.Round(Convert.ToDouble(table.Rows[i].ItemArray[pairMatch.Pos-1]), 2),
                                    Math.Round(Convert.ToDouble(table.Rows[i].ItemArray[pairMatch.ColBind-1]), 2) };

                                    if (!(bool)ComparisonMethod.Invoke(serviceType, compMethodParams)) found = false;
                                }
                                break;
                            default:
                                if (DataService.IsTextEmptyOrNull(pairMatch.Value)) found = false;
                                else if (!DataService.IsDigitsOnly(pairMatch.Value)) found = false;
                                else
                                {
                                    // @Warning: Pair match col pos normally begins at 1,
                                    // but ItemArray begins at 0. So index reducing is mandatory!
                                    compMethodParams = new object[] { pairMatch.Value, table.Rows[i].ItemArray[pairMatch.Pos - 1].ToString() };

                                    if (!(bool)ComparisonMethod.Invoke(serviceType, compMethodParams)) found = false;
                                }
                                break;
                        }

                    }
                    else break;
                }

                if (found)
                {
                    List<ExcelPairMatch> rowOutput = new List<ExcelPairMatch>();
                    for (int j = 0; j < outputPositions.Length; j++)
                    {
                        ExcelPairMatch match = new ExcelPairMatch(i, table.Rows[i].ItemArray[outputPositions[j] - 1].ToString());
                        rowOutput.Add(match);
                    }

                    matches.Add(rowOutput);

                    if (!multipleMatch) return matches;
                }
            }

            if (matches.Count == 0) return null;
            else return matches;
        }

        /// <summary>
        /// Get header row position checking last columns for each iterated row.
        /// </summary>
        /// <param name="table">DataTable from Excel sheet file</param>
        /// <param name="rowDefaultPos">Default row position from average files</param>
        /// <returns>Found header position or default one.</returns>
        public static int GetHeaderRowPos(DataTable table, int rowDefaultPos)
        {
            int _colQty = table.Columns.Count;
            int _rowQty = table.Rows.Count;
            int _pos = rowDefaultPos;

            for (int i = 1; i < _rowQty; i++)
            {
                if (!Equals(table.Rows[i].ItemArray[_colQty - 1], DBNull.Value) || !Equals(table.Rows[i].ItemArray[_colQty - 2], DBNull.Value))
                {
                    _pos = i;
                    break;
                }
            }

            return _pos;
        }

        /// <summary>
        /// Get header array texts and check with registered shipment header bindings if columns positions need to be changed.
        /// </summary>
        /// <param name="headerNames">Array of header texts from file</param>
        /// <param name="registeredShipmentHeaderBindings">List of default header names and respective positions</param>
        /// <returns>Updated list of shipment header names and positions.</returns>
        public static List<ExcelHeaderBinding> CheckAttrHeaderArrayPos(object[] headerNames, List<ExcelHeaderBinding> registeredShipmentHeaderBindings)
        {
            if (!Equals(headerNames.Length, registeredShipmentHeaderBindings.Count))
            {
                string _msg = "File header array size is different from registered header array size (" + headerNames.Length.ToString() + " != " + registeredShipmentHeaderBindings.Count.ToString() + ").";
                throw new Exceptions.InvalidEqualityException(_msg);
            }
            else
            {
                foreach (var headerBinding in registeredShipmentHeaderBindings)
                {
                    for (int j = 0; j < headerNames.Length; j++)
                    {

                        if (Equals(headerBinding.Value, headerNames[j].ToString())) headerBinding.Pos = j + 1;
                    }
                }

                return registeredShipmentHeaderBindings;
            }
        }

        /// <summary>
        /// Get DataSet info from excel sheet file.
        /// </summary>
        /// <param name="filePath">Full excel file path</param>
        /// <returns>DataSet Excel Table with sheets and contents.</returns>
        public static DataSet OpenExcelSheetGetInfo(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    // The result of each spreadsheet is in result.Tables
                    DataSet result = reader.AsDataSet();

                    stream.Close();
                    return result;
                }
            }
        }
    }
}
