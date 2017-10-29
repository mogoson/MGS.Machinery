/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  CrankSlider.cs
 *  Description  :  Define CrankSlider component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/25/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using Developer.MathExtension.Planimetry;
using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/CrankSlider")]
    [ExecuteInEditMode]
    public class CrankSlider : CrankLinkMechanism
    {
        #region Property and Field
        /// <summary>
        /// Joint of link bar and slider.
        /// </summary>
        public Transform lsJoint;

        /// <summary>
        /// All mechanism is set Intact.
        /// </summary>
        public bool isIntact { get { return crank && linkBar && lsJoint; } }

        /// <summary>
        /// lsJoint start local position.
        /// </summary>
        public Vector3 lsJointPosition { protected set; get; }

        /// <summary>
        /// link bar start local position.
        /// </summary>
        protected Vector3 linkPosition;

        /// <summary>
        /// lsJoint start local euler angles.
        /// </summary>
        protected Vector3 lsJointAngles;

        /// <summary>
        /// Line from link bar to slider.
        /// </summary>
		protected Line linkLine;

        /// <summary>
        /// Circle base link bar.
        /// </summary>
		protected Circle linkCircle;

        /// <summary>
        /// Radius of the circle that base link bar.
        /// </summary>
		protected double linkRadius;

        /// <summary>
        /// Link bar and slider joint is on the right of link bar on start.
        /// </summary>
		protected bool isRight;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
                Initialize();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Drive bars on editor node.
        /// </summary>
        protected virtual void Update()
        {
            if (Application.isPlaying)
                return;

            if (isIntact)
            {
                if (!isInitialized)
                    Initialize();

                DriveLinkBars();
            }
            else
                isInitialized = false;
        }
#endif

        /// <summary>
        /// Drive link bar and slider.
        /// </summary>
		protected override void DriveLinkBars()
        {
            //Rivet joints.
            lsJoint.localEulerAngles = lsJointAngles;
            crank.transform.localPosition = Vector3.zero;
            linkBar.transform.localPosition = linkPosition;

            var linkPoint = CorrectPoint(GetLinkPosition());
            linkCircle = new Circle(linkPoint, linkRadius);
            var points = Planimetry.GetIntersections(linkCircle, linkLine);
            if (points == null)
            {
                isLock = true;
                return;
            }

            isLock = false;
            Point point;
            if (points.Count == 1)
                point = points[0];
            else
                point = isRight ? points[0] : points[1];
            lsJoint.localPosition = new Vector3((float)point.x, (float)point.y, 0);

            //Drive linkBar.
            linkBar.DriveMechanism();
        }

        /// <summary>
        /// Clear angles z and set y to 90.
        /// </summary>
        /// <param name="angles">Local euler angles.</param>
        /// <returns>Correct lsJoint angles.</returns>
        protected Vector3 CorrectLSJointAngles(Vector3 angles)
        {
            return new Vector3(angles.x, 90, 0);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public override void Initialize()
        {
            //Correct crank.
            crank.transform.localEulerAngles = CorrectAngles(crank.transform.localEulerAngles);
            crank.Awake();

            //Correct lsJoint.
            lsJointAngles = CorrectLSJointAngles(lsJoint.localEulerAngles);
            lsJoint.localEulerAngles = lsJointAngles;

            //Save start local position.
            linkPosition = CorrectPosition(linkBar.transform.localPosition);
            lsJointPosition = CorrectPosition(lsJoint.localPosition);

            //Initialize CrankSlider mathematical model.
            var lsJointPoint = CorrectPoint(lsJoint.localPosition);
            var linkPoint = CorrectPoint(GetLinkPosition());
            var direction = transform.InverseTransformDirection(ProjectDirection(lsJoint.forward));
            var directionPoint = CorrectPoint(lsJoint.localPosition + direction);
            linkRadius = Planimetry.GetDistance(linkPoint, lsJointPoint);
            linkLine = Line.GetLine(lsJointPoint, directionPoint);
            isRight = lsJointPoint.x - linkPoint.x >= 0;
            isInitialized = true;
        }

        /// <summary>
        /// Project direction vector on plane(Normal is transform.forward).
        /// </summary>
        /// <param name="direction">World space direction.</param>
        /// <returns>Project direction.</returns>
        public Vector3 ProjectDirection(Vector3 direction)
        {
            direction = Vector3.ProjectOnPlane(direction, transform.forward);
            if (direction == Vector3.zero)
                direction = transform.right;
            return direction;
        }
        #endregion
    }
}