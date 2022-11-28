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

using UnityEngine;

namespace MGS.Machineries.Demo
{
    public class MachineryDriver : MonoBehaviour
    {
        public MechanismSettings[] settings;

        protected virtual void Update()
        {
            foreach (var setting in settings)
            {
                if (Input.GetKey(setting.positive))
                {
                    setting.mechanism.Drive(setting.velocity, DriveMode.Ignore);
                }
                else if (Input.GetKey(setting.negative))
                {
                    setting.mechanism.Drive(-setting.velocity, DriveMode.Ignore);
                }
            }
        }
    }
}