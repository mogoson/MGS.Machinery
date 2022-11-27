/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SliderArmMechanism.cs
 *  Description  :  Arm with slider joints.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Arm with slider joints.
    /// </summary>
	public abstract class SliderArmMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Slider joints of arm.
        /// </summary>
        [Tooltip("Slider joints of arm.")]
        public List<SliderMechanism> sliders = new List<SliderMechanism>();

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckSlidersStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check all the sliders are stuck.
        /// </summary>
        /// <returns>All the sliders are stuck?</returns>
        protected bool CheckSlidersStuck()
        {
            foreach (var slider in sliders)
            {
                if (!slider.IsStuck)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Clamp slider index in the range.
        /// </summary>
        /// <param name="index">Index of slider.</param>
        /// <returns>Correct index of slider.</returns>
        protected int ClampSliderIndex(int index)
        {
            return Mathf.Clamp(index, 0, sliders.Count - 1);
        }
        #endregion
    }
}