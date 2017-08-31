/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: TelescopicJointEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/2/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.      TelescopicJointEditor       Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     3/2/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TelescopicJoint), true)]
    [CanEditMultipleObjects]
    public class TelescopicJointEditor : MechanismEditor
    {
        #region Property and Field
        protected TelescopicJoint script { get { return target as TelescopicJoint; } }
        protected Vector3 axis { get { return script.transform.forward; } }
        protected Vector3 zeroPoint
        {
            get
            {
                if (Application.isPlaying)
                {
                    var point = script.startPosition;
                    if (script.transform.parent)
                        point = script.transform.parent.TransformPoint(point);
                    return point;
                }
                else
                    return script.transform.position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            GUI.color = blue;
            Handles.color = blue;
            Handles.Label(zeroPoint, "Zero");
            Handles.SphereCap(0, zeroPoint, Quaternion.identity, nodeSize);
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            DrawStroke();
            DrawRockers(script.rockers, script.transform, blue);
        }

        protected virtual void DrawStroke()
        {
            DrawArrow(zeroPoint, axis, script.stroke, nodeSize, "Stroke", blue);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            script.stroke = Mathf.Clamp(script.stroke, 0, float.MaxValue);
        }
        #endregion
    }
}