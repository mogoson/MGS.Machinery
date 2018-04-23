/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankRockerEditor.cs
 *  Description  :  Custom editor for CrankRocker.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(CrankRocker), true)]
    [CanEditMultipleObjects]
    public class CrankRockerEditor : CrankLinkEditor
    {
        #region Field and Property
        protected new CrankRocker Target { get { return target as CrankRocker; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (!Target.IsIntact)
                return;

            if (Target.editMode == EditMode.Free)
            {
                DrawRotationHandle(Target.crank.transform);
                DrawPositionHandle(Target.link.transform);
                DrawPositionHandle(Target.rocker.transform);
                DrawPositionHandle(Target.joint);
            }
            else if (Target.editMode == EditMode.Hinge)
                DrawRotationHandle(Target.crank.transform);

            DrawCircleCap(Target.crank.transform.position, Target.crank.transform.rotation, AreaRadius);
            DrawSphereArrow(Target.crank.transform.position, Target.crank.transform.forward, ArrowLength, NodeSize, Blue, "Axis");

            var offset = Target.link.transform.position - Target.crank.transform.position;
            DrawSphereArrow(Target.crank.transform.position, offset, AreaRadius, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.crank.transform.position, Target.link.transform.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.link.transform.position, Target.joint.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.joint.position, Target.rocker.transform.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.rocker.transform.position, Target.crank.transform.position, NodeSize, Blue, string.Empty);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 160, Screen.height - 120, 150, 70);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Hinge Editor", "Window");
            DrawHingeEditorTool();

            GUILayout.BeginHorizontal("TextField");
            EditorGUI.BeginChangeCheck();
            Target.inertia = GUILayout.Toggle(Target.inertia, "Inertia");
            Target.restrict = GUILayout.Toggle(Target.restrict, "Restrict");
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(Target);
                MarkSceneDirty();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}