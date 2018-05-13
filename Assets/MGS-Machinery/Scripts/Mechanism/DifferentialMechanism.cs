/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DifferentialMechanism.cs
 *  Description  :  Define DifferentialMechanism component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/13/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Differential mechanism group.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/DifferentialMechanism")]
    public class DifferentialMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanism units to drive.
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