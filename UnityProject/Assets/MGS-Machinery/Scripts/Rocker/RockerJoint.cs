/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerJoint.cs
 *  Description  :  Define RockerJoint component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;
using MGS.Machinery;

namespace MGS.Machinery
{
    /// <summary>
    /// Rocker look at joint.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/RockerJoint")]
    [ExecuteInEditMode]
    public class RockerJoint : RockerMechanism
    {
        #region Field and Property
        /// <summary>
        /// Keep up mode.
        /// </summary>
        public KeepUpMode keepUp = KeepUpMode.TransformUp;

        /// <summary>
        /// Reference forward as world up for look at.
        /// </summary>
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

        #region Public Method
        /// <summary>
        /// Drive rocker by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity = 0, DriveType type = DriveType.Ignore)
        {
            transform.LookAt(joint, WorldUp);
        }
        #endregion
    }
}