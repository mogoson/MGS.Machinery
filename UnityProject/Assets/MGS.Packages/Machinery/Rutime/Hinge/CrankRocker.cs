/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankRocker.cs
 *  Description  :  Define CrankRocker component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Mathematics;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Crank rocker hinge.
    /// </summary>
    [ExecuteInEditMode]
    public class CrankRocker : CrankLinkMechanism
    {
        #region Field and Property
        /// <summary>
        /// Rocker joint.
        /// </summary>
        [Tooltip("Rocker joint.")]
        public RockerMechanism rocker;

        /// <summary>
        /// Joint for link and rocker.
        /// </summary>
        [Tooltip("Joint for link and rocker.")]
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
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (rocker.IsStuck)
                {
                    return true;
                }
                return base.IsStuck;
            }
        }

        /// <summary>
        /// All the joints of this mechanism are set intact.
        /// </summary>
        public override bool IsIntact { get { return crank && link && rocker && joint; } }

        /// <summary>
        /// Radius of the circle that bese link.
        /// </summary>
        protected double linkRadius = 0;

        /// <summary>
        /// Radius of the circle that bese rocker.
        /// </summary>
        protected double rockerRadius = 0;

        /// <summary>
        /// Joint of link and rocker is on the top of rocker at start?
        /// </summary>
        protected bool isTop = false;
        #endregion

        #region Protected Method
        /// <summary>
        /// Drive joints those link with this mechanism.
        /// </summary>
        /// <returns>Drive joints is effective?</returns>
        protected override bool DriveLinkJoints()
        {
            //Rivet joints.
            crank.transform.localPosition = CorrectPosition(crank.transform.localPosition);
            link.transform.localPosition = CorrectPosition(link.transform.localPosition);
            rocker.transform.localPosition = CorrectPosition(rocker.transform.localPosition);

            var linkCircle = new Circle(CorrectPoint(GetLinkPosition()), linkRadius);
            var rockerCircle = new Circle(CorrectPoint(rocker.transform.localPosition), rockerRadius);

            var vectors = Geometry.GetIntersections(linkCircle, rockerCircle);
            if (vectors == null)
            {
                return false;
            }

            var vector = Vector2D.Zero;
            if (vectors.Count == 1)
            {
                vector = vectors[0];
            }
            else
            {
                //Adapt restrict and intertia.
                var rID = restrict ? 1 : 0;
                if (inertia)
                {
                    vector = vectors[rID];
                }
                else
                {
                    var isVector2Top = vectors[0].y - vectors[1].y >= 0;
                    if (isVector2Top == isTop)
                    {
                        vector = vectors[rID];
                    }
                    else
                    {
                        vector = vectors[1 - rID];
                    }
                }
            }

            joint.localPosition = new Vector3((float)vector.x, (float)vector.y);
            link.Drive(0, DriveMode.Ignore);
            rocker.Drive(0, DriveMode.Ignore);
            return true;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            //Correct crank.
            crank.transform.localEulerAngles = CorrectAngles(crank.transform.localEulerAngles);
            crank.Initialize();

            //Initialize CrankRocker mathematical model.
            var rockerPoint = CorrectPoint(rocker.transform.localPosition);
            var lrJointPoint = CorrectPoint(joint.localPosition);

            linkRadius = Vector2D.Distance(CorrectPoint(GetLinkPosition()), lrJointPoint);
            rockerRadius = Vector2D.Distance(rockerPoint, lrJointPoint);
            isTop = lrJointPoint.y - rockerPoint.y >= 0;
        }
        #endregion
    }
}