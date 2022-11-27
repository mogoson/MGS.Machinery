/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DriveMode.cs
 *  Description  :  Mode of mechanism drive.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machinery
{
    /// <summary>
    /// Mode of mechanism drive.
    /// </summary>
    public enum DriveMode
    {
        /// <summary>
        /// Ignore drive mode.
        /// </summary>
        Ignore = 0,

        /// <summary>
        /// Linear drive mode.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// Angular drive mode.
        /// </summary>
        Angular = 2
    }
}