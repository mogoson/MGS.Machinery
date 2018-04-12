/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankRocker.cs
 *  Description  :  Define CrankRocker component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
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
        /// Rocker of crankrocker.
        /// </summary>
        public RockerMechanism rocker;

        /// <summary>
        /// Joint of link bar and rocker.
        /// </summary>
        public Transform lrJoint;

        /// <summary>
        /// Use inertia to limit crankrocker.
        /// </summary>
        [HideInInspector]
        public bool useInertia = false;

        /// <summary>
        /// Use virtual restrict to limit crankrocker.
        /// </summary>
        [HideInInspector]
        public bool useRestrict = false;

        /// <summary>
        /// All mechanism is set Intact.
        /// </summary>
        public bool IsIntact { get { return crank && linkBar && rocker && lrJoint; } }

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
        protected double linkRadius;

        /// <summary>
        /// Rocker and link bar joint is on the top of rocker on start.
        /// </summary>
        protected bool isTop;
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
        /// <summary>
        /// Drive bars on editor node.
        /// </summary>
        protected virtual void Update()
        {
            if (Application.isPlaying)
                return;

            if (IsIntact)
            {
                if (!IsInitialized)
                    Initialize();

                DriveLinkBars();
            }
            else
                IsInitialized = false;
        }
#endif
        /// <summary>
        /// Drive link bar and rocker.
        /// </summary>
        protected override void DriveLinkBars()
        {
            //Rivet joints.
            lrJoint.localEulerAngles = Vector3.zero;
            crank.transform.localPosition = Vector3.zero;
            linkBar.transform.localPosition = linkPosition;
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
                var rID = useRestrict ? 1 : 0;
                if (useInertia)
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
            lrJoint.localPosition = new Vector3((float)point.x, (float)point.y);

            //Drive bars.
            rocker.DriveMechanism();
            linkBar.DriveMechanism();
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

            //Save start local position.
            linkPosition = CorrectPosition(linkBar.transform.localPosition);
            rockerPosition = CorrectPosition(rocker.transform.localPosition);

            //Initialize CrankRocker mathematical model.
            var rockerPoint = CorrectPoint(rocker.transform.localPosition);
            var lrJointPoint = CorrectPoint(lrJoint.localPosition);
            var rockerRadius = Planimetry.GetDistance(rockerPoint, lrJointPoint);
            var linkPoint = CorrectPoint(GetLinkPosition());
            isTop = lrJointPoint.y - rockerPoint.y >= 0;
            rockerCircle = new Circle(rockerPoint, rockerRadius);
            linkRadius = Planimetry.GetDistance(linkPoint, lrJointPoint);
            IsInitialized = true;
        }
        #endregion
    }
}