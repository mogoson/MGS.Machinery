/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  FreeCrankEditor.cs
 *  Description  :  Custom editor for FreeCrank.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  6/20/2018
 *  Description  :  Optimize display of node and axis.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.Machinery
{
    [CustomEditor(typeof(FreeCrank), true)]
    [CanEditMultipleObjects]
    public class FreeCrankEditor : MechanismEditor
    {
        #region Field and Property
        protected FreeCrank Target { get { return target as FreeCrank; } }

        protected Vector3 Axis { get { return Target.transform.forward; } }

        protected Vector3 ZeroAxis
        {
            get
            {
                var axis = Target.transform.up;
                if (Application.isPlaying)
                {
                    axis = Quaternion.Euler(Target.StartAngles) * Vector3.up;
                    if (Target.transform.parent)
                    {
                        axis = Target.transform.parent.rotation * axis;
                    }
                }
                return axis;
            }
        }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            DrawAdaptiveSphereCap(Target.transform.position, Quaternion.identity, NodeSize);
            DrawCircleCap(Target.transform.position, Target.transform.rotation, FixedAreaRadius);

            DrawAdaptiveSphereArrow(Target.transform.position, Axis, ArrowLength, NodeSize, "Axis");
            DrawSphereArrow(Target.transform.position, ZeroAxis, FixedArrowLength, NodeSize, "Zero");
            DrawSphereArrow(Target.transform.position, Target.transform.up, FixedAreaRadius, NodeSize);
            DrawArea();

            Handles.color = Blue;
            DrawRockers(Target.rockers, Target.transform);
        }

        protected virtual void DrawArea()
        {
            Handles.color = TransparentBlue;
            Handles.DrawSolidDisc(Target.transform.position, Axis, FixedAreaRadius);
        }
        #endregion
    }
}