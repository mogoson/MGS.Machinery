/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LimitCrank.cs
 *  Description  :  Define LimitCrank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Crank rotate around the axis Z in the limit range.
    /// </summary>
    public class LimitCrank : FreeCrank
    {
        #region Field and Property
        /// <summary>
        /// Range limit of angle.
        /// </summary>
        [Tooltip("Range limit of angle.")]
        public Range range = new Range(-45, 45);

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckAngleStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check current angle is stuck.
        /// </summary>
        /// <returns>Angle is stuck?</returns>
        protected bool CheckAngleStuck()
        {
            return Angle <= range.min || Angle >= range.max;
        }

        /// <summary>
        /// Rotate crank.
        /// </summary>
        /// <param name="angle">Current rotate angle of crank.</param>
        protected override void RotateCrank(float angle)
        {
            angle = Mathf.Clamp(Angle, range.min, range.max);
            base.RotateCrank(angle);
        }
        #endregion
    }
}