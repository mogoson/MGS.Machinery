/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CentrifugalVibrator.cs
 *  Description  :  Define CentrifugalVibrator component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/23/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Centrifugal vibrator.
    /// </summary>
    public class CentrifugalVibrator : Mechanism
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
        public Vector3 StartPosition { protected set; get; }

        /// <summary>
        /// Current rotate angle.
        /// </summary>
        protected float currentAngle;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode = DriveMode.Ignore)
        {
            currentAngle += velocity;
            var direction = Quaternion.AngleAxis(currentAngle, transform.forward) * transform.right;
            transform.localPosition = StartPosition + GetLocalDirection(direction) * amplitudeRadius;
            return true;
        }

        /// <summary>
        /// Get local direction from wold direction.
        /// </summary>
        /// <param name="direction">Wold direction.</param>
        /// <returns>Local direction.</returns>
        protected Vector3 GetLocalDirection(Vector3 direction)
        {
            if (transform.parent)
            {
                return transform.parent.InverseTransformVector(direction);
            }
            return direction;
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