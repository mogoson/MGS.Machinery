/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Motor.cs
 *  Description  :  Define motor component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Motor provide power to drive axle.
    /// </summary>
    public class Motor : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Max revolution velocity.
        /// </summary>
        [Tooltip("Max revolution velocity.")]
        [SerializeField]
        protected float maxRPM = 60;

        /// <summary>
        /// Threshold of lerp rpm.
        /// </summary>
        [Tooltip("Threshold of lerp rpm.")]
        public float threshold = 1;

        /// <summary>
        /// Damper of engine rpm.
        /// </summary>
        [Tooltip("Damper of engine rpm.")]
        public float damper = 0.5f;

        /// <summary>
        /// Axle drive by this motor.
        /// </summary>
        [Tooltip("Axle drive by this motor.")]
        public Mechanism axle;

        /// <summary>
        /// Max revolution velocity (r/min).
        /// </summary>
        public float MaxRPM
        {
            set
            {
                maxRPM = value;
                targetRPM = maxRPM;
            }
            get { return maxRPM; }
        }

        /// <summary>
        /// Target revolution velocity.
        /// </summary>
        protected float targetRPM = 0;

        /// <summary>
        /// Current revolution velocity.
        /// </summary>
        protected float currentRPM = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Update motor.
        /// </summary>
        protected virtual void Update()
        {
            if (Mathf.Abs(currentRPM - targetRPM) <= threshold)
            {
                currentRPM = targetRPM;
            }
            else
            {
                currentRPM = Mathf.Lerp(currentRPM, targetRPM, damper * Time.deltaTime);
            }

            if (currentRPM == 0)
            {
                enabled = false;
            }
            else
            {
                if (axle.IsStuck)
                {
                    Debug.LogWarning("The axle is stuck, drive is cancelled.");
                    return;
                }

                axle.Drive(currentRPM * 6, DriveMode.Angular);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Turn on motor.
        /// </summary>
        public virtual void TurnOn()
        {
            targetRPM = maxRPM;
            enabled = true;
        }

        /// <summary>
        /// Turn off motor.
        /// </summary>
        public virtual void TurnOff()
        {
            targetRPM = 0;
        }
        #endregion
    }
}