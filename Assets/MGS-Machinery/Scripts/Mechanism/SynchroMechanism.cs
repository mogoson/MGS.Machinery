/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SynchroMechanism.cs
 *  Description  :  Define SynchroMechanism component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
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
        /// Drive mechanism.
        /// </summary>
        /// <param name="speedRatio">Speed ratio.</param>
        public override void Drive(float speedRatio)
        {
            foreach (var mechanism in mechanisms)
            {
                mechanism.Drive(speedRatio);
            }
        }
        #endregion
    }
}