/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveEditor.cs
 *  DeTargetion  :  Editor for MonoCurveCollider.
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
    [CustomEditor(typeof(MonoCurveCollider), true)]
    public class MonoCurveColliderEditor : Editor
    {
        protected MonoCurveCollider Target { get { return target as MonoCurveCollider; } }
        protected bool enabled;

        protected virtual void OnEnable()
        {
            enabled = Target.enabled;
        }

        public override void OnInspectorGUI()
        {
            DrawCaptionInspector();
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                OnInspectorChange();
            }
            ChangeCheckInspector();
        }

        protected virtual void DrawCaptionInspector()
        {
            EditorGUILayout.HelpBox(string.Format("Segments: {0}", Target.Segments), MessageType.Info);
        }

        protected virtual void ChangeCheckInspector()
        {
            if (Target.enabled != enabled)
            {
                Target.Rebuild(Target.GetComponent<IMonoCurve>());
                enabled = Target.enabled;
            }
        }

        protected virtual void OnInspectorChange()
        {
            Target.Segment = Mathf.Max(Target.Segment, 1E-3F);
            Target.Radius = Mathf.Max(Target.Radius, 1E-3F);
            Target.Rebuild(Target.GetComponent<IMonoCurve>());
        }
    }
}