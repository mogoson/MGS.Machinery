/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  LimitCrankEditor.cs
 *  Description  :  Custom editor for LimitCrank.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/2/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    [CustomEditor(typeof(LimitCrank), true)]
    [CanEditMultipleObjects]
    public class LimitCrankEditor : FreeCrankEditor
    {
        #region Property and Field
        protected new LimitCrank Script { get { return target as LimitCrank; } }
        #endregion

        #region Protected Method
        protected override void DrawArea()
        {
            var minAxis = Quaternion.AngleAxis(Script.minAngle, Axis) * ZeroAxis;
            var maxAxis = Quaternion.AngleAxis(Script.maxAngle, Axis) * ZeroAxis;

            DrawArrow(Script.transform.position, minAxis, arrowLength, nodeSize, "Min", blue);
            DrawArrow(Script.transform.position, maxAxis, arrowLength, nodeSize, "Max", blue);

            Handles.color = transparentBlue;
            Handles.DrawSolidArc(Script.transform.position, Axis, minAxis, Script.maxAngle - Script.minAngle, areaRadius);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Script.maxAngle = Mathf.Clamp(Script.maxAngle, Script.minAngle, float.MaxValue);
        }
        #endregion
    }
}