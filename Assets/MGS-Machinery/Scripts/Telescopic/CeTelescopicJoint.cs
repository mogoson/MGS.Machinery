/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: CeTelescopicJoint.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/24/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       CeTelescopicJoint          Ignore.
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

    [AddComponentMenu("Developer/Machinery/CeTelescopicJoint")]
    public class CeTelescopicJoint : TelescopicJoint
    {
        #region Protected Method
        /// <summary>
        /// Drive joint.
        /// </summary>
        /// <param name="mSpeed">Move speed.</param>
        protected override void DriveJoint(float mSpeed)
        {
            lockRecord = displacement;
            displacement += mSpeed * Time.deltaTime;
            displacement = Mathf.Clamp(displacement, -stroke, stroke);
            DriveJoint();
            if (CheckRockersLock())
            {
                displacement = lockRecord;
                DriveJoint();
            }//if()_end
        }//DriveJoint()_end
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            DriveJoint(speed * speedControl);
            if (displacement == 0)
                tState = TelescopicState.Shrink;
            else if (displacement <= -stroke || displacement >= stroke)
                tState = TelescopicState.Extend;
        }//DriveM...()_end
        #endregion
    }//class_end
}//namespace_end