/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Limiter.cs
 *  Description  :  Abstract limiter.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/29/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Abstract limiter.
    /// </summary>
    public abstract class Limiter : MonoBehaviour, ILimiter
    {
        #region Field And Property
        /// <summary>
        /// Limiter is triggered?
        /// </summary>
        public abstract bool IsTriggered { get; }
        #endregion
    }
}