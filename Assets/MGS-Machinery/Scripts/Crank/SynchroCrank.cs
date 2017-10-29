/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  SynchroCrank.cs
 *  Description  :  Define SynchroCrank component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/25/2015
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/SynchroCrank")]
    public class SynchroCrank : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Crank mechanism.
        /// </summary>
        public List<CrankMechanism> cranks = new List<CrankMechanism>();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            foreach (var crank in cranks)
            {
                crank.DriveMechanism(speedControl);
            }
        }
        #endregion
    }
}