using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpMiscLibrary.Services
{
    /// <summary>
    /// I/O service methods.
    /// </summary>
    public class IOService
    {
        /// <summary>
        /// Copy single file to a list of directories if they all exist.
        /// </summary>
        /// <param name="fileOriginPath">Origin file path</param>
        /// <param name="fileName">Origin file name</param>
        /// <param name="filesDestinationPath">List of destination files' path</param>
        /// <returns>Status of the copy (possible or not)</returns>
        private static bool _copyFileToLocationList(string fileOriginPath, string fileName, List<string> filesDestinationPath)
        {
            if (!File.Exists(fileOriginPath + fileName))
            {
                throw new ApplicationException("File doesn't exist. Make sure that the following file exists in its full path: " + fileOriginPath + fileName);
            }
            else
            {
                foreach (var targetPath in filesDestinationPath)
                {
                    if (!Directory.Exists(targetPath))
                    {
                        throw new ApplicationException("Destination folder doesn't exist. Make sure that the following folder exists in its full path: " + targetPath);
                    }
                    else
                    {
                        File.Copy(fileOriginPath + fileName, targetPath + fileName);
                    }
                }
            }
            return true;
        }
    }
}
