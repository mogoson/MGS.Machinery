/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveCacherEditor.cs
 *  DeTargetion  :  Editor for MonoCurveCacher.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/17/2021
 *  DeTargetion  :  Initial development version.
 *************************************************************************/
 
using UnityEditor;
using UnityEngine;

namespace MGS.Curve.Editors
{
    [CustomEditor(typeof(MonoCurveCacher), true)]
    public class MonoCurveCacherEditor : Editor
    {
        protected MonoCurveCacher Target { get { return target as MonoCurveCacher; } }

        public override void OnInspectorGUI()
        {
            DrawCacheInspector();
            DrawDefaultInspector();
        }

        protected virtual void DrawCacheInspector()
        {
            EditorGUILayout.BeginHorizontal("Box");

            if (GUILayout.Button("Build"))
            {
                var cacheFile = EditorUtility.SaveFilePanel("Save Cache File",
                    Application.streamingAssetsPath, Target.name, "mcc");

                if (!string.IsNullOrEmpty(cacheFile))
                {
                    Target.Build(cacheFile);
                }
            }

            if (GUILayout.Button("Load"))
            {
                var cacheFile = EditorUtility.OpenFilePanel("Open Cache File",
                    Application.streamingAssetsPath, "mcc");

                if (!string.IsNullOrEmpty(cacheFile))
                {
                    Target.Load(cacheFile);
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}