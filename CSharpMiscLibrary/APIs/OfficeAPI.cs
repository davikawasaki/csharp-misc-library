using System;
using CSharpMiscLibrary.Services;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Outlook = Microsoft.Office.Interop.Outlook;
using Powerpoint = Microsoft.Office.Interop.PowerPoint;
using System.IO;

namespace CSharpMiscLibrary.APIs
{
    /// <summary>
    /// Class that handles office API communication.
    /// </summary>
    public class OfficeAPI
    {
        /// <summary>
        /// Generic private method to run a VBA macro independently of Office Application.
        /// </summary>
        /// <param name="oApp">Office application object</param>
        /// <param name="oRunArgs">Office arguments, starting with macro name</param>
        /// <returns>Object returned from VBA execution</returns>
        private static object _RunGenericVBAMacro(object oApp, object[] oRunArgs)
        {
            try
            {
                return oApp.GetType().InvokeMember("Run", System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod,
                    null, oApp, oRunArgs);

            }
            catch (Exception _exp)
            {
                return "ERROR | " + _exp.Message;
            }

        }

        /// <summary>
        /// Call VBA script from one of Office applications.
        /// List of accepted apps: Excel, Word, Powerpoint, Outlook
        /// </summary>
        /// <param name="processName">Name of the process being executed</param>
        /// <param name="officeAppName">One of the office app names</param>
        /// <param name="filePath">File path with the VBA script to be executed</param>
        /// <param name="macroName">VBA script name with module/procedure name</param>
        /// <param name="macroArgs">List of object args to be used in the script</param>
        /// <param name="showAlerts">Boolean status to show alerts on open application</param>
        /// <param name="visibleApp">Boolean status to show open application</param>
        /// <param name="saveFile">Boolean status to save or not the file</param>
        /// <param name="stw">Stream writer object to output message to stream buffer</param>
        /// <returns>Returned string from VBA Main Function</returns>
        public static string RunVBAscript(string processName, string officeAppName, string filePath, string macroName, object[] macroArgs, bool showAlerts = false, bool visibleApp = false, bool saveFile = false, StreamWriter stw = null)
        {
            object[] _macroNameArray = new object[] { macroName };
            object[] combinedMacroNameAndArgs = ObjectService.ConcatArrays(_macroNameArray, macroArgs);
            
            switch (officeAppName)
            {
                case ConstantsService.NAME_SW_OFFICE_EXCEL:
                    return _RunVBAonExcel(processName, officeAppName, filePath, combinedMacroNameAndArgs, showAlerts, visibleApp, saveFile, stw);
                case ConstantsService.NAME_SW_OFFICE_WORD:
                    return _RunVBAonWord(processName, officeAppName, filePath, combinedMacroNameAndArgs, showAlerts, visibleApp, saveFile, stw);
                case ConstantsService.NAME_SW_OFFICE_POWERPOINT:
                    return _RunVBAonPowerpoint(processName, officeAppName, filePath, combinedMacroNameAndArgs, showAlerts, visibleApp, saveFile, stw);
                case ConstantsService.NAME_SW_OFFICE_OUTLOOK:
                    return _RunVBAonOutlook(processName, officeAppName, filePath, macroName, showAlerts, visibleApp, saveFile, stw);
                default:
                    return "ERROR | Office application " + officeAppName + " not specified or accepted!";
            }
        }

        /// <summary>
        /// Call VBA script from an Excel application.
        /// </summary>
        /// <param name="processName">Name of the process being executed</param>
        /// <param name="officeAppName">One of the office app names</param>
        /// <param name="filePath">File path with the VBA script to be executed</param>
        /// <param name="combinedMacroNameAndArgs">Array of object with VBA script name with module/procedure name and args</param>
        /// <param name="showAlerts">Boolean status to show alerts on open application</param>
        /// <param name="visibleApp">Boolean status to show open application</param>
        /// <param name="saveFile">Boolean status to save or not the file</param>
        /// <param name="stw">Stream writer object to output message to stream buffer</param>
        /// <returns>Returned string from VBA Main Function</returns>
        private static string _RunVBAonExcel(string processName, string officeAppName, string filePath, object[] combinedMacroNameAndArgs, bool showAlerts = false, bool visibleApp = false, bool saveFile = false, StreamWriter stw = null)
        {
            Excel.Application _xlApp = new Excel.Application
            {
                DisplayAlerts = showAlerts,
                Visible = visibleApp
            };

            if (_xlApp == null)
            {
                throw new ApplicationException("Excel could not be started. Check if Office Excel is properly installed in your machine/server. If the error persists, contact XPress robot developers and show a printscreen of this log.");
            }
            else
            {
                string message = DateTime.Now + " [INFO] " + officeAppName + " opened successfully!";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                message = DateTime.Now + " [INFO] " + processName + " is running at " + officeAppName + "...";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                // Start Excel and open the workbook.
                Excel.Workbook _xlWorkBook = _xlApp.Workbooks.Open(filePath);

                // Run the macros by supplying the necessary arguments
                // Docs: https://docs.microsoft.com/en-us/dotnet/api/microsoft.office.interop.excel._application.run?view=excel-pia
                // object _result = _xlApp.Run(macroName, false, false);
                object _result = _RunGenericVBAMacro(_xlApp, combinedMacroNameAndArgs);

                // Clean-up: Close the workbook
                _xlWorkBook.Close(saveFile);
                ObjectService.ReleaseObject(_xlWorkBook);

                // Clean-up: Close the excel application
                _xlApp.Quit();
                ObjectService.ReleaseObject(_xlApp);

                if (_result == null) _result = "ERROR | Something wrong in the " + officeAppName + " script happened. Contact XPress robot developers and show a printscreen of this log.";
                return _result.ToString();
            }
        }

