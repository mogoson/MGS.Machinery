/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GearCrank.cs
 *  Description  :  Define GearCrank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/12/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Crank free rotate around the axis Z.
    /// </summary>
    public class GearCrank : FreeCrank
    {
        #region Field and Property
        /// <summary>
        /// Radius of gear.
        /// </summary>
        [Tooltip("Radius of gear.")]
        public float radius = 0.5f;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is unrestricted?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            if (mode == DriveMode.Linear)
            {
                velocity *= Mathf.Rad2Deg / radius;
            }

            Angle += velocity * Time.deltaTime;
            RotateCrank(Angle);
            return DriveRockers(velocity, mode);
        }
        #endregion
    }
}