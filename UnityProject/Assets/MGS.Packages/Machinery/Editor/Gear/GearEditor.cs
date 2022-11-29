/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  GearEditor.cs
 *  Description  :  Custom editor for GearMechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machineries
{
    [CustomEditor(typeof(Gear), true)]
    [CanEditMultipleObjects]
    public class GearEditor : MechanismEditor
    {
        #region Field and Property
        protected Gear Target { get { return target as Gear; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = HandleColor;
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, Target.radius);
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.forward, ArrowLength, NodeSize, "Axis");
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                Target.radius = Mathf.Max(Mathf.Epsilon, Target.radius);
            }
        }
        #endregion
    }
}