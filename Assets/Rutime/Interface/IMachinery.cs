/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMachinery.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  11/28/2022
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machineries
{
    /// <summary>
    /// Interface of machinery.
    /// </summary>
    public interface IMachinery
    {
        /// <summary>
        /// Drive mechanism of machinery by velocity.
        /// </summary>
        /// <param name="name">Name of mechanism.</param>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is effective?</returns>
        bool Drive(string name, float velocity, DriveMode mode);
    }
}