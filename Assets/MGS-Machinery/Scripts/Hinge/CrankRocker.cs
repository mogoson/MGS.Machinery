/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: CrankRocker.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/25/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         CrankRocker              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     12/25/2015       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using Math.Planimetry;
    using UnityEngine;

    [AddComponentMenu("Developer/Machinery/CrankRocker")]
    [ExecuteInEditMode]
    public class CrankRocker : CrankLinkMechanism
    {
        #region Property and Field
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
        public bool inertia = false;

        /// <summary>
        /// Use virtual restrict to limit crankrocker.
        /// </summary>
        [HideInInspector]
        public bool restrict = false;

        /// <summary>
        /// All mechanism is set Intact.
        /// </summary>
        public bool isIntact { get { return crank && linkBar && rocker && lrJoint; } }

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
        protected bool top;
        #endregion

        #region Protected Method
        protected void Awake()
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                Initialize();
#else
            Initialize();
#endif
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
                if (!initialized)
                    Initialize();
                DriveLinkBars();
            }
            else
                initialized = false;
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
                Lock = true;
                return;
            }
            Lock = false;
            Point point;
            if (points.Count == 1)
                point = points[0];
            else
            {
                //Adapt intertia and restrict.
                var rID = restrict ? 1 : 0;
                point = inertia ? points[rID] : (points[0].y - points[1].y >= 0 == top ? points[rID] : points[1 - rID]);
            }
            lrJoint.localPosition = new Vector3((float)point.x, (float)point.y, 0);

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
            top = lrJointPoint.y - rockerPoint.y >= 0;
            rockerCircle = new Circle(rockerPoint, rockerRadius);
            linkRadius = Planimetry.GetDistance(linkPoint, lrJointPoint);
            initialized = true;
        }
        #endregion
    }
}