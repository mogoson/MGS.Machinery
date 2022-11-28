/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechanismDriver.cs
 *  Description  :  Define driver for test mechanism quickly.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries.Demo
{
    [RequireComponent(typeof(Mechanism))]
    public class MechanismDriver : MonoBehaviour
    {
        #region Field and Property
        public float velocity = 50;
        public DriveMode mode = DriveMode.Ignore;
        public KeyCode positive = KeyCode.P;
        public KeyCode negative = KeyCode.N;

        protected Mechanism mechanism;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            mechanism = GetComponent<Mechanism>();
        }

        protected virtual void Update()
        {
            if (Input.GetKey(positive))
            {
                mechanism.Drive(velocity, mode);
            }
            else if (Input.GetKey(negative))
            {
                mechanism.Drive(-velocity, mode);
            }
        }
        #endregion
    }
}