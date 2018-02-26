/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SeTelescopicArm.cs
 *  Description  :  Define SeTelescopicArm component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/SeTelescopicArm")]
    public class SeTelescopicArm : TelescopicArmMechanism
    {
        #region Property and Field
        /// <summary>
        /// Current id of drive joint.
        /// </summary>
        public int JointIndex { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Clamp joint index in the range.
        /// </summary>
        protected virtual void ClampIndex()
        {
            JointIndex = Mathf.Clamp(JointIndex, 0, tJoints.Count - 1);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            ClampIndex();
            var currentJoint = tJoints[JointIndex];
            currentJoint.DriveMechanism(speedControl);

            if (currentJoint.speed * speedControl >= 0)
            {
                if (currentJoint.TState == TelescopicState.Extend)
                    JointIndex++;
            }
            else
            {
                if (currentJoint.TState == TelescopicState.Shrink)
                    JointIndex--;
            }
        }
        #endregion
    }
}