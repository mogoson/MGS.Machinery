/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerJointEditor.cs
 *  Description  :  Custom editor for RockerJoint.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
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
        protected RockerJoint Script { get { return target as RockerJoint; } }
        protected SerializedProperty reference;
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            reference = serializedObject.FindProperty("reference");
        }

        protected virtual void OnSceneGUI()
        {
            if (!Script.rockJoint)
                return;

            DrawPositionHandle(Script.rockJoint);

            Handles.color = blue;
            DrawSphereCap(Script.transform.position, Quaternion.identity, nodeSize);
            DrawSphereCap(Script.rockJoint.position, Quaternion.identity, nodeSize);
            Handles.DrawLine(Script.transform.position, Script.rockJoint.position);
            DrawArrow(Script.transform.position, Script.WorldUp, arrowLength, nodeSize, blue, "Keep Up");
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (Script.keepUp == KeepUpMode.ReferenceForward)
            {
                EditorGUILayout.PropertyField(reference);
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endregion
    }
}