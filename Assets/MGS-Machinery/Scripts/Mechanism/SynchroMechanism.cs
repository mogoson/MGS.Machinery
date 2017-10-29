/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  SynchroMechanism.cs
 *  Description  :  Define SynchroMechanism component.
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
    [AddComponentMenu("Developer/Machinery/SynchroMechanism")]
    public class SynchroMechanism : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Mechanisms.
        /// </summary>
        public List<Mechanism> mechanisms = new List<Mechanism>();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanisms.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            foreach (var mechanism in mechanisms)
            {
                mechanism.DriveMechanism(speedControl);
            }
        }
        #endregion
    }
}