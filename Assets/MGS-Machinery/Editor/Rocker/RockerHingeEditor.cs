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
        protected RockerHinge Script { get { return target as RockerHinge; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            DrawSphereCap(Script.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(Script.transform.position, Script.transform.rotation, areaRadius);
            Handles.DrawWireArc(Script.transform.position, Script.transform.right, Script.Axis, -180, areaRadius);
            DrawArrow(Script.transform.position, Script.transform.up, areaRadius, nodeSize, string.Empty, blue);

            var axisStart = Script.transform.position - Script.Axis * areaRadius;
            var axisEnd = Script.transform.position + Script.Axis * arrowLength;
            DrawArrow(axisStart, axisEnd, nodeSize, "Axis", blue);

            if (Script.rockJoint)
                DrawArrow(Script.transform.position, Script.rockJoint.forward, areaRadius, nodeSize, string.Empty, blue);
        }
        #endregion
    }
}