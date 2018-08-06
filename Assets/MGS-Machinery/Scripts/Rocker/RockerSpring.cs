/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerSpring.cs
 *  Description  :  Define RockerSpring component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/24/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.CurvePipe;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Rocker spring look at joint.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/RockerSpring")]
    public class RockerSpring : RockerJoint
    {
        #region Field and Property
        /// <summary>
        /// Spring of rocker.
        /// </summary>
        public HelixPipe spring;

        /// <summary>
        /// Top padding of spring.
        /// </summary>
        public float top = 0;

        /// <summary>
        /// Bottom padding of spring.
        /// </summary>
        public float bottom = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive spring.
        /// </summary>
        protected virtual void DriveSpring()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && spring == null)
                return;
#endif
            //Rivet spring.
            spring.transform.localPosition = Vector3.zero;
            spring.transform.localRotation = Quaternion.Euler(90, 0, 0);

            //Rebuild spring.
            spring.bottomEllipse.center.y = bottom;
            spring.topEllipse.center.y = Vector3.Distance(transform.position, joint.position) - top;
            spring.Rebuild();
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
            base.Drive(velocity, type);
            DriveSpring();
        }
        #endregion
    }
}