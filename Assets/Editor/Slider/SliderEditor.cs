﻿/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SliderEditor.cs
 *  Description  :  Custom editor for Slider.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machineries
{
    [CustomEditor(typeof(Slider), true)]
    [CanEditMultipleObjects]
    public class SliderEditor : MechanismEditor
    {
        #region Field and Property
        protected Slider Target { get { return target as Slider; } }

        protected Vector3 Axis { get { return Target.transform.forward; } }

        protected Vector3 ZeroPoint
        {
            get
            {
                var position = Target.transform.position;
                if (Application.isPlaying)
                {
                    position = Target.StartPosition;
                    if (Target.transform.parent)
                    {
                        position = Target.transform.parent.TransformPoint(position);
                    }
                }
                return position;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = HandleColor;
            DrawAdaptiveSphereCap(ZeroPoint, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);

            DrawStroke();
            DrawRockers(Target.rockers, Target.transform);
            Handles.Label(ZeroPoint, "Zero");
        }

        protected virtual void DrawStroke()
        {
            DrawSphereArrow(ZeroPoint, Axis, Target.stroke.min, NodeSize, "Min");
            DrawSphereArrow(ZeroPoint, Axis, Target.stroke.max, NodeSize, "Max");
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                Target.stroke.max = Mathf.Clamp(Target.stroke.max, Target.stroke.min, float.MaxValue);
            }
        }
        #endregion
    }
}