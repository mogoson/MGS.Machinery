/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerHinge.cs
 *  Description  :  Define RockerHinge component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Extention;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Hinge rotate around the axis base on rocker joint.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/RockerHinge")]
    [ExecuteInEditMode]
    public class RockerHinge : RockerMechanism
    {
        #region Field and Property
        /// <summary>
        /// Rotate Axis.
        /// </summary>
        public Vector3 Axis { get { return transform.forward; } }

        /// <summary>
        /// Zero Axis.
        /// </summary>
        public Vector3 ZeroAxis
        {
            get
            {
                var axis = Vector3.up;
                if (transform.parent)
                {
                    axis = transform.parent.up;
                }
                return axis;
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
            joint.position = transform.position;
            var angle = -EVector3.ProjectAngle(joint.forward, ZeroAxis, Axis);
            var euler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
            transform.localRotation = Quaternion.Euler(euler);
        }
        #endregion
    }
}