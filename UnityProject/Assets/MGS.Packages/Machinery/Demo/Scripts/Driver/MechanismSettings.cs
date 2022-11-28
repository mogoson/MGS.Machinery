/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MechanismSettings.cs
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
    [Serializable]
    public struct MechanismSettings
    {
        public Mechanism mechanism;
        public float velocity;
        public KeyCode positive;
        public KeyCode negative;
    }
}