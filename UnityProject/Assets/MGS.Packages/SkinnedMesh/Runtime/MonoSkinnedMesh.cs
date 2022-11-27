/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoSkinnedMesh.cs
 *  Description  :  Define Skin to render dynamic mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Render dynamic skinned mesh.
    /// </summary>
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public abstract class MonoSkinnedMesh : MonoBehaviour, ISkinnedMesh
    {
        /// <summary>
        /// Skinned mesh renderer of skin.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        protected SkinnedMeshRenderer meshRenderer;

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        protected MeshCollider meshCollider;

        /// <summary>
        /// Mesh of skin.
        /// </summary>
        [HideInInspector]
        [SerializeField]
        protected Mesh mesh;

        /// <summary>
        /// Skinned mesh renderer of skin.
        /// </summary>
        public SkinnedMeshRenderer Renderer { get { return meshRenderer; } }

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        public MeshCollider Collider { get { return meshCollider; } }

        /// <summary>
        /// Reset component.
        /// </summary>
        protected virtual void Reset()
        {
            meshRenderer = GetComponent<SkinnedMeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
            mesh = new Mesh { name = "Skin" };
            mesh.MarkDynamic();
        }

        /// <summary>
        /// Rebuild the mesh of skin.
        /// </summary>
        /// <param name="mesh">Mesh of skin.</param>
        protected abstract void Rebuild(Mesh mesh);

        /// <summary>
        /// Rebuild the mesh of skin.
        /// </summary>
        public virtual void Rebuild()
        {
            Rebuild(mesh);

#if !UNITY_5_5_OR_NEWER
            //Mesh.Optimize was removed in version 5.5.2p4.
            mesh.Optimize();
#endif
            meshRenderer.sharedMesh = mesh;
            meshRenderer.localBounds = mesh.bounds;

            if (meshCollider)
            {
                meshCollider.sharedMesh = null;
                meshCollider.sharedMesh = mesh;
            }
        }

        /// <summary>
        /// Attach MeshCollider to skin.
        /// </summary>
        public void AttachCollider()
        {
            var meshCollider = GetComponent<MeshCollider>();
            if (meshCollider == null)
            {
                meshCollider = gameObject.AddComponent<MeshCollider>();
            }
        }

        /// <summary>
        /// Remove MeshCollider from skin.
        /// </summary>
        public void RemoveCollider()
        {
            if (meshCollider)
            {
                Destroy(meshCollider);
                meshCollider = null;
            }
        }
    }
}