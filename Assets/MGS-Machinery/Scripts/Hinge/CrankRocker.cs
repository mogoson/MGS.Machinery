/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankRocker.cs
 *  Description  :  Define CrankRocker component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Mathematics;
using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/CrankRocker")]
    [ExecuteInEditMode]
    public class CrankRocker : CrankLinkMechanism
    {
        #region Field and Property
        /// <summary>
        /// Rocker joint.
        /// </summary>
        public RockerMechanism rocker;

        /// <summary>
        /// Joint for link and rocker.
        /// </summary>
        public Transform joint;

        /// <summary>
        /// Use inertia to limit.
        /// </summary>
        [HideInInspector]
        public bool inertia = false;

        /// <summary>
        /// Use virtual restrict to limit.
        /// </summary>
        [HideInInspector]
        public bool restrict = false;

        /// <summary>
        /// All the joints of this mechanism are set intact.
        /// </summary>
        public bool IsIntact { get { return crank && link && rocker && joint; } }

        /// <summary>
        /// link bar start local position.
        /// </summary>
        protected Vector3 linkPosition;

        /// <summary>
        /// rocker start local position.
        /// </summary>
        protected Vector3 rockerPosition;

        /// <summary>
        /// Circle bese rocker.
        /// </summary>
        protected Circle rockerCircle;

        /// <summary>
        /// Circle base link bar.
        /// </summary>
        protected Circle linkCircle;

        /// <summary>
        /// Radius of the circle that bese link bar.
        /// </summary>
        protected double linkRadius = 1;

        /// <summary>
        /// Rocker and link bar joint is on the top of rocker on start.
        /// </summary>
        protected bool isTop = false;

#if UNITY_EDITOR
        /// <summary>
        /// This mechanism is initialized?
        /// </summary>
        private bool isInitialized = false;
#endif
        #endregion

        #region Protected Method
        protected void Awake()
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
                    Initialize();
                DriveLinkJoints();
            }
            else
                isInitialized = false;
        }
#endif
        /// <summary>
        /// Drive link bar and rocker.
        /// </summary>
        protected override void DriveLinkJoints()
        {
            //Rivet joints.
            joint.localEulerAngles = Vector3.zero;
            crank.transform.localPosition = Vector3.zero;
            link.transform.localPosition = linkPosition;
            rocker.transform.localPosition = rockerPosition;

            var linkPoint = CorrectPoint(GetLinkPosition());
            linkCircle = new Circle(linkPoint, linkRadius);
            var points = Planimetry.GetIntersections(linkCircle, rockerCircle);
            if (points == null)
            {
                IsLock = true;
                return;
            }

            IsLock = false;
            var point = Point.Zero;
            if (points.Count == 1)
                point = points[0];
            else
            {
                //Adapt restrict and intertia.
                var rID = restrict ? 1 : 0;
                if (inertia)
                    point = points[rID];
                else
                {
                    var isPointTop = points[0].y - points[1].y >= 0;
                    if (isPointTop == isTop)
                        point = points[rID];
                    else
                        point = points[1 - rID];
                }
            }
            joint.localPosition = new Vector3((float)point.x, (float)point.y);

            //Drive bars.
            rocker.Drive();
            link.Drive();
        }

        protected void Initialize()
        {
            //Correct crank.
            crank.transform.localEulerAngles = CorrectAngles(crank.transform.localEulerAngles);
            crank.Awake();

            //Save start local position.
            linkPosition = CorrectPosition(link.transform.localPosition);
            rockerPosition = CorrectPosition(rocker.transform.localPosition);

            //Initialize CrankRocker mathematical model.
            var rockerPoint = CorrectPoint(rocker.transform.localPosition);
            var lrJointPoint = CorrectPoint(joint.localPosition);
            var rockerRadius = Point.Distance(rockerPoint, lrJointPoint);
            var linkPoint = CorrectPoint(GetLinkPosition());
            isTop = lrJointPoint.y - rockerPoint.y >= 0;
            rockerCircle = new Circle(rockerPoint, rockerRadius);
            linkRadius = Point.Distance(linkPoint, lrJointPoint);
        }
        #endregion
    }
}