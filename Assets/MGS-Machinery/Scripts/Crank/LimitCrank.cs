/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LimitCrank.cs
 *  Description  :  Define LimitCrank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/LimitCrank")]
    public class LimitCrank : FreeCrank
    {
        #region Field and Property
        /// <summary>
        /// Min angle limit of crank.
        /// </summary>
        public float minAngle = -45;

        /// <summary>
        /// Max angle limit of crank.
        /// </summary>
        public float maxAngle = 45;
        #endregion

        #region Protected Method
        /// <summary>
        /// Rotate crank in the range by speed.
        /// </summary>
        /// <param name="rSpeed">Rotate speed.</param>
        protected override void DriveCrank(float rSpeed)
        {
            lockRecord = Angle;
            Angle += rSpeed * Time.deltaTime;
            Angle = Mathf.Clamp(Angle, minAngle, maxAngle);
            DriveCrank();

            if (CheckRockersLock())
            {
                Angle = lockRecord;
                DriveCrank();
            }
        }
        #endregion
    }
}