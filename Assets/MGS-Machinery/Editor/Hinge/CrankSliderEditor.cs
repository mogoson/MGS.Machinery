/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  CrankSliderEditor.cs
 *  Description  :  Custom editor for CrankSlider.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/13/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    [CustomEditor(typeof(CrankSlider), true)]
    [CanEditMultipleObjects]
    public class CrankSliderEditor : CrankLinkEditor
    {
        #region Property and Field
        protected new CrankSlider Script { get { return target as CrankSlider; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                if (Application.isPlaying)
                    return Script.transform.TransformPoint(Script.LSJointPosition);
                else
                    return Script.lsJoint.position;
            }
        }
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
                DrawPositionHandle(Script.lsJoint);
                DrawRotationHandle(Script.lsJoint);
            }
            else if (Script.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(Script.crank.transform);
            }

            DrawSphereCap(Script.crank.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(Script.crank.transform.position, Script.crank.transform.rotation, areaRadius);
            DrawArrow(Script.crank.transform.position, Script.crank.transform.forward, arrowLength, nodeSize, "Axis", blue);

            var offset = (Script.linkBar.transform.position - Script.crank.transform.position).normalized;
            DrawArrow(Script.crank.transform.position, offset, areaRadius, nodeSize, string.Empty, blue);
            DrawArrow(Script.crank.transform.position, Script.linkBar.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(Script.linkBar.transform.position, Script.lsJoint.position, nodeSize, string.Empty, blue);

            var axis = Script.ProjectDirection(Script.lsJoint.forward).normalized;
            Handles.DrawLine(ZeroPoint, Script.lsJoint.position);
            DrawArrow(ZeroPoint, axis, arrowLength, nodeSize, string.Empty, blue);
            DrawArrow(ZeroPoint, -axis, arrowLength, nodeSize, string.Empty, blue);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 160, Screen.height - 95, 150, 45);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Hinge Editor", "Window");
            DrawHingeEditorTool();
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion
    }
}