/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Transmission.cs
 *  Description  :  Define Transmission component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Transmission for mechanisms.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Transmission")]
    public class Transmission : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanism drive by this transmission.
        /// </summary>
        public List<MechanismUnit> mechanismUnits = new List<MechanismUnit>();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive mechanisms.
        /// </summary>
        /// <param name="velocity">Drive velocity.</param>
        public override void Drive(float velocity)
        {
            foreach (var unit in mechanismUnits)
            {
                unit.mechanism.Drive(velocity * unit.coefficient);
            }
        }
        #endregion
    }
}