/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerRivet.cs
 *  Description  :  Define RockerRivet component.
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
    /// Rocker keep same position with joint.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/RockerRivet")]
    [ExecuteInEditMode]
    public class RockerRivet : RockerMechanism
    {
        #region Public Method
        /// <summary>
        /// Drive rocker by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity = 0, DriveType type = DriveType.Ignore)
        {
            transform.position = joint.position;
        }
        #endregion
    }
}