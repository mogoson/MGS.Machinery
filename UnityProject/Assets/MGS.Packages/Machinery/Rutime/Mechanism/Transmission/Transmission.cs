/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Transmission.cs
 *  Description  :  Define Transmission component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Transmission for mechanisms.
    /// </summary>
    public class Transmission : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Mechanisms drive by this transmission.
        /// </summary>
        [Tooltip("Mechanisms drive by this transmission.")]
        public List<MechanismUnit> mechanismUnits = new List<MechanismUnit>();

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckUnitStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check if one of the units is stuck.
        /// </summary>
        /// <returns>One of the units is stuck?</returns>
        protected bool CheckUnitStuck()
        {
            foreach (var unit in mechanismUnits)
            {
                if (unit.IsStuck)
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
        /// <returns>Drive is unrestricted?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            foreach (var unit in mechanismUnits)
            {
                if (!unit.Drive(velocity, mode))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}