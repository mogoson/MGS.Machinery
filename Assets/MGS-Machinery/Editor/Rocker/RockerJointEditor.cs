/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: RockerJointEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 1/17/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       RockerJointEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     1/17/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(RockerJoint), true)]
    [CanEditMultipleObjects]
    public class RockerJointEditor : MechanismEditor
    {
        #region Property and Field
        protected SerializedProperty upTransform;
        protected RockerJoint script { get { return target as RockerJoint; } }
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
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            Handles.SphereCap(0, script.rockJoint.position, Quaternion.identity, nodeSize);
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