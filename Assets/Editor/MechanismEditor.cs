/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechanismEditor.cs
 *  Description  :  Custom base editor for mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/11/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using MGS.Common.Editors;
using UnityEngine;

namespace MGS.Machineries
{
    public partial class MechanismEditor : SceneEditor
    {
        protected readonly Color HandleColor = Color.white;

        protected readonly Color AreaColor = new Color(1, 1, 1, 0.1f);

        protected const float NodeSize = 0.125f;
        protected const float AreaRadius = 1.25f;
        protected const float ArrowLength = 2f;
        protected const float LineLength = 10;

        protected const float FixedAreaRadius = 0.5f;
        protected const float FixedArrowLength = 0.75f;

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
                    if (Event.current.command)
                    {
                        DrawPositionHandle(rocker.transform);
                    }
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