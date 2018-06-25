/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechanismUnit.cs
 *  Description  :  Define mechanism unit.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/25/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace Mogoson.Machinery
{
    /// <summary>
    /// Mechanism unit.
    /// </summary>
    [Serializable]
    public struct MechanismUnit
    {
        #region Field and Property
        /// <summary>
        /// Mechanism to drive.
        /// </summary>
        public Mechanism mechanism;

        /// <summary>
        /// Coefficient of velocity.
        /// </summary>
        public float coefficient;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mechanism">Mechanism to drive.</param>
        /// <param name="coefficient">Coefficient of velocity.</param>
        public MechanismUnit(Mechanism mechanism, float coefficient)
        {
            this.mechanism = mechanism;
            this.coefficient = coefficient;
        }
        #endregion
    }
}