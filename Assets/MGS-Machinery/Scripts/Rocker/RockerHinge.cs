/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: RockerHinge.cs
 *  Author: Mogoson   Version: 1.0   Date: 9/5/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          RockerHinge             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     9/5/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEngine;
    using Vector3;

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
            var angle = -DVector3.RotateAngle(rockJoint.forward, zeroAxis, axis);
            transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
        }
        #endregion
    }
}