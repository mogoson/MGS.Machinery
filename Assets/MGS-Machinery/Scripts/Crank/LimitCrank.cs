/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LimitCrank.cs
 *  Description  :  Define LimitCrank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Crank rotate around the axis Z in the limit range.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/LimitCrank")]
    public class LimitCrank : FreeCrank
    {
        #region Field and Property
        /// <summary>
        /// Range limit of angle.
        /// </summary>
        public Range range = new Range(-45, 45);
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
            Angle += velocity * Time.deltaTime;
            Angle = Mathf.Clamp(Angle, range.min, range.max);
            DriveCrank();

            if (CheckTriggers())
            {
                Angle = triggerRecord;
                DriveCrank();
            }
        }
        #endregion
    }
}