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

namespace MGS.Mathematics
{
    /// <summary>
    /// Line in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Line
    {
        /*  Line Definition
         *  if k == double.PositiveInfinity
         *  line is : x = b
         *  else line is : y = kx + b
         */

        #region Field and Property
        /// <summary>
        /// Slope of line.
        /// </summary>
        public double k;

        /// <summary>
        /// Intercept of line.
        /// </summary>
        public double b;

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
            get { return new Line(double.PositiveInfinity, 0); }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="k">Slope of line.</param>
        /// <param name="b">Intercept of line.</param>
        public Line(double k, double b)
        {
            this.k = k;
            this.b = b;
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Get the line that pass vector v1 and v2.
        /// </summary>
        /// <param name="v1">Vector p1.</param>
        /// <param name="v2">Vector p2.</param>
        /// <returns>The line that pass vector v1 and v2.</returns>
        public static Line FromPoints(Vector2D v1, Vector2D v2)
        {
            /*
             *  Ay = kAx + b, By = kBx + b
             * 
             *       By - Ay
             *  k = --------- , b = Ay - kAx
             *       Bx - Ax
             */

            var dx = v2.x - v1.x;
            var dy = v2.y - v1.y;
            var k = 0d;
            var b = 0d;

            if (dx == 0)
            {
                k = double.PositiveInfinity;
                b = v1.x;
            }
            else
            {
                k = dy / dx;
                b = v1.y - k * v1.x;
            }
            return new Line(k, b);
        }
        #endregion
    }
}
