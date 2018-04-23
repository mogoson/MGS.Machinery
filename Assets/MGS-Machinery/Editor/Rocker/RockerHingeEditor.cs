/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerHingeEditor.cs
 *  Description  :  Custom editor for RockerHinge.
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
    [CustomEditor(typeof(RockerHinge), true)]
    [CanEditMultipleObjects]
    public class RockerHingeEditor : BaseEditor
    {
        #region Field and Property
        protected RockerHinge Target { get { return target as RockerHinge; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, AreaRadius);
            Handles.DrawWireArc(Target.transform.position, Target.transform.right, Target.Axis, -180, AreaRadius);
            DrawSphereArrow(Target.transform.position, Target.transform.up, AreaRadius, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.transform.position - Target.Axis * AreaRadius, Target.transform.position + Target.Axis * ArrowLength, NodeSize, Blue, "Axis");

            if (Target.joint)
                DrawSphereArrow(Target.transform.position, Target.joint.forward, AreaRadius, NodeSize, Blue, string.Empty);
        }
        #endregion
    }
}