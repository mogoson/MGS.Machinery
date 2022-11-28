/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Slider.cs
 *  Description  :  Define Slider component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Slider joint move on the axis Z.
    /// </summary>
    public class Slider : SliderMechanism
    {
        #region Field and Property
        /// <summary>
        /// Local axis of move.
        /// </summary>
        protected Vector3 Aixs
        {
            get
            {
                var axis = transform.forward;
                if (transform.parent)
                {
                    axis = transform.parent.InverseTransformDirection(axis);
                }
                return axis;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            Displacement += velocity * Time.deltaTime;
            Displacement = Mathf.Clamp(Displacement, stroke.min, stroke.max);

            MoveSlider(Displacement);
            return DriveRockers(velocity, mode);
        }

        /// <summary>
        /// Move slider.
        /// </summary>
        /// <param name="displacement">Displacement of slider.</param>
        protected void MoveSlider(float displacement)
        {
            transform.localPosition = StartPosition + Aixs * displacement;
        }
        #endregion
    }
}