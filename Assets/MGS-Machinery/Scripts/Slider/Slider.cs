/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Slider.cs
 *  Description  :  Define Slider component.
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
    /// Slider joint move on the axis Z.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/Slider")]
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
        /// Move slider.
        /// </summary>
        /// <param name="velocity">Move velocity.</param>
        protected override void DriveSlider(float velocity)
        {
            triggerRecord = Displacement;
            Displacement += velocity * Time.deltaTime;
            Displacement = Mathf.Clamp(Displacement, stroke.min, stroke.max);
            MoveSlider(Displacement);

            if (CheckTriggers())
            {
                Displacement = triggerRecord;
                MoveSlider(Displacement);
            }
        }

        /// <summary>
        /// Move slider.
        /// </summary>
        /// <param name="displacement">Displacement of slider.</param>
        protected void MoveSlider(float displacement)
        {
            transform.localPosition = StartPosition + Aixs * displacement;
            DriveRockers();
        }
        #endregion
    }
}