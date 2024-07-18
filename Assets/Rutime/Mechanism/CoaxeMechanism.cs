/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CoaxeMechanism.cs
 *  Description  :  Define EngageMechanism component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  12/09/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Coaxe mechanism.
    /// </summary>
    public abstract class CoaxeMechanism : Mechanism, ICoaxeMechanism
    {
        /// <summary>
        /// Coaxe mechanisms.
        /// </summary>
        [Tooltip("Coaxe mechanisms.")]
        [SerializeField]
        protected List<Mechanism> coaxes;

        /// <summary>
        /// Link coaxe.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void LinkCoaxe(IMechanism mechanism)
        {
            var coaxe = mechanism as Mechanism;
            if (coaxe == null || coaxes.Contains(coaxe))
            {
                return;
            }

            coaxes.Add(coaxe);
        }

        /// <summary>
        /// Break coaxe.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void BreakCoaxe(IMechanism mechanism)
        {
            coaxes.Remove(mechanism as Mechanism);
        }

        /// <summary>
        /// Drive coaxial mechanisms by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected bool DriveCoaxes(float velocity)
        {
            foreach (var coaxe in coaxes)
            {
                if (!coaxe.Drive(velocity, DriveMode.Angular))
                {
                    return false;
                }
            }
            return true;
        }
    }
}