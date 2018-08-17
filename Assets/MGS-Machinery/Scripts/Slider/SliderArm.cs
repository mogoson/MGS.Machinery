/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SliderArm.cs
 *  Description  :  Define SliderArm component.
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
    /// Arm with sequence slider joints.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/SliderArm")]
    public class SliderArm : SliderArmMechanism
    {
        #region Field and Property
        /// <summary>
        /// Current index of drive slider.
        /// </summary>
        protected int sliderIndex = 0;
        #endregion

        #region Public Method
        /// <summary>
        /// Drive arm by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity, DriveType type = DriveType.Ignore)
        {
            var currentJoint = sliders[sliderIndex];
            currentJoint.Drive(velocity, DriveType.Ignore);

            if (velocity >= 0)
            {
                if (currentJoint.State == TelescopicState.Maximum)
                    sliderIndex++;
            }
            else
            {
                if (currentJoint.State == TelescopicState.Minimum)
                    sliderIndex--;
            }

            sliderIndex = ClampIndex(sliderIndex);
        }
        #endregion
    }
}