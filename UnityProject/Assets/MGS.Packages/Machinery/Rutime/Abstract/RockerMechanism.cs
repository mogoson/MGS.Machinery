/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerMechanism.cs
 *  Description  :  Rocker mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Rocker mechanism.
    /// </summary>
    public abstract class RockerMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Look at joint.
        /// </summary>
        [Tooltip("Look at joint.")]
        public Transform joint;
        #endregion

        #region Protected Method
        /// <summary>
        /// Update mechanism.
        /// </summary>
        protected virtual void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (joint == null)
            {
                return;
            }

            Drive(0, DriveMode.Ignore);
        }
        #endregion
    }
}