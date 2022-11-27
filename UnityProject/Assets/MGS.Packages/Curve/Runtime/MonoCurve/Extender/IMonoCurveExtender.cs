/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMonoCurveExtender.cs
 *  Description  :  Define interface of extender that base on mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/30/2021
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Curve
{
    /// <summary>
    /// Interface of extender that base on mono curve.
    /// </summary>
    public interface IMonoCurveExtender
    {
        /// <summary>
        /// Rebuild extender base mono curve.
        /// </summary>
        /// <param name="curve"></param>
        void Rebuild(IMonoCurve curve);
    }
}