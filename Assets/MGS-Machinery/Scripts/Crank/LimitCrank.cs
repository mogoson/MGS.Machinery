/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  LimitCrank.cs
 *  Description  :  Define LimitCrank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/24/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/LimitCrank")]
    public class LimitCrank : FreeCrank
    {
        #region Property and Field
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
            lockRecord = angle;
            angle += rSpeed * Time.deltaTime;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            DriveCrank();

            if (CheckRockersLock())
            {
                angle = lockRecord;
                DriveCrank();
            }
        }
        #endregion
    }
}