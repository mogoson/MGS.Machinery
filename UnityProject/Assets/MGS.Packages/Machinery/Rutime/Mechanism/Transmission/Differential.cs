/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Differential.cs
 *  Description  :  Define Differential component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/1/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Ordinary differential.
    /// </summary>
    public class Differential : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Left gear of differential.
        /// </summary>
        [Tooltip("Left gear of differential.")]
        public Gear leftGear;

        /// <summary>
        /// Right gear of differential.
        /// </summary>
        [Tooltip("Right gear of differential.")]
        public Gear rightGear;

        /// <summary>
        /// Left axle of differential.
        /// </summary>
        [Tooltip("Left axle of differential.")]
        public Axle leftAxle;

        /// <summary>
        /// Right axle of differential.
        /// </summary>
        [Tooltip("Right axle of differential.")]
        public Axle rightAxle;

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (leftGear.IsStuck || rightGear.IsStuck ||
                    leftAxle.IsStuck || rightAxle.IsStuck)
                {
                    return true;
                }
                return base.IsStuck;
            }
        }

        /// <summary>
        /// Offset coefficient of differential.
        /// </summary>
        public float Coefficient
        {
            set
            {
                coefficient = value;
                leftCoefficient = 1 - coefficient;
                rightCoefficient = 1 + coefficient;
            }
            get { return coefficient; }
        }

        /// <summary>
        /// Offset coefficient of differential.
        /// </summary>
        protected float coefficient = 0;

        /// <summary>
        /// Offset coefficient of left axle.
        /// </summary>
        protected float leftCoefficient = 1;

        /// <summary>
        /// Offset coefficient of right axle.
        /// </summary>
        protected float rightCoefficient = 1;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is unrestricted?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode = DriveMode.Ignore)
        {
            mode = DriveMode.Angular;

            if (!leftGear.Drive(velocity * coefficient, mode))
            {
                return false;
            }

            if (!rightGear.Drive(velocity * coefficient, mode))
            {
                return false;
            }

            if (!leftAxle.Drive(-velocity * leftCoefficient, mode))
            {
                return false;
            }

            return rightAxle.Drive(velocity * rightCoefficient, mode);
        }
        #endregion
    }
}