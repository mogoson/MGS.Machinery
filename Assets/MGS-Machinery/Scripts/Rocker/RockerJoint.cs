/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: RockerJoint.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/24/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          RockerJoint             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     12/24/2015       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEngine;

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