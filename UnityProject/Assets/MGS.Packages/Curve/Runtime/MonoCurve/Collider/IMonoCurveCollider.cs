/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IMonoCurveCollider.cs
 *  Description  :  Define interface of collider that base on mono curve.
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
    /// Interface of collider that base on mono curve.
    /// </summary>
    public interface IMonoCurveCollider : IMonoCurveExtender
    {
        /// <summary>
        /// Segment length of mono curve.
        /// </summary>
        float Segment { set; get; }

        /// <summary>
        /// Segments count of collider.
        /// </summary>
        int Segments { get; }

        /// <summary>
        /// Radius of collider.
        /// </summary>
        float Radius { set; get; }

        /// <summary>
        /// Collider is trigger?
        /// </summary>
        bool IsTrigger { set; get; }

        /// <summary>
        /// PhysicMaterial of collider.
        /// </summary>
        PhysicMaterial Material { set; get; }
    }
}