/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankSliderEditor.cs
 *  Description  :  Custom editor for CrankSlider.
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
            DrawArrow(Script.crank.transform.position, Script.crank.transform.forward, arrowLength, nodeSize, blue, "Axis");

            var offset = (Script.linkBar.transform.position - Script.crank.transform.position).normalized;
            DrawArrow(Script.crank.transform.position, offset, areaRadius, nodeSize, blue, string.Empty);
            DrawArrow(Script.crank.transform.position, Script.linkBar.transform.position, nodeSize, blue, string.Empty);
            DrawArrow(Script.linkBar.transform.position, Script.lsJoint.position, nodeSize, blue, string.Empty);

            var axis = Script.ProjectDirection(Script.lsJoint.forward).normalized;
            Handles.DrawLine(ZeroPoint, Script.lsJoint.position);
            DrawArrow(ZeroPoint, axis, arrowLength, nodeSize, blue, string.Empty);
            DrawArrow(ZeroPoint, -axis, arrowLength, nodeSize, blue, string.Empty);

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