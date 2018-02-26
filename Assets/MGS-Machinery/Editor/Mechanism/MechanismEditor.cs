/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechanismEditor.cs
 *  Description  :  Custom editor for mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/17/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using Developer.EditorExtension;
using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    public class MechanismEditor : GenericEditor
    {
        #region Protected Method
        protected virtual void DrawRockers(List<RockerMechanism> rockers, Transform driver, Color color)
        {
            if (rockers == null)
                return;

            Handles.color = color;
            foreach (var rocker in rockers)
            {
                if (rocker)
                {
                    DrawPositionHandle(rocker.transform);
                    DrawArrow(driver.position, rocker.transform.position, nodeSize, blue, string.Empty);

                    if (rocker.rockJoint)
                        DrawArrow(rocker.transform.position, rocker.rockJoint.transform.position, nodeSize, blue, string.Empty);
                }
            }
        }
        #endregion
    }
}