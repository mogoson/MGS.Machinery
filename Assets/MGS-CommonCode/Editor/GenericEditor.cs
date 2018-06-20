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
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/20/2018
 *  Description  :  Add method for draw adaptive graph.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace Mogoson.UEditor
{
    public class GenericEditor : Editor
    {
        #region Field and Property
        protected readonly Color Blue = new Color(0, 1, 1, 1);
        protected readonly Color TransparentBlue = new Color(0, 1, 1, 0.1f);
        protected readonly Vector3 MoveSnap = Vector3.one;

#if UNITY_5_5_OR_NEWER
        protected readonly Handles.CapFunction CircleCap = Handles.CircleHandleCap;
        protected readonly Handles.CapFunction SphereCap = Handles.SphereHandleCap;
#else
        protected readonly Handles.DrawCapFunction CircleCap = Handles.CircleCap;
        protected readonly Handles.DrawCapFunction SphereCap = Handles.SphereCap;
#endif
        protected const float NodeSize = 0.125f;
        protected const float AreaRadius = 1.25f;
        protected const float ArrowLength = 2f;
        protected const float LineLength = 10;

        protected const float FixedAreaRadius = 0.5f;
        protected const float FixedArrowLength = 0.75f;
        #endregion

        #region Protected Method
        protected void DrawCircleCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
                CircleCap(0, position, rotation, size, EventType.Repaint);
#else
            CircleCap(0, position, rotation, size);
#endif
        }

        protected void DrawAdaptiveCircleCap(Vector3 position, Quaternion rotation, float size)
        {
            DrawCircleCap(position, rotation, size * HandleUtility.GetHandleSize(position));
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
                SphereCap(0, position, rotation, size, EventType.Repaint);
#else
            SphereCap(0, position, rotation, size);
#endif
        }

        protected void DrawAdaptiveSphereCap(Vector3 position, Quaternion rotation, float size)
        {
            DrawSphereCap(position, rotation, size * HandleUtility.GetHandleSize(position));
        }

        protected void DrawAdaptiveWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            Handles.DrawWireArc(center, normal, from, angle, radius * HandleUtility.GetHandleSize(center));
        }

        protected void DrawSphereArrow(Vector3 start, Vector3 end, float size, string text = "")
        {
            Handles.DrawLine(start, end);
            DrawAdaptiveSphereCap(end, Quaternion.identity, size);
            Handles.Label(end, text);
        }

        protected void DrawSphereArrow(Vector3 start, Vector3 direction, float length, float size, string text = "")
        {
            DrawSphereArrow(start, start + direction.normalized * length, size, text);
        }

        protected void DrawAdaptiveSphereArrow(Vector3 start, Vector3 direction, float length, float size, string text = "")
        {
            DrawSphereArrow(start, direction, length * HandleUtility.GetHandleSize(start), size, text);
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