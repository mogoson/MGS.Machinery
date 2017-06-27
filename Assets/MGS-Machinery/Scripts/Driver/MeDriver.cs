/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: MeDriver.cs
 *  Author: Mogoson   Version: 1.0   Date: 8/13/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.           MeDriver               Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     8/13/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEngine;

    /// <summary>
    /// Mechanism Driver.
    /// </summary>
    [AddComponentMenu("Developer/Machinery/MeDriver")]
    [RequireComponent(typeof(Mechanism))]
    public class MeDriver : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Positive key input.
        /// </summary>
        public KeyCode positiveKey = KeyCode.P;

        /// <summary>
        /// Negative key input.
        /// </summary>
        public KeyCode negativeKey = KeyCode.N;

        /// <summary>
        /// Target drive mechanism.
        /// </summary>
        protected Mechanism mechanism;
        #endregion

        #region Protected Method
        /// <summary>
        /// Find mechanism.
        /// </summary>
        protected virtual void Start()
        {
            mechanism = GetComponent<Mechanism>();
        }//Start()_end

        /// <summary>
        /// Check input and drive mechanism.
        /// </summary>
        protected virtual void Update()
        {
            if (Input.GetKey(positiveKey))
                mechanism.DriveMechanism(1);
            if (Input.GetKey(negativeKey))
                mechanism.DriveMechanism(-1);
        }//U...()_end
        #endregion
    }//class_end
}//namespace_end