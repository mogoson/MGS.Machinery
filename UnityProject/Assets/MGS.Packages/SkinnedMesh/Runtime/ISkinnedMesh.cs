/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISkinnedMesh.cs
 *  Description  :  Define interface of skinned mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Interface of skinned mesh.
    /// </summary>
    public interface ISkinnedMesh
    {
        /// <summary>
        /// Skinned mesh renderer of skin.
        /// </summary>
        SkinnedMeshRenderer Renderer { get; }

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        MeshCollider Collider { get; }

        /// <summary>
        /// Rebuild the mesh of skin.
        /// </summary>
        void Rebuild();

        /// <summary>
        /// Attach collider to skin mesh.
        /// </summary>
        void AttachCollider();

        /// <summary>
        /// Remove collider from skin mesh.
        /// </summary>
        void RemoveCollider();
    }
}