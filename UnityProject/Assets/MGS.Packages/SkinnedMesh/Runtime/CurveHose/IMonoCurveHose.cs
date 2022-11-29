/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMonoCurveHose.cs
 *  Description  :  Define interface of mono curve hose.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Curve;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Interface of mono curve hose.
    /// </summary>
    public interface IMonoCurveHose : IMonoCurveRenderer, ISkinnedMesh
    {
        /// <summary>
        /// Polygon of hose cross section.
        /// </summary>
        int Polygon { set; get; }

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        float Radius { set; get; }

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        bool Seal { set; get; }
    }
}