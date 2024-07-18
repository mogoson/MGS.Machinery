/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Synchronizer.cs
 *  Description  :  Define Synchronizer component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/27/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Synchronizer for mechanisms.
    /// </summary>
    public class Synchronizer : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanisms drive by this synchronizer.
        /// </summary>
        [Tooltip("Mechanisms drive by this synchronizer.")]
        public List<Mechanism> mechanisms = new List<Mechanism>();

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckMechanismStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check if one of the mechanisms is stuck.
        /// </summary>
        /// <returns>One of the mechanisms is stuck?</returns>
        protected bool CheckMechanismStuck()
        {
            foreach (var mechanism in mechanisms)
            {
                if (mechanism.IsStuck)
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
            foreach (var mechanism in mechanisms)
            {
                if (!mechanism.Drive(velocity, mode))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}