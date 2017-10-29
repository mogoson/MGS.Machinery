/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  TelescopicJointEditor.cs
 *  Description  :  Custom editor for TelescopicJoint.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/2/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
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
            DrawSphereCap(zeroPoint, Quaternion.identity, nodeSize);
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
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