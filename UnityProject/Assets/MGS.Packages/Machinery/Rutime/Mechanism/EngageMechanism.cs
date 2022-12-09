/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ChainBelt.cs
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
    /// Engage mechanism.
    /// </summary>
    public abstract class EngageMechanism : Mechanism, IEngageMechanism
    { /// <summary>
      /// Engage mechanisms.
      /// </summary>
        [Tooltip("Engage mechanisms.")]
        [SerializeField]
        protected List<Mechanism> engages;

        /// <summary>
        /// Link engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void LinkEngage(IMechanism mechanism)
        {
            var engage = mechanism as Mechanism;
            if (engage == null || engages.Contains(engage))
            {
                return;
            }

            engages.Add(engage);
        }

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void BreakEngage(IMechanism mechanism)
        {
            engages.Remove(mechanism as Mechanism);
        }

        /// <summary>
        /// Drive engage mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected bool DriveEngages(float velocity)
        {
            foreach (var engage in engages)
            {
                if (!engage.Drive(velocity, DriveMode.Linear))
                {
                    return false;
                }
            }
            return true;
        }
    }
}