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
        protected TelescopicJoint Script { get { return target as TelescopicJoint; } }

        protected Vector3 Axis { get { return Script.transform.forward; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                if (Application.isPlaying)
                {
                    var point = Script.StartPosition;
                    if (Script.transform.parent)
                        point = Script.transform.parent.TransformPoint(point);
                    return point;
                }
                else
                    return Script.transform.position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            GUI.color = blue;
            Handles.color = blue;
            Handles.Label(ZeroPoint, "Zero");
            DrawSphereCap(ZeroPoint, Quaternion.identity, nodeSize);
            DrawSphereCap(Script.transform.position, Quaternion.identity, nodeSize);
            DrawStroke();
            DrawRockers(Script.rockers, Script.transform, blue);
        }

        protected virtual void DrawStroke()
        {
            DrawArrow(ZeroPoint, Axis, Script.stroke, nodeSize, "Stroke", blue);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Script.stroke = Mathf.Clamp(Script.stroke, 0, float.MaxValue);
        }
        #endregion
    }
}