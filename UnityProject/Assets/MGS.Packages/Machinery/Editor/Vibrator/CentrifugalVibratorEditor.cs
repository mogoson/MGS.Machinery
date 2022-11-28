/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CentrifugalVibratorEditor.cs
 *  Description  :  Custom editor for CentrifugalVibrator.
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
    [CustomEditor(typeof(CentrifugalVibrator), true)]
    [CanEditMultipleObjects]
    public class CentrifugalVibratorEditor : MechanismEditor
    {
        #region Field and Property
        protected CentrifugalVibrator Target { get { return target as CentrifugalVibrator; } }

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
            Handles.color = Blue;
            DrawAdaptiveSphereCap(StartPosition, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(StartPosition, Target.transform.rotation, Target.amplitudeRadius);

            DrawSphereArrow(StartPosition, Target.transform.position, NodeSize);
            DrawAdaptiveSphereArrow(StartPosition, Target.transform.forward, ArrowLength, NodeSize, "Axis");
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