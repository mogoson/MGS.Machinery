/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GenericEditor.cs
 *  Description  :  Define generic editor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace Developer.EditorExtension
{
    public class GenericEditor : Editor
    {
        #region Property and Field
        protected readonly Color blue = new Color(0, 1, 1, 1);
        protected readonly Color transparentBlue = new Color(0, 1, 1, 0.1f);

        protected const float nodeSize = 0.05f;
        protected const float arrowLength = 0.75f;
        protected const float lineLength = 10;
        protected const float areaRadius = 0.5f;
        #endregion

        #region Protected Method
        protected virtual void DrawArrow(Vector3 start, Vector3 end, float size, Color color, string text)
        {
            var gColor = GUI.color;
            var hColor = Handles.color;

            GUI.color = color;
            Handles.color = color;

            Handles.DrawLine(start, end);
            DrawSphereCap(end, Quaternion.identity, size);
            Handles.Label(end, text);

            GUI.color = gColor;
            Handles.color = hColor;
        }

        protected virtual void DrawArrow(Vector3 start, Vector3 direction, float length, float size, Color color, string text)
        {
            var end = start + direction.normalized * length;
            DrawArrow(start, end, size, color, text);
        }

        protected void DrawPositionHandle(Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            var position = Handles.PositionHandle(transform.position, GetPivotRotation(transform));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "Change Position");
                transform.position = position;
                MarkSceneDirty();
            }
        }

        protected void DrawRotationHandle(Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            var rotation = Handles.RotationHandle(transform.rotation, transform.position);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "Change Rotation");
                transform.rotation = rotation;
                MarkSceneDirty();
            }
        }

        protected Quaternion GetPivotRotation(Transform transform)
        {
            if (Tools.pivotRotation == PivotRotation.Local)
                return transform.rotation;
            else
                return Quaternion.identity;
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
                Handles.SphereHandleCap(0, position, rotation, size, EventType.Repaint);
#else
            Handles.SphereCap(0, position, rotation, size);
#endif
        }

        protected void DrawCircleCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
                Handles.CircleHandleCap(0, position, rotation, size, EventType.Repaint);
#else
            Handles.CircleCap(0, position, rotation, size);
#endif
        }

        protected void MarkSceneDirty()
        {
#if UNITY_5_3_OR_NEWER
            EditorSceneManager.MarkAllScenesDirty();
#else
            EditorApplication.MarkSceneDirty();
#endif
        }
        #endregion
    }
}