        /// <summary>
        /// Call VBA script from a Word application.
        /// </summary>
        /// <param name="processName">Name of the process being executed</param>
        /// <param name="officeAppName">One of the office app names</param>
        /// <param name="filePath">File path with the VBA script to be executed</param>
        /// <param name="combinedMacroNameAndArgs">Array of object with VBA script name with module/procedure name and args</param>
        /// <param name="showAlerts">Boolean status to show alerts on open application</param>
        /// <param name="visibleApp">Boolean status to show open application</param>
        /// <param name="saveFile">Boolean status to save or not the file</param>
        /// <param name="stw">Stream writer object to output message to stream buffer</param>
        /// <returns>Returned string from VBA Main Function</returns>
        private static string _RunVBAonWord(string processName, string officeAppName, string filePath, object[] combinedMacroNameAndArgs, bool showAlerts = false, bool visibleApp = false, bool saveFile = false, StreamWriter stw = null)
        {
            Word.Application _wrdApp = new Word.Application
            {
                DisplayAlerts = showAlerts ? Word.WdAlertLevel.wdAlertsAll : Word.WdAlertLevel.wdAlertsNone,
                Visible = visibleApp
            };

            if (_wrdApp == null)
            {
                throw new ApplicationException("Word could not be started. Check if Office Word is properly installed in your machine/server. If the error persists, contact XPress robot developers and show a printscreen of this log.");
            }
            else
            {
                string message = DateTime.Now + " [INFO] " + officeAppName + " opened successfully!";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                message = DateTime.Now + " [INFO] " + processName + " is running at " + officeAppName + "...";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                // Start Word and Document
                Word.Document _wDocument = _wrdApp.Documents.Open(filePath);

                // Run the macros by supplying the necessary arguments
                // Docs: https://docs.microsoft.com/en-us/dotnet/api/microsoft.office.interop.word._application.run?view=word-pia
                //object _result = _wrdApp.Run(macroName, false, false);
                object _result = _RunGenericVBAMacro(_wrdApp, combinedMacroNameAndArgs);

                // Clean-up: Close the document
                _wDocument.Close(saveFile);
                ObjectService.ReleaseObject(_wDocument);

                // Clean-up: Close the word application
                _wrdApp.Quit();
                ObjectService.ReleaseObject(_wrdApp);

                if (_result == null) _result = "ERROR | Something wrong in the " + officeAppName + " script happened. Contact XPress robot developers and show a printscreen of this log.";
                return _result.ToString();
            }
        }

