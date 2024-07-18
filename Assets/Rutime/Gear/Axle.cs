/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Axle.cs
 *  Description  :  Define Axle component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/5/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Axle rotate around axis Z.
    /// </summary>
    public class Axle : CoaxeMechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckCoaxeStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check one of the coaxes is stuck?
        /// </summary>
        /// <returns>Return true if one of the coaxes is stuck.</returns>
        protected bool CheckCoaxeStuck()
        {
            foreach (var coaxe in coaxes)
            {
                if (coaxe.IsStuck)
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
        protected override bool OnDrive(float velocity, DriveMode mode = DriveMode.Ignore)
        {
            transform.Rotate(Vector3.forward, velocity, Space.Self);
            return DriveCoaxes(velocity);
        }
        #endregion
    }
}