/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GearCrankEditor.cs
 *  Description  :  Custom editor for GearCrank.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/12/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
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
            DrawSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, Target.radius);
            DrawSphereArrow(Target.transform.position, Axis, ArrowLength, NodeSize, Blue, "Axis");
            DrawSphereArrow(Target.transform.position, ZeroAxis, Target.radius + 0.25f, NodeSize, Blue, "Zero");
            DrawSphereArrow(Target.transform.position, Target.transform.up, Target.radius, NodeSize, Blue, string.Empty);
            DrawArea();
            DrawRockers(Target.rockers, Target.transform, Blue);
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
                Target.radius = Mathf.Max(Mathf.Epsilon, Target.radius);
        }
        #endregion
    }
}