/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICurvePipe.cs
 *  Description  :  Define interface of curve pipe.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Curve;

namespace Mogoson.CurvePipe
{
    /// <summary>
    /// Interface of curve pipe.
    /// </summary>
    public interface ICurvePipe : ICurve
    {
        #region Field and Property
        /// <summary>
        /// Polygon of pipe cross section.
        /// </summary>
        int Polygon { set; get; }

        /// <summary>
        /// Segment length of subdivide pipe.
        /// </summary>
        float Segment { set; get; }

        /// <summary>
        /// Radius of pipe mesh.
        /// </summary>
        float Radius { set; get; }

        /// <summary>
        /// Is seal at both ends of pipe?
        /// </summary>
        bool Seal { set; get; }
        #endregion
    }
}