/*************************************************************************
 *  Copyright © 2015-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Planimetry.cs
 *  Description  :  Define planimetry.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace Mogoson.Mathematics
{
    #region Struct
    /// <summary>
    /// Point in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Point
    {
        //========Point Definition========================
        //             |
        //           y |________.point
        //             |        |
        //  ___________|________|_________________
        //             |        x
        //             |
        //================================================

        #region Field and Property
        /// <summary>
        /// X of point.
        /// </summary>
        public double x;

        /// <summary>
        /// Y of point.
        /// </summary>
        public double y;

        /// <summary>
        /// Origin(0,0) of plane rectangular coordinate system.
        /// </summary>
        public static Point Zero
        {
            get { return new Point(); }
        }

        /// <summary>
        /// Point(1,1) in plane rectangular coordinate system.
        /// </summary>
        public static Point One
        {
            get { return new Point(1, 1); }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X of point.</param>
        /// <param name="y">Y of point.</param>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Center of p1 and p2.
        /// </summary>
        /// <param name="p1">Point p1.</param>
        /// <param name="p2">Point p2.</param>
        /// <returns>The center of of p1 and p2.</returns>
        public static Point Center(Point p1, Point p2)
        {
            return (p1 + p2) * 0.5;
        }

        /// <summary>
        /// Distance from p1 to point p2.
        /// </summary>
        /// <param name="p1">Point p1.</param>
        /// <param name="p2">Point p2.</param>
        /// <returns>Distance from p1 to point p2.</returns>
        public static double Distance(Point p1, Point p2)
        {
            //----------------------------------------
            //              _______________________
            //             /        2           2
            //  |p1p2| = \/(x2 - x1) + (y2 - y1)
            //----------------------------------------

            var dx2 = Math.Pow(p2.x - p1.x, 2);
            var dy2 = Math.Pow(p2.y - p1.y, 2);
            return Math.Sqrt(dx2 + dy2);
        }

        public static Point operator +(Point lhs, Point rhs)
        {
            return new Point(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Point operator -(Point lhs, Point rhs)
        {
            return new Point(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Point operator -(Point p)
        {
            return new Point(-p.x, -p.y);
        }

        public static Point operator *(Point lhs, double rhs)
        {
            return new Point(lhs.x * rhs, lhs.y * rhs);
        }

        public static Point operator *(double lhs, Point rhs)
        {
            return rhs * lhs;
        }

        public static bool operator ==(Point lhs, Point rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(Point lhs, Point rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }

    /// <summary>
    /// Circle in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Circle
    {
        //========Circle Definition=======================
        //                       2           2     2
        //  Circle is : (x - c.x) + (y - c.y)  = r
        //================================================

        #region Field and Property
        /// <summary>
        /// Center.
        /// </summary>
        public Point c;

        /// <summary>
        /// Radius.
        /// </summary>
        public double r;

        /// <summary>
        /// Unit circle.
        /// </summary>
        public static Circle Unit
        {
            get { return new Circle(Point.Zero, 1); }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="c">Center.</param>
        /// <param name="r">Radius.</param>
        public Circle(Point c, double r)
        {
            this.c = c;
            this.r = r;
        }
        #endregion
    }

    /// <summary>
    /// Line in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Line
    {
        //========Line Definition=========================
        //  if k == double.PositiveInfinity
        //  line is : x = b
        //  else line is : y = kx + b
        //================================================

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
        /// Get the line that pass p1 and p2.
        /// </summary>
        /// <param name="p1">Point p1.</param>
        /// <param name="p2">Point p2.</param>
        /// <returns>The line that pass p1 and p2.</returns>
        public static Line FromPoints(Point p1, Point p2)
        {
            //--------------------------------
            //  Ay = kAx + b, By = kBx + b
            //
            //       By - Ay
            //  k = --------- , b = Ay - kAx
            //       Bx - Ax
            //--------------------------------

            var dx = p2.x - p1.x;
            var dy = p2.y - p1.y;
            var k = 0d;
            var b = 0d;

            if (dx == 0)
            {
                k = double.PositiveInfinity;
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
    #endregion

    #region Enum
    /// <summary>
    /// Position relation.
    /// </summary>
    public enum Relation
    {
        Undefined = 0,
        Coincidence = 1,
        External = 2, Internal = 3,
        Parallel = 4, Vertical = 5, Intersect = 6,
        OutsideTangent = 7, InsideTangent = 8
    }
    #endregion

    /// <summary>
    /// Planimetry.
    /// </summary>
    public static class Planimetry
    {
        #region Distance
        /// <summary>
        /// Gets the distance from line L1 to line L2.
        /// </summary>
        /// <param name="L1">line L1.</param>
        /// <param name="L2">line L2.</param>
        /// <returns>The distance from line L1 to line L2.</returns>
        public static double GetDistance(Line L1, Line L2)
        {
            //--------------------------------------------------------
            //  y = kx + b <=> kx - y + b = 0
            //  let: A = k, B = -1, C = b
            //  so:
            //         |C1 - C2|                  |b1 - b2|
            //  d = _________________ <=> d = ___________________
            //          _______                     _______
            //         / 2   2                     /     2
            //       \/ A + B                    \/ 1 + k
            //--------------------------------------------------------

            if (L1.k == L2.k)
            {
                var dB = Math.Abs(L2.b - L1.b);
                if (L1.k == 0 || L1.k == double.PositiveInfinity)
                    return dB;
                else
                    return dB / Math.Sqrt(1 + Math.Pow(L1.k, 2));
            }
            else
                return 0;
        }

        /// <summary>
        /// Gets the distance from point to line.
        /// </summary>
        /// <param name="p">Point.</param>
        /// <param name="L">Line.</param>
        /// <returns>The distance from point to line.</returns>
		public static double GetDistance(Point p, Line L)
        {
            //----------------------------------------------------------
            //  y = kx + b <=> kx - y + b = 0
            //  let: A = k, B = -1, C = b
            //  so:
            //       |Ax0 + By0 + C|             |kx0 -y0 + b|
            //  d = _________________ <=> d = ___________________
            //          _______                     _______
            //         / 2   2                     /     2
            //       \/ A + B                    \/ 1 + k
            //----------------------------------------------------------

            if (L.k == 0)
                return Math.Abs(p.y - L.b);
            else if (L.k == double.PositiveInfinity)
                return Math.Abs(p.x - L.b);
            else
                return Math.Abs(L.k * p.x - p.y + L.b) / Math.Sqrt(1 + Math.Pow(L.k, 2));
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
            var cd = Point.Distance(c1.c, c2.c);
            var rd = c1.r + c2.r;
            var rp = Math.Abs(c1.r - c2.r);

            if (cd > rd)
                re = Relation.External;
            else if (cd == rd)
                re = Relation.OutsideTangent;
            else
            {
                if (cd > rp)
                    re = Relation.Intersect;
                else if (cd == rp)
                {
                    if (rp == 0)
                        re = Relation.Coincidence;
                    else
                        re = Relation.InsideTangent;
                }
                else
                    re = Relation.Internal;
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
                re = Relation.External;
            else if (d == c.r)
                re = Relation.OutsideTangent;
            else
                re = Relation.Intersect;
            return re;
        }

        /// <summary>
        /// Get relation of circle and point.
        /// </summary>
        /// <param name="c">Circle.</param>
        /// <param name="p">Point.</param>
        /// <returns>Relation of circle and point.</returns>
        public static Relation GetRelation(Circle c, Point p)
        {
            var re = Relation.Undefined;
            var cp = Point.Distance(c.c, p);

            if (cp > c.r)
                re = Relation.External;
            else if (cp == c.r)
                re = Relation.Coincidence;
            else
                re = Relation.Internal;
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
                    re = Relation.Coincidence;
                else
                    re = Relation.Parallel;
            }
            else
                re = Relation.Intersect;
            return re;
        }

        /// <summary>
        /// Get relation of line and point.
        /// </summary>
        /// <param name="L">Line.</param>
        /// <param name="p">Point.</param>
        /// <returns>Relation of line and point.</returns>
        public static Relation GetRelation(Line L, Point p)
        {
            var re = Relation.Undefined;
            if (L.k == double.PositiveInfinity)
            {
                if (p.x == L.b)
                    re = Relation.Coincidence;
                else
                    re = Relation.External;
            }
            else
            {
                if (p.y == L.k * p.x + L.b)
                    re = Relation.Coincidence;
                else
                    re = Relation.External;
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
        public static List<Point> GetIntersections(Circle c1, Circle c2)
        {
            //-------------------------------------------------------
            //        2         2    2       2         2    2
            //  (x - m) + (y - n) = r, (x - p) + (y - q) = L
            //
            //  get:
            //                           2   2   2   2   2   2
            //  2(m - p)x + 2(n - q)y + p + q + r - m - n - L = 0
            //
            //                         2   2   2   2   2   2
            //        m - p           p + q + r - m - n - L
            //  y = --------- x  +  -------------------------
            //        q - n                2(q - n)
            //
            //                              2   2   2   2   2   2
            //             m - p           p + q + r - m - n - L
            //  let: k = ---------,  b = ------------------------
            //             q - n                 2(q - n)
            //
            //  so: y = kx + b
            //-------------------------------------------------------

            var re = GetRelation(c1, c2);
            if (re == Relation.InsideTangent || re == Relation.OutsideTangent || re == Relation.Intersect)
            {
                var k = 0d;
                var b = 0d;
                var dx = c2.c.x - c1.c.x;
                var dy = c2.c.y - c1.c.y;
                var temp = Math.Pow(c2.c.x, 2) + Math.Pow(c2.c.y, 2) + Math.Pow(c1.r, 2)
                          - Math.Pow(c1.c.x, 2) - Math.Pow(c1.c.y, 2) - Math.Pow(c2.r, 2);
                if (dy == 0)
                {
                    k = double.PositiveInfinity;
                    b = temp / (2 * dx);
                }
                else
                {
                    k = -dx / dy;
                    b = temp / (2 * dy);
                }
                return GetIntersections(c1, new Line(k, b));
            }
            else
                return null;
        }

        /// <summary>
        /// Get intersections of circle and line.
        /// </summary>
        /// <param name="C">Circle.</param>
        /// <param name="L">Line.</param>
        /// <returns>Intersections of circle and line.</returns>
        public static List<Point> GetIntersections(Circle C, Line L)
        {
            //--------------------------------------------------------------------
            //          2          2   2
            //  (x - m) + (y - n) = r
            //   y = kx + b
            //  get:
            //        2    2                      2        2    2
            //  (1 + k  ) x + 2[k(b - n) - m]x + m + (b - n) - r  = 0
            //  let:
            //           2                           2        2    2
            //  a = 1 + k, b = 2[k(b - n) - m], c = m + (b - n) - r
            //
            //         2
            //  get: ax + bx + c = 0
            //                                    ______                 ______
            //               2             -b + \/delta           -b - \/delta
            //  so: delta = b - 4ac, x1 = ----------------, x2 = ----------------
            //                                  2a                      2a
            //--------------------------------------------------------------------

            var re = GetRelation(C, L);
            if (re == Relation.OutsideTangent || re == Relation.Intersect)
            {
                var points = new List<Point>();

                if (L.k == double.PositiveInfinity)
                {
                    var x1 = L.b;
                    var dy = Math.Sqrt(Math.Pow(C.r, 2) - Math.Pow(x1 - C.c.x, 2));
                    var y1 = dy + C.c.y;
                    points.Add(new Point(x1, y1));

                    if (re == Relation.Intersect)
                    {
                        var x2 = x1;
                        var y2 = -dy + C.c.y;
                        points.Add(new Point(x2, y2));
                    }
                }
                else
                {
                    var a = 1 + Math.Pow(L.k, 2);
                    var b = 2 * (L.k * (L.b - C.c.y) - C.c.x);
                    var c = Math.Pow(C.c.x, 2) + Math.Pow(L.b - C.c.y, 2) - Math.Pow(C.r, 2);
                    var delta = Math.Pow(b, 2) - 4 * a * c;

                    var x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    var y1 = L.k * x1 + L.b;
                    points.Add(new Point(x1, y1));

                    if (re == Relation.Intersect)
                    {
                        var x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                        var y2 = L.k * x2 + L.b;
                        points.Add(new Point(x2, y2));
                    }
                }
                return points;
            }
            else
                return null;
        }

        /// <summary>
        /// Get intersection of two lines.
        /// </summary>
        /// <param name="L1">Line L1.</param>
        /// <param name="L2">Line L2.</param>
        /// <returns>Intersection of two lines.</returns>
        public static List<Point> GetIntersections(Line L1, Line L2)
        {
            //------------------------------------
            //  y = k1x + b1    y = k2x + b2
            //
            //              b1 - b2
            //  get: x = -------------
            //              k2 - k1
            //------------------------------------

            if (L1.k == L2.k)
                return null;

            var x = 0d;
            var y = 0d;
            if (L1.k == double.PositiveInfinity)
            {
                x = L1.b;
                y = L2.k * x + L2.b;
            }
            else if (L2.k == double.PositiveInfinity)
            {
                x = L2.b;
                y = L1.k * x + L1.b;
            }
            else
            {
                x = (L1.b - L2.b) / (L2.k - L1.k);
                y = L1.k * x + L1.b;
            }
            return new List<Point> { new Point(x, y) };
        }
        #endregion
    }
}