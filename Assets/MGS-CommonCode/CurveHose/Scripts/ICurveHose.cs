/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICurveHose.cs
 *  Description  :  Define interface of curve hose.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;
using Mogoson.Skin;

namespace Mogoson.CurveHose
{
    /// <summary>
    /// Interface of curve hose.
    /// </summary>
    public interface ICurveHose : ICurve, ISkin
    {
        #region Field and Property
        /// <summary>
        /// Polygon of hose cross section.
        /// </summary>
        int Polygon { set; get; }

        /// <summary>
        /// Segment length of subdivide hose.
        /// </summary>
        float Segment { set; get; }

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        float Radius { set; get; }

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        bool Seal { set; get; }
        #endregion
    }
}