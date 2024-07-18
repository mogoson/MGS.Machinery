﻿/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SliderArm.cs
 *  Description  :  Define SliderArm component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machineries
{
    /// <summary>
    /// Arm with sequence slider joints.
    /// </summary>
    public class SliderArm : SliderArmMechanism
    {
        #region Field and Property
        /// <summary>
        /// Current index of drive slider.
        /// </summary>
        protected int sliderIndex = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Check all the sliders are stuck.
        /// </summary>
        /// <returns>All the sliders are stuck?</returns>
        protected override bool CheckSlidersStuck()
        {
            foreach (var slider in sliders)
            {
                if (!slider.IsStuck)
                {
                    return false;
                }
                else
                {
                    var current = sliders[sliderIndex];
                    if (slider != current && slider.State != current.State)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            var current = sliders[sliderIndex];
            if (!current.Drive(velocity, mode))
            {
                return false;
            }

            if (velocity >= 0)
            {
                if (current.State == TelescopicState.Maximum)
                {
                    sliderIndex++;
                }
            }
            else
            {
                if (current.State == TelescopicState.Minimum)
                {
                    sliderIndex--;
                }
            }

            sliderIndex = ClampSliderIndex(sliderIndex);
            return true;
        }
        #endregion
    }
}