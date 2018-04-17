/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TelescopicJoint.cs
 *  Description  :  Define TelescopicJoint component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/TelescopicJoint")]
    public class TelescopicJoint : TelescopicJointMechanism
    {
        #region Protected Method
        /// <summary>
        /// Move joint.
        /// </summary>
        /// <param name="moveSpeed">Move speed.</param>
        protected virtual void DriveJoint(float moveSpeed)
        {
            lockRecord = Displacement;
            Displacement += moveSpeed * Time.deltaTime;
            Displacement = Mathf.Clamp(Displacement, 0, stroke);
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

            if (Displacement <= 0)
                TState = TelescopicState.Shrink;
            else if (Displacement >= stroke)
                TState = TelescopicState.Extend;
        }
        #endregion
    }
}