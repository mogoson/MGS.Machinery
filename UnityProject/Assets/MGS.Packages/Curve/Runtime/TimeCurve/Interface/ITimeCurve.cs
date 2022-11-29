/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ITimeCurve.cs
 *  Description  :  Define time curve interface.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/24/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Interface of time curve.
    /// </summary>
    public interface ITimeCurve : ICurve
    {
        /// <summary>
        /// Evaluate the curve at time.
        /// </summary>
        /// <param name="t">The time within the curve you want to evaluate (the horizontal axis in the curve graph).</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        Vector3 Evaluate(float t);
    }
}