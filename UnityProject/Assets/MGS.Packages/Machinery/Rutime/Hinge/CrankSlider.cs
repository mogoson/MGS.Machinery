/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankSlider.cs
 *  Description  :  Define CrankSlider component.
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
    /// Crank slider hinge.
    /// </summary>
    [ExecuteInEditMode]
    public class CrankSlider : CrankLinkMechanism
    {
        #region Field and Property
        /// <summary>
        /// Slider joint.
        /// </summary>
        [Tooltip("Slider joint.")]
        public Transform slider;

        /// <summary>
        /// All the joints of this mechanism are set intact.
        /// </summary>
        public override bool IsIntact { get { return crank && link && slider; } }

        /// <summary>
        /// Radius of the circle that base link.
        /// </summary>
		protected double linkRadius = 0;

        /// <summary>
        /// Slider is on the right of link at start.
        /// </summary>
		protected bool isRight = false;
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
            slider.localEulerAngles = CorrectJointAngles(slider.localEulerAngles);

            var linkCircle = new Circle(CorrectPoint(GetLinkPosition()), linkRadius);
            var slideLine = Line.FromPoints(CorrectPoint(slider.localPosition),
                CorrectPoint(slider.localPosition + transform.InverseTransformDirection(ProjectDirection(slider.forward))));

            var vectors = Geometry.GetIntersections(linkCircle, slideLine);
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
                vector = isRight ? vectors[0] : vectors[1];
            }

            slider.localPosition = new Vector3((float)vector.x, (float)vector.y);
            link.Drive(0, DriveMode.Ignore);
            return true;
        }

        /// <summary>
        /// Clear angles z and set y to 90.
        /// </summary>
        /// <param name="angles">Local euler angles.</param>
        /// <returns>Correct angles.</returns>
        protected Vector3 CorrectJointAngles(Vector3 angles)
        {
            return new Vector3(angles.x, 90);
        }

        /// <summary>
        /// Project direction vector on plane(Normal is transform.forward).
        /// </summary>
        /// <param name="direction">World space direction.</param>
        /// <returns>Project direction.</returns>
        protected Vector3 ProjectDirection(Vector3 direction)
        {
            var project = Vector3.ProjectOnPlane(direction, transform.forward);
            if (project == Vector3.zero)
            {
                project = transform.right;
            }
            return project;
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

            //Initialize CrankSlider mathematical model.
            var lsJointPoint = CorrectPoint(slider.localPosition);
            var linkPoint = CorrectPoint(GetLinkPosition());

            linkRadius = Vector2D.Distance(linkPoint, lsJointPoint);
            isRight = lsJointPoint.x - linkPoint.x >= 0;
        }
        #endregion
    }
}