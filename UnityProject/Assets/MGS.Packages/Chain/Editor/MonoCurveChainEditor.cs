/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveChainEditor.cs
 *  Description  :  Editor for MonoCurveChain component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/3/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Curve;
using UnityEditor;
using UnityEngine;

namespace MGS.Chain.Editors
{
    [CustomEditor(typeof(MonoCurveChain), true)]
    public class MonoCurveChainEditor : Editor
    {
        protected MonoCurveChain Target { get { return target as MonoCurveChain; } }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                OnInspectorChange();
            }

            DrawChainInspector();
        }

        protected virtual void OnInspectorChange()
        {
            Target.Segment = Mathf.Max(Target.Segment, 1E-3F);
            Target.Rebuild(Target.GetComponent<IMonoCurve>());
        }

        protected virtual void DrawChainInspector()
        {
            EditorGUILayout.BeginHorizontal("Box");
            if (GUILayout.Button("Rebuild"))
            {
                Target.Rebuild();
            }
            if (GUILayout.Button("Clear"))
            {
                Target.Clear();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}