/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerLimiter.cs
 *  Description  :  Define RockerLimiter component.
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
    /// Limiter for RockerJoint.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/RockerLimiter")]
    [RequireComponent(typeof(RockerJoint))]
    [ExecuteInEditMode]
    public class RockerLimiter : LimiterMechanism
    {
        #region Field and Property
        /// <summary>
        /// Distance range of rocker.
        /// </summary>
        public Range distance = new Range(1, 10);

        /// <summary>
        /// Limiter is triggered?
        /// </summary>
        public override bool IsTriggered
        {
            get
            {
                var currentDistance = GetDistance();
                return currentDistance <= distance.min || currentDistance >= distance.max;
            }
        }

        /// <summary>
        /// Target rocker of limiter.
        /// </summary>
        public RockerJoint Rocker { protected set; get; }
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            Rocker = GetComponent<RockerJoint>();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get distance from rocker to joint.
        /// </summary>
        /// <returns>Distance from rocker to joint.</returns>
        public virtual float GetDistance()
        {
            return Vector3.Distance(Rocker.transform.position, Rocker.joint.position);
        }
        #endregion
    }
}