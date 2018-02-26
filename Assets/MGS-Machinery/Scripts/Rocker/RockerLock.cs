/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerLock.cs
 *  Description  :  Define RockerLock component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/RockerLock")]
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