/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerHingeEditor.cs
 *  Description  :  Custom editor for RockerHinge.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/16/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    [CustomEditor(typeof(RockerHinge), true)]
    [CanEditMultipleObjects]
    public class RockerHingeEditor : MechanismEditor
    {
        #region Property and Field
        protected RockerHinge script { get { return target as RockerHinge; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(script.transform.position, script.transform.rotation, areaRadius);
            Handles.DrawWireArc(script.transform.position, script.transform.right, script.axis, -180, areaRadius);
            DrawArrow(script.transform.position, script.transform.up, areaRadius, nodeSize, string.Empty, blue);

            var axisStart = script.transform.position - script.axis * areaRadius;
            var axisEnd = script.transform.position + script.axis * arrowLength;
            DrawArrow(axisStart, axisEnd, nodeSize, "Axis", blue);

            if (script.rockJoint)
                DrawArrow(script.transform.position, script.rockJoint.forward, areaRadius, nodeSize, string.Empty, blue);
        }
        #endregion
    }
}