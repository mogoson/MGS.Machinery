/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Belt.cs
 *  Description  :  Define Belt component.
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
    /// Belt with UV animation.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class Belt : EngageMechanism
    {
        #region Field and Property
        /// <summary>
        /// Coefficient of velocity.
        /// </summary>
        [Tooltip("Coefficient of velocity.")]
        public float coefficient = 1;

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

        /// <summary>
        /// Renderer of belt.
        /// </summary>
        protected Renderer beltRenderer;
        #endregion

        #region Protected Method
        /// <summary>
        /// Check one of the engages is stuck?
        /// </summary>
        /// <returns>One of the engages is stuck?</returns>
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
        protected override bool OnDrive(float velocity, DriveMode mode = DriveMode.Ignore)
        {
            beltRenderer.material.mainTextureOffset += new Vector2(velocity * coefficient, 0);
            return DriveEngages(-velocity);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize belt.
        /// </summary>
        public override void Initialize()
        {
            beltRenderer = GetComponent<Renderer>();
            IsInitialized = true;
        }
        #endregion
    }
}