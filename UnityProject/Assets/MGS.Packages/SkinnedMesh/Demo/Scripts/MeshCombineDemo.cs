/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MeshCombineDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  10/15/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.SkinnedMesh.demo
{
    public class MeshCombineDemo : MonoBehaviour
    {
        public GameObject origin;
        public GameObject reborn;
        private bool mergeSubMeshes = false;

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();

            mergeSubMeshes = GUILayout.Toggle(mergeSubMeshes, "Merge Sub Meshes");
            if (GUILayout.Button("Combine"))
            {
                reborn.transform.position = origin.transform.position;
                MeshCombineUtility.CombineMeshes(origin, reborn, mergeSubMeshes);
                reborn.transform.position = Vector3.right * 5;
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}