/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LimitCrankEditor.cs
 *  Description  :  Custom editor for LimitCrank.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(LimitCrank), true)]
    [CanEditMultipleObjects]
    public class LimitCrankEditor : FreeCrankEditor
    {
        #region Field and Property
        protected new LimitCrank Target { get { return target as LimitCrank; } }
        #endregion

        #region Protected Method
        protected override void DrawArea()
        {
            var minAxis = Quaternion.AngleAxis(Target.minAngle, Axis) * ZeroAxis;
            var maxAxis = Quaternion.AngleAxis(Target.maxAngle, Axis) * ZeroAxis;

            Handles.color = TransparentBlue;
            Handles.DrawSolidArc(Target.transform.position, Axis, minAxis, Target.maxAngle - Target.minAngle, AreaRadius);

            DrawSphereArrow(Target.transform.position, minAxis, ArrowLength, NodeSize, Blue, "Min");
            DrawSphereArrow(Target.transform.position, maxAxis, ArrowLength, NodeSize, Blue, "Max");
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
                Target.maxAngle = Mathf.Clamp(Target.maxAngle, Target.minAngle, float.MaxValue);
        }
        #endregion
    }
}