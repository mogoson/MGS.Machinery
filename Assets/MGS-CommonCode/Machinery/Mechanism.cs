/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/25/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Base mechanism.
    /// </summary>
    public abstract class Mechanism : MonoBehaviour, IMechanism
    {
        #region Protected Method
        protected virtual void Awake()
        {
            Initialize();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public abstract void Drive(float velocity, DriveType type);
        #endregion
    }

    /// <summary>
    /// Trigger for mechanism.
    /// </summary>
    public abstract class TriggerMechanism : Mechanism, ITriggerMechanism
    {
        #region Field and Property
        /// <summary>
        /// Trigger is triggered?
        /// </summary>
        public abstract bool IsTriggered { get; }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive trigger by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity = 0, DriveType type = DriveType.Ignore) { }
        #endregion
    }
}