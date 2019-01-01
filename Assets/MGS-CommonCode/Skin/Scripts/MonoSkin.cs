/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoSkin.cs
 *  Description  :  Define Skin to render dynamic mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Skin
{
    /// <summary>
    /// Render dynamic skinned mesh.
    /// </summary>
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public abstract class MonoSkin : MonoBehaviour, ISkin
    {
        #region Field and Property
        /// <summary>
        /// Skinned mesh renderer of skin.
        /// </summary>
        protected SkinnedMeshRenderer meshRenderer;

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        protected MeshCollider meshCollider;

        /// <summary>
        /// Mesh of skin.
        /// </summary>
        protected Mesh mesh;

#if UNITY_EDITOR
        /// <summary>
        /// Mono skin is initialized?
        /// </summary>
        private bool isInitialized = false;
#endif
        /// <summary>
        /// Skinned mesh renderer of skin.
        /// </summary>
        public SkinnedMeshRenderer Renderer { get { return meshRenderer; } }

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        public MeshCollider Collider { get { return meshCollider; } }
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            Rebuild();
        }

        protected virtual void Awake()
        {
            Initialize();
            Rebuild();
        }

        /// <summary>
        /// Initialize mono skin.
        /// </summary>
        protected virtual void Initialize()
        {
#if UNITY_EDITOR
            if (isInitialized)
            {
                return;
            }
            isInitialized = true;
#endif
            meshRenderer = GetComponent<SkinnedMeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
            mesh = new Mesh { name = "Skin" };
        }

        /// <summary>
        /// Rebuild the mesh of skin.
        /// </summary>
        protected abstract void RebuildMesh();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of skin.
        /// </summary>
        public virtual void Rebuild()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Initialize();
            }
#endif
            RebuildMesh();
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
            meshCollider = GetComponent<MeshCollider>();
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
        #endregion
    }
}