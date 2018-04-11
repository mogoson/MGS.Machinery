/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CenterTelescopicJointEditor.cs
 *  Description  :  Custom editor for CeTelescopicJoint.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(CeTelescopicJoint), true)]
    [CanEditMultipleObjects]
    public class CenterTelescopicJointEditor : TelescopicJointEditor
    {
        #region Protected Method
        protected override void DrawStroke()
        {
            DrawSphereArrow(ZeroPoint, Axis, Target.stroke, NodeSize, Blue, "+Stroke");
            DrawSphereArrow(ZeroPoint, -Axis, Target.stroke, NodeSize, Blue, "-Stroke");
        }
        #endregion
    }
}