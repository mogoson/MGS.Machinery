/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  SyTelescopicArm.cs
 *  Description  :  Define SyTelescopicArm component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/24/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/SyTelescopicArm")]
    public class SyTelescopicArm : TelescopicArmMechanism
    {
        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            foreach (var joint in tJoints)
            {
                joint.DriveMechanism(speedControl);
            }
        }
        #endregion
    }
}