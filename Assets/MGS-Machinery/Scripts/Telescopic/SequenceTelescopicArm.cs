/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SequenceTelescopicArm.cs
 *  Description  :  Define SequenceTelescopicArm component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/SequenceTelescopicArm")]
    public class SequenceTelescopicArm : TelescopicArmMechanism
    {
        #region Field and Property
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
        /// Drive arm.
        /// </summary>
        /// <param name="speedRatio">Speed ratio.</param>
        public override void Drive(float speedRatio)
        {
            ClampIndex();
            var currentJoint = tJoints[JointIndex];
            currentJoint.Drive(speedRatio);

            if (currentJoint.speed * speedRatio >= 0)
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