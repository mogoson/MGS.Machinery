/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICurvePath.cs
 *  Description  :  Define interface of path that base on curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;

namespace Mogoson.CurvePath
{
    /// <summary>
    /// Interface of curve path.
    /// </summary>
    public interface ICurvePath : ICurve
    {
        #region Method
        /// <summary>
        /// Rebuild path.
        /// </summary>
        void Rebuild();
        #endregion
    }
}