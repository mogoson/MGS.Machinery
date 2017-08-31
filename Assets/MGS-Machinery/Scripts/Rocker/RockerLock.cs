/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: RockerLock.cs
 *  Author: Mogoson Version: 1.0   Date: 8/31/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          RockerLock              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson      8/31/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEngine;

    [AddComponentMenu("Developer/Machinery/RockerLock")]
    [RequireComponent(typeof(RockerJoint))]
    [ExecuteInEditMode]
    public class RockerLock : RockerLockMechanism
    {
        #region Protected Method
        /// <summary>
        /// Execute in edit mode.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
        }
        #endregion
    }
}