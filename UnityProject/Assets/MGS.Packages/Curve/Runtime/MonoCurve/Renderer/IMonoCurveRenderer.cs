/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMonoCurveRenderer.cs
 *  Description  :  Define interface of renderer that base on mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/15/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Interface of renderer that base on mono curve.
    /// </summary>
    public interface IMonoCurveRenderer : IMonoCurveExtender
    {
        /// <summary>
        /// Renderer component.
        /// </summary>
        Renderer Renderer { get; }

        /// <summary>
        /// Segment length of mono curve.
        /// </summary>
        float Segment { set; get; }

        /// <summary>
        /// Segments count of renderer.
        /// </summary>
        int Segments { get; }
    }
}