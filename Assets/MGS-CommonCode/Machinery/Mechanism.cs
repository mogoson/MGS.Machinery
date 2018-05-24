/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  5/24/2018
 *  Description  :  Define mechanism interface.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Mechanism interface.
    /// </summary>
    public interface IMechanism
    {
        /// <summary>
        /// Drive mechanism.
        /// </summary>
        /// <param name="velocity">Drive velocity.</param>
        void Drive(float velocity);
    }

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
        /// Drive mechanism.
        /// </summary>
        /// <param name="velocity">Drive velocity.</param>
        public abstract void Drive(float velocity);
        #endregion
    }

    /// <summary>
    /// Mechanism unit.
    /// </summary>
    [Serializable]
    public struct MechanismUnit
    {
        #region Field and Property
        /// <summary>
        /// Mechanism to drive.
        /// </summary>
        public Mechanism mechanism;

        /// <summary>
        /// Coefficient of velocity.
        /// </summary>
        public float coefficient;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mechanism">Mechanism to drive.</param>
        /// <param name="coefficient">Coefficient of velocity.</param>
        public MechanismUnit(Mechanism mechanism, float coefficient)
        {
            this.mechanism = mechanism;
            this.coefficient = coefficient;
        }
        #endregion
    }
}