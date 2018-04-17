/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CenterTelescopicJoint.cs
 *  Description  :  Define CenterTelescopicJoint component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/CenterTelescopicJoint")]
    public class CenterTelescopicJoint : TelescopicJoint
    {
        #region Protected Method
        /// <summary>
        /// Move joint.
        /// </summary>
        /// <param name="moveSpeed">Move speed.</param>
        protected override void DriveJoint(float moveSpeed)
        {
            lockRecord = Displacement;
            Displacement += moveSpeed * Time.deltaTime;
            Displacement = Mathf.Clamp(Displacement, -stroke, stroke);
            DriveJoint();

            if (CheckRockersLock())
            {
                Displacement = lockRecord;
                DriveJoint();
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive joint.
        /// </summary>
        /// <param name="speedRatio">Speed ratio.</param>
        public override void Drive(float speedRatio)
        {
            DriveJoint(speed * speedRatio);

            if (Displacement == 0)
                TState = TelescopicState.Shrink;
            else if (Displacement <= -stroke || Displacement >= stroke)
                TState = TelescopicState.Extend;
        }
        #endregion
    }
}