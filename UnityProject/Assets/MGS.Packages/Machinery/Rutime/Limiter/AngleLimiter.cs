/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AngleLimiter.cs
 *  Description  :  Angle limiter.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/29/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Angle limiter.
    /// </summary>
    public class AngleLimiter : Limiter
    {
        #region Field And Property
        /// <summary>
        /// Angle range of limiter.
        /// </summary>
        [Tooltip("Angle range of limiter.")]
        public Range range = new Range(-45, 45);

        /// <summary>
        /// Space of angle.
        /// </summary>
        [Tooltip("Space of angle.")]
        public Space space = Space.Self;

        /// <summary>
        /// Axis to limit angle.
        /// </summary>
        [Tooltip("Axis to limit angle.")]
        public Axis axis = Axis.Z;

        /// <summary>
        /// Anchor to check angle.
        /// </summary>
        [Tooltip("Anchor to check angle.")]
        public Transform anchor;

        /// <summary>
        /// Limiter is triggered?
        /// </summary>
        public override bool IsTriggered
        {
            get
            {
                var currentAngle = CurrentAngle;
                return currentAngle <= range.min || currentAngle >= range.max;
            }
        }

        /// <summary>
        /// Current angle of axis on space.
        /// </summary>
        public float CurrentAngle
        {
            get
            {
                var angles = anchor.localEulerAngles;
                if (space == Space.World)
                {
                    angles = anchor.eulerAngles;
                }

                var angle = 0f;
                switch (axis)
                {
                    case Axis.X:
                        angle = angles.x;
                        break;

                    case Axis.Y:
                        angle = angles.y;
                        break;

                    case Axis.Z:
                        angle = angles.z;
                        break;
                }

                return angle;
            }
        }
        #endregion

        #region Proteced Method
        /// <summary>
        /// Reset component.
        /// </summary>
        protected virtual void Reset()
        {
            anchor = transform;
        }
        #endregion
    }
}