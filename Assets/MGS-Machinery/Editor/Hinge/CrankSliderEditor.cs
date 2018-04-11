/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankSliderEditor.cs
 *  Description  :  Custom editor for CrankSlider.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(CrankSlider), true)]
    [CanEditMultipleObjects]
    public class CrankSliderEditor : CrankLinkEditor
    {
        #region Field and Property
        protected new CrankSlider Target { get { return target as CrankSlider; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                if (Application.isPlaying)
                    return Target.transform.TransformPoint(Target.LSJointPosition);
                else
                    return Target.lsJoint.position;
            }
        }
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
                DrawPositionHandle(Target.linkBar.transform);
                DrawPositionHandle(Target.lsJoint);
                DrawRotationHandle(Target.lsJoint);
            }
            else if (Target.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(Target.crank.transform);
            }

            DrawSphereCap(Target.crank.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.crank.transform.position, Target.crank.transform.rotation, AreaRadius);
            DrawSphereArrow(Target.crank.transform.position, Target.crank.transform.forward, ArrowLength, NodeSize, Blue, "Axis");

            var offset = Target.linkBar.transform.position - Target.crank.transform.position;
            DrawSphereArrow(Target.crank.transform.position, offset, AreaRadius, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.crank.transform.position, Target.linkBar.transform.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.linkBar.transform.position, Target.lsJoint.position, NodeSize, Blue, string.Empty);

            var axis = Target.ProjectDirection(Target.lsJoint.forward);
            Handles.DrawLine(ZeroPoint, Target.lsJoint.position);
            DrawSphereArrow(ZeroPoint, axis, ArrowLength, NodeSize, Blue, string.Empty);
            DrawSphereArrow(ZeroPoint, -axis, ArrowLength, NodeSize, Blue, string.Empty);

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