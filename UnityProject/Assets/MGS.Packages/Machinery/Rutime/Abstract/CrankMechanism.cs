/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankMechanism.cs
 *  Description  :  Crank mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Crank mechanism.
    /// </summary>
	public abstract class CrankMechanism : LinkRockerMechanism
    {
        #region Field and Property
        /// <summary>
        /// Current rotate angle of crank.
        /// </summary>
        public float Angle { protected set; get; }

        /// <summary>
        /// Start eulerAngles.
        /// </summary>
        public Vector3 StartAngles { protected set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize crank.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            StartAngles = transform.localEulerAngles;
        }
        #endregion
    }
}