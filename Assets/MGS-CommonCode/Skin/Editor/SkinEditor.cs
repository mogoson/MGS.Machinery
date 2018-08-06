/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SkinEditor.cs
 *  Description  :  Editor for MonoSkin component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.UEditor;
using UnityEditor;
using UnityEngine;

namespace Mogoson.Skin
{
    [CustomEditor(typeof(MonoSkin), true)]
    public class SkinEditor : GenericEditor
    {
        #region Field and Property
        protected MonoSkin Target { get { return target as MonoSkin; } }
        #endregion

        #region Protected Method
        protected virtual void OnEnable()
        {
            if (!Application.isPlaying)
            {
                Target.Rebuild();
                Undo.undoRedoPerformed = () => { Target.Rebuild(); };
            }
        }

        protected virtual void OnDisable()
        {
            EditorUtility.UnloadUnusedAssetsImmediate(false);
            Undo.undoRedoPerformed = null;
        }
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
                Target.Rebuild();
        }
        #endregion
    }
}