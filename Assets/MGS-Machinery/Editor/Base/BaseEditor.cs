/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  BaseEditor.cs
 *  Description  :  Custom base editor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.UEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    public class BaseEditor : GenericEditor
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
                    DrawSphereArrow(driver.position, rocker.transform.position, NodeSize, Blue, string.Empty);

                    if (rocker.joint)
                        DrawSphereArrow(rocker.transform.position, rocker.joint.transform.position, NodeSize, Blue, string.Empty);
                }
            }
        }
        #endregion
    }
}