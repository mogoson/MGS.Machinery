/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  FreeCrank.cs
 *  Description  :  Define free crank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Crank free rotate around the axis Z.
    /// </summary>
    public class FreeCrank : CrankMechanism
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
            Angle += velocity * Time.deltaTime;
            RotateCrank(Angle);
            return DriveRockers(velocity, mode);
        }

        /// <summary>
        /// Rotate crank.
        /// </summary>
        /// <param name="angle">Current rotate angle of crank.</param>
        protected virtual void RotateCrank(float angle)
        {
            transform.localRotation = Quaternion.Euler(StartAngles + new Vector3(0, 0, angle));
        }
        #endregion
    }
}