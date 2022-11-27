/*************************************************************************
 *  Copyright © 2018-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SceneEditor.cs
 *  Description  :  Define scene editor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEditor;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace MGS.Common.Editors
{
    public class SceneEditor : Editor
    {
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

        protected void DrawCircleCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
            {
                CircleCap(0, position, rotation, size, EventType.Repaint);
            }
#else
            CircleCap(0, position, rotation, size);
#endif
        }

        protected void DrawAdaptiveCircleCap(Vector3 position, Quaternion rotation, float size)
        {
            DrawCircleCap(position, rotation, size * GetHandleSize(position));
        }

        protected void DrawSphereCap(Vector3 position, Quaternion rotation, float size)
        {
#if UNITY_5_5_OR_NEWER
            if (Event.current.type == EventType.Repaint)
            {
                SphereCap(0, position, rotation, size, EventType.Repaint);
            }
#else
            SphereCap(0, position, rotation, size);
#endif
        }

        protected void DrawAdaptiveSphereCap(Vector3 position, Quaternion rotation, float size)
        {
            DrawSphereCap(position, rotation, size * GetHandleSize(position));
        }

        protected void DrawAdaptiveWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            Handles.DrawWireArc(center, normal, from, angle, radius * GetHandleSize(center));
        }

        protected void DrawAdaptiveSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius)
        {
            Handles.DrawSolidArc(center, normal, from, angle, radius * GetHandleSize(center));
        }

        protected void DrawAdaptiveSolidDisc(Vector3 center, Vector3 normal, float radius)
        {
            Handles.DrawSolidDisc(center, normal, radius * GetHandleSize(center));
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
            DrawSphereArrow(start, direction, length * GetHandleSize(start), size, text);
        }

        protected void DrawPositionHandle(Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            var position = Handles.PositionHandle(transform.position, GetPivotRotation(transform));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "CHANGE_POSITION_HANDLE");
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
                Undo.RecordObject(transform, "CHANGE_ROTATION_HANDLE");
                transform.rotation = rotation;
                MarkSceneDirty();
            }
        }

#if UNITY_5_5_OR_NEWER
        protected void DrawFreeMoveHandle(Vector3 position, Quaternion rotation, float size, Vector3 snap, Handles.CapFunction capFunc, Action<Vector3> callback)
#else
        protected void DrawFreeMoveHandle(Vector3 position, Quaternion rotation, float size, Vector3 snap, Handles.DrawCapFunction capFunc, Action<Vector3> callback)
#endif
        {
            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.FreeMoveHandle(position, Quaternion.identity, GetHandleSize(position) * size, snap, capFunc);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "CHANGE_FREE_MOVE_HANDLE");
                InvokeAction(callback, newPosition);
                MarkSceneDirty();
            }
        }

#if UNITY_5_5_OR_NEWER
        protected void DrawAdaptiveButton(Vector3 position, Quaternion direction, float size, float pickSize, Handles.CapFunction capFunc, Action callback)
#else
        protected void DrawAdaptiveButton(Vector3 position, Quaternion direction, float size, float pickSize, Handles.DrawCapFunction capFunc, Action callback)
#endif
        {
            var scale = GetHandleSize(position);
            if (Handles.Button(position, direction, size * scale, pickSize * scale, capFunc))
            {
                Undo.RecordObject(target, "CLICK_BUTTON");
                InvokeAction(callback);
                MarkSceneDirty();
            }
        }

        protected float GetHandleSize(Vector3 position)
        {
            return HandleUtility.GetHandleSize(position);
        }

        protected Quaternion GetPivotRotation(Transform transform)
        {
            if (Tools.pivotRotation == PivotRotation.Local)
            {
                return transform.rotation;
            }
            else
            {
                return Quaternion.identity;
            }
        }

        protected void MarkSceneDirty()
        {
#if UNITY_5_3_OR_NEWER
            EditorSceneManager.MarkAllScenesDirty();
#else
            EditorApplication.MarkSceneDirty();
#endif
        }

        protected void InvokeAction(Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }
        }

        protected void InvokeAction<T>(Action<T> action, T arg)
        {
            if (action != null)
            {
                action.Invoke(arg);
            }
        }
    }
}