/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ILogger.cs
 *  Description  :  Interface of logger.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/5/2015
 *  Description  :  Initial development version.
 *************************************************************************/

namespace Mogoson.IO
{
    /// <summary>
    /// Interface of logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a formatted message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        void Log(string format, params object[] args);

        /// <summary>
        /// Logs a formatted error message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        void LogError(string format, params object[] args);

        /// <summary>
        /// Logs a formatted warning message.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">Format arguments.</param>
        void LogWarning(string format, params object[] args);
    }
}