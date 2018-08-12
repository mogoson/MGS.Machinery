/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurvePathEditor.cs
 *  DeTargetion  :  Editor for MonoCurvePath.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  DeTargetion  :  Initial development version.
 *************************************************************************/

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.CurvePath
{
    [CustomEditor(typeof(MonoCurvePath), true)]
    public class CurvePathEditor : GenericEditor
    {
        #region Field and Property
        protected MonoCurvePath Target { get { return target as MonoCurvePath; } }
        protected const float Delta = 0.05f;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (!Application.isPlaying)
            {
                Target.Rebuild();
                Undo.undoRedoPerformed = () => { Target.Rebuild(); };
            }
        }

        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            for (float t = 0; t < Target.MaxKey; t += Delta)
            {
                Handles.DrawLine(Target.GetPointAt(t), Target.GetPointAt(t + Delta));
            }
        }

        protected virtual void OnDisable()
        {
            Undo.undoRedoPerformed = null;
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
                Target.Rebuild();
        }
        #endregion
    }
}