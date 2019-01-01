/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  FileLogger.cs
 *  Description  :  Loggger for log to local file.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/19/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.DesignPattern;
using System;
using System.IO;

namespace Mogoson.IO
{
    /// <summary>
    /// Loggger for log to local file.
    /// </summary>
    public sealed class FileLogger : Singleton<FileLogger>, ILogger
    {
        #region Field and Property
        /// <summary>
        /// Path of log file.
        /// </summary>
        public string FilePath { set; get; }
        #endregion

        #region Private Method
        /// <summary>
        /// Constructor.
        /// </summary>
        private FileLogger() { }

        /// <summary>
        /// Logs a formatted message to local file.
        /// </summary>
        /// <param name="tag">Tag of log message.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        private void LogToFile(string tag, string format, params object[] args)
        {
            var formatLog = string.Format("[{0}] - {1} - {2}\r\n", DateTime.Now, tag, string.Format(format, args));
            try
            {
                File.AppendAllText(FilePath, formatLog);
            }
#if DEBUG
            catch (Exception ex)
            {
                throw ex;
            }
#else
            catch { return; }
#endif
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Logs a formatted message to local file.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        public void Log(string format, params object[] args)
        {
            LogToFile("Log", format, args);
        }

        /// <summary>
        /// Logs a formatted error message to local file.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        public void LogError(string format, params object[] args)
        {
            LogToFile("Error", format, args);
        }

        /// <summary>
        /// Logs a formatted warning message to local file.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        public void LogWarning(string format, params object[] args)
        {
            LogToFile("Warning", format, args);
        }
        #endregion
    }
}