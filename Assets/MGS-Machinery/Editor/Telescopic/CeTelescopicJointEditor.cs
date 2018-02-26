/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CeTelescopicJointEditor.cs
 *  Description  :  Custom editor for CeTelescopicJoint.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;

namespace Developer.Machinery
{
    [CustomEditor(typeof(CeTelescopicJoint), true)]
    [CanEditMultipleObjects]
    public class CeTelescopicJointEditor : TelescopicJointEditor
    {
        #region Protected Method
        protected override void DrawStroke()
        {
            DrawArrow(ZeroPoint, Axis, Script.stroke, nodeSize, blue, "+Stroke");
            DrawArrow(ZeroPoint, -Axis, Script.stroke, nodeSize, blue, "-Stroke");
        }
        #endregion
    }
}