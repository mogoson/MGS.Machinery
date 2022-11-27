/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IEngageMechanism.cs
 *  Description  :  Interface for engage mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machinery
{
    /// <summary>
    /// Interface for engage mechanism.
    /// </summary>
    public interface IEngageMechanism : IMechanism
    {
        #region Method
        /// <summary>
        /// Link engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        void LinkEngage(IMechanism mechanism);

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        void BreakEngage(IMechanism mechanism);
        #endregion
    }
}