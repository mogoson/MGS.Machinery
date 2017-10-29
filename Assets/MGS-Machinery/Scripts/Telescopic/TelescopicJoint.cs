/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  TelescopicJoint.cs
 *  Description  :  Define TelescopicJoint component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/24/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/TelescopicJoint")]
    public class TelescopicJoint : TelescopicJointMechanism
    {
        #region Protected Method
        /// <summary>
        /// Drive joint.
        /// </summary>
        /// <param name="mSpeed">Move speed.</param>
        protected virtual void DriveJoint(float mSpeed)
        {
            lockRecord = displacement;
            displacement += mSpeed * Time.deltaTime;
            displacement = Mathf.Clamp(displacement, 0, stroke);
            DriveJoint();

            if (CheckRockersLock())
            {
                displacement = lockRecord;
                DriveJoint();
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            DriveJoint(speed * speedControl);

            if (displacement <= 0)
                tState = TelescopicState.Shrink;
            else if (displacement >= stroke)
                tState = TelescopicState.Extend;
        }
        #endregion
    }
}