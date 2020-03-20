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
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/20/2018
 *  Description  :  Optimize display of crank and axis.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machinery
{
    [CustomEditor(typeof(CrankSlider), true)]
    [CanEditMultipleObjects]
    public class CrankSliderEditor : CrankLinkEditor
    {
        #region Field and Property
        protected new CrankSlider Target { get { return target as CrankSlider; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (!Target.IsIntact)
            {
                return;
            }

            if (Target.editMode == EditMode.Free)
            {
                DrawRotationHandle(Target.crank.transform);
                DrawPositionHandle(Target.crank.transform);
                DrawPositionHandle(Target.link.transform);
                DrawPositionHandle(Target.slider);
                DrawRotationHandle(Target.slider);

                Target.crank.transform.localPosition = CorrectPosition(Target.crank.transform.localPosition);
                Target.crank.transform.localEulerAngles = CorrectAngles(Target.crank.transform.localEulerAngles);
                Target.link.transform.localPosition = CorrectPosition(Target.link.transform.localPosition);
                Target.slider.localPosition = CorrectPosition(Target.slider.localPosition);
                Target.slider.localEulerAngles = CorrectJointAngles(Target.slider.localEulerAngles);
            }
            else if (Target.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(Target.crank.transform);
                Target.crank.transform.localEulerAngles = CorrectAngles(Target.crank.transform.localEulerAngles);
            }

            var radius = Vector3.Distance(Target.crank.transform.position, Target.link.transform.position);
            DrawAdaptiveSphereCap(Target.crank.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.crank.transform.position, Target.crank.transform.rotation, radius);
            DrawAdaptiveSphereArrow(Target.crank.transform.position, Target.crank.transform.forward, ArrowLength, NodeSize, "Axis");

            DrawSphereArrow(Target.crank.transform.position, Target.link.transform.position, NodeSize);
            DrawSphereArrow(Target.link.transform.position, Target.slider.position, NodeSize);

            var axis = ProjectDirection(Target.slider.forward);
            Handles.DrawLine(Target.slider.position, Target.slider.position);
            DrawSphereArrow(Target.slider.position, axis, ArrowLength, NodeSize);
            DrawSphereArrow(Target.slider.position, -axis, ArrowLength, NodeSize);

            DrawSceneGUI();
        }

        protected void DrawSceneGUI()
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
            {
                project = Target.transform.right;
            }
            return project;
        }
        #endregion
    }
}