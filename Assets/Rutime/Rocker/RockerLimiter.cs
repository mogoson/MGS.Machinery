/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerLimiter.cs
 *  Description  :  Define RockerLimiter component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Limiter for RockerJoint.
    /// </summary>
    [RequireComponent(typeof(RockerJoint))]
    [ExecuteInEditMode]
    public class RockerLimiter : Limiter
    {
        #region Field and Property
        /// <summary>
        /// Distance range of rocker.
        /// </summary>
        [Tooltip("Distance range of rocker.")]
        public Range distance = new Range(1, 10);

        /// <summary>
        /// Limiter is triggered?
        /// </summary>
        public override bool IsTriggered
        {
            get
            {
                var currentDis = GetDistance();
                return currentDis <= distance.min || currentDis >= distance.max;
            }
        }

        /// <summary>
        /// Target rocker of limiter.
        /// </summary>
        public RockerJoint Rocker { protected set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize limiter.
        /// </summary>
        public virtual void Awake()
        {
            Rocker = GetComponent<RockerJoint>();
        }

        /// <summary>
        /// Get distance from rocker to joint.
        /// </summary>
        /// <returns>Distance from rocker to joint.</returns>
        public float GetDistance()
        {
            return Vector3.Distance(Rocker.transform.position, Rocker.joint.position);
        }
        #endregion
    }
}