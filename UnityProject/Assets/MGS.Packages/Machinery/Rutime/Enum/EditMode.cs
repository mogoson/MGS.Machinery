/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DriveType.cs
 *  Description  :  Edit mode of Hinge Editor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machinery
{
    /// <summary>
    /// Edit mode of Hinge Editor.
    /// </summary>
    public enum EditMode
    {
        /// <summary>
        /// Free mode.
        /// </summary>
        Free = 0,

        /// <summary>
        /// Hinge mode.
        /// </summary>
        Hinge = 1,

        /// <summary>
        /// Lock mode.
        /// </summary>
        Lock = 2
    }
}