        /// <summary>
        /// Call VBA script from a Powerpoint application.
        /// </summary>
        /// <param name="processName">Name of the process being executed</param>
        /// <param name="officeAppName">One of the office app names</param>
        /// <param name="filePath">File path with the VBA script to be executed</param>
        /// <param name="combinedMacroNameAndArgs"> Array of object with VBA script name with module/procedure name and args</param>
        /// <param name="showAlerts">Boolean status to show alerts on open application</param>
        /// <param name="visibleApp">Boolean status to show open application</param>
        /// <param name="saveFile">Boolean status to save or not the file</param>
        /// <param name="stw">Stream writer object to output message to stream buffer</param>
        /// <returns>Returned string from VBA Main Function</returns>
        private static string _RunVBAonPowerpoint(string processName, string officeAppName, string filePath, object[] combinedMacroNameAndArgs, bool showAlerts = false, bool visibleApp = false, bool saveFile = false, StreamWriter stw = null)
        {
            Powerpoint.Application _pwpApp = new Powerpoint.Application
            {
                DisplayAlerts = showAlerts ? Powerpoint.PpAlertLevel.ppAlertsAll : Powerpoint.PpAlertLevel.ppAlertsNone,
                Visible = visibleApp ? MsoTriState.msoCTrue : MsoTriState.msoFalse
            };

            if (_pwpApp == null)
            {
                throw new ApplicationException("Powerpoint could not be started. Check if Office Powerpoint is properly installed in your machine/server. If the error persists, contact XPress robot developers and show a printscreen of this log.");
            }
            else
            {
                string message = DateTime.Now + " [INFO] " + officeAppName + " opened successfully!";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                message = DateTime.Now + " [INFO] " + processName + " is running at " + officeAppName + "...";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                // Start Powerpoint and Document
                Powerpoint.Presentation _pwpPres = _pwpApp.Presentations.Open(filePath);

                // Run the macros by supplying the necessary arguments
                // Docs: https://docs.microsoft.com/en-us/dotnet/api/microsoft.office.interop.word._application.run?view=word-pia
                object _result = _RunGenericVBAMacro(_pwpPres, combinedMacroNameAndArgs);

                // Clean-up: Close the document
                _pwpPres.Close();
                ObjectService.ReleaseObject(_pwpPres);

                // Clean-up: Close the word application
                _pwpApp.Quit();
                ObjectService.ReleaseObject(_pwpApp);

                if (_result == null) _result = "ERROR | Something wrong in the " + officeAppName + " script happened. Contact XPress robot developers and show a printscreen of this log.";
                return _result.ToString();
            }
        }

        /// <summary>
        /// Call VBA script from an Outlook application.
        /// </summary>
        /// <param name="processName">Name of the process being executed</param>
        /// <param name="officeAppName">One of the office app names</param>
        /// <param name="filePath">File path with the VBA script to be executed</param>
        /// <param name="macroName">VBA script name with module/procedure name</param>
        /// <param name="showAlerts">Boolean status to show alerts on open application</param>
        /// <param name="visibleApp">Boolean status to show open application</param>
        /// <param name="saveFile">Boolean status to save or not the file</param>
        /// <param name="stw">Stream writer object to output message to stream buffer</param>
        /// <returns>Returned string from VBA Main Function</returns>
        private static string _RunVBAonOutlook(string processName, string officeAppName, string filePath, string macroName, bool showAlerts = false, bool visibleApp = false, bool saveFile = false, StreamWriter stw = null)
        {
            Outlook.Application _otApp = new Outlook.Application();

            if (_otApp == null)
            {
                throw new ApplicationException("Outlook could not be started. Check if Office Outlook is properly installed in your machine/server. If the error persists, contact XPress robot developers and show a printscreen of this log.");
            }
            else
            {
                string message = DateTime.Now + " [INFO] " + officeAppName + " opened successfully!";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                message = DateTime.Now + " [INFO] " + processName + " is running at " + officeAppName + "...";
                if (stw != null) stw.WriteLine(message + "\n\r");
                Console.WriteLine(message);

                Outlook.MailItem _otSI;
                // Start outlook and mailItem
                if (filePath != null) _otSI = _otApp.Session.OpenSharedItem(filePath) as Outlook.MailItem;
                else _otSI = _otApp.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

                if (visibleApp) _otSI.Display();

                string _result = null;
                // Run the macros by supplying the necessary arguments
                // @Read: https://stackoverflow.com/questions/50878070/execute-vba-outlook-macro-from-powershell-or-c-sharp-console-app
                try
                {
                    // TODO: Test this workaround to run macros on outlook
                    Outlook.Explorer _otExp = _otApp.ActiveExplorer();
                    CommandBar _otCb = _otExp.CommandBars.Add(macroName + "Proxy", Temporary: true);
                    CommandBarControl _otBtn = _otCb.Controls.Add(MsoControlType.msoControlButton, 1);
                    _otBtn.OnAction = macroName;
                    _otBtn.Execute();
                    _otCb.Delete();
                    ObjectService.ReleaseObject(_otCb);
                    ObjectService.ReleaseObject(_otExp);
                    _result = " [INFO] " + officeAppName + " script executed. Check manually if it was executed successfully.";
                }
                catch (Exception)
                {
                    _result = "ERROR | Something wrong in the " + officeAppName + " script happened. Contact XPress robot developers and show a printscreen of this log.";
                }
                

                if (filePath != null)
                {
                    // Clean-up: Close the mail item
                    if (!saveFile) _otSI.Close(Outlook.OlInspectorClose.olDiscard);
                    else _otSI.Close(Outlook.OlInspectorClose.olSave);
                    ObjectService.ReleaseObject(_otSI);
                }

                // Clean-up: Close the excel application
                _otApp.Quit();
                ObjectService.ReleaseObject(_otApp);

                return _result;
            }
        }
    }
}
