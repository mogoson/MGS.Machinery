/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  VectorExtension.cs
 *  Description  :  Extension for UnityEngine.Vector3.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/6/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Extention
{
    /// <summary>
    /// Extension for UnityEngine.Vector3.
    /// </summary>
    public static class EVector3
    {
        #region Public Method
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
        #endregion
    }
}