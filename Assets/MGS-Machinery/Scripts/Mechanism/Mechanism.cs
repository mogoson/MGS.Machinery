/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: Mechanism.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/24/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     12/24/2015       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using Math.Planimetry;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Custom Edit Mode.
    /// </summary>
    public enum EditMode
    {
        Edit, Hinge, Lock
    }//enum_end

    /// <summary>
    /// Custom Axis.
    /// </summary>
    public enum CustomAxis
    {
        Default, TransformForward
    }//enum_end

    /// <summary>
    /// Telescopic Mechanism's State.
    /// </summary>
    public enum TelescopicState
    {
        Shrink, Drift, Extend
    }//enum_end

    /// <summary>
    /// Mechanism.
    /// </summary>
    public abstract class Mechanism : MonoBehaviour
    {
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public abstract void DriveMechanism(float speedControl);
    }//class_end

    /// <summary>
    /// Crank Mechanism.
    /// </summary>
	public abstract class CrankMechanism : RockerLockLinkMechanism
    {
        #region Property and Field
        /// <summary>
        /// Crank drive speed.
        /// </summary>
        public float speed = 60;

        /// <summary>
        /// Current angle of crank.
        /// </summary>
        public float angle { protected set; get; }

        /// <summary>
        /// Start eulerAngles.
        /// </summary>
        public Vector3 startAngles { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Rotate the crank by angle.
        /// </summary>
        protected virtual void DriveCrank()
        {
            transform.localRotation = Quaternion.Euler(startAngles + new Vector3(0, 0, angle));
            DriveRockers();
        }//DriveArm()_end
        #endregion

        #region Public Method
        /// <summary>
        /// Find rocker locks and save start angles.
        /// </summary>
        public new virtual void Awake()
        {
            base.Awake();
            startAngles = transform.localEulerAngles;
        }//Awake()_end
        #endregion
    }//class_end

    /// <summary>
    /// Crank Link Mechanism.
    /// </summary>
	public abstract class CrankLinkMechanism : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Power crank.
        /// </summary>
        public CrankMechanism crank;

        /// <summary>
        /// Gearing link bar.
        /// </summary>
        public RockerMechanism linkBar;

        /// <summary>
        /// Custom edit mode [Only work in editor script].
        /// </summary>
        [HideInInspector]
        public EditMode editMode = EditMode.Lock;

        /// <summary>
        /// Dead lock state.
        /// </summary>
        public bool Lock { protected set; get; }

        /// <summary>
        /// Drive direction.
        /// </summary>
        public bool positive { protected set; get; }

        /// <summary>
        /// Mechanism is initialized.
        /// </summary>
        public bool initialized { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Get link bar position base on this transform.
        /// </summary>
        /// <returns>Local position.</returns>
        protected virtual Vector3 GetLinkPosition()
        {
            return transform.InverseTransformPoint(linkBar.transform.position);
        }//GetLP()_end

        /// <summary>
        /// Correct position to project point.
        /// </summary>
        /// <param name="position">Local position.</param>
        /// <returns>Correct point.</returns>
        protected Point CorrectPoint(Vector3 position)
        {
            return new Point(position.x, position.y);
        }//CorrectPoint()_end

        /// <summary>
        /// Clear angles x and y.
        /// </summary>
        /// <param name="angles">Local euler angles.</param>
        /// <returns>Correct angles.</returns>
        protected Vector3 CorrectAngles(Vector3 angles)
        {
            return new Vector3(0, 0, angles.z);
        }//CorrectAngles()_end

        /// <summary>
        /// Clear position z.
        /// </summary>
        /// <param name="position">Local position.</param>
        /// <returns>Correct position.</returns>
        protected Vector3 CorrectPosition(Vector3 position)
        {
            return new Vector3(position.x, position.y, 0);
        }//CorrectPosition()_end

        /// <summary>
        /// Drive bars.
        /// </summary>
        protected abstract void DriveLinkBars();
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            if (crank.speed * speedControl >= 0)
            {
                if (Lock && positive)
                    return;
                positive = true;
            }
            else
            {
                if (Lock && !positive)
                    return;
                positive = false;
            }//if()_end
            crank.DriveMechanism(speedControl);
            DriveLinkBars();
        }//DriveM...()_end
        #endregion
    }//class_end

    /// <summary>
    /// Telescopic Joint Mechanism.
    /// </summary>
	public abstract class TelescopicJointMechanism : RockerLockLinkMechanism
    {
        #region Property and Field
        /// <summary>
        /// Stroke of joint.
        /// </summary>
        public float stroke = 1;

        /// <summary>
        /// Drive speed of joint.
        /// </summary>
        public float speed = 1;

        /// <summary>
        /// Displacement of joint.
        /// </summary>
        public float displacement { protected set; get; }

        /// <summary>
        /// Telescopic joint state.
        /// </summary>
        public TelescopicState tState { protected set; get; }

        /// <summary>
        /// Start position of joint.
        /// </summary>
        public Vector3 startPosition { protected set; get; }

        /// <summary>
        /// Local move axis.
        /// </summary>
        protected Vector3 aixs
        {
            get
            {
                var forward = transform.forward;
                if (transform.parent)
                    forward = transform.parent.InverseTransformDirection(forward);
                return forward;
            }//get_end
        }//aixs_end
        #endregion

        #region Protected Method
        protected override void Awake()
        {
            base.Awake();
            startPosition = transform.localPosition;
            tState = TelescopicState.Shrink;
        }//Awake()_end

        /// <summary>
        /// Drive joint.
        /// </summary>
        protected virtual void DriveJoint()
        {
            tState = TelescopicState.Drift;
            transform.localPosition = startPosition + aixs * displacement;
            DriveRockers();
        }//DriveJoint()_end
        #endregion
    }//class_end

    /// <summary>
    /// Telescopic Arm Mechanism.
    /// </summary>
	public abstract class TelescopicArmMechanism : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Telescopic joint of arm.
        /// </summary>
        public List<TelescopicJointMechanism> tJoints = new List<TelescopicJointMechanism>();
        #endregion
    }//class_end

    /// <summary>
    /// Rocker Mechanism.
    /// </summary>
	public abstract class RockerMechanism : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Rocker look at joint.
        /// </summary>
        public Transform rockJoint;
        #endregion

        #region Public method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        public abstract void DriveMechanism();
        #endregion
    }//class_end

    /// <summary>
    /// Rocker Lock Mechanism.
    /// </summary>
    public abstract class RockerLockMechanism : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Min stroke of the rocker.
        /// </summary>
        public float minStroke = 1;

        /// <summary>
        /// Max stroke of the rocker.
        /// </summary>
        public float maxStroke = 10;

        /// <summary>
        /// Rocker's lock state.
        /// </summary>
        public bool isLock
        {
            get
            {
                var distance = GetDistance();
                return distance <= minStroke || distance >= maxStroke;
            }//get_end
        }//isLock_end

        /// <summary>
        /// Lock target roker joint.
        /// </summary>
        public RockerJoint rJoint { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Find RockerJoint.
        /// </summary>
        protected virtual void Awake()
        {
            rJoint = GetComponent<RockerJoint>();
        }//Awake()_end
        #endregion

        #region Public Method
        /// <summary>
        /// Get distance from this transform to rJoint.rockJoint transform.
        /// </summary>
        /// <returns>Distance</returns>
        public virtual float GetDistance()
        {
            return Vector3.Distance(transform.position, rJoint.rockJoint.position);
        }//Get...()_end
        #endregion
    }//class_end

    /// <summary>
    /// Rocker Lock Link Mechanism.
    /// </summary>
    public abstract class RockerLockLinkMechanism : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Rockers that drive by mechanism. 
        /// </summary>
        public List<RockerMechanism> rockers = new List<RockerMechanism>();

        /// <summary>
        /// Rocker locks in this mechanism.
        /// </summary>
        protected List<RockerLockMechanism> rLocks = new List<RockerLockMechanism>();

        /// <summary>
        /// Record lock angle.
        /// </summary>
        protected float lockRecord = 0;
        #endregion

        #region Protected Method
        /// <summary>
        /// Find rocker locks.
        /// </summary>
        protected virtual void Awake()
        {
            foreach (var rocker in rockers)
            {
                var rlock = rocker.GetComponent<RockerLockMechanism>();
                if (rlock)
                    rLocks.Add(rlock);
            }//foreach()_end
        }//Awake()_end

        /// <summary>
        /// Check rockers's lock state.
        /// </summary>
        /// <returns>Return true if one of the rockers is lock.</returns>
        protected virtual bool CheckRockersLock()
        {
            foreach (var rLock in rLocks)
            {
                if (rLock.isLock)
                    return true;
            }//foreach()_end
            return false;
        }//Check...()_end

        /// <summary>
        /// Drive the rockers that join at this mechanism.
        /// </summary>
        protected virtual void DriveRockers()
        {
            foreach (var rocker in rockers)
            {
                rocker.DriveMechanism();
            }
        }//DriveRockers()_end
        #endregion
    }//class_end
}//namespace_end