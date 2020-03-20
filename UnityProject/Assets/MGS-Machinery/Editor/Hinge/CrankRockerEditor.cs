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
            {
                return;
            }

            if (Target.editMode == EditMode.Free)
            {
                DrawRotationHandle(Target.crank.transform);
                DrawPositionHandle(Target.crank.transform);
                DrawPositionHandle(Target.link.transform);
                DrawPositionHandle(Target.rocker.transform);
                DrawPositionHandle(Target.joint);

                Target.crank.transform.localPosition = CorrectPosition(Target.crank.transform.localPosition);
                Target.crank.transform.localEulerAngles = CorrectAngles(Target.crank.transform.localEulerAngles);
                Target.link.transform.localPosition = CorrectPosition(Target.link.transform.localPosition);
                Target.rocker.transform.localPosition = CorrectPosition(Target.rocker.transform.localPosition);
                Target.joint.localPosition = CorrectPosition(Target.joint.transform.localPosition);
                Target.joint.localEulerAngles = Vector3.zero;
            }
            else if (Target.editMode == EditMode.Hinge)
            {
                DrawRotationHandle(Target.crank.transform);
                Target.crank.transform.localEulerAngles = CorrectAngles(Target.crank.transform.localEulerAngles);
            }

            var radius = Vector3.Distance(Target.crank.transform.position, Target.link.transform.position);
            DrawCircleCap(Target.crank.transform.position, Target.crank.transform.rotation, radius);
            DrawAdaptiveSphereArrow(Target.crank.transform.position, Target.crank.transform.forward, ArrowLength, NodeSize, "Axis");

            DrawSphereArrow(Target.crank.transform.position, Target.link.transform.position, NodeSize);
            DrawSphereArrow(Target.link.transform.position, Target.joint.position, NodeSize);
            DrawSphereArrow(Target.joint.position, Target.rocker.transform.position, NodeSize);
            DrawSphereArrow(Target.rocker.transform.position, Target.crank.transform.position, NodeSize);

            DrawSceneGUI();
        }

        protected virtual void DrawSceneGUI()
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