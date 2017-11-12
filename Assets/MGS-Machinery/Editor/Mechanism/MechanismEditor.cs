/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  MechanismEditor.cs
 *  Description  :  Custom editor for mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/17/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_5_3_OR_NEWER
using UnityEditor.SceneManagement;
#endif

namespace Developer.Machinery
{
    public class MechanismEditor : Editor
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
        protected virtual void DrawArrow(Vector3 start, Vector3 end, float size, string text, Color color)
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

        protected virtual void DrawArrow(Vector3 start, Vector3 direction, float length, float size, string text, Color color)
        {
            var end = start + direction.normalized * length;
            DrawArrow(start, end, size, text, color);
        }

        protected virtual void DrawRockers(List<RockerMechanism> rockers, Transform driver, Color color)
        {
            if (rockers == null)
                return;

            Handles.color = color;
            foreach (var rocker in rockers)
            {
                if (rocker)
                {
                    DrawPositionHandle(rocker.transform);
                    DrawArrow(driver.position, rocker.transform.position, nodeSize, string.Empty, blue);

                    if (rocker.rockJoint)
                        DrawArrow(rocker.transform.position, rocker.rockJoint.transform.position, nodeSize, string.Empty, blue);
                }
            }
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