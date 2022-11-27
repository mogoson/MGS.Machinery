/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankLinkMechanism.cs
 *  Description  :  Crank mechanism with link joints.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Mathematics;
using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Crank mechanism with link joints.
    /// </summary>
	public abstract class CrankLinkMechanism : Mechanism
    {
        #region Field and Property
        /// <summary>
        /// Power crank.
        /// </summary>
        [Tooltip("Power crank.")]
        public CrankMechanism crank;

        /// <summary>
        /// Link rocker.
        /// </summary>
        [Tooltip("Link rocker.")]
        public RockerMechanism link;

        /// <summary>
        /// Edit mode of Hinge Editor.
        /// </summary>
        [HideInInspector]
        public EditMode editMode = EditMode.Lock;

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (crank.IsStuck || link.IsStuck)
                {
                    return true;
                }
                return base.IsStuck;
            }
        }

        /// <summary>
        /// All the joints of this mechanism are set intact.
        /// </summary>
        public abstract bool IsIntact { get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Awake mechanism.
        /// </summary>
        protected override void Awake()
        {
            if (!Application.isPlaying)
            {
                if (!IsIntact)
                {
                    return;
                }
            }

            Initialize();
        }

        /// <summary>
        /// Update mechanism.
        /// </summary>
        protected virtual void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            if (IsIntact)
            {
                if (!IsInitialized)
                {
                    Initialize();
                }

                Drive(0, DriveMode.Ignore);
            }
            else
            {
                IsInitialized = false;
            }
        }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is unrestricted?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            if (!crank.Drive(velocity, mode))
            {
                return false;
            }

            return DriveLinkJoints();
        }

        /// <summary>
        /// Drive joints those link with this mechanism.
        /// </summary>
        /// <returns>Drive joints is unrestricted?</returns>
        protected abstract bool DriveLinkJoints();

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
        /// Correct position to project point.
        /// </summary>
        /// <param name="position">Local position.</param>
        /// <returns>Correct point.</returns>
        protected Vector2D CorrectPoint(Vector3 position)
        {
            return new Vector2D(position.x, position.y);
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
        /// Get local position of link rocker base on this transform.
        /// </summary>
        /// <returns>Local position of link rocker.</returns>
        protected virtual Vector3 GetLinkPosition()
        {
            return transform.InverseTransformPoint(link.transform.position);
        }
        #endregion
    }
}