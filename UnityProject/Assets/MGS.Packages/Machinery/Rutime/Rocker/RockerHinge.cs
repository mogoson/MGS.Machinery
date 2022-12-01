/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerHinge.cs
 *  Description  :  Define RockerHinge component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Mathematics;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Hinge rotate around the axis base on rocker joint.
    /// </summary>
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

        #region Protected Method
        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity = 0, DriveMode mode = DriveMode.Ignore)
        {
            joint.position = transform.position;

            var angle = -VectorUtility.ProjectAngle(joint.forward, ZeroAxis, Axis);
            var euler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
            transform.localRotation = Quaternion.Euler(euler);

            return true;
        }
        #endregion
    }
}