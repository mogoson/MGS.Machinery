/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LinearVibratorEditor.cs
 *  Description  :  Custom editor for LinearVibrator.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machineries
{
    [CustomEditor(typeof(LinearVibrator), true)]
    [CanEditMultipleObjects]
    public class LinearVibratorEditor : MechanismEditor
    {
        #region Field and Property
        protected LinearVibrator Target { get { return target as LinearVibrator; } }

        protected Vector3 StartPosition
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
            DrawAdaptiveSphereCap(StartPosition, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);

            DrawSphereArrow(StartPosition, Target.transform.forward, -Target.amplitudeRadius, NodeSize);
            DrawSphereArrow(StartPosition, Target.transform.forward, Target.amplitudeRadius, NodeSize);
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                Target.amplitudeRadius = Mathf.Max(Mathf.Epsilon, Target.amplitudeRadius);
            }
        }
        #endregion
    }
}