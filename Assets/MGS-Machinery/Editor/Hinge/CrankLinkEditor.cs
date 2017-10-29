/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  CrankLinkEditor.cs
 *  Description  :  Custom editor for CrankLink.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/13/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    public class CrankLinkEditor : MechanismEditor
    {
        #region Property and Field
        protected CrankLinkMechanism script { get { return target as CrankLinkMechanism; } }
        protected readonly string[] hingeEditorButtons = { "Free", "Hinge", "Lock" };
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            Handles.DrawLine(script.transform.position, script.transform.position + script.transform.right * lineLength);
            Handles.DrawLine(script.transform.position, script.transform.position + script.transform.up * lineLength);
        }

        protected void DrawHingeEditorTool()
        {
            EditorGUI.BeginChangeCheck();
            script.editMode = (EditMode)GUILayout.SelectionGrid((int)script.editMode, hingeEditorButtons, hingeEditorButtons.Length);
            if (EditorGUI.EndChangeCheck())
                MarkSceneDirty();

            if (script.editMode == EditMode.Free)
                script.enabled = false;
            else
                script.enabled = true;
        }
        #endregion
    }
}