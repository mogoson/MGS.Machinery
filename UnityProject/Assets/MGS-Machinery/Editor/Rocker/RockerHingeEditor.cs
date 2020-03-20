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
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/20/2018
 *  Description  :  Optimize display of editor.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machinery
{
    [CustomEditor(typeof(RockerHinge), true)]
    [CanEditMultipleObjects]
    public class RockerHingeEditor : MechanismEditor
    {
        #region Field and Property
        protected RockerHinge Target { get { return target as RockerHinge; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawAdaptiveCircleCap(Target.transform.position, Target.transform.rotation, AreaRadius);
            DrawAdaptiveSphereArrow(Target.transform.position, Target.transform.up, AreaRadius, NodeSize);

            DrawAdaptiveWireArc(Target.transform.position, Target.transform.right, Target.Axis, -180, AreaRadius);
            DrawAdaptiveSphereArrow(Target.transform.position, -Target.Axis, AreaRadius, 0);
            DrawAdaptiveSphereArrow(Target.transform.position, Target.Axis, ArrowLength, NodeSize, "Axis");

            if (Target.joint)
            {
                DrawAdaptiveSphereArrow(Target.transform.position, Target.joint.forward, AreaRadius, NodeSize);
                DrawAdaptiveSphereArrow(Target.transform.position, Target.joint.forward, ArrowLength, NodeSize, "Joint");
            }
        }
        #endregion
    }
}