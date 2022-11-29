/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveUtility.cs
 *  Description  :  Utility for mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/15/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Utility for mono curve.
    /// </summary>
    public sealed class MonoCurveUtility
    {
        /// <summary>
        /// Get segment count of mono curve.
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="segment"></param>
        /// <param name="differ">Differentiation.</param>
        /// <returns>Detail count of mono curve.</returns>
        public static int GetSegmentCount(IMonoCurve curve, float segment, out float differ)
        {
            //AwayFromZero means that 12.5 -> 13
            var count = (int)Math.Round(curve.Length / segment, MidpointRounding.AwayFromZero);
            count = Mathf.Max(count, 1);
            differ = curve.Length / count;
            return count;
        }
    }
}