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
        protected SerializedProperty reference;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            reference = serializedObject.FindProperty("reference");
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
            DrawArrow(script.transform.position, script.worldUp, arrowLength, nodeSize, "Keep Up", blue);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (script.keepUp == KeepUpMode.ReferenceForward)
            {
                EditorGUILayout.PropertyField(reference);
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endregion
    }
}