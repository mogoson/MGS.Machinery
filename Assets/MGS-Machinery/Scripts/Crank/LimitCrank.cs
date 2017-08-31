/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: LimitCrank.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/24/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          LimitCrank              Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     12/24/2015       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEngine;

    [AddComponentMenu("Developer/Machinery/LimitCrank")]
    public class LimitCrank : FreeCrank
    {
        #region Property and Field
        /// <summary>
        /// Min angle limit of crank.
        /// </summary>
        public float minAngle = -45;

        /// <summary>
        /// Max angle limit of crank.
        /// </summary>
        public float maxAngle = 45;
        #endregion

        #region Protected Method
        /// <summary>
        /// Rotate crank in the range by speed.
        /// </summary>
        /// <param name="rSpeed">Rotate speed.</param>
        protected override void DriveCrank(float rSpeed)
        {
            lockRecord = angle;
            angle += rSpeed * Time.deltaTime;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            DriveCrank();
            if (CheckRockersLock())
            {
                angle = lockRecord;
                DriveCrank();
            }
        }
        #endregion
    }
}