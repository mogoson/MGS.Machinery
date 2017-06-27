/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: SeTelescopicArm.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/24/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       SeTelescopicArm            Ignore.
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

    [AddComponentMenu("Developer/Machinery/SeTelescopicArm")]
    public class SeTelescopicArm : TelescopicArmMechanism
    {
        #region Property and Field
        /// <summary>
        /// Current id of drive joint.
        /// </summary>
        public int jointIndex { protected set; get; }
        #endregion

        #region Protected Method
        /// <summary>
        /// Clamp joint index in the range.
        /// </summary>
        protected virtual void ClampIndex()
        {
            jointIndex = Mathf.Clamp(jointIndex, 0, tJoints.Count - 1);
        }//ClampIndex()_end
        #endregion

        #region Public Method
		/// <summary>
		/// Drive the mechanism.
		/// </summary>
		/// <param name="speedControl">Speed control.</param>
		public override void DriveMechanism (float speedControl)
		{
			ClampIndex();
            var currentJoint = tJoints[jointIndex];
            currentJoint.DriveMechanism(speedControl);
			if(currentJoint.speed * speedControl >= 0)
			{
				if (currentJoint.tState == TelescopicState.Extend)
                    jointIndex++;
			}
			else
			{
				if (currentJoint.tState == TelescopicState.Shrink)
                    jointIndex--;
            }//if()_end
        }//DriveM...()_end
        #endregion
    }//class_end
}//namespace_end