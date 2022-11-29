/*************************************************************************
 *  Copyright ? 2018-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Maths.cs
 *  Description  :  Define mathematical concepts and methods.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/29/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Mathematics
{
    /// <summary>
    /// Mathematical concepts and methods.
    /// </summary>
    public sealed class Maths
    {
        #region Public Method
        /// <summary>
        /// Interpolates between a and b by t.
        /// </summary>
        /// <param name="from">Start value of interpolate value.</param>
        /// <param name="to">End value of interpolate value.</param>
        /// <param name="t">t is clamped between 0 and 1.</param>
        /// <returns></returns>
        public static float Lerp(float from, float to, float t)
        {
            return from + (to - from) * t;
        }
        #endregion
    }
}