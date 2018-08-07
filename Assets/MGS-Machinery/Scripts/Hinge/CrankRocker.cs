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
    /// <summary>
    /// Crank rocker hinge.
    /// </summary>
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
        public override bool IsIntact { get { return crank && link && rocker && joint; } }

        /// <summary>
        /// Start local position of link.
        /// </summary>
        protected Vector3 linkPosition;

        /// <summary>
        /// Start local position of rocker.
        /// </summary>
        protected Vector3 rockerPosition;

        /// <summary>
        /// Circle bese rocker.
        /// </summary>
        protected Circle rockerCircle;

        /// <summary>
        /// Circle base link.
        /// </summary>
        protected Circle linkCircle;

        /// <summary>
        /// Radius of the circle that bese link.
        /// </summary>
        protected double linkRadius = 1;

        /// <summary>
        /// Joint of link and rocker is on the top of rocker at start?
        /// </summary>
        protected bool isTop = false;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive joints those link with this mechanism.
        /// </summary>
        protected override void DriveLinkJoints()
        {
            //Rivet joints.
            crank.transform.localPosition = CorrectPosition(crank.transform.localPosition);
            link.transform.localPosition = linkPosition;
            rocker.transform.localPosition = CorrectPosition(rocker.transform.localPosition);
            joint.localEulerAngles = Vector3.zero;

            var linkPoint = CorrectPoint(GetLinkPosition());
            linkCircle = new Circle(linkPoint, linkRadius);

            var rockerPoint = CorrectPoint(rocker.transform.localPosition);
            rockerCircle = new Circle(rockerPoint, rockerCircle.r);

            var vectors = Planimetry.GetIntersections(linkCircle, rockerCircle);
            if (vectors == null)
            {
                IsLock = true;
                return;
            }

            IsLock = false;
            var vector = Vector.Zero;
            if (vectors.Count == 1)
                vector = vectors[0];
            else
            {
                //Adapt restrict and intertia.
                var rID = restrict ? 1 : 0;
                if (inertia)
                    vector = vectors[rID];
                else
                {
                    var isVectorTop = vectors[0].y - vectors[1].y >= 0;
                    if (isVectorTop == isTop)
                        vector = vectors[rID];
                    else
                        vector = vectors[1 - rID];
                }
            }

            joint.localPosition = new Vector3((float)vector.x, (float)vector.y);
            link.Drive(0, DriveType.Ignore);
            rocker.Drive(0, DriveType.Ignore);
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
            crank.Initialize();

            //Save start local position.
            linkPosition = CorrectPosition(link.transform.localPosition);
            rockerPosition = CorrectPosition(rocker.transform.localPosition);

            //Initialize CrankRocker mathematical model.
            var rockerPoint = CorrectPoint(rocker.transform.localPosition);
            var lrJointPoint = CorrectPoint(joint.localPosition);

            isTop = lrJointPoint.y - rockerPoint.y >= 0;
            rockerCircle = new Circle(rockerPoint, Vector.Distance(rockerPoint, lrJointPoint));
            linkRadius = Vector.Distance(CorrectPoint(GetLinkPosition()), lrJointPoint);
        }
        #endregion
    }
}