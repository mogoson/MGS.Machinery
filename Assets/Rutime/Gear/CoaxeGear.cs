/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CoaxeGear.cs
 *  Description  :  Define CoaxeGear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/12/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Coaxe gear with the same axis as another gear.
    /// </summary>
    public class CoaxeGear : Gear
    {
        #region Protected Method
        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            var angular = velocity;
            var linear = velocity;

            if (mode == DriveMode.Linear)
            {
                angular = velocity / radius * Mathf.Rad2Deg;
            }
            else
            {
                linear = velocity * Mathf.Deg2Rad * radius;
            }

            if (!DriveCoaxes(angular))
            {
                return false;
            }

            return DriveEngages(-linear);
        }
        #endregion
    }
}