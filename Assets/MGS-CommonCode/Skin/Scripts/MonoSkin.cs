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

        /// <summary>
        /// Skinned mesh renderer of skin.
        /// </summary>
        public Renderer Renderer { get { return meshRenderer; } }

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        public Collider Collider { get { return meshCollider; } }

        /// <summary>
        /// Mesh of skin.
        /// </summary>
        public Mesh Mesh { get { return mesh; } }
        #endregion

        #region Protected Method
        protected virtual void Reset()
        {
            Rebuild();
        }

        protected virtual void Awake()
        {
            meshRenderer = GetComponent<SkinnedMeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
            mesh = new Mesh { name = "Skin" };

            Rebuild();
        }

        /// <summary>
        /// Create vertices of skin mesh.
        /// </summary>
        /// <returns></returns>
        protected abstract Vector3[] CreateVertices();

        /// <summary>
        /// Create triangles of skin mesh.
        /// </summary>
        /// <returns>Triangles of skin mesh.</returns>
        protected abstract int[] CreateTriangles();
        #endregion

        #region Public Method
        /// <summary>
        /// Rebuild the mesh of skin.
        /// </summary>
        public virtual void Rebuild()
        {
#if UNITY_EDITOR
            if (meshRenderer == null)
                meshRenderer = GetComponent<SkinnedMeshRenderer>();

            if (meshCollider == null)
                meshCollider = GetComponent<MeshCollider>();

            if (mesh == null)
                mesh = new Mesh { name = "Skin" };
#endif
            mesh.Clear();
            mesh.vertices = CreateVertices();
            mesh.triangles = CreateTriangles();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshRenderer.sharedMesh = mesh;
            meshRenderer.localBounds = mesh.bounds;

            if (meshCollider)
                meshCollider.sharedMesh = mesh;
        }

        /// <summary>
        /// Attach MeshCollider to skin.
        /// </summary>
        public void AttachCollider()
        {
            if (meshCollider)
                return;
            else
            {
                var collider = GetComponent<MeshCollider>();
                if (collider == null)
                    collider = gameObject.AddComponent<MeshCollider>();
                this.meshCollider = collider;
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