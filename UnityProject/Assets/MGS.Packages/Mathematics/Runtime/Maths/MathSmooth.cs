/*************************************************************************
 *  Copyright © 2016-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MathSmooth.cs
 *  Description  :  Mathematical method to smooth data.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/21/2016
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Mathematics
{
    /// <summary>
    /// Linear Smooth.
    /// </summary>
    public sealed class LinearSmooth
    {
        #region Public Method
        /// <summary>
        /// Three point linear smooth.
        /// </summary>
        /// <param name="source">Data source.</param>
        /// <returns>Smooth result.</returns>
        public static float[] ThreePointSmooth(float[] source)
        {
            var n = source.Length;
            var r = new float[n];
            if (n < 3)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = source[i];
                }
            }
            else
            {
                r[0] = (5 * source[0] + 2 * source[1] - source[2]) / 6;
                for (int i = 1; i < n - 1; i++)
                {
                    r[i] = (source[i - 1] + source[i] + source[i + 1]) / 3;
                }
                r[n - 1] = (5 * source[n - 1] + 2 * source[n - 2] - source[n - 3]) / 6;
            }
            return r;
        }

        /// <summary>
        /// Five point linear smooth.
        /// </summary>
        /// <param name="source">Data source.</param>
        /// <returns>Smooth result.</returns>
        public static float[] FivePointSmooth(float[] source)
        {
            var n = source.Length;
            var r = new float[n];
            if (n < 5)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = source[i];
                }
            }
            else
            {
                r[0] = (3 * source[0] + 2 * source[1] + source[2] - source[4]) / 5;
                r[1] = (4 * source[0] + 3 * source[1] + 2 * source[2] + source[3]) / 10;
                for (int i = 2; i < n - 2; i++)
                {
                    r[i] = (source[i - 2] + source[i - 1] + source[i] + source[i + 1] + source[i + 2]) / 5;
                }
                r[n - 2] = (4 * source[n - 1] + 3 * source[n - 2] + 2 * source[n - 3] + source[n - 4]) / 10;
                r[n - 1] = (3 * source[n - 1] + 2 * source[n - 2] + source[n - 3] - source[n - 5]) / 5;
            }
            return r;
        }

        /// <summary>
        /// Seven point linear smooth.
        /// </summary>
        /// <param name="source">Data source.</param>
        /// <returns>Smooth result.</returns>
        public static float[] SevenPointSmooth(float[] source)
        {
            var n = source.Length;
            var r = new float[n];
            if (n < 7)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = source[i];
                }
            }
            else
            {
                r[0] = (13 * source[0] + 10 * source[1] + 7 * source[2] + 4 * source[3] +
                    source[4] - 2 * source[5] - 5 * source[6]) / 28;

                r[1] = (5 * source[0] + 4 * source[1] + 3 * source[2] + 2 * source[3] +
                    source[4] - source[6]) / 14;

                r[2] = (7 * source[0] + 6 * source[1] + 5 * source[2] + 4 * source[3] +
                    3 * source[4] + 2 * source[5] + source[6]) / 28;

                for (int i = 3; i < n - 3; i++)
                {
                    r[i] = (source[i - 3] + source[i - 2] + source[i - 1] + source[i] +
                        source[i + 1] + source[i + 2] + source[i + 3]) / 7;
                }

                r[n - 3] = (7 * source[n - 1] + 6 * source[n - 2] + 5 * source[n - 3] +
                    4 * source[n - 4] + 3 * source[n - 5] + 2 * source[n - 6] + source[n - 7]) / 28;

                r[n - 2] = (5 * source[n - 1] + 4 * source[n - 2] + 3 * source[n - 3] +
                    2 * source[n - 4] + source[n - 5] - source[n - 7]) / 14;

                r[n - 1] = (13 * source[n - 1] + 10 * source[n - 2] + 7 * source[n - 3] +
                    4 * source[n - 4] + source[n - 5] - 2 * source[n - 6] - 5 * source[n - 7]) / 28;
            }
            return r;
        }
        #endregion
    }

    /// <summary>
    /// Quadratic Smooth.
    /// </summary>
    public sealed class QuadraticSmooth
    {
        #region Public Method
        /// <summary>
        /// Five point quadratic smooth.
        /// </summary>
        /// <param name="fs">Data source.</param>
        /// <returns>Smooth result.</returns>
        public static float[] FivePointSmooth(float[] fs)
        {
            var n = fs.Length;
            var r = new float[n];
            if (n < 5)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = fs[i];
                }
            }
            else
            {
                r[0] = (31 * fs[0] + 9 * fs[1] - 3 * fs[2] - 5 * fs[3] + 3 * fs[4]) / 35;
                r[1] = (9 * fs[0] + 13 * fs[1] + 12 * fs[2] + 6 * fs[3] - 5 * fs[4]) / 35;
                for (int i = 2; i < n - 2; i++)
                {
                    r[i] = (-3 * (fs[i - 2] + fs[i + 2]) +
                              12 * (fs[i - 1] + fs[i + 1]) + 17 * fs[i]) / 35;
                }
                r[n - 2] = (9 * fs[n - 1] + 13 * fs[n - 2] + 12 * fs[n - 3] + 6 * fs[n - 4] - 5 * fs[n - 5]) / 35;
                r[n - 1] = (31 * fs[n - 1] + 9 * fs[n - 2] - 3 * fs[n - 3] - 5 * fs[n - 4] + 3 * fs[n - 5]) / 35;
            }
            return r;
        }

        /// <summary>
        /// Seven point quadratic smooth.
        /// </summary>
        /// <param name="fs">Data source.</param>
        /// <returns>Smooth result.</returns>
        public static float[] SevenPointSmooth(float[] fs)
        {
            var n = fs.Length;
            var r = new float[n];
            if (n < 7)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = fs[i];
                }
            }
            else
            {
                r[0] = (32 * fs[0] + 15 * fs[1] + 3 * fs[2] - 4 * fs[3] -
                    6 * fs[4] - 3 * fs[5] + 5 * fs[6]) / 42;

                r[1] = (5 * fs[0] + 4 * fs[1] + 3 * fs[2] + 2 * fs[3] +
                    fs[4] - fs[6]) / 14;

                r[2] = (fs[0] + 3 * fs[1] + 4 * fs[2] + 4 * fs[3] +
                    3 * fs[4] + fs[5] - 2 * fs[6]) / 14;

                for (int i = 3; i < n - 3; i++)
                {
                    r[i] = (-2 * (fs[i - 3] + fs[i + 3]) + 3 * (fs[i - 2] + fs[i + 2]) +
                        6 * (fs[i - 1] + fs[i + 1]) + 7 * fs[i]) / 21;
                }

                r[n - 3] = (fs[n - 1] + 3 * fs[n - 2] + 4 * fs[n - 3] + 4 * fs[n - 4] +
                    3 * fs[n - 5] + fs[n - 6] - 2 * fs[n - 7]) / 14;

                r[n - 2] = (5 * fs[n - 1] + 4 * fs[n - 2] + 3 * fs[n - 3] + 2 * fs[n - 4] +
                    fs[n - 5] - fs[n - 7]) / 14;

                r[n - 1] = (32 * fs[n - 1] + 15 * fs[n - 2] + 3 * fs[n - 3] - 4 * fs[n - 4] -
                    6 * fs[n - 5] - 3 * fs[n - 6] + 5 * fs[n - 7]) / 42;
            }
            return r;
        }
        #endregion
    }

    /// <summary>
    /// Cubic Smooth.
    /// </summary>
    public sealed class CubicSmooth
    {
        #region Public Method
        /// <summary>
        /// Five point cubic smooth.
        /// </summary>
        /// <param name="fs">Data source.</param>
        /// <returns>Smooth result.</returns>
        public static float[] FivePointSmooth(float[] fs)
        {
            var n = fs.Length;
            var r = new float[n];
            if (n < 5)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = fs[i];
                }
            }
            else
            {
                r[0] = (69 * fs[0] + 4 * fs[1] - 6 * fs[2] +
                    4 * fs[3] - fs[4]) / 70;

                r[1] = (2 * fs[0] + 27 * fs[1] + 12 * fs[2] -
                    8 * fs[3] + 2 * fs[4]) / 35;

                for (int i = 2; i < n - 2; i++)
                {
                    r[i] = (-3 * (fs[i - 2] + fs[i + 2]) + 12 * (fs[i - 1] +
                        fs[i + 1]) + 17 * fs[i]) / 35;
                }

                r[n - 2] = (2 * fs[n - 5] - 8 * fs[n - 4] + 12 * fs[n - 3] +
                    27 * fs[n - 2] + 2 * fs[n - 1]) / 35;

                r[n - 1] = (-fs[n - 5] + 4 * fs[n - 4] - 6 * fs[n - 3] +
                    4 * fs[n - 2] + 69 * fs[n - 1]) / 70;
            }
            return r;
        }

        /// <summary>
        /// Seven point cubic smooth.
        /// </summary>
        /// <param name="fs">Data source.</param>
        /// <returns>Smooth result.</returns>
        public static float[] SevenPointSmooth(float[] fs)
        {
            var n = fs.Length;
            var r = new float[n];
            if (n < 7)
            {
                for (int i = 0; i < n; i++)
                {
                    r[i] = fs[i];
                }
            }
            else
            {
                r[0] = (39 * fs[0] + 8 * fs[1] - 4 * fs[2] - 4 * fs[3] +
                    fs[4] + 4 * fs[5] - 2 * fs[6]) / 42;

                r[1] = (8 * fs[0] + 19 * fs[1] + 16 * fs[2] + 6 * fs[3] -
                    4 * fs[4] - 7 * fs[5] + 4 * fs[6]) / 42;

                r[2] = (-4 * fs[0] + 16 * fs[1] + 19 * fs[2] + 12 * fs[3] +
                    2 * fs[4] - 4 * fs[5] + fs[6]) / 42;

                for (int i = 3; i <= n - 4; i++)
                {
                    r[i] = (-2 * (fs[i - 3] + fs[i + 3]) + 3 * (fs[i - 2] +
                        fs[i + 2]) + 6 * (fs[i - 1] + fs[i + 1]) + 7 * fs[i]) / 21;
                }

                r[n - 3] = (-4 * fs[n - 1] + 16 * fs[n - 2] + 19 * fs[n - 3] +
                    12 * fs[n - 4] + 2 * fs[n - 5] - 4 * fs[n - 6] + fs[n - 7]) / 42;

                r[n - 2] = (8 * fs[n - 1] + 19 * fs[n - 2] + 16 * fs[n - 3] +
                    6 * fs[n - 4] - 4 * fs[n - 5] - 7 * fs[n - 6] + 4 * fs[n - 7]) / 42;

                r[n - 1] = (39 * fs[n - 1] + 8 * fs[n - 2] - 4 * fs[n - 3] -
                    4 * fs[n - 4] + fs[n - 5] + 4 * fs[n - 6] - 2 * fs[n - 7]) / 42;
            }
            return r;
        }
        #endregion
    }
}