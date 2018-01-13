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
        protected CrankLinkMechanism Script { get { return target as CrankLinkMechanism; } }
        protected readonly string[] hingeEditorButtons = { "Free", "Hinge", "Lock" };
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            Handles.DrawLine(Script.transform.position, Script.transform.position + Script.transform.right * lineLength);
            Handles.DrawLine(Script.transform.position, Script.transform.position + Script.transform.up * lineLength);
        }

        protected void DrawHingeEditorTool()
        {
            EditorGUI.BeginChangeCheck();
            Script.editMode = (EditMode)GUILayout.SelectionGrid((int)Script.editMode, hingeEditorButtons, hingeEditorButtons.Length);
            if (EditorGUI.EndChangeCheck())
                MarkSceneDirty();

            if (Script.editMode == EditMode.Free)
                Script.enabled = false;
            else
                Script.enabled = true;
        }
        #endregion
    }
}