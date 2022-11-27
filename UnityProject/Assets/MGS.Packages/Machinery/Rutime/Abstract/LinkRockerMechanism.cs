/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LinkRockerMechanism.cs
 *  Description  :  Mechanism with link rockers.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Mechanism with link rockers.
    /// </summary>
    public abstract class LinkRockerMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Rockers that link with this mechanism. 
        /// </summary>
        [Tooltip("Rockers that link with this mechanism. ")]
        public List<RockerMechanism> rockers = new List<RockerMechanism>();

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckRockerStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Check one of the rockers is stuck?
        /// </summary>
        /// <returns>Return true if one of the rockers is stuck.</returns>
        protected bool CheckRockerStuck()
        {
            foreach (var rocker in rockers)
            {
                if (rocker.IsStuck)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Drive the rockers that join at this mechanism.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is unrestricted?</returns>
        protected bool DriveRockers(float velocity, DriveMode mode)
        {
            foreach (var rocker in rockers)
            {
                if (!rocker.Drive(velocity, mode))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}