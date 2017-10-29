/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerJoint.cs
 *  Description  :  Define RockerJoint component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/24/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/RockerJoint")]
    [ExecuteInEditMode]
    public class RockerJoint : RockerMechanism
    {
        #region Property and Field
        /// <summary>
        /// Keep up mode.
        /// </summary>
        public CustomAxis keepUp = CustomAxis.Default;

        /// <summary>
        /// Transform's forward as world up for look at.
        /// </summary>
        [HideInInspector]
        public Transform upTransform;

        /// <summary>
        /// World Up for look at.
        /// </summary>
        public Vector3 worldUp
        {
            get
            {
                if (keepUp == CustomAxis.TransformForward && upTransform)
                    return upTransform.forward;
                else
                    return transform.up;
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
            transform.LookAt(rockJoint, worldUp);
        }
        #endregion
    }
}