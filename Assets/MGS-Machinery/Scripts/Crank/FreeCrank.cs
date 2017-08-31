/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: FreeCrank.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/24/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.          FreeCrank               Ignore.
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

    [AddComponentMenu("Developer/Machinery/FreeCrank")]
    public class FreeCrank : CrankMechanism
    {
        #region Protected Method
        /// <summary>
        /// Rotate the crank by speed.
        /// </summary>
        /// <param name="rSpeed">Speed of rotate crank.</param>
        protected virtual void DriveCrank(float rSpeed)
        {
            lockRecord = angle;
            angle += rSpeed * Time.deltaTime;
            DriveCrank();
            if (CheckRockersLock())
            {
                angle = lockRecord;
                DriveCrank();
            }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            DriveCrank(speed * speedControl);
        }
        #endregion
    }
}