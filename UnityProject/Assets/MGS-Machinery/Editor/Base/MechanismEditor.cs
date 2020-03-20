/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechanismEditor.cs
 *  Description  :  Custom base editor for mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.UCommonEditor;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machinery
{
    public partial class MechanismEditor : BaseEditor
    {
        #region Protected Method
        protected void DrawRockers(List<RockerMechanism> rockers, Transform driver)
        {
            if (rockers == null)
            {
                return;
            }

            foreach (var rocker in rockers)
            {
                if (rocker)
                {
                    DrawPositionHandle(rocker.transform);
                    DrawSphereArrow(driver.position, rocker.transform.position, NodeSize);

                    if (rocker.joint)
                    {
                        DrawSphereArrow(rocker.transform.position, rocker.joint.transform.position, NodeSize);
                    }
                }
            }
        }
        #endregion
    }
}