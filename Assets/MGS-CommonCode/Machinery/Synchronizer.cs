/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Synchronizer.cs
 *  Description  :  Define Synchronizer component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Synchronizer for mechanisms.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Synchronizer")]
    public class Synchronizer : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanisms drive by this synchronizer.
        /// </summary>
        public List<Mechanism> mechanisms = new List<Mechanism>();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive mechanisms.
        /// </summary>
        /// <param name="velocity">Drive velocity.</param>
        public override void Drive(float velocity)
        {
            foreach (var mechanism in mechanisms)
            {
                mechanism.Drive(velocity);
            }
        }
        #endregion
    }
}