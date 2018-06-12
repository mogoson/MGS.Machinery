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
 *  Version      :  0.1.1
 *  Date         :  5/24/2018
 *  Description  :  Define mechanism interface.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.2
 *  Date         :  6/3/2018
 *  Description  :  Add interface and abstract class.
 *************************************************************************/

using System;
using UnityEngine;

namespace Mogoson.Machinery
{
    #region Enum
    /// <summary>
    /// Type of mechanism drive.
    /// </summary>
    public enum DriveType
    {
        /// <summary>
        /// Ignore drive type.
        /// </summary>
        Ignore = 0,

        /// <summary>
        /// Linear drive.
        /// </summary>
        Linear = 1,

        /// <summary>
        /// Angular drive.
        /// </summary>
        Angular = 2
    }
    #endregion

    #region Interface
    /// <summary>
    /// Mechanism interface.
    /// </summary>
    public interface IMechanism
    {
        #region Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        void Drive(float velocity, DriveType type);
        #endregion
    }

    /// <summary>
    /// Trigger mechanism.
    /// </summary>
    public interface ITriggerMechanism : IMechanism
    {
        #region Property
        /// <summary>
        /// Trigger is triggered?
        /// </summary>
        bool IsTriggered { get; }
        #endregion
    }
    #endregion

    #region Abstract Class
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
    #endregion

    #region Struct
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
    #endregion
}