/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MeshCombineUtility.cs
 *  Description  :  Utility for combine skinned mesh.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  10/15/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.SkinnedMesh
{
    /// <summary>
    /// Utility for combine skinned mesh.
    /// </summary>
    public sealed class MeshCombineUtility
    {
        /// <summary>
        /// Combine the children meshes of origin to the reborn object.
        /// </summary>
        /// <param name="origin">Root of the meshes to combine.</param>
        /// <param name="reborn">Combined mesh attach to it. </param>
        /// <param name="mergeSubMeshes">Should all meshes be combined into a single submesh?</param>
        public static void CombineMeshes(GameObject origin, GameObject reborn, bool mergeSubMeshes = true)
        {
            var filters = origin.GetComponentsInChildren<MeshFilter>();
            CombineMeshes(filters, reborn, mergeSubMeshes);
        }

        /// <summary>
        /// Combine the meshes of filters to the reborn object.
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="reborn"></param>
        /// <param name="mergeSubMeshes"></param>
        public static void CombineMeshes(IEnumerable<MeshFilter> filters, GameObject reborn, bool mergeSubMeshes = true)
        {
            var filter = reborn.GetComponent<MeshFilter>();
            if (filter == null)
            {
                filter = reborn.AddComponent<MeshFilter>();
            }
            var rebornMesh = CombineMeshes(filters, reborn.transform.position, mergeSubMeshes);
            filter.sharedMesh = rebornMesh;

            var renderer = reborn.GetComponent<MeshRenderer>();
            if (renderer == null)
            {
                renderer = reborn.AddComponent<MeshRenderer>();
            }
            if (mergeSubMeshes)
            {
                var enumerator = filters.GetEnumerator(); enumerator.MoveNext();
                var sharedMat = enumerator.Current.GetComponent<MeshRenderer>().sharedMaterial;
                renderer.sharedMaterials = new Material[] { sharedMat };
            }
            else
            {
                var sharedMaterials = new List<Material>();
                foreach (var filterItem in filters)
                {
                    var mats = filterItem.GetComponent<MeshRenderer>().sharedMaterials;
                    sharedMaterials.AddRange(mats);
                }
                renderer.sharedMaterials = sharedMaterials.ToArray();
            }
        }

        /// <summary>
        /// Combine the meshes of filters to a mesh.
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="center"></param>
        /// <param name="mergeSubMeshes"></param>
        /// <returns></returns>
        public static Mesh CombineMeshes(IEnumerable<MeshFilter> filters, Vector3 center, bool mergeSubMeshes = true)
        {
            var combines = new List<CombineInstance>();
            foreach (var filter in filters)
            {
                var pos = filter.transform.position - center;
                var combine = new CombineInstance
                {
                    mesh = filter.sharedMesh,
                    transform = Matrix4x4.TRS(pos, filter.transform.rotation, filter.transform.lossyScale)
                };
                combines.Add(combine);
            }
            var rebornMesh = new Mesh();
            rebornMesh.CombineMeshes(combines.ToArray(), mergeSubMeshes);

#if !UNITY_5_5_OR_NEWER
            //Mesh.Optimize was removed in version 5.5.2p4.
            rebornMesh.Optimize();
#endif
            return rebornMesh;
        }
    }
}