/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMechanism.cs
 *  Description  :  Define interface for mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  5/24/2018
 *  Description  :  Modify mechanism interface.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.2
 *  Date         :  6/3/2018
 *  Description  :  Add interface for trigger mechanism.
 *************************************************************************/

namespace Mogoson.Machinery
{
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

    /// <summary>
    /// Mechanism with engaged mechanisms.
    /// </summary>
    public interface IEngageMechanism : IMechanism
    {
        /// <summary>
        /// Build engage for mechanism.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        void BuildEngage(IEngagedMechanism engage);

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="engage">Engage mechanism.</param>
        void BreakEngage(IEngagedMechanism engage);
    }

    /// <summary>
    /// Mechanism can be engaged to power mechanism.
    /// </summary>
    public interface IEngagedMechanism : IMechanism
    {
        /// <summary>
        /// Engage this mechanism to power mechanism.
        /// </summary>
        /// <param name="engage">Power mechanism.</param>
        void EngageTo(IEngageMechanism engage);

        /// <summary>
        /// Break engage from power mechanism.
        /// </summary>
        void EngageBreak();
    }

    /// <summary>
    /// Mechanism with coaxed mechanisms.
    /// </summary>
    public interface ICoaxeMechanism : IMechanism
    {
        /// <summary>
        /// Build coaxe for mechanism.
        /// </summary>
        /// <param name="coaxe">Coaxe mechanism.</param>
        void BuildCoaxed(ICoaxedMechanism coaxe);

        /// <summary>
        /// Break coaxed.
        /// </summary>
        /// <param name="coaxe">Coaxe mechanism.</param>
        void BreakCoaxed(ICoaxedMechanism coaxe);
    }

    /// <summary>
    /// Mechanism can be coaxed to power mechanism.
    /// </summary>
    public interface ICoaxedMechanism : IMechanism
    {
        /// <summary>
        /// Coaxe this mechanism to power mechanism.
        /// </summary>
        /// <param name="coaxe">Power mechanism.</param>
        void CoaxeTo(ICoaxeMechanism coaxe);

        /// <summary>
        /// Break coaxed from power mechanism.
        /// </summary>
        void CoaxeBreak();
    }
}