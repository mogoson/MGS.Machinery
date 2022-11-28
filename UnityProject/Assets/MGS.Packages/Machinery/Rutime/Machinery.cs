/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Machinery.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/28/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Machinery.
    /// </summary>
    public class Machinery : MonoBehaviour, IMachinery
    {
        /// <summary>
        /// Mechanisms of machinery.
        /// </summary>
        public List<Mechanism> mechanisms;

        /// <summary>
        /// Drive mechanism of machinery by velocity.
        /// </summary>
        /// <param name="name">Name of mechanism.</param>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        public bool Drive(string name, float velocity, DriveMode mode)
        {
            var mechanism = FindMechanism(name);
            if (mechanism == null)
            {
                return false;
            }
            return mechanism.Drive(velocity, mode);
        }

        /// <summary>
        /// Find mechanism by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Mechanism FindMechanism(string name)
        {
            foreach (var mechanism in mechanisms)
            {
                if (mechanism.name == name)
                {
                    return mechanism;
                }
            }
            return null;
        }
    }
}