/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveRenderer.cs
 *  Description  :  Renderer for mono curve.
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
    /// Renderer for mono curve.
    /// </summary>
    public abstract class MonoCurveRenderer : MonoCurveExtender, IMonoCurveRenderer
    {
        /// <summary>
        /// Detail length for renderer.
        /// </summary>
        [SerializeField]
        protected float segment = 0.25f;

        /// <summary>
        /// Segment length of mono curve.
        /// </summary>
        public float Segment
        {
            set { segment = value; }
            get { return segment; }
        }

        /// <summary>
        /// Segments count of renderer.
        /// </summary>
        public virtual int Segments { protected set; get; }

        /// <summary>
        /// Renderer component.
        /// </summary>
        public abstract Renderer Renderer { get; }
    }
}