/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  WormGear.cs
 *  Description  :  Define WormGear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/18/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Worm gear.
    /// </summary>
    public class WormGear : Gear
    {
        #region Field and Property
        /// <summary>
        /// Count of gear teeth.
        /// </summary>
        [Tooltip("Count of gear teeth.")]
        public int teeth = 36;
        #endregion
    }
}