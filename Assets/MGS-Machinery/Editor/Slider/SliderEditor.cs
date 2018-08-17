/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SliderEditor.cs
 *  Description  :  Custom editor for Slider.
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
                if (Application.isPlaying)
                {
                    if (Target.transform.parent)
                        return Target.transform.parent.TransformPoint(Target.StartPosition);
                    else
                        return Target.StartPosition;
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
                Target.stroke.max = Mathf.Clamp(Target.stroke.max, Target.stroke.min, float.MaxValue);
        }
        #endregion
    }
}