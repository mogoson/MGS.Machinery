/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerLockEditor.cs
 *  Description  :  Custom editor for RockerLock.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/19/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    [CustomEditor(typeof(RockerLock), true)]
    [CanEditMultipleObjects]
    public class RockerLockEditor : MechanismEditor
    {
        #region Property and Field
        protected RockerLock Script { get { return target as RockerLock; } }
        protected const float half = 0.5f;
        #endregion

        #region Private Method
        protected virtual void OnSceneGUI()
        {
            if (!Script.RJoint.rockJoint)
                return;

            var offset = Script.RJoint.rockJoint.position - Script.transform.position;
            var center = Script.transform.position + offset * half;
            var minOffset = offset.normalized * Script.minStroke * half;
            var maxOffset = offset.normalized * Script.maxStroke * half;
            var nearMin = center - minOffset;
            var nearMax = center - maxOffset;
            var farMin = center + minOffset;
            var farMax = center + maxOffset;

            Handles.color = GUI.color = blue;
            Handles.DrawLine(nearMax, farMax);

            Handles.Label(nearMin, "Near Min");
            Handles.Label(nearMax, "Near Max");
            DrawSphereCap(nearMin, Quaternion.identity, nodeSize);
            DrawSphereCap(nearMax, Quaternion.identity, nodeSize);

            Handles.Label(farMin, "Far Min");
            Handles.Label(farMax, "Far Max");
            DrawSphereCap(farMin, Quaternion.identity, nodeSize);
            DrawSphereCap(farMax, Quaternion.identity, nodeSize);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 195, Screen.height - 115, 185, 65);

            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Stroke Editor", "Window");
            GUILayout.Label("Current Distance: " + Script.GetDistance(), "TextField");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Set To Min"))
                Script.minStroke = Script.GetDistance();
            if (GUILayout.Button("Set To Max"))
                Script.maxStroke = Script.GetDistance();

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Script.minStroke = Mathf.Clamp(Script.minStroke, 0, float.MaxValue);
            Script.maxStroke = Mathf.Clamp(Script.maxStroke, Script.minStroke, float.MaxValue);
        }
        #endregion
    }
}