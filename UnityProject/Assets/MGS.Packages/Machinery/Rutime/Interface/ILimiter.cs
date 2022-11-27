/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ILimiter.cs
 *  Description  :  Interface for limiter.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/29/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machinery
{
    /// <summary>
    /// Interface for limiter.
    /// </summary>
    public interface ILimiter
    {
        #region Property
        /// <summary>
        /// Limiter is triggered?
        /// </summary>
        bool IsTriggered { get; }
        #endregion
    }
}