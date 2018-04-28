/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankSliderEditor.cs
 *  Description  :  Custom editor for CrankSlider.
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
                    return Target.transform.TransformPoint(Target.JointPosition);
                else
                    return Target.joint.position;
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
                DrawPositionHandle(Target.link.transform);
                DrawPositionHandle(Target.joint);
                DrawRotationHandle(Target.joint);

                Target.crank.transform.localPosition = Vector3.zero;
                Target.crank.transform.localEulerAngles = CorrectAngles(Target.crank.transform.localEulerAngles);
                Target.link.transform.localPosition = CorrectPosition(Target.link.transform.localPosition);
                Target.joint.localPosition = CorrectPosition(Target.joint.localPosition);
                Target.joint.localEulerAngles = CorrectJointAngles(Target.joint.localEulerAngles);
            }
            else if (Target.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(Target.crank.transform);
                Target.crank.transform.localEulerAngles = CorrectAngles(Target.crank.transform.localEulerAngles);
            }

            DrawSphereCap(Target.crank.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.crank.transform.position, Target.crank.transform.rotation, AreaRadius);
            DrawSphereArrow(Target.crank.transform.position, Target.crank.transform.forward, ArrowLength, NodeSize, Blue, "Axis");

            var offset = Target.link.transform.position - Target.crank.transform.position;
            DrawSphereArrow(Target.crank.transform.position, offset, AreaRadius, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.crank.transform.position, Target.link.transform.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.link.transform.position, Target.joint.position, NodeSize, Blue, string.Empty);

            var axis = ProjectDirection(Target.joint.forward);
            Handles.DrawLine(ZeroPoint, Target.joint.position);
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

        protected Vector3 CorrectJointAngles(Vector3 angles)
        {
            return new Vector3(angles.x, 90);
        }

        protected Vector3 ProjectDirection(Vector3 direction)
        {
            var project = Vector3.ProjectOnPlane(direction, Target.transform.forward);
            if (project == Vector3.zero)
                project = Target.transform.right;
            return project;
        }
        #endregion
    }
}