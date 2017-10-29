/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerHinge.cs
 *  Description  :  Define RockerHinge component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  9/5/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using Developer.VectorExtention;
using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/RockerHinge")]
    [ExecuteInEditMode]
    public class RockerHinge : RockerMechanism
    {
        #region Property and Field
        /// <summary>
        /// Rotate Axis.
        /// </summary>
        public Vector3 axis { get { return transform.forward; } }

        /// <summary>
        /// Zero Axis.
        /// </summary>
        public Vector3 zeroAxis
        {
            get
            {
                if (transform.parent)
                    return transform.parent.up;
                else
                    return Vector3.up;
            }
        }
        #endregion

        #region Protected Method
#if UNITY_EDITOR
        protected virtual void Update()
        {
            if (!Application.isPlaying && rockJoint)
                DriveMechanism();
        }
#endif
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        public override void DriveMechanism()
        {
            rockJoint.position = transform.position;

            var angle = -EVector3.RotateAngle(rockJoint.forward, zeroAxis, axis);
            var euler = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
            transform.localRotation = Quaternion.Euler(euler);
        }
        #endregion
    }
}