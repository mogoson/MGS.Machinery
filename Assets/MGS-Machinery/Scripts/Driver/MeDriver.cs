/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  MeDriver.cs
 *  Description  :  Define driver for test mechanism quickly.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  8/13/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/MeDriver")]
    [RequireComponent(typeof(Mechanism))]
    public class MeDriver : MonoBehaviour
    {
        #region Property and Field
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