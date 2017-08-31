/*************************************************************************
 *  Copyright (C), 2015-2016, Mogoson tech. Co., Ltd.
 *  FileName: SynchroCrank.cs
 *  Author: Mogoson   Version: 1.0   Date: 12/25/2015
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         SynchroCrank             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     12/25/2015       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using System.Collections.Generic;
    using UnityEngine;

    [AddComponentMenu("Developer/Machinery/SynchroCrank")]
    public class SynchroCrank : Mechanism
    {
        #region Property and Field
        /// <summary>
        /// Crank mechanism.
        /// </summary>
        public List<CrankMechanism> cranks = new List<CrankMechanism>();
        #endregion

        #region Public Method
        /// <summary>
        /// Drive the mechanism.
        /// </summary>
        /// <param name="speedControl">Speed control.</param>
        public override void DriveMechanism(float speedControl)
        {
            foreach (var crank in cranks)
            {
                crank.DriveMechanism(speedControl);
            }
        }
        #endregion
    }
}