/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: Planimetry.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/9/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          Planimetry              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     12/9/2015       1.0        Build this file.
 *************************************************************************/

namespace Developer.Math.Planimetry
{
    using System;
    using System.Collections.Generic;

    #region Struct
    /// <summary>
    /// Express a point in the plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Point
    {
        //========Point Definition===================
        //             |
        //           y |________.point
        //             |        |
        //  ___________|________|_________________
        //             |        x
        //             |
        //===========================================
        public double x;
        public double y;

        /// <summary>
        /// Origin(0,0) of plane rectangular coordinate system.
        /// </summary>
        public static Point Zero
        {
            get { return new Point(); }
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /// <summary>
    /// Circle in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Circle
    {
        //========Circle Definition==================
        //                       2           2     2
        //  Circle is : (x - c.x) + (y - c.y)  = r
        //===========================================

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

        public Circle(Point c, double r)
        {
            this.c = c;
            this.r = r;
        }
    }

    /// <summary>
    /// Line in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Line
    {
        //========Line Definition===========
        //  if k == double.PositiveInfinity
        //  line is : x = b
        //  else line is : y = kx + b
        //==================================

        /// <summary>
        /// Slope of line.
        /// </summary>
        public double k;

        /// <summary>
        /// Intercept of line.
        /// </summary>
        public double b;

        /// <summary>
        /// Horizontal line(x axis).
        /// </summary>
        public static Line horizontal
        {
            get { return new Line(0, 0); }
        }

        /// <summary>
        /// Vertical line(y axis).
        /// </summary>
        public static Line vertical
        {
            get { return new Line(double.PositiveInfinity, 0); }
        }

        /// <summary>
        /// Get a line that pass point A and point B.
        /// </summary>
        public static Line GetLine(Point A, Point B)
        {
            //------------------------------------------
            //  Ay = kAx + b, By = kBx + b
            //
            //       By - Ay
            //  k = --------- , b = Ay - kAx
            //       Bx - Ax
            //------------------------------------------

            var dx = B.x - A.x;
            var dy = B.y - A.y;
            var k = 0d;
            var b = 0d;

            if (dx == 0)
            {
                k = double.PositiveInfinity;
                b = A.x;
            }
            else
            {
                k = dy / dx;
                b = A.y - k * A.x;
            }
            return new Line(k, b);
        }

        /// <summary>
        /// Get center point of point A and point B.
        /// </summary>
        public static Point GetCenter(Point A, Point B)
        {
            return new Point((B.x + A.x) / 2, (B.y + A.y) / 2);
        }

        public Line(double k, double b)
        {
            this.k = k;
            this.b = b;
        }
    }

    /// <summary>
    /// Triangle in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Triangle
    {
        //========Triangle Definition=======
        //                 A
        //                /\
        //            c  /  \ b
        //              /    \
        //           B /______\ C
        //                 a
        //==================================

        /// <summary>
        /// Vertex A;
        /// </summary>
        public Point A;

        /// <summary>
        /// Vertex B;
        /// </summary>
        public Point B;

        /// <summary>
        /// Vertex C;
        /// </summary>
        public Point C;

        /// <summary>
        /// AB length.
        /// </summary>
        public double a { get { return Planimetry.GetDistance(B, C); } }

        /// <summary>
        /// BC length.
        /// </summary>
        public double b { get { return Planimetry.GetDistance(A, C); } }

        /// <summary>
        /// AC length.
        /// </summary>
        public double c { get { return Planimetry.GetDistance(A, B); } }

        /// <summary>
        /// Center of gravity.
        /// </summary>
        public Point G
        {
            get
            {
                if (Check(A, B, C))
                {
                    var p1 = Line.GetCenter(A, B);
                    var p2 = Line.GetCenter(A, C);

                    var L1 = Line.GetLine(p1, C);
                    var L2 = Line.GetLine(p2, B);
                    return Planimetry.GetIntersections(L1, L2)[0];
                }
                else
                    return new Point(double.NaN, double.NaN);
            }
        }

        public Triangle(Point A, Point B, Point C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }

        /// <summary>
        /// /Check the three vertexs can be constituted a triangle.
        /// </summary>
        public static bool Check(Point A, Point B, Point C)
        {
            var dABx = B.x - A.x;
            var dABy = B.y - A.y;

            var dBCx = C.x - B.x;
            var dBCy = C.y - B.y;

            if (dABx == dBCx && dABy == dBCy)
                return false;
            else
                return true;
        }

        /// <summary>
        /// /Check the three side can be constituted a triangle.
        /// </summary>
        public static bool Check(double a, double b, double c)
        {
            return a > 0 && b > 0 && c > 0 && a + b > c && a - b < c;
        }

        /// <summary>
        /// Check the triangle is reasonable or not.
        /// </summary>
        public static bool Check(Triangle t)
        {
            return Check(t.A, t.B, t.C);
        }

        public static double GetAngleABC(double a, double b, double c)
        {
            //-----------------------------
            //             2   2   2
            //            a + c - b
            //	cos x = ---------------
            //               2ac
            //-----------------------------

            if (a == 0 || b == 0 || c == 0)
                return double.NaN;

            var m = Math.Pow(a, 2) + Math.Pow(c, 2) - Math.Pow(b, 2);
            var d = 2 * a * c;
            return Math.Acos(m / d);
        }
    }
    #endregion

    #region Enum
    /// <summary>
    /// Position relation.
    /// </summary>
    public enum Relation
    {
        Coincidence,
        External, Internal,
        Parallel, Vertical, Intersect,
        OutsideTangent, InsideTangent
    }
    #endregion

    /// <summary>
    /// Planimetry.
    /// </summary>
    public static class Planimetry
    {
        #region Distance
        /// <summary>
        /// Gets the distance from point1 to point2.
        /// </summary>
        /// <returns>The distance.</returns>
        /// <param name="p1">Point 1.</param>
        /// <param name="p2">Point 2.</param>
        public static double GetDistance(Point p1, Point p2)
        {
            //----------------------------------------
            //              _______________________
            //             /        2           2
            //  |p1p2| = \/(x2 - x1) + (y2 - y1)
            //----------------------------------------

            var dx_2 = Math.Pow(p2.x - p1.x, 2);
            var dy_2 = Math.Pow(p2.y - p1.y, 2);
            return Math.Sqrt(dx_2 + dy_2);
        }

        /// <summary>
        /// Gets the distance from line1 to line2,
        /// The two lines must are parallel.
        /// </summary>
        /// <param name="L1">line1</param>
        /// <param name="L2">line2</param>
        /// <returns>Distance</returns>
        public static double GetDistance(Line L1, Line L2)
        {
            //------------------------------------------------------------------
            //  y = kx + b <=> kx - y + b = 0 <=> A = k, B = -1, C = b
            //
            //         |C1 - C2|                  |b1 - b2|
            //  d = _________________ <=> d = ___________________
            //          _______                     _______
            //         / 2   2                     /     2
            //       \/ A + B                    \/ 1 + k
            //------------------------------------------------------------------

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
        /// <param name="p">Point</param>
        /// <param name="L">Line</param>
        /// <returns>Distance</returns>
		public static double GetDistance(Point p, Line L)
        {
            //------------------------------------------------------------------
            //  y = kx + b <=> kx - y + b = 0 <=> A = k, B = -1, C = b
            //
            //       |Ax0 + By0 + C|             |kx0 -y0 + b|
            //  d = _________________ <=> d = ___________________
            //          _______                     _______
            //         / 2   2                     /     2
            //       \/ A + B                    \/ 1 + k
            //------------------------------------------------------------------

            if (L.k == 0)
                return Math.Abs(p.y - L.b);
            if (L.k == double.PositiveInfinity)
                return Math.Abs(p.x - L.b);
            else
                return Math.Abs(L.k * p.x - p.y + L.b) / Math.Sqrt(1 + Math.Pow(L.k, 2));
        }
        #endregion

        #region Relation
        /// <summary>
        /// Get relation of two circles.
        /// </summary>
        /// <param name="c1">Circle 1</param>
        /// <param name="c2">Circle 2</param>
        /// <returns>Position relation</returns>
        public static Relation GetRelation(Circle c1, Circle c2)
        {
            var re = Relation.External;
            var cd = GetDistance(c1.c, c2.c);
            var d = c1.r + c2.r;
            var p = Math.Abs(c1.r - c2.r);

            if (cd > d)
                re = Relation.External;
            if (cd == d)
                re = Relation.OutsideTangent;
            if (cd < d)
                re = Relation.Intersect;
            if (cd == p)
                re = Relation.InsideTangent;
            if (cd < p)
                re = Relation.Internal;
            if (cd == 0 && p == 0)
                re = Relation.Coincidence;
            return re;
        }

        /// <summary>
        /// Get relation of circle and line.
        /// </summary>
        /// <param name="c">Circle</param>
        /// <param name="L">Line</param>
        /// <returns>Position relation</returns>
        public static Relation GetRelation(Circle c, Line L)
        {
            var re = Relation.External;
            var d = GetDistance(c.c, L);
            if (d > c.r)
                re = Relation.External;
            if (d == c.r)
                re = Relation.OutsideTangent;
            if (d < c.r)
                re = Relation.Intersect;
            return re;
        }

        /// <summary>
        /// Get relation of circle and point.
        /// </summary>
        /// <param name="c">Circle</param>
        /// <param name="p">Point</param>
        /// <returns>Position relation</returns>
        public static Relation GetRelation(Circle c, Point p)
        {
            var re = Relation.External;
            var cp = GetDistance(c.c, p);
            if (cp > c.r)
                re = Relation.External;
            if (cp == c.r)
                re = Relation.Coincidence;
            if (cp < c.r)
                re = Relation.Internal;
            return re;
        }

        /// <summary>
        /// Get relation of two lines.
        /// </summary>
        /// <param name="L1">Line 1</param>
        /// <param name="L2">Line 2</param>
        /// <returns>Position relation</returns>
        public static Relation GetRelation(Line L1, Line L2)
        {
            var re = Relation.Coincidence;
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
        /// <param name="L">Line</param>
        /// <param name="p">Point</param>
        /// <returns>Position relation</returns>
        public static Relation GetRelation(Line L, Point p)
        {
            var re = Relation.External;
            if (L.k == double.PositiveInfinity)
            {
                if (p.x == L.b)
                    re = Relation.Coincidence;
                else
                    re = Relation.External;
            }
            else
            {
                //Meet the linear equation.
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
        /// Get two circles's intersections.
        /// </summary>
        /// <param name="c1">Circle 1</param>
        /// <param name="c2">Circle 2</param>
        /// <returns>Intersections</returns>
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
        /// <param name="c">Circle</param>
        /// <param name="L">Line</param>
        /// <returns>Intersections</returns>
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
                var I = new List<Point>();

                if (L.k == double.PositiveInfinity)
                {
                    var x1 = L.b;
                    var dy = Math.Sqrt(Math.Pow(C.r, 2) - Math.Pow(x1 - C.c.x, 2));
                    var y1 = dy + C.c.y;
                    I.Add(new Point(x1, y1));

                    if (re == Relation.Intersect)
                    {
                        var x2 = x1;
                        var y2 = -dy + C.c.y;
                        I.Add(new Point(x2, y2));
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
                    I.Add(new Point(x1, y1));

                    if (re == Relation.Intersect)
                    {
                        var x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                        var y2 = L.k * x2 + L.b;
                        I.Add(new Point(x2, y2));
                    }
                }
                return I;
            }
            else
                return null;
        }

        /// <summary>
        /// Get intersections of two lines,
        /// The two lines must are Vertical.
        /// </summary>
        /// <param name="L1">Line 1</param>
        /// <param name="L2">Line 2</param>
        /// <returns>Intersections</returns>
        public static List<Point> GetIntersections(Line L1, Line L2)
        {
            //--------------------------------------------
            //  y = k1x + b1    y = k2x + b2
            //
            //              b1 - b2
            //  get: x = -------------
            //              k2 - k1
            //--------------------------------------------

            if (L1.k == L2.k)
                return null;

            var x = (L1.b - L2.b) / (L2.k - L1.k);
            var y = L1.k * x + L1.b;

            var I = new List<Point>();
            I.Add(new Point(x, y));
            return I;
        }
        #endregion
    }
}