﻿/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  RockerJointEditor.cs
 *  Description  :  Custom editor for RockerJoint.
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
    [CustomEditor(typeof(RockerJoint), true)]
    [CanEditMultipleObjects]
    public class RockerJointEditor : MechanismEditor
    {
        #region Field and Property
        protected RockerJoint Target { get { return target as RockerJoint; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            if (Target.joint == null)
            {
                return;
            }

            Handles.color = HandleColor;
            DrawPositionHandle(Target.joint);
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawAdaptiveSphereCap(Target.joint.position, Quaternion.identity, NodeSize);

            Handles.DrawLine(Target.transform.position, Target.joint.position);
            DrawAdaptiveSphereArrow(Target.transform.position, Target.WorldUp, ArrowLength, NodeSize, "Up");
        }
        #endregion
    }
}