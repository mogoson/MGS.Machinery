/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerRivet.cs
 *  Description  :  Define RockerRivet component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/RockerRivet")]
    [ExecuteInEditMode]
    public class RockerRivet : RockerMechanism
    {
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
            transform.position = rockJoint.position;
        }
        #endregion
    }
}