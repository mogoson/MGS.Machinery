/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Slider.cs
 *  Description  :  Define Stepper component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  12/08/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Stepper motor for mechanism.
    /// </summary>
    public abstract class Stepper : MonoBehaviour
    {
        /// <summary>
        /// Mechanism drive by stepper.
        /// </summary>
        protected abstract Mechanism Mechanism { get; }

        /// <summary>
        /// Current mileage.
        /// </summary>
        protected abstract float Current { get; }

        /// <summary>
        /// Target mileage.
        /// </summary>
        protected float mileage;

        /// <summary>
        /// Velocity to change current mileage.
        /// </summary>
        protected float velocity;

        /// <summary>
        /// Drive mechanism to mileage at velocity.
        /// </summary>
        /// <param name="mileage"></param>
        /// <param name="velocity"></param>
        public virtual void Drive(float mileage, float velocity)
        {
            this.mileage = mileage;
            this.velocity = velocity;
            enabled = true;
        }

        /// <summary>
        /// Update stepper.
        /// </summary>
        protected virtual void Update()
        {
            if (Current == mileage || Mechanism.IsStuck)
            {
                enabled = false;
                return;
            }

            var stepping = Mathf.MoveTowards(Current, mileage, velocity * Time.deltaTime);
            var delta = stepping - Current;
            Mechanism.Drive(delta, DriveMode.Ignore);
        }
    }
}