/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: VectorExtension.cs
 *  Author: Mogoson   Version: 1.0   Date: 1/6/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.        VectorExtension           Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     1/6/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Vector3
{
    using UnityEngine;

    public static class DVector3
    {
        #region Public Method
        /// <summary>
        /// Calculate rotate angle of two vector int the range[0 ~ 360].
        /// </summary>
        /// <param name="from">Start vector.</param>
        /// <param name="to">End vector.</param>
        /// <param name="normal">Custom normal.</param>
        /// <returns>Rotate angle of two vector.</returns>
        public static float RotateAngle(Vector3 from, Vector3 to, Vector3 normal)
        {
            //Project.
            from = Vector3.ProjectOnPlane(from, normal);
            to = Vector3.ProjectOnPlane(to, normal);

            //Project is Vector3.zero.
            if (from == Vector3.zero || to == Vector3.zero)
                return 0;

            //Calculate reference.
            var ftCross = Vector3.Cross(from, to);
            var toCross = Vector3.Cross(from, ftCross);
            var tcDot = Vector3.Dot(to, toCross);
            var ncDot = Vector3.Dot(normal, ftCross);

            //Convert to rotate angle.
            var a = Vector3.Angle(from, to);
            if ((ncDot > 0 && tcDot >= 0) || (ncDot < 0 && tcDot <= 0))
                a = 360 - a;
            return a;
        }//RotateAngle()_end
        #endregion
    }//class_end
}//namespace_end