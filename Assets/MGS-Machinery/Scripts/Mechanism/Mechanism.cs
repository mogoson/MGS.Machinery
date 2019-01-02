/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Mechanism.cs
 *  Description  :  Define abstract mechanisms.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  4/26/2018
 *  Description  :  Optimize abstract mechanisms define.
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
        /// Triggers attached on link rockers.
        /// </summary>
        protected List<TriggerMechanism> triggers = new List<TriggerMechanism>();

        /// <summary>
        /// Record value on trigger is triggered.
        /// </summary>
        protected float triggerRecord = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Check trigger is triggered.
        /// </summary>
        /// <returns>Return true if one of the triggers is triggered.</returns>
        protected bool CheckTriggers()
        {
            foreach (var trigger in triggers)
            {
                if (trigger.IsTriggered)
                {
                    return true;
                }
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
                rocker.Drive(0, DriveType.Ignore);
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public override void Initialize()
        {
            triggers.Clear();
            foreach (var rocker in rockers)
            {
                var trigger = rocker.GetComponent<TriggerMechanism>();
                if (trigger)
                {
                    triggers.Add(trigger);
                }
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
        /// Rotate crank by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        protected abstract void DriveCrank(float velocity, DriveType type);
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
        /// Drive crank by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity, DriveType type)
        {
            DriveCrank(velocity, type);
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
            if (IsIntact)
            {
                Initialize();
            }
        }

#if UNITY_EDITOR
        protected virtual void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

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
            {
                isInitialized = false;
            }
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
        protected Vector CorrectPoint(Vector3 position)
        {
            return new Vector(position.x, position.y);
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
        /// Drive crank link mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity, DriveType type)
        {
            if (velocity >= 0)
            {
                if (IsLock && isPositive)
                {
                    return;
                }
                isPositive = true;
            }
            else
            {
                if (IsLock && !isPositive)
                {
                    return;
                }
                isPositive = false;
            }
            crank.Drive(velocity, type);
            DriveLinkJoints();
        }
        #endregion
    }

    /// <summary>
    /// Rocker Mechanism.
    /// </summary>
    public abstract class RockerMechanism : Mechanism
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
            {
                Drive(0, DriveType.Ignore);
            }
        }
#endif
        #endregion

        #region Public Method
        /// <summary>
        /// Drive rocker by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override abstract void Drive(float velocity = 0, DriveType type = DriveType.Ignore);
        #endregion
    }

    /// <summary>
    /// Slider joint mechanism.
    /// </summary>
    public abstract class SliderMechanism : RockerLinkMechanism
    {
        #region Field and Property
        /// <summary>
        /// Stroke of slider.
        /// </summary>
        public Range stroke = new Range(-1, 1);

        /// <summary>
        /// Displacement of slider.
        /// </summary>
        public float Displacement { protected set; get; }

        /// <summary>
        /// Telescopic state of slider.
        /// </summary>
        public TelescopicState State
        {
            get
            {
                var state = TelescopicState.Between;
                if (Displacement <= stroke.min)
                {
                    state = TelescopicState.Minimum;
                }
                else if (Displacement >= stroke.max)
                {
                    state = TelescopicState.Maximum;
                }
                return state;
            }
        }

        /// <summary>
        /// Start position of slider.
        /// </summary>
        public Vector3 StartPosition { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Move slider by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of move.</param>
        protected abstract void DriveSlider(float velocity);
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
        /// Drive slider by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override void Drive(float velocity, DriveType type = DriveType.Ignore)
        {
            DriveSlider(velocity);
        }
        #endregion
    }

    /// <summary>
    /// Arm with slider joints.
    /// </summary>
	public abstract class SliderArmMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Slider joints of arm.
        /// </summary>
        public List<SliderMechanism> sliders = new List<SliderMechanism>();
        #endregion

        #region Protected Method
        /// <summary>
        /// Clamp slider index in the range.
        /// </summary>
        /// <param name="index">Index of slider.</param>
        /// <returns>Correct index of slider.</returns>
        protected int ClampIndex(int index)
        {
            return Mathf.Clamp(index, 0, sliders.Count - 1);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive arm by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="type">Type of drive.</param>
        public override abstract void Drive(float velocity, DriveType type = DriveType.Ignore);
        #endregion
    }
}