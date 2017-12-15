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
        protected new CrankSlider script { get { return target as CrankSlider; } }

        protected Vector3 zeroPoint
        {
            get
            {
                if (Application.isPlaying)
                    return script.transform.TransformPoint(script.lsJointPosition);
                else
                    return script.lsJoint.position;
            }
        }
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
                DrawPositionHandle(script.lsJoint);
                DrawRotationHandle(script.lsJoint);
            }
            else if (script.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(script.crank.transform);
            }

            DrawSphereCap(script.crank.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(script.crank.transform.position, script.crank.transform.rotation, areaRadius);
            DrawArrow(script.crank.transform.position, script.crank.transform.forward, arrowLength, nodeSize, "Axis", blue);

            var offset = (script.linkBar.transform.position - script.crank.transform.position).normalized;
            DrawArrow(script.crank.transform.position, offset, areaRadius, nodeSize, string.Empty, blue);
            DrawArrow(script.crank.transform.position, script.linkBar.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.linkBar.transform.position, script.lsJoint.position, nodeSize, string.Empty, blue);

            var axis = script.ProjectDirection(script.lsJoint.forward).normalized;
            Handles.DrawLine(zeroPoint, script.lsJoint.position);
            DrawArrow(zeroPoint, axis, arrowLength, nodeSize, string.Empty, blue);
            DrawArrow(zeroPoint, -axis, arrowLength, nodeSize, string.Empty, blue);

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