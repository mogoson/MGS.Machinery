/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerLock.cs
 *  Description  :  Define RockerLock component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Lock for rocker joint.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/RockerLock")]
    [RequireComponent(typeof(RockerJoint))]
    [ExecuteInEditMode]
    public class RockerLock : RockerLockMechanism
    {
        #region Protected Method
        //Execute in edit mode.
        protected override void Awake()
        {
            base.Awake();
        }
        #endregion
    }
}