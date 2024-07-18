/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerJoint.cs
 *  Description  :  Define RockerJoint component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Rocker look at joint.
    /// </summary>
    [ExecuteInEditMode]
    public class RockerJoint : RockerMechanism
    {
        #region Field and Property
        /// <summary>
        /// Keep up mode.
        /// </summary>
        [Tooltip("Keep up mode.")]
        public KeepUpMode keepUp = KeepUpMode.TransformUp;

        /// <summary>
        /// Reference forward as world up for look at.
        /// </summary>
        [Tooltip("Reference forward as world up for look at.")]
        public Transform reference;

        /// <summary>
        /// World up for look at.
        /// </summary>
        public Vector3 WorldUp
        {
            get
            {
                var up = transform.up;
                if (keepUp == KeepUpMode.ReferenceForward && reference)
                {
                    up = reference.forward;
                }
                return up;
            }
        }
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity = 0, DriveMode mode = DriveMode.Ignore)
        {
            transform.LookAt(joint, WorldUp);
            return true;
        }
        #endregion
    }
}