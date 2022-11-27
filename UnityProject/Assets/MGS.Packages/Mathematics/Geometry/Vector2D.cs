/*************************************************************************
 *  Copyright © 2015-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Vector2D.cs
 *  Description  :  Vector2D in plane rectangular coordinate system.
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
    /// Vector in plane rectangular coordinate system.
    /// </summary>
    [Serializable]
    public struct Vector2D
    {
        /*  Vector Definition
         * 
         *             |
         *           y |________.vector
         *             |        |
         *  ___________|________|___________
         *             |        x
         *             |
         */

        #region Field and Property
        /// <summary>
        /// X of vector.
        /// </summary>
        public double x;

        /// <summary>
        /// Y of vector.
        /// </summary>
        public double y;

        /// <summary>
        /// Origin(0,0) of plane rectangular coordinate system.
        /// </summary>
        public static Vector2D Zero
        {
            get { return new Vector2D(); }
        }

        /// <summary>
        /// Vector(1,1) in plane rectangular coordinate system.
        /// </summary>
        public static Vector2D One
        {
            get { return new Vector2D(1, 1); }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X of vector.</param>
        /// <param name="y">Y of vector.</param>
        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Equals?
        /// </summary>
        /// <param name="obj">Target obj.</param>
        /// <returns>Equals?</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Get hash code.
        /// </summary>
        /// <returns>Hash code.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Center of vector v1 and v2.
        /// </summary>
        /// <param name="v1">Vector v1.</param>
        /// <param name="v2">Vector v2.</param>
        /// <returns>The center of vector v1 and v2.</returns>
        public static Vector2D Center(Vector2D v1, Vector2D v2)
        {
            return (v1 + v2) * 0.5f;
        }

        /// <summary>
        /// Distance from vector v1 to v2.
        /// </summary>
        /// <param name="v1">Vector v1.</param>
        /// <param name="v2">Vector v2.</param>
        /// <returns>Distance from vector v1 to v2.</returns>
        public static double Distance(Vector2D v1, Vector2D v2)
        {
            /*
             *              _______________________
             *             /        2           2
             *  |p1p2| = \/(x2 - x1) + (y2 - y1)
             */

            var dx2 = Math.Pow(v2.x - v1.x, 2);
            var dy2 = Math.Pow(v2.y - v1.y, 2);
            return Math.Sqrt(dx2 + dy2);
        }

        /// <summary>
        /// Operator +
        /// </summary>
        /// <param name="lhs">Vector1.</param>
        /// <param name="rhs">Vector2.</param>
        /// <returns>lhs+rhs</returns>
        public static Vector2D operator +(Vector2D lhs, Vector2D rhs)
        {
            return new Vector2D(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        /// <summary>
        /// Operator -
        /// </summary>
        /// <param name="lhs">Vector1.</param>
        /// <param name="rhs">Vector2.</param>
        /// <returns>lhs-rhs</returns>
        public static Vector2D operator -(Vector2D lhs, Vector2D rhs)
        {
            return new Vector2D(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        /// <summary>
        /// Operator -
        /// </summary>
        /// <param name="p">Vector</param>
        /// <returns>-Vector</returns>
        public static Vector2D operator -(Vector2D p)
        {
            return new Vector2D(-p.x, -p.y);
        }

        /// <summary>
        /// Operator *
        /// </summary>
        /// <param name="lhs">Vector.</param>
        /// <param name="rhs">double.</param>
        /// <returns>lhs*rhs</returns>
        public static Vector2D operator *(Vector2D lhs, double rhs)
        {
            return new Vector2D(lhs.x * rhs, lhs.y * rhs);
        }

        /// <summary>
        /// Operator *
        /// </summary>
        /// <param name="lhs">double.</param>
        /// <param name="rhs">Vector.</param>
        /// <returns>lhs*rhs</returns>
        public static Vector2D operator *(double lhs, Vector2D rhs)
        {
            return rhs * lhs;
        }

        /// <summary>
        /// Operator ==
        /// </summary>
        /// <param name="lhs">Vector1.</param>
        /// <param name="rhs">Vector2.</param>
        /// <returns>lhs==rhs?</returns>
        public static bool operator ==(Vector2D lhs, Vector2D rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        /// <summary>
        /// Operator !=
        /// </summary>
        /// <param name="lhs">Vector1.</param>
        /// <param name="rhs">Vector2.</param>
        /// <returns>lhs!=rhs?</returns>
        public static bool operator !=(Vector2D lhs, Vector2D rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}