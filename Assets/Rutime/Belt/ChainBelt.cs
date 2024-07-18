/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ChainBelt.cs
 *  Description  :  Chain belt of MonoCurveChain.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  12/09/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Chain;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Chain belt of MonoCurveChain.
    /// </summary>
    [RequireComponent(typeof(MonoCurveChain))]
    public class ChainBelt : EngageMechanism
    {
        /// <summary>
        /// Chain to drive.
        /// </summary>
        protected MonoCurveChain chain;

        /// <summary>
        /// Initialize belt.
        /// </summary>
        public override void Initialize()
        {
            chain = GetComponent<MonoCurveChain>();
            IsInitialized = true;
        }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode)
        {
            chain.Drive(velocity);
            return DriveEngages(-velocity);
        }
    }
}