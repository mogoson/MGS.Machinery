/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankHelix.cs
 *  Description  :  Define CrankHelix component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  8/12/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.CurvePath;
using UnityEngine;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Crank helix hinge.
    /// </summary>
    [AddComponentMenu("Mogoson/Machinery/CrankSlider")]
    public class CrankHelix : Mechanism
    {
        #region Field and Property
        public CrankMechanism crank;

        public HelixPath helix;

        public Transform joint;
        #endregion

        #region Private Method

        #endregion

        #region Public Method
        public override void Drive(float velocity, DriveType type)
        {
            crank.Drive(velocity, type);

            var point = helix.GetPointAt(crank.Angle * Mathf.Deg2Rad);
            var jointPos = transform.InverseTransformPoint(point);
            joint.localPosition = new Vector3(joint.localPosition.x, joint.localPosition.y, jointPos.z);
        }
        #endregion
    }
}