/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMonoCurveCacher.cs
 *  Description  :  Define interface of cacher that base on mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/30/2021
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Curve
{
    /// <summary>
    /// Interface of cacher that base on mono curve.
    /// </summary>
    public interface IMonoCurveCacher
    {
        /// <summary>
        /// Build cache file from mono curve.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool Build(string file);

        /// <summary>
        /// Load cache file to mono curve.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool Load(string file);
    }
}