/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SynchroMechanism.cs
 *  Description  :  Define SynchroMechanism component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/SynchroMechanism")]
    public class SynchroMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanisms.
        /// </summary>
        public List<Mechanism> mechanisms = new List<Mechanism>();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanisms.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            foreach (var mechanism in mechanisms)
            {
                mechanism.DriveMechanism(speedControl);
            }
        }
        #endregion
    }
}