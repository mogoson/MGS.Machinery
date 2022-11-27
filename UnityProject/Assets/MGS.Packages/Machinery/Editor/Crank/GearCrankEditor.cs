/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GearCrankEditor.cs
 *  Description  :  Custom editor for GearCrank.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/12/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machinery
{
    [CustomEditor(typeof(GearCrank), true)]
    [CanEditMultipleObjects]
    public class GearCrankEditor : FreeCrankEditor
    {
        #region Field and Property
        protected new GearCrank Target { get { return target as GearCrank; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, Target.radius);
            DrawAdaptiveSphereArrow(Target.transform.position, Axis, ArrowLength, NodeSize, "Axis");

            var end = Target.transform.position + ZeroAxis * Target.radius;
            Handles.DrawLine(Target.transform.position, end);
            DrawAdaptiveSphereArrow(end, ZeroAxis, FixedArrowLength, NodeSize, "Zero");
            DrawSphereArrow(Target.transform.position, Target.transform.up, Target.radius, NodeSize);
            DrawArea();

            Handles.color = Blue;
            DrawRockers(Target.rockers, Target.transform);
        }

        protected override void DrawArea()
        {
            Handles.color = TransparentBlue;
            Handles.DrawSolidDisc(Target.transform.position, Axis, Target.radius);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                Target.radius = Mathf.Max(Mathf.Epsilon, Target.radius);
            }
        }
        #endregion
    }
}