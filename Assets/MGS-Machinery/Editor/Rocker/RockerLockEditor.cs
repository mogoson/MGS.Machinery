/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerLockEditor.cs
 *  Description  :  Custom editor for RockerLock.
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
    [CustomEditor(typeof(RockerLock), true)]
    [CanEditMultipleObjects]
    public class RockerLockEditor : BaseEditor
    {
        #region Field and Property
        protected RockerLock Target { get { return target as RockerLock; } }
        protected const float Half = 0.5f;
        #endregion

        #region Private Method
        protected virtual void OnSceneGUI()
        {
            if (Target.RJoint.rockJoint == null)
                return;

            var offset = Target.RJoint.rockJoint.position - Target.transform.position;
            var center = Target.transform.position + offset * Half;
            var minOffset = offset.normalized * Target.minStroke * Half;
            var maxOffset = offset.normalized * Target.maxStroke * Half;
            var nearMin = center - minOffset;
            var nearMax = center - maxOffset;
            var farMin = center + minOffset;
            var farMax = center + maxOffset;

            Handles.color = Blue;
            Handles.DrawLine(nearMax, farMax);
            DrawSphereCap(nearMin, Quaternion.identity, NodeSize);
            DrawSphereCap(nearMax, Quaternion.identity, NodeSize);
            DrawSphereCap(farMin, Quaternion.identity, NodeSize);
            DrawSphereCap(farMax, Quaternion.identity, NodeSize);

            GUI.color = Blue;
            Handles.Label(nearMin, "Near Min");
            Handles.Label(nearMax, "Near Max");
            Handles.Label(farMin, "Far Min");
            Handles.Label(farMax, "Far Max");

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 195, Screen.height - 115, 185, 65);
            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Stroke Editor", "Window");
            GUILayout.Label("Current Distance: " + Target.GetDistance(), "TextField");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Set To Min"))
                Target.minStroke = Target.GetDistance();
            if (GUILayout.Button("Set To Max"))
                Target.maxStroke = Target.GetDistance();

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                Target.minStroke = Mathf.Clamp(Target.minStroke, 0, float.MaxValue);
                Target.maxStroke = Mathf.Clamp(Target.maxStroke, Target.minStroke, float.MaxValue);
            }
        }
        #endregion
    }
}