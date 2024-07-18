/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  WormShaft.cs
 *  Description  :  Define WormShaft component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/18/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Worm shaft.
    /// </summary>
    public class WormShaft : Axle
    {
        #region Field and Property
        /// <summary>
        /// Gears drived by this worm.
        /// </summary>
        [Tooltip("Gears drived by this worm.")]
        public List<WormGear> gears;

        /// <summary>
        /// Count of worm threads.
        /// </summary>
        [Tooltip("Count of worm threads.")]
        public int threads = 1;

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckGearStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check one of the gears is stuck?
        /// </summary>
        /// <returns>Return true if one of the gears is stuck.</returns>
        protected bool CheckGearStuck()
        {
            foreach (var gear in gears)
            {
                if (gear.IsStuck)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            if (!base.OnDrive(velocity, mode))
            {
                return false;
            }

            return DriveGears(velocity);
        }

        /// <summary>
        /// Drive worm gears by angular velocity.
        /// </summary>
        /// <param name="velocity">Angular velocity of drive.</param>
        /// <returns>Drive gears is effective?</returns>
        protected bool DriveGears(float velocity)
        {
            foreach (var gear in gears)
            {
                if (!gear.Drive(velocity * threads / gear.teeth, DriveMode.Angular))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}