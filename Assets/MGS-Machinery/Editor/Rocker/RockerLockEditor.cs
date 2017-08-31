/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: RockerLockEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 1/19/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.        RockerLockEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     1/19/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(RockerLock), true)]
    [CanEditMultipleObjects]
    public class RockerLockEditor : MechanismEditor
    {
        #region Property and Field
        protected RockerLock script { get { return target as RockerLock; } }
        #endregion

        #region Private Method
        protected virtual void OnSceneGUI()
        {
            if (!script.rJoint.rockJoint)
                return;
            var offset = script.rJoint.rockJoint.position - script.transform.position;
            var center = script.transform.position + offset * 0.5f;
            var minOffset = offset.normalized * script.minStroke * 0.5f;
            var maxOffset = offset.normalized * script.maxStroke * 0.5f;
            var nearMin = center - minOffset;
            var nearMax = center - maxOffset;
            var farMin = center + minOffset;
            var farMax = center + maxOffset;

            Handles.color = GUI.color = blue;
            Handles.DrawLine(nearMax, farMax);

            Handles.Label(nearMin, "NearMin");
            Handles.Label(nearMax, "NearMax");
            Handles.SphereCap(0, nearMin, Quaternion.identity, nodeSize);
            Handles.SphereCap(0, nearMax, Quaternion.identity, nodeSize);

            Handles.Label(farMin, "FarMin");
            Handles.Label(farMax, "FarMax");
            Handles.SphereCap(0, farMin, Quaternion.identity, nodeSize);
            Handles.SphereCap(0, farMax, Quaternion.identity, nodeSize);

            DrawSceneTool();
        }

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 195, Screen.height - 115, 185, 65);

            GUI.color = Color.white;
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Stroke Editor", "Window");
            GUILayout.Label("Current Distance: " + script.GetDistance(), "TextField");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Set To Min"))
                script.minStroke = script.GetDistance();
            if (GUILayout.Button("Set To Max"))
                script.maxStroke = script.GetDistance();

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            Handles.EndGUI();
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            script.minStroke = Mathf.Clamp(script.minStroke, 0, float.MaxValue);
            script.maxStroke = Mathf.Clamp(script.maxStroke, script.minStroke, float.MaxValue);
        }
        #endregion
    }
}