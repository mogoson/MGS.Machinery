/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: MechanismEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 1/17/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       MechanismEditor            Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     1/17/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class MechanismEditor : Editor
    {
        #region Property and Field
        protected Color blue = new Color(0, 1, 1, 1);
        protected Color transparentBlue = new Color(0, 1, 1, 0.1f);

        protected float nodeSize = 0.05f;
        protected float arrowLength = 0.75f;
        protected float lineLength = 10;
        protected float areaRadius = 0.5f;
        #endregion

        #region Protected Method
        protected virtual void DrawArrow(Vector3 start, Vector3 end, float size, string text, Color color)
        {
            var gC = GUI.color;
            var hC = Handles.color;

            GUI.color = color;
            Handles.color = color;

            Handles.DrawLine(start, end);
            Handles.SphereCap(0, end, Quaternion.identity, size);
            Handles.Label(end, text);

            GUI.color = gC;
            Handles.color = hC;
        }//DrawArrow()_end

        protected virtual void DrawArrow(Vector3 start, Vector3 direction, float length, float size, string text, Color color)
        {
            var end = start + direction.normalized * length;
            DrawArrow(start, end, size, text, color);
        }//DrawArrow()_end

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
                }//if()_end
            }//foreach()_end
        }//DrawRocker()_end

        protected void DrawPositionHandle(Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            var position = Handles.PositionHandle(transform.position, GetPivotRotation(transform));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "Change Position");
                transform.position = position;
            }//if()_end
        }//DrawP...()_end

        protected void DrawRotationHandle(Transform transform)
        {
            EditorGUI.BeginChangeCheck();
            var rotation = Handles.RotationHandle(transform.rotation, transform.position);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(transform, "Change Rotation");
                transform.rotation = rotation;
            }//if()_end
        }//DrawR...()_end

        protected Quaternion GetPivotRotation(Transform transform)
        {
            var rotation = Quaternion.identity;
            if (Tools.pivotRotation == PivotRotation.Local)
                rotation = transform.rotation;
            return rotation;
        }//GetP...()_end
        #endregion
    }//class_end
}//namespace_end