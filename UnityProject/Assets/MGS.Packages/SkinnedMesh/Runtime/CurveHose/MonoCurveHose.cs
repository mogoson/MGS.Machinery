/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveHose.cs
 *  Description  :  Define MonoCurveHose to render dynamic hose mesh base
 *                  on mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Curve;
using UnityEngine;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Render dynamic hose mesh base on mono curve.
    /// </summary>
    [ExecuteInEditMode]
    public abstract class MonoCurveHose : MonoSkinnedMesh, IMonoCurveHose
    {
        /// <summary>
        /// Segment length of mono curve.
        /// </summary>
        [SerializeField]
        protected float segment = 0.25f;

        /// <summary>
        /// Polygon of hose cross section.
        /// </summary>
        [SerializeField]
        protected int polygon = 8;

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        [SerializeField]
        protected float radius = 0.1f;

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        [SerializeField]
        protected bool seal = false;

        /// <summary>
        /// Renderer component.
        /// </summary>
        Renderer IMonoCurveRenderer.Renderer { get { return meshRenderer; } }

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
        /// Polygon of hose cross section.
        /// </summary>
        public int Polygon
        {
            set { polygon = value; }
            get { return polygon; }
        }

        /// <summary>
        /// Radius of hose mesh.
        /// </summary>
        public float Radius
        {
            set { radius = value; }
            get { return radius; }
        }

        /// <summary>
        /// Is seal at both ends of hose?
        /// </summary>
        public bool Seal
        {
            set { seal = value; }
            get { return seal; }
        }

        /// <summary>
        /// [MESSAGE] On mono curve rebuild.
        /// </summary>
        /// <param name="curve"></param>
        private void OnMonoCurveRebuild(IMonoCurve curve)
        {
            Rebuild(curve);
        }

        /// <summary>
        /// Rebuild renderer base mono curve.
        /// </summary>
        /// <param name="curve"></param>
        public abstract void Rebuild(IMonoCurve curve);
    }
}