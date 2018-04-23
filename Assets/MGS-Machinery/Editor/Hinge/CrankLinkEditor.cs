/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CrankLinkEditor.cs
 *  Description  :  Custom editor for CrankLink.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/21/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Mogoson.Machinery
{
    public class CrankLinkEditor : BaseEditor
    {
        #region Field and Property
        protected CrankLinkMechanism Target { get { return target as CrankLinkMechanism; } }
        protected readonly string[] HingeEditorButtons = { "Free", "Hinge", "Lock" };
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            Handles.DrawLine(Target.transform.position, Target.transform.position + Target.transform.right * LineLength);
            Handles.DrawLine(Target.transform.position, Target.transform.position + Target.transform.up * LineLength);
        }

        protected void DrawHingeEditorTool()
        {
            EditorGUI.BeginChangeCheck();
            Target.editMode = (EditMode)GUILayout.SelectionGrid((int)Target.editMode, HingeEditorButtons, HingeEditorButtons.Length);

            if (Target.editMode == EditMode.Free)
                Target.enabled = false;
            else
                Target.enabled = true;

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(Target);
                MarkSceneDirty();
            }
        }
        #endregion
    }
}