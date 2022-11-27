/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoSkinEditor.cs
 *  Description  :  Editor for MonoSkin component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace MGS.SkinnedMesh.Editors
{
    [CustomEditor(typeof(MonoSkinnedMesh), true)]
    public class MonoSkinEditor : Editor
    {
        protected MonoSkinnedMesh Target { get { return target as MonoSkinnedMesh; } }

        protected virtual void OnEnable()
        {
            if (!Application.isPlaying)
            {
                Target.Rebuild();
                Undo.undoRedoPerformed += Target.Rebuild;
            }
        }

        protected virtual void OnDisable()
        {
            EditorUtility.UnloadUnusedAssetsImmediate(false);
            Undo.undoRedoPerformed -= Target.Rebuild;
        }

        public override void OnInspectorGUI()
        {
            DrawCaptionInspector();
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                OnInspectorChange();
            }
        }

        protected virtual void DrawCaptionInspector()
        {
            var caption = CollectCaption();
            if (!string.IsNullOrEmpty(caption))
            {
                EditorGUILayout.HelpBox(caption, MessageType.Info);
            }
        }

        protected virtual string CollectCaption()
        {
            if (Target.Renderer.sharedMesh == null)
            {
                return null;
            }

            var mesh = Target.Renderer.sharedMesh;
            return string.Format("MeshCount: {0}  Vertices: {1}  Triangles: {2}",
                mesh.subMeshCount, mesh.vertexCount, mesh.triangles.Length / 3);
        }

        protected virtual void OnInspectorChange()
        {
            Target.Rebuild();
        }
    }
}