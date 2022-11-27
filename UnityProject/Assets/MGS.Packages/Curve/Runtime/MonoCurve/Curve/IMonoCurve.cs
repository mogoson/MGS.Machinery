/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMonoCurve.cs
 *  Description  :  Define interface of mono curve that base on curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Interface of mono curve.
    /// </summary>
    public interface IMonoCurve
    {
        /// <summary>
        /// Length of mono curve.
        /// </summary>
        float Length { get; }

        /// <summary>
        /// Rebuild mono curve.
        /// </summary>
        void Rebuild();

        /// <summary>
        /// Evaluate point on mono curve at length.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        Vector3 Evaluate(float len);

        /// <summary>
        /// Evaluate point on mono curve at normalized time int the range[0,1].
        /// </summary>
        /// <param name="t">The normalized time.</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        Vector3 EvaluateNormalized(float t);

        /// <summary>
        /// Evaluate local point on mono curve at length.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        Vector3 LocalEvaluate(float len);

        /// <summary>
        /// Evaluate local point on mono curve at normalized time int the range[0,1].
        /// </summary>
        /// <param name="t">The normalized time.</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        Vector3 LocalEvaluateNormalized(float t);
    }
}