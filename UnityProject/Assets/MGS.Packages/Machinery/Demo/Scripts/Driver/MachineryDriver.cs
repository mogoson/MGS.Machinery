/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MachineryDriver.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/28/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Machineries.Demo
{
    [RequireComponent(typeof(Machinery))]
    public class MachineryDriver : MonoBehaviour
    {
        public MechanismSettings[] settings;
        protected Machinery machinery;

        protected virtual void Awake()
        {
            machinery = GetComponent<Machinery>();
        }

        protected virtual void Update()
        {
            foreach (var s in settings)
            {
                if (Input.GetKey(s.positive))
                {
                    machinery.Drive(s.mechanism, s.velocity * Time.deltaTime, DriveMode.Ignore);
                }
                else if (Input.GetKey(s.negative))
                {
                    machinery.Drive(s.mechanism, -s.velocity * Time.deltaTime, DriveMode.Ignore);
                }
            }
        }
    }

    [Serializable]
    public struct MechanismSettings
    {
        public string mechanism;
        public float velocity;
        public KeyCode positive;
        public KeyCode negative;
    }
}