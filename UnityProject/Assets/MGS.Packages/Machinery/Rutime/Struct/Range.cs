/*************************************************************************
 *  Copyright © 2017-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Range.cs
 *  Description  :  Define range form min to max.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/8/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Machineries
{
    /// <summary>
    /// Range form min to max.
    /// </summary>
    [Serializable]
    public struct Range
    {
        /// <summary>
        /// Min value of range.
        /// </summary>
        public float min;

        /// <summary>
        /// Max value of range.
        /// </summary>
        public float max;

        /// <summary>
        /// Length of range from min to max.
        /// </summary>
        public float Length
        {
            get { return max - min; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="min">Min value of range.</param>
        /// <param name="max">Max value of range.</param>
        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}