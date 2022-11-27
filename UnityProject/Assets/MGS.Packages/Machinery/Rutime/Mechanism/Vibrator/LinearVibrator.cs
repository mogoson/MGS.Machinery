/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LinearVibrator.cs
 *  Description  :  Define LinearVibrator component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Linear vibrator.
    /// </summary>
    public class LinearVibrator : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Amplitude radius of vibrator.
        /// </summary>
        [Tooltip("Amplitude radius of vibrator.")]
        public float amplitudeRadius = 0.1f;

        /// <summary>
        /// Start loacal position.
        /// </summary>
        public Vector2 StartPosition { protected set; get; }

        /// <summary>
        /// Vibrate local axis.
        /// </summary>
        protected Vector2 LocalAxis
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

        /// <summary>
        /// Current offset base on start position.
        /// </summary>
        protected float currentOffset;

        /// <summary>
        /// Vibrate direction.
        /// </summary>
        protected int direction = 1;
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
            currentOffset += velocity * Mathf.Deg2Rad * direction * Time.deltaTime;
            if (currentOffset < -amplitudeRadius || currentOffset > amplitudeRadius)
            {
                direction *= -1;
                currentOffset = Mathf.Clamp(currentOffset, -amplitudeRadius, amplitudeRadius);
            }
            transform.localPosition = StartPosition + LocalAxis * currentOffset;
            return true;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize vibrator.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            StartPosition = transform.localPosition;
        }
        #endregion
    }
}