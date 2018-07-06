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
    /// <summary>
    /// Arm with sequence telescopic joints.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/SequenceTelescopicArm")]
    public class SequenceTelescopicArm : TelescopicArmMechanism
    {
        #region Field and Property
        /// <summary>
        /// Current index of drive joint.
        /// </summary>
        protected int jointIndex = 0;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive arm by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity, DriveType type = DriveType.Ignore)
        {
            var currentJoint = joints[jointIndex];
            currentJoint.Drive(velocity, DriveType.Ignore);

            if (velocity >= 0)
            {
                if (currentJoint.State == TelescopicState.Maximum)
                    jointIndex++;
            }
            else
            {
                if (currentJoint.State == TelescopicState.Minimum)
                    jointIndex--;
            }

            jointIndex = ClampIndex(jointIndex);
        }
        #endregion
    }
}