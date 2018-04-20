/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankRockerEditor.cs
 *  Description  :  Custom editor for CrankRocker.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    [CustomEditor(typeof(CrankRocker), true)]
    [CanEditMultipleObjects]
    public class CrankRockerEditor : CrankLinkEditor
    {
        #region Field and Property
        protected new CrankRocker Target { get { return target as CrankRocker; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();

            if (!Target.IsIntact)
                return;

            if (Target.editMode == EditMode.Free)
            {
                DrawRotationHandle(Target.crank.transform);
                DrawPositionHandle(Target.linkBar.transform);
                DrawPositionHandle(Target.rocker.transform);
                DrawPositionHandle(Target.lrJoint);
            }
            else if (Target.editMode == EditMode.Hinge)
                DrawRotationHandle(Target.crank.transform);

            DrawCircleCap(Target.crank.transform.position, Target.crank.transform.rotation, AreaRadius);
            DrawSphereArrow(Target.crank.transform.position, Target.crank.transform.forward, ArrowLength, NodeSize, Blue, "Axis");

            var offset = Target.linkBar.transform.position - Target.crank.transform.position;
            DrawSphereArrow(Target.crank.transform.position, offset, AreaRadius, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.crank.transform.position, Target.linkBar.transform.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.linkBar.transform.position, Target.lrJoint.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.lrJoint.position, Target.rocker.transform.position, NodeSize, Blue, string.Empty);
            DrawSphereArrow(Target.rocker.transform.position, Target.crank.transform.position, NodeSize, Blue, string.Empty);

            DrawHingeEditor();
        }
        #endregion
    }
}