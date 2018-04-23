/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerRivet.cs
 *  Description  :  Define RockerRivet component.
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
    /// Rocker keep same position with joint.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/RockerRivet")]
    [ExecuteInEditMode]
    public class RockerRivet : RockerMechanism
    {
        #region Protected Method
#if UNITY_EDITOR
        protected virtual void Update()
        {
            if (!Application.isPlaying && joint)
                Drive();
        }
#endif
        #endregion

        #region Public Method
        /// <summary>
        /// Drive rocker.
        /// </summary>
        public override void Drive()
        {
            transform.position = joint.position;
        }
        #endregion
    }
}