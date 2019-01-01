/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LogUtilitySettings.cs
 *  Description  :  Settings of log utility.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/19/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.IO
{
    /// <summary>
    /// Settings of log utility.
    /// </summary>
    static class LogUtilitySettings
    {
        #region Field and Property
#if !UNITY_EDITOR
        /// <summary>
        /// Path of log file.
        /// </summary>
        static readonly string FilePath = Application.persistentDataPath + "/Log.txt";
#endif
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize log utility.
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
#if UNITY_EDITOR
            LogUtility.Logger = UnityDebugger.Instance;
#else
            FileLogger.Instance.FilePath = FilePath;
            LogUtility.Logger = FileLogger.Instance;
#endif
        }
        #endregion
    }
}