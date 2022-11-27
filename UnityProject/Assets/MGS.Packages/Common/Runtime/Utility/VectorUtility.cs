/*************************************************************************
 *  Copyright © 2018-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  VectorUtility.cs
 *  Description  :  Utility for unity vector.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/1/2019
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Common.Utility
{
    /// <summary>
    /// Utility for unity vector.
    /// </summary>
    public sealed class VectorUtility
    {
        /// <summary>
        /// Calculate project angle of two vectors in the range(0~360).
        /// </summary>
        /// <param name="from">Start vector.</param>
        /// <param name="to">End vector.</param>
        /// <param name="normal">Normal of plane to project.</param>
        /// <returns>Project angle of two vectors.</returns>
        public static float ProjectAngle(Vector3 from, Vector3 to, Vector3 normal)
        {
            //Project.
            from = Vector3.ProjectOnPlane(from, normal);
            to = Vector3.ProjectOnPlane(to, normal);

            //Project is Vector3.zero.
            if (from == Vector3.zero || to == Vector3.zero)
            {
                return 0;
            }

            //Calculate reference.
            var ftCross = Vector3.Cross(from, to);
            var toCross = Vector3.Cross(from, ftCross);
            var tcDot = Vector3.Dot(to, toCross);
            var ncDot = Vector3.Dot(normal, ftCross);

            //Convert to rotate angle.
            var angle = Vector3.Angle(from, to);
            if ((ncDot > 0 && tcDot >= 0) || (ncDot < 0 && tcDot <= 0))
            {
                angle = 360 - angle;
            }
            return angle;
        }

        /// <summary>
        /// Gets the signed angle in degrees between from and to.
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        /// <returns>The signed angle in degrees between the two vectors.</returns>
        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            var num = Vector2.Angle(from, to);
            var num2 = Mathf.Sign(from.x * to.y - from.y * to.x);
            return num * num2;
        }

        /// <summary>
        /// Calculates the signed angle between vectors from and to in relation to axis.
        /// </summary>
        /// <param name="from">The vector from which the angular difference is measured.</param>
        /// <param name="to">The vector to which the angular difference is measured.</param>
        /// <param name="axis">A vector around which the other vectors are rotated.</param>
        /// <returns>Returns the signed angle between from and to in degrees.</returns>
        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
        {
            var num = Vector3.Angle(from, to);
            var num2 = from.y * to.z - from.z * to.y;
            var num3 = from.z * to.x - from.x * to.z;
            var num4 = from.x * to.y - from.y * to.x;
            var num5 = Mathf.Sign(axis.x * num2 + axis.y * num3 + axis.z * num4);
            return num * num5;
        }
    }
}