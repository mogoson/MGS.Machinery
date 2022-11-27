/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerLimiterEditor.cs
 *  Description  :  Custom editor for RockerLock.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  1.1
 *  Date         :  6/20/2018
 *  Description  :  Optimize display of node.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machinery
{
    [CustomEditor(typeof(RockerLimiter), true)]
    [CanEditMultipleObjects]
    public class RockerLimiterEditor : MechanismEditor
    {
        #region Field and Property
        protected RockerLimiter Target { get { return target as RockerLimiter; } }
        protected const float Half = 0.5f;
        #endregion

        #region Private Method
        protected virtual void OnSceneGUI()
        {
            if (Target.Rocker.joint == null)
            {
                return;
            }

            var offset = Target.Rocker.joint.position - Target.transform.position;
            var center = Target.transform.position + offset * Half;
            var minOffset = offset.normalized * Target.distance.min * Half;
            var maxOffset = offset.normalized * Target.distance.max * Half;
            var nearMin = center - minOffset;
            var nearMax = center - maxOffset;
            var farMin = center + minOffset;
            var farMax = center + maxOffset;

            Handles.color = Blue;
            Handles.DrawLine(nearMax, farMax);
            DrawAdaptiveSphereCap(nearMin, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(nearMax, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(farMin, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(farMax, Quaternion.identity, NodeSize);

            Handles.Label(nearMin, "Near Min");
            Handles.Label(nearMax, "Near Max");
            Handles.Label(farMin, "Far Min");
            Handles.Label(farMax, "Far Max");

            DrawSceneGUI();
        }

        protected void DrawSceneGUI()
        {
            var rect = new Rect(Screen.width - 195, Screen.height - 115, 185, 65);
            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Stroke Editor", "Window");
            GUILayout.Label("Current Distance: " + Target.GetDistance(), "TextField");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Set To Min"))
            {
                Target.distance.min = Target.GetDistance();
                EditorUtility.SetDirty(Target);
            }
            if (GUILayout.Button("Set To Max"))
            {
                Target.distance.max = Target.GetDistance();
                EditorUtility.SetDirty(Target);
            }

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
                Target.distance.min = Mathf.Clamp(Target.distance.min, 0, float.MaxValue);
                Target.distance.max = Mathf.Clamp(Target.distance.max, Target.distance.min, float.MaxValue);
            }
        }
        #endregion
    }
}