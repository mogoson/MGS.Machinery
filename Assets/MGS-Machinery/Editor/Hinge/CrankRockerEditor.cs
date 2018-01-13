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
        protected new CrankRocker Script { get { return target as CrankRocker; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (!Script.IsIntact)
                return;

            if (Script.editMode == EditMode.Free)
            {
                DrawRotationHandle(Script.crank.transform);
                DrawPositionHandle(Script.linkBar.transform);
                DrawPositionHandle(Script.rocker.transform);
                DrawPositionHandle(Script.lrJoint);
            }
            else if (Script.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(Script.crank.transform);
            }

            DrawCircleCap(Script.crank.transform.position, Script.crank.transform.rotation, areaRadius);
            DrawArrow(Script.crank.transform.position, Script.crank.transform.forward, arrowLength, nodeSize, "Axis", blue);

            var offset = (Script.linkBar.transform.position - Script.crank.transform.position).normalized;
            DrawArrow(Script.crank.transform.position, offset, areaRadius, nodeSize, string.Empty, blue);
            DrawArrow(Script.crank.transform.position, Script.linkBar.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(Script.linkBar.transform.position, Script.lrJoint.position, nodeSize, string.Empty, blue);
            DrawArrow(Script.lrJoint.position, Script.rocker.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(Script.rocker.transform.position, Script.crank.transform.position, nodeSize, string.Empty, blue);

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
            Script.useInertia = GUILayout.Toggle(Script.useInertia, "Inertia");
            Script.useRestrict = GUILayout.Toggle(Script.useRestrict, "Restrict");
            if (EditorGUI.EndChangeCheck())
                MarkSceneDirty();
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}