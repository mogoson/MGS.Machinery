/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveChain.cs
 *  Description  :  Define chain base on MonoCurve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/4/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.Curve;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Chain
{
    /// <summary>
    /// Chain base on MonoCurve.
    /// </summary>
    [ExecuteInEditMode]
    public class MonoCurveChain : MonoChain
    {
        /// <summary>
        /// Length of chain.
        /// </summary>
        public override float Length { get { return curve.Length; } }

        /// <summary>
        /// Center curve of chain.
        /// </summary>
        protected IMonoCurve curve;

        /// <summary>
        /// Current motion of chain.
        /// </summary>
        protected float motion;

        /// <summary>
        /// Rebuild chain base curve.
        /// </summary>
        /// <param name="curve"></param>
        public virtual void Rebuild(IMonoCurve curve)
        {
            this.curve = curve;
            Rebuild();
        }

        /// <summary>
        /// Drive the chain.
        /// </summary>
        /// <param name="velocity">Linear velocity.</param>
        public virtual void Drive(float velocity)
        {
            motion += velocity + Length;
            motion %= Length;

            foreach (var node in nodes)
            {
                TowNodeAlongCurve(node, motion);
            }
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
        /// Anchor nodes to chain.
        /// </summary>
        /// <param name="node"></param>
        protected override void AnchorNodes(List<Node> nodes)
        {
            TowNodesAlongCurve(nodes, 0);
        }

        /// <summary>
        /// Tow nodes along curve.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="motion"></param>
        protected void TowNodesAlongCurve(List<Node> nodes, float motion)
        {
            foreach (var node in nodes)
            {
                TowNodeAlongCurve(node, motion);
            }
        }

        /// <summary>
        /// Tow node along center curve.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="motion"></param>
        protected virtual void TowNodeAlongCurve(Node node, float motion)
        {
            var len = (motion + node.ID * piece) % Length;
            var nodePos = curve.Evaluate(len);

            var nextLen = (motion + (node.ID + 1) * piece) % Length;
            var nextPos = curve.Evaluate(nextLen);

            var secant = (nextPos - nodePos).normalized;
            var worldUp = Vector3.Cross(secant, transform.up);

            node.transform.position = nodePos;
            node.transform.LookAt(nextPos, worldUp);
        }
    }
}