/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ISkin.cs
 *  Description  :  Define interface of skinned mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/23/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Skin
{
    /// <summary>
    /// Interface of skinned mesh.
    /// </summary>
    public interface ISkin
    {
        #region Property
        /// <summary>
        /// Mesh renderer of skin.
        /// </summary>
        Renderer Renderer { get; }

        /// <summary>
        /// Mesh collider of skin.
        /// </summary>
        Collider Collider { get; }

        /// <summary>
        /// Mesh of skin.
        /// </summary>
        Mesh Mesh { get; }
        #endregion

        #region Method
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
        #endregion
    }
}