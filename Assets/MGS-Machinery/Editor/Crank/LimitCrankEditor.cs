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
        protected new LimitCrank script { get { return target as LimitCrank; } }
        #endregion

        #region Protected Method
        protected override void DrawArea()
        {
            var minAxis = Quaternion.AngleAxis(script.minAngle, axis) * zeroAxis;
            var maxAxis = Quaternion.AngleAxis(script.maxAngle, axis) * zeroAxis;

            DrawArrow(script.transform.position, minAxis, arrowLength, nodeSize, "Min", blue);
            DrawArrow(script.transform.position, maxAxis, arrowLength, nodeSize, "Max", blue);

            Handles.color = transparentBlue;
            Handles.DrawSolidArc(script.transform.position, axis, minAxis, script.maxAngle - script.minAngle, areaRadius);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            script.maxAngle = Mathf.Clamp(script.maxAngle, script.minAngle, float.MaxValue);
        }
        #endregion
    }
}