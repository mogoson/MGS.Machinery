/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoChain.cs
 *  Description  :  MonoChain component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/09/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Chain
{
    /// <summary>
    /// MonoChain component.
    /// </summary>
    public abstract class MonoChain : MonoBehaviour, IChain
    {
        /// <summary>
        /// Prefab of chain node.
        /// </summary>
        [SerializeField]
        protected GameObject node;

        /// <summary>
        /// Prefab of chain roller.
        /// </summary>
        [SerializeField]
        protected GameObject link;

        /// <summary>
        /// Segment length of chain node.
        /// </summary>
        [SerializeField]
        protected float segment = 0.1f;

        /// <summary>
        /// Piece length of chain node.
        /// </summary>
        protected float piece;

        /// <summary>
        /// Nodes of chain.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        protected List<Node> nodes = new List<Node>();

        /// <summary>
        /// Prefab of chain node.
        /// </summary>
        public GameObject Node
        {
            set { node = value; }
            get { return node; }
        }

        /// <summary>
        /// Prefab of chain link.
        /// </summary>
        public GameObject Link
        {
            set { link = value; }
            get { return link; }
        }

        /// <summary>
        /// Length of chain.
        /// </summary>
        public abstract float Length { get; }

        /// <summary>
        ///  Segment length of chain node (or link).
        /// </summary>
        public float Segment
        {
            set
            {
                if (value > 0)
                {
                    segment = value;
                }
            }
            get { return segment; }
        }

        /// <summary>
        /// Rebuild chain nodes.
        /// </summary>
        public virtual void Rebuild()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && (node == null || link == null))
            {
                return;
            }
#endif
            var segmentCount = GetSegmentCount(Length, segment, out piece);
            while (nodes.Count < segmentCount)
            {
                AddNodeToLast();
            }

            while (nodes.Count > segmentCount)
            {
                RemoveLastNode();
            }

            AnchorNodes(nodes);
        }

        /// <summary>
        /// Clear chain all nodes.
        /// </summary>
        public virtual void Clear()
        {
            foreach (var node in nodes)
            {
#if UNITY_EDITOR
                DestroyImmediate(node.gameObject);
#else
                Destroy(node.gameObject);
#endif
            }
            nodes.Clear();
        }

        /// <summary>
        /// Add a node to chain last.
        /// </summary>
        protected void AddNodeToLast()
        {
            //Create node.
            var nodePrefab = node;
            if (Link != null && nodes.Count % 2 == 1)
            {
                nodePrefab = Link;
            }
            var nodeClone = Instantiate(nodePrefab);
            nodeClone.transform.SetParent(transform);

            //Add new node to chain nodes.
            var newNode = nodeClone.GetComponent<Node>();
            newNode.ID = nodes.Count;
            nodes.Add(newNode);
        }

        /// <summary>
        /// Remove the last node of chain nodes.
        /// </summary>
        protected void RemoveLastNode()
        {
            if (nodes.Count > 0)
            {
                var node = nodes[nodes.Count - 1];
#if UNITY_EDITOR
                DestroyImmediate(node.gameObject);
#else
                Destroy(node.gameObject);
#endif
                nodes.Remove(node);
            }
        }

        /// <summary>
        /// Get segment count.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="segment"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        protected int GetSegmentCount(float length, float segment, out float piece)
        {
            //AwayFromZero means that 12.5 -> 13
            var count = (int)Math.Round(length / segment, MidpointRounding.AwayFromZero);
            count = Mathf.Max(count, 1);
            piece = length / count;
            return count;
        }

        /// <summary>
        /// Anchor nodes to chain.
        /// </summary>
        /// <param name="node"></param>
        protected abstract void AnchorNodes(List<Node> nodes);
    }
}