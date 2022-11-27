/*************************************************************************
 *  Copyright © 2015-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Geometry.cs
 *  Description  :  Define geometry.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Mathematics
{
    /// <summary>
    /// Geometry.
    /// </summary>
    public sealed class Geometry
    {
        #region Distance
        /// <summary>
        /// Gets the distance from line L1 to line L2.
        /// </summary>
        /// <param name="L1">line L1.</param>
        /// <param name="L2">line L2.</param>
        /// <returns>The distance from line L1 to line L2.</returns>
        public static float GetDistance(Line L1, Line L2)
        {
            /*
             *  y = kx + b <=> kx - y + b = 0
             *  let: A = k, B = -1, C = b
             *  so:
             *        |C1 - C2|               |b1 - b2|
             *  d = _____________  <=>  d = _____________
             *         _______                 _______
             *        / 2   2                 /     2
             *      \/ A + B                \/ 1 + k
             */

            var dis = 0f;
            if (L1.k == L2.k)
            {
                dis = Mathf.Abs(L2.b - L1.b);
                if (L1.k != 0 && L1.k != float.PositiveInfinity)
                {
                    dis /= Mathf.Sqrt(1 + Mathf.Pow(L1.k, 2));
                }
            }
            return dis;
        }

        /// <summary>
        /// Gets the distance from vector to line.
        /// </summary>
        /// <param name="p">Vector2.</param>
        /// <param name="L">Line.</param>
        /// <returns>The distance from vector to line.</returns>
		public static float GetDistance(Vector2 p, Line L)
        {
            /*
             *  y = kx + b <=> kx - y + b = 0
             *  let: A = k, B = -1, C = b
             *  so:
             *       |Ax0 + By0 + C|             |kx0 -y0 + b|
             *  d = _________________  <=>  d = _______________
             *         _______                     _______
             *        / 2   2                     /     2
             *      \/ A + B                    \/ 1 + k
             */

            var dis = 0f;
            if (L.k == 0)
            {
                dis = Mathf.Abs(p.y - L.b);
            }
            else if (L.k == float.PositiveInfinity)
            {
                dis = Mathf.Abs(p.x - L.b);
            }
            else
            {
                dis = Mathf.Abs(L.k * p.x - p.y + L.b) / Mathf.Sqrt(1 + Mathf.Pow(L.k, 2));
            }
            return dis;
        }
        #endregion

        #region Relation
        /// <summary>
        /// Get relation of two circles.
        /// </summary>
        /// <param name="c1">Circle c1.</param>
        /// <param name="c2">Circle c2.</param>
        /// <returns>Relation of two circles.</returns>
        public static Relation GetRelation(Circle c1, Circle c2)
        {
            var re = Relation.Undefined;
            var cd = Vector2.Distance(c1.c, c2.c);
            var rd = c1.r + c2.r;
            var rp = Mathf.Abs(c1.r - c2.r);

            if (cd > rd)
            {
                re = Relation.External;
            }
            else if (cd == rd)
            {
                re = Relation.OutsideTangent;
            }
            else
            {
                if (cd > rp)
                {
                    re = Relation.Intersect;
                }
                else if (cd == rp)
                {
                    if (rp == 0)
                    {
                        re = Relation.Coincidence;
                    }
                    else
                    {
                        re = Relation.InsideTangent;
                    }
                }
                else
                {
                    re = Relation.Internal;
                }
            }
            return re;
        }

        /// <summary>
        /// Get relation of circle and line.
        /// </summary>
        /// <param name="c">Circle.</param>
        /// <param name="L">Line.</param>
        /// <returns>Relation of circle and line.</returns>
        public static Relation GetRelation(Circle c, Line L)
        {
            var re = Relation.Undefined;
            var d = GetDistance(c.c, L);

            if (d > c.r)
            {
                re = Relation.External;
            }
            else if (d == c.r)
            {
                re = Relation.OutsideTangent;
            }
            else
            {
                re = Relation.Intersect;
            }
            return re;
        }

        /// <summary>
        /// Get relation of circle and vector.
        /// </summary>
        /// <param name="c">Circle.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>Relation of circle and vector.</returns>
        public static Relation GetRelation(Circle c, Vector2 v)
        {
            var re = Relation.Undefined;
            var cp = Vector2.Distance(c.c, v);

            if (cp > c.r)
            {
                re = Relation.External;
            }
            else if (cp == c.r)
            {
                re = Relation.Coincidence;
            }
            else
            {
                re = Relation.Internal;
            }
            return re;
        }

        /// <summary>
        /// Get relation of two lines.
        /// </summary>
        /// <param name="L1">Line L1.</param>
        /// <param name="L2">Line L2.</param>
        /// <returns>Relation of two lines.</returns>
        public static Relation GetRelation(Line L1, Line L2)
        {
            var re = Relation.Undefined;
            if (L1.k == L2.k)
            {
                if (L1.b == L2.b)
                {
                    re = Relation.Coincidence;
                }
                else
                {
                    re = Relation.Parallel;
                }
            }
            else
            {
                re = Relation.Intersect;
            }
            return re;
        }

        /// <summary>
        /// Get relation of line and vector.
        /// </summary>
        /// <param name="L">Line.</param>
        /// <param name="v">Vector2.</param>
        /// <returns>Relation of line and vector.</returns>
        public static Relation GetRelation(Line L, Vector2 v)
        {
            var re = Relation.Undefined;
            if (L.k == float.PositiveInfinity)
            {
                if (v.x == L.b)
                {
                    re = Relation.Coincidence;
                }
                else
                {
                    re = Relation.External;
                }
            }
            else
            {
                if (v.y == L.k * v.x + L.b)
                {
                    re = Relation.Coincidence;
                }
                else
                {
                    re = Relation.External;
                }
            }
            return re;
        }
        #endregion

        #region Intersection
        /// <summary>
        /// Get intersections of two circles.
        /// </summary>
        /// <param name="c1">Circle c1.</param>
        /// <param name="c2">Circle c2.</param>
        /// <returns>Intersections of two circles.</returns>
        public static List<Vector2> GetIntersections(Circle c1, Circle c2)
        {
            /*
             *               2          2    2          2          2    2
             *  Base: (x - m)  + (y - n)  = r  , (x - p)  + (y - q)  = L
             *
             *  get:
             *                                 2   2   2   2   2   2
             *        2(m - p)x + 2(n - q)y + p + q + r - m - n - L = 0
             *
             *                               2   2   2   2   2   2
             *              m - p           p + q + r - m - n - L
             *        y = --------- x  +  -------------------------
             *              q - n                2(q - n)
             *
             *                               2   2   2   2   2   2
             *              m - p           p + q + r - m - n - L
             *  let:  k = ---------,  b = ------------------------
             *              q - n                 2(q - n)
             *
             *  so:   y = kx + b
             */

            var re = GetRelation(c1, c2);
            if (re == Relation.InsideTangent || re == Relation.OutsideTangent || re == Relation.Intersect)
            {
                var k = 0f;
                var b = 0f;
                var dx = c2.c.x - c1.c.x;
                var dy = c2.c.y - c1.c.y;
                var temp = Mathf.Pow(c2.c.x, 2) + Mathf.Pow(c2.c.y, 2) + Mathf.Pow(c1.r, 2) - Mathf.Pow(c1.c.x, 2) - Mathf.Pow(c1.c.y, 2) - Mathf.Pow(c2.r, 2);
                if (dy == 0)
                {
                    k = float.PositiveInfinity;
                    b = temp / (2 * dx);
                }
                else
                {
                    k = -dx / dy;
                    b = temp / (2 * dy);
                }
                return GetIntersections(c1, new Line(k, b));
            }
            return null;
        }

        /// <summary>
        /// Get intersections of circle and line.
        /// </summary>
        /// <param name="C">Circle.</param>
        /// <param name="L">Line.</param>
        /// <returns>Intersections of circle and line.</returns>
        public static List<Vector2> GetIntersections(Circle C, Line L)
        {
            /*
             *          2          2   2
             *  (x - m) + (y - n) = r
             *   y = kx + b
             *  get:
             *        2    2                      2        2    2
             *  (1 + k  ) x + 2[k(b - n) - m]x + m + (b - n) - r  = 0
             *  let:
             *           2                           2        2    2
             *  a = 1 + k, b = 2[k(b - n) - m], c = m + (b - n) - r
             *
             *         2
             *  get: ax + bx + c = 0
             *                                    ______                 ______
             *               2             -b + \/delta           -b - \/delta
             *  so: delta = b - 4ac, x1 = ----------------, x2 = ----------------
             *                                  2a                      2a
             */

            var re = GetRelation(C, L);
            if (re == Relation.OutsideTangent || re == Relation.Intersect)
            {
                var points = new List<Vector2>();
                if (L.k == float.PositiveInfinity)
                {
                    var x1 = L.b;
                    var dy = Mathf.Sqrt(Mathf.Pow(C.r, 2) - Mathf.Pow(x1 - C.c.x, 2));
                    var y1 = dy + C.c.y;
                    points.Add(new Vector2(x1, y1));

                    if (re == Relation.Intersect)
                    {
                        var x2 = x1;
                        var y2 = -dy + C.c.y;
                        points.Add(new Vector2(x2, y2));
                    }
                }
                else
                {
                    var a = 1 + Mathf.Pow(L.k, 2);
                    var b = 2 * (L.k * (L.b - C.c.y) - C.c.x);
                    var c = Mathf.Pow(C.c.x, 2) + Mathf.Pow(L.b - C.c.y, 2) - Mathf.Pow(C.r, 2);
                    var delta = Mathf.Pow(b, 2) - 4 * a * c;

                    var x1 = (-b + Mathf.Sqrt(delta)) / (2 * a);
                    var y1 = L.k * x1 + L.b;
                    points.Add(new Vector2(x1, y1));

                    if (re == Relation.Intersect)
                    {
                        var x2 = (-b - Mathf.Sqrt(delta)) / (2 * a);
                        var y2 = L.k * x2 + L.b;
                        points.Add(new Vector2(x2, y2));
                    }
                }
                return points;
            }
            return null;
        }

        /// <summary>
        /// Get intersection of two lines.
        /// </summary>
        /// <param name="L1">Line L1.</param>
        /// <param name="L2">Line L2.</param>
        /// <returns>Intersection of two lines.</returns>
        public static List<Vector2> GetIntersections(Line L1, Line L2)
        {
            /*
             *  y1 = k1x + b1, y2 = k2x + b2
             *
             *              b1 - b2
             *  get: x = -------------
             *              k2 - k1
             */

            if (L1.k == L2.k)
            {
                return null;
            }

            var x = 0f;
            var y = 0f;
            if (L1.k == float.PositiveInfinity)
            {
                x = L1.b;
                y = L2.k * x + L2.b;
            }
            else if (L2.k == float.PositiveInfinity)
            {
                x = L2.b;
                y = L1.k * x + L1.b;
            }
            else
            {
                x = (L1.b - L2.b) / (L2.k - L1.k);
                y = L1.k * x + L1.b;
            }
            return new List<Vector2> { new Vector2(x, y) };
        }
        #endregion
    }
}