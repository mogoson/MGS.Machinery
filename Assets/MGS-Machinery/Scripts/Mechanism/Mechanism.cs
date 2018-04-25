/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Mathematics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoson.Machinery
{
#if UNITY_EDITOR
    /// <summary>
    /// Edit mode of Hinge Editor (Only work for editor script).
    /// </summary>
    public enum EditMode
    {
        Free = 0,
        Hinge = 1,
        Lock = 2
    }
#endif

    /// <summary>
    /// Mode of keep up.
    /// </summary>
    public enum KeepUpMode
    {
        TransformUp = 0,
        ReferenceForward = 1
    }

    /// <summary>
    /// State of telescopic joint.
    /// </summary>
    public enum TelescopicState
    {
        Minimum = 0,
        Between = 1,
        Maximum = 2
    }

    /// <summary>
    /// Range from min to max.
    /// </summary>
    [Serializable]
    public struct Range
    {
        /// <summary>
        /// Min value of range.
        /// </summary>
        public float min;

        /// <summary>
        /// Max value of range.
        /// </summary>
        public float max;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="min">Min value of range.</param>
        /// <param name="max">Max value of range.</param>
        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }

    /// <summary>
    /// Base mechanism.
    /// </summary>
    public abstract class Mechanism : MonoBehaviour
    {
        #region Protected Method
        protected virtual void Awake()
        {
            Initialize();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Drive mechanism.
        /// </summary>
        /// <param name="speedRatio">Speed ratio.</param>
        public abstract void Drive(float speedRatio);
        #endregion
    }

    /// <summary>
    /// Mechanism with link rockers.
    /// </summary>
    public abstract class RockerLinkMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Rockers that link with this mechanism. 
        /// </summary>
        public List<RockerMechanism> rockers = new List<RockerMechanism>();

        /// <summary>
        /// Limiters attached on link rockers.
        /// </summary>
        protected List<LimiterMechanism> limiters = new List<LimiterMechanism>();

        /// <summary>
        /// Record value on limiter is triggered.
        /// </summary>
        protected float triggerRecord = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Check limiter is triggered.
        /// </summary>
        /// <returns>Return true if one of the limiters is triggered.</returns>
        protected bool CheckLimiterTrigger()
        {
            foreach (var limiter in limiters)
            {
                if (limiter.IsTriggered)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Drive the rockers that join at this mechanism.
        /// </summary>
        protected void DriveRockers()
        {
            foreach (var rocker in rockers)
            {
                rocker.Drive();
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public override void Initialize()
        {
            limiters.Clear();
            foreach (var rocker in rockers)
            {
                var limiter = rocker.GetComponent<LimiterMechanism>();
                if (limiter)
                    limiters.Add(limiter);
            }
        }
        #endregion
    }

    /// <summary>
    /// Crank mechanism.
    /// </summary>
	public abstract class CrankMechanism : RockerLinkMechanism
    {
        #region Field and Property
        /// <summary>
        /// Rotate speed of crank.
        /// </summary>
        public float speed = 50;

        /// <summary>
        /// Current rotate angle of crank.
        /// </summary>
        public float Angle { protected set; get; }

        /// <summary>
        /// Start eulerAngles.
        /// </summary>
        public Vector3 StartAngles { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Rotate crank.
        /// </summary>
        /// <param name="rotateSpeed">Rotate speed of crank.</param>
        protected abstract void DriveCrank(float rotateSpeed);
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize crank.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            StartAngles = transform.localEulerAngles;
        }

        /// <summary>
        /// Drive crank.
        /// </summary>
        /// <param name="speedRatio">Speed ratio.</param>
        public override void Drive(float speedRatio)
        {
            DriveCrank(speed * speedRatio);
        }
        #endregion
    }

    /// <summary>
    /// Crank mechanism with link joints.
    /// </summary>
	public abstract class CrankLinkMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Power crank.
        /// </summary>
        public CrankMechanism crank;

        /// <summary>
        /// Link rocker.
        /// </summary>
        public RockerMechanism link;

#if UNITY_EDITOR
        /// <summary>
        /// Edit mode of Hinge Editor (Only work for editor script).
        /// </summary>
        [HideInInspector]
        public EditMode editMode = EditMode.Lock;

        /// <summary>
        /// This mechanism is initialized? (Only work for editor script).
        /// </summary>
        [HideInInspector]
        public bool isInitialized = false;
#endif
        /// <summary>
        /// All the joints of this mechanism are set intact.
        /// </summary>
        public abstract bool IsIntact { get; }

        /// <summary>
        /// Is dead lock?
        /// </summary>
        public bool IsLock { protected set; get; }

        /// <summary>
        /// Drive speed is positive?
        /// </summary>
        private bool isPositive = false;
        #endregion

        #region Protected Method
        protected override void Awake()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
                Initialize();
        }

#if UNITY_EDITOR
        protected virtual void Update()
        {
            if (Application.isPlaying)
                return;

            if (IsIntact)
            {
                if (!isInitialized)
                {
                    Initialize();
                    isInitialized = true;
                }
                DriveLinkJoints();
            }
            else
                isInitialized = false;
        }
#endif

        /// <summary>
        /// Get local position of link rocker base on this transform.
        /// </summary>
        /// <returns>Local position of link rocker.</returns>
        protected virtual Vector3 GetLinkPosition()
        {
            return transform.InverseTransformPoint(link.transform.position);
        }

        /// <summary>
        /// Correct position to project point.
        /// </summary>
        /// <param name="position">Local position.</param>
        /// <returns>Correct point.</returns>
        protected Point CorrectPoint(Vector3 position)
        {
            return new Point(position.x, position.y);
        }

        /// <summary>
        /// Clear angles x and y.
        /// </summary>
        /// <param name="angles">Local euler angles.</param>
        /// <returns>Correct angles.</returns>
        protected Vector3 CorrectAngles(Vector3 angles)
        {
            return new Vector3(0, 0, angles.z);
        }

        /// <summary>
        /// Clear position Z.
        /// </summary>
        /// <param name="position">Local position.</param>
        /// <returns>Correct position.</returns>
        protected Vector3 CorrectPosition(Vector3 position)
        {
            return new Vector3(position.x, position.y);
        }

        /// <summary>
        /// Drive joints those link with this mechanism.
        /// </summary>
        protected abstract void DriveLinkJoints();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive crank link mechanism.
        /// </summary>
        /// <param name="speedRatio">Speed ratio.</param>
        public override void Drive(float speedRatio)
        {
            if (crank.speed * speedRatio >= 0)
            {
                if (IsLock && isPositive)
                    return;
                isPositive = true;
            }
            else
            {
                if (IsLock && !isPositive)
                    return;
                isPositive = false;
            }
            crank.Drive(speedRatio);
            DriveLinkJoints();
        }
        #endregion
    }

    /// <summary>
    /// Rocker Mechanism.
    /// </summary>
    public abstract class RockerMechanism : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Look at joint.
        /// </summary>
        public Transform joint;
        #endregion

        #region Protected Method
#if UNITY_EDITOR
        protected virtual void Update()
        {
            if (!Application.isPlaying && joint)
                Drive();
        }
#endif
        #endregion

        #region Public method
        /// <summary>
        /// Drive rocker.
        /// </summary>
        public abstract void Drive();
        #endregion
    }

    /// <summary>
    /// Limiter for mechanism.
    /// </summary>
    public abstract class LimiterMechanism : MonoBehaviour
    {
        #region Field and Property
        /// <summary>
        /// Limiter is triggered?
        /// </summary>
        public abstract bool IsTriggered { get; }
        #endregion
    }

    /// <summary>
    /// Telescopic joint mechanism.
    /// </summary>
    public abstract class TelescopicJointMechanism : RockerLinkMechanism
    {
        #region Field and Property
        /// <summary>
        /// Stroke of joint.
        /// </summary>
        public Range stroke = new Range(-1, 1);

        /// <summary>
        /// Move speed of joint.
        /// </summary>
        public float speed = 1;

        /// <summary>
        /// Displacement of joint.
        /// </summary>
        public float Displacement { protected set; get; }

        /// <summary>
        /// Telescopic state of joint.
        /// </summary>
        public TelescopicState State
        {
            get
            {
                if (Displacement <= stroke.min)
                    return TelescopicState.Minimum;
                else if (Displacement >= stroke.max)
                    return TelescopicState.Maximum;
                else
                    return TelescopicState.Between;
            }
        }

        /// <summary>
        /// Start position of joint.
        /// </summary>
        public Vector3 StartPosition { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Move joint.
        /// </summary>
        /// <param name="moveSpeed">Speed of move joint.</param>
        protected abstract void DriveJoint(float moveSpeed);
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize joint.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            StartPosition = transform.localPosition;
        }

        /// <summary>
        /// Drive joint.
        /// </summary>
        /// <param name="speedRatio">Speed ratio.</param>
        public override void Drive(float speedRatio)
        {
            DriveJoint(speed * speedRatio);
        }
        #endregion
    }

    /// <summary>
    /// Arm with telescopic joints.
    /// </summary>
	public abstract class TelescopicArmMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Telescopic joints of arm.
        /// </summary>
        public List<TelescopicJointMechanism> joints = new List<TelescopicJointMechanism>();
        #endregion

        #region Protected Method
        /// <summary>
        /// Clamp joint index in the range.
        /// </summary>
        /// <param name="index">Index of joint.</param>
        /// <returns>Correct index of joint.</returns>
        protected int ClampIndex(int index)
        {
            return Mathf.Clamp(index, 0, joints.Count - 1);
        }
        #endregion
    }
}