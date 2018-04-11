/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TelescopicJoint.cs
 *  Description  :  Define TelescopicJoint component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
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
        /// Drive joint.
        /// </summary>
        /// <param name="mSpeed">Move speed.</param>
        protected virtual void DriveJoint(float mSpeed)
        {
            lockRecord = Displacement;
            Displacement += mSpeed * Time.deltaTime;
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
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            DriveJoint(speed * speedControl);

            if (Displacement <= 0)
                TState = TelescopicState.Shrink;
            else if (Displacement >= stroke)
                TState = TelescopicState.Extend;
        }
        #endregion
    }
}