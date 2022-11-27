/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICoaxeMechanism.cs
 *  Description  :  Interface for coaxe mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machinery
{
    /// <summary>
    /// Interface for coaxe mechanism.
    /// </summary>
    public interface ICoaxeMechanism : IMechanism
    {
        #region Method
        /// <summary>
        /// Link coaxe.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        void LinkCoaxe(IMechanism mechanism);

        /// <summary>
        /// Break coaxe.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        void BreakCoaxe(IMechanism mechanism);
        #endregion
    }
}