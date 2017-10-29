/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerJointEditor.cs
 *  Description  :  Custom editor for RockerJoint.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/17/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    [CustomEditor(typeof(RockerJoint), true)]
    [CanEditMultipleObjects]
    public class RockerJointEditor : MechanismEditor
    {
        #region Property and Field
        protected RockerJoint script { get { return target as RockerJoint; } }
        protected SerializedProperty upTransform;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            upTransform = serializedObject.FindProperty("upTransform");
        }

        protected virtual void OnSceneGUI()
        {
            if (!script.rockJoint)
                return;

            DrawPositionHandle(script.rockJoint);
            Handles.color = blue;
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
            DrawSphereCap(script.rockJoint.position, Quaternion.identity, nodeSize);
            Handles.DrawLine(script.transform.position, script.rockJoint.position);
            DrawArrow(script.transform.position, script.worldUp, arrowLength, nodeSize, "Up", blue);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (script.keepUp == CustomAxis.TransformForward)
            {
                EditorGUILayout.PropertyField(upTransform);
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endregion
    }
}