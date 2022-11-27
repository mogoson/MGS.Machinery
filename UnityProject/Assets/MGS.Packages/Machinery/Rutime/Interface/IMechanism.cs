/*************************************************************************
 *  Copyright © 2015-2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMechanism.cs
 *  Description  :  Define interface for mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/17/2018
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  1.1
 *  Date         :  5/24/2018
 *  Description  :  Modify mechanism interface.
 *  
 *  Author       :  Mogoson
 *  Version      :  1.2
 *  Date         :  6/3/2018
 *  Description  :  Add interface for mechanism.
 *  
 *  Author       :  Mogoson
 *  Version      :  1.3
 *  Date         :  3/20/2020
 *  Description  :  Migrate Code.
 *************************************************************************/

namespace MGS.Machinery
{
    /// <summary>
    /// Mechanism interface.
    /// </summary>
    public interface IMechanism
    {
        #region Property
        /// <summary>
        /// Mechanism is initialized?
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        bool IsStuck { get; }
        #endregion

        #region Method
        /// <summary>
        /// Initialize mechanism.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is unrestricted?</returns>
        bool Drive(float velocity, DriveMode mode);
        #endregion
    }
}