/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MeDriver.cs
 *  Description  :  Define driver for test mechanism quickly.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/MeDriver")]
    [RequireComponent(typeof(Mechanism))]
    public class MeDriver : MonoBehaviour
    {
        #region Field and Property
        public KeyCode positiveKey = KeyCode.P;
        public KeyCode negativeKey = KeyCode.N;

        protected Mechanism mechanism;
        #endregion

        #region Protected Method
        protected virtual void Start()
        {
            mechanism = GetComponent<Mechanism>();
        }

        protected virtual void Update()
        {
            if (Input.GetKey(positiveKey))
                mechanism.DriveMechanism(1.0f);
            else if (Input.GetKey(negativeKey))
                mechanism.DriveMechanism(-1.0f);
        }
        #endregion
    }
}