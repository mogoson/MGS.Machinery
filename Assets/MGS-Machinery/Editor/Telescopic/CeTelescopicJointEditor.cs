/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  CeTelescopicJointEditor.cs
 *  Description  :  Custom editor for CeTelescopicJoint.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/2/2017
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
            DrawArrow(ZeroPoint, Axis, Script.stroke, nodeSize, "+Stroke", blue);
            DrawArrow(ZeroPoint, -Axis, Script.stroke, nodeSize, "-Stroke", blue);
        }
        #endregion
    }
}