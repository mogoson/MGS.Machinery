/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TelescopicJointEditor.cs
 *  Description  :  Custom editor for TelescopicJoint.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(TelescopicJoint), true)]
    [CanEditMultipleObjects]
    public class TelescopicJointEditor : BaseEditor
    {
        #region Field and Property
        protected TelescopicJoint Target { get { return target as TelescopicJoint; } }

        protected Vector3 Axis { get { return Target.transform.forward; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                if (Application.isPlaying)
                {
                    var point = Target.StartPosition;
                    if (Target.transform.parent)
                        point = Target.transform.parent.TransformPoint(point);
                    return point;
                }
                else
                    return Target.transform.position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawSphereCap(ZeroPoint, Quaternion.identity, NodeSize);
            DrawSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawStroke();
            DrawRockers(Target.rockers, Target.transform, Blue);

            GUI.color = Blue;
            Handles.Label(ZeroPoint, "Zero");
        }

        protected virtual void DrawStroke()
        {
            DrawSphereArrow(ZeroPoint, Axis, Target.stroke.min, NodeSize, Blue, "Min");
            DrawSphereArrow(ZeroPoint, Axis, Target.stroke.max, NodeSize, Blue, "Max");
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
                Target.stroke.max = Mathf.Clamp(Target.stroke.max, Target.stroke.min, float.MaxValue);
        }
        #endregion
    }
}