/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerHingeEditor.cs
 *  Description  :  Custom editor for RockerHinge.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
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
            DrawArrow(Script.transform.position, Script.transform.up, areaRadius, nodeSize, blue, string.Empty);

            var axisStart = Script.transform.position - Script.Axis * areaRadius;
            var axisEnd = Script.transform.position + Script.Axis * arrowLength;
            DrawArrow(axisStart, axisEnd, nodeSize, blue, "Axis");

            if (Script.rockJoint)
                DrawArrow(Script.transform.position, Script.rockJoint.forward, areaRadius, nodeSize, blue, string.Empty);
        }
        #endregion
    }
}