/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SinHose.cs
 *  Description  :  Render dynamic hose mesh base on sin curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/31/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using UnityEngine;

namespace Mogoson.CurveHose
{
    /// <summary>
    /// Render dynamic hose mesh base on sin curve.
    /// </summary>
    [AddComponentMenu("Mogoson/CurveHose/SinHose")]
    public class SinHose : MonoCurveHose
    {
        #region Field and Property
        /// <summary>
        /// Args of sin curve.
        /// </summary>
        public SinArgs args = new SinArgs(1, 1, 0, 0);

        /// <summary>
        /// Max key of sin curve.
        /// </summary>
        public float maxKey = 2 * Mathf.PI;

        /// <summary>
        /// Curve for hose.
        /// </summary>
        protected override ICurve Curve { get { return curve; } }

        /// <summary>
        /// Curve of hose.
        /// </summary>
        protected SinCurve curve = new SinCurve();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild hose.
        /// </summary>
        public override void Rebuild()
        {
            curve.args = args;
            curve.MaxKey = maxKey;
            base.Rebuild();
        }
        #endregion
    }
}