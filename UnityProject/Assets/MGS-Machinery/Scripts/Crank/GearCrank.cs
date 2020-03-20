/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GearCrank.cs
 *  Description  :  Define GearCrank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/12/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;
using MGS.Machinery;

namespace MGS.Machinery
{
    /// <summary>
    /// Crank free rotate around the axis Z.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/GearCrank")]
    public class GearCrank : FreeCrank
    {
        #region Field and Property
        /// <summary>
        /// Radius of gear.
        /// </summary>
        public float radius = 0.5f;
        #endregion

        #region Protected Method
        /// <summary>
        /// Rotate crank by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        protected override void DriveCrank(float velocity, DriveType type = DriveType.Ignore)
        {
            triggerRecord = Angle;
            if (type == DriveType.Linear)
            {
                velocity *= Mathf.Rad2Deg / radius;
            }
            Angle += velocity * Time.deltaTime;
            RotateCrank(Angle);

            if (CheckTriggers())
            {
                Angle = triggerRecord;
                RotateCrank(Angle);
            }
        }
        #endregion
    }
}