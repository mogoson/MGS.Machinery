/*************************************************************************
 *  Copyright © 2015-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Line.cs
 *  Description  :  Line in plane rectangular coordinate system.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Mathematics
{
    /// <summary>
    /// Line in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Line
    {
        /*  Line Definition
         *  if k == float.PositiveInfinity
         *  line is : x = b
         *  else line is : y = kx + b
         */

        #region Field and Property
        /// <summary>
        /// Slope of line.
        /// </summary>
        public float k;

        /// <summary>
        /// Intercept of line.
        /// </summary>
        public float b;

        /// <summary>
        /// Horizontal line (x axis).
        /// </summary>
        public static Line Horizontal
        {
            get { return new Line(0, 0); }
        }

        /// <summary>
        /// Vertical line (y axis).
        /// </summary>
        public static Line Vertical
        {
            get { return new Line(float.PositiveInfinity, 0); }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="k">Slope of line.</param>
        /// <param name="b">Intercept of line.</param>
        public Line(float k, float b)
        {
            this.k = k;
            this.b = b;
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get the line that pass vector v1 and v2.
        /// </summary>
        /// <param name="p1">Vector2 p1.</param>
        /// <param name="p2">Vector2 p2.</param>
        /// <returns>The line that pass vector v1 and v2.</returns>
        public static Line FromPoints(Vector2 p1, Vector2 p2)
        {
            /*
             *  Ay = kAx + b, By = kBx + b
             * 
             *       By - Ay
             *  k = --------- , b = Ay - kAx
             *       Bx - Ax
             */

            var dx = p2.x - p1.x;
            var dy = p2.y - p1.y;
            var k = 0f;
            var b = 0f;

            if (dx == 0)
            {
                k = float.PositiveInfinity;
                b = p1.x;
            }
            else
            {
                k = dy / dx;
                b = p1.y - k * p1.x;
            }
            return new Line(k, b);
        }
        #endregion
    }
}
