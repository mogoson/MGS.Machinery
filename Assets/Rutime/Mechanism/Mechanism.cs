/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/25/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Base mechanism.
    /// </summary>
    public abstract class Mechanism : MonoBehaviour, IMechanism
    {
        #region Field And Property
        /// <summary>
        /// Mechanism is initialized?
        /// </summary>
        public virtual bool IsInitialized { protected set; get; }

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public virtual bool IsStuck
        {
            get
            {
                if (!isDriveEffective)
                {
                    return true;
                }

                return CheckLimiterTriggered();
            }
        }

        /// <summary>
        /// Limiters attached on mechanism.
        /// </summary>
        protected List<ILimiter> limiters = new List<ILimiter>();

        /// <summary>
        /// The last velocity drive succeed.
        /// </summary>
        protected float lastVelocity = 0;

        /// <summary>
        /// The last drive is effective?
        /// </summary>
        protected bool isDriveEffective = true;
        #endregion

        #region Protected Method
        /// <summary>
        /// Awake mechanism.
        /// </summary>
        protected virtual void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Check if one of limiters is triggered?
        /// </summary>
        /// <returns></returns>
        protected bool CheckLimiterTriggered()
        {
            foreach (var limiter in limiters)
            {
                if (limiter.IsTriggered)
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
        protected abstract bool OnDrive(float velocity, DriveMode mode);
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public virtual void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            limiters.AddRange(GetComponents<ILimiter>());
            IsInitialized = true;
        }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        public virtual bool Drive(float velocity, DriveMode mode)
        {
            if (Application.isPlaying)
            {
                if (IsStuck)
                {
                    if (velocity * lastVelocity > 0)
                    {
                        return false;
                    }
                }
                else
                {
                    lastVelocity = velocity;
                }
            }

            isDriveEffective = OnDrive(velocity, mode);
            return isDriveEffective;
        }
        #endregion
    }
}