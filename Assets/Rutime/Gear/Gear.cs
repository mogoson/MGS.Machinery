/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Gear.cs
 *  Description  :  Define Gear component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Gear rotate around axis Z.
    /// </summary>
    public class Gear : Axle, IEngageMechanism
    {
        #region Field and Property
        /// <summary>
        /// Engage mechanisms.
        /// </summary>
        [Tooltip("Engage mechanisms.")]
        [SerializeField]
        protected List<Mechanism> engages;

        /// <summary>
        /// Radius of gear.
        /// </summary>
        [Tooltip("Radius of gear.")]
        public float radius = 0.5f;

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckEngageStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check one of the engages is stuck?
        /// </summary>
        /// <returns>Return true if one of the engages is stuck.</returns>
        protected bool CheckEngageStuck()
        {
            foreach (var engage in engages)
            {
                if (engage.IsStuck)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            var angular = velocity;
            var linear = velocity;

            if (mode == DriveMode.Linear)
            {
                angular = velocity / radius * Mathf.Rad2Deg;
            }
            else
            {
                linear = velocity * Mathf.Deg2Rad * radius;
            }

            transform.Rotate(Vector3.forward, angular, Space.Self);
            if (!DriveCoaxes(angular))
            {
                return false;
            }
            return DriveEngages(-linear);
        }

        /// <summary>
        /// Drive engage mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected bool DriveEngages(float velocity)
        {
            foreach (var engage in engages)
            {
                if (!engage.Drive(velocity, DriveMode.Linear))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Link engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void LinkEngage(IMechanism mechanism)
        {
            var engage = mechanism as Mechanism;
            if (engage == null || engages.Contains(engage))
            {
                return;
            }

            engages.Add(engage);
        }

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void BreakEngage(IMechanism mechanism)
        {
            engages.Remove(mechanism as Mechanism);
        }
        #endregion
    }
}