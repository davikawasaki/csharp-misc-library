using System;
using System.Linq;
using System.Reflection;

namespace CSharpMiscLibrary.Services
{
    /// <summary>
    /// Common service for object manipulation.
    /// </summary>
    public class ObjectService
    {
        /// <summary>
        /// Create instance from class name.
        /// </summary>
        /// <param name="className">Name of the class</param>
        /// <returns>Instantiated class.</returns>
        public static object CreateInstanceFromClassName(string className)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes()
                .First(t => t.Name == className);
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Release COM interop service object and then call garbage collector.
        /// </summary>
        /// <param name="obj">Instance to be released from memory</param>
        public static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Concat two arrays.
        /// Simple version based on: https://msdn.microsoft.com/en-us/library/vstudio/bb302894%28v=vs.100%29.aspx
        /// </summary>
        /// <param name="front">Array to be appended in the front.</param>
        /// <param name="back">Array to be appended in the back.</param>
        /// <returns>Combined object array.</returns>
        public static object[] ConcatArrays(object[] front, object[] back)
        {
            object[] _combinedArrays = new object[front.Length + back.Length];
            Array.Copy(front, _combinedArrays, front.Length);
            Array.Copy(back, 0, _combinedArrays, front.Length, back.Length);
            return _combinedArrays;
        }
    }
}
