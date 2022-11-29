/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveLineRenderer.cs
 *  Description  :  Line renderer for mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/16/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Line renderer for mono curve.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class MonoCurveLineRenderer : MonoCurveRenderer
    {
        /// <summary>
        /// Renderer component.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        protected LineRenderer lineRenderer;

        /// <summary>
        /// Renderer component.
        /// </summary>
        public override Renderer Renderer { get { return lineRenderer; } }

        /// <summary>
        /// Reset component.
        /// </summary>
        protected override void Reset()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            base.Reset();
        }

        /// <summary>
        /// Rebuild renderer base curve.
        /// </summary>
        /// <param name="curve"></param>
        public override void Rebuild(IMonoCurve curve)
        {
            if (curve == null || curve.Length == 0)
            {
                Segments = 0;
                lineRenderer.SetVertexCount(0);
                return;
            }

            var differ = 0f;
            Segments = MonoCurveUtility.GetSegmentCount(curve, segment, out differ) + 1;
            lineRenderer.SetVertexCount(Segments);
            for (int i = 0; i < Segments; i++)
            {
                lineRenderer.SetPosition(i, curve.LocalEvaluate(i * differ));
            }
        }
    }
}