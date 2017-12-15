/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  CrankRockerEditor.cs
 *  Description  :  Custom editor for CrankRocker.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/3/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    [CustomEditor(typeof(CrankRocker), true)]
    [CanEditMultipleObjects]
    public class CrankRockerEditor : CrankLinkEditor
    {
        #region Property and Field
        protected new CrankRocker script { get { return target as CrankRocker; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (!script.isIntact)
                return;

            if (script.editMode == EditMode.Free)
            {
                DrawRotationHandle(script.crank.transform);
                DrawPositionHandle(script.linkBar.transform);
                DrawPositionHandle(script.rocker.transform);
                DrawPositionHandle(script.lrJoint);
            }
            else if (script.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(script.crank.transform);
            }

            DrawCircleCap(script.crank.transform.position, script.crank.transform.rotation, areaRadius);
            DrawArrow(script.crank.transform.position, script.crank.transform.forward, arrowLength, nodeSize, "Axis", blue);

            var offset = (script.linkBar.transform.position - script.crank.transform.position).normalized;
            DrawArrow(script.crank.transform.position, offset, areaRadius, nodeSize, string.Empty, blue);
            DrawArrow(script.crank.transform.position, script.linkBar.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.linkBar.transform.position, script.lrJoint.position, nodeSize, string.Empty, blue);
            DrawArrow(script.lrJoint.position, script.rocker.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.rocker.transform.position, script.crank.transform.position, nodeSize, string.Empty, blue);

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
            script.useInertia = GUILayout.Toggle(script.useInertia, "Inertia");
            script.useRestrict = GUILayout.Toggle(script.useRestrict, "Restrict");
            if (EditorGUI.EndChangeCheck())
                MarkSceneDirty();
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}