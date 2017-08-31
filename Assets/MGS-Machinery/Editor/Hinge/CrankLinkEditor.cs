/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CrankLinkEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/13/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         CrankLinkEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     3/13/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    public class CrankLinkEditor : MechanismEditor
    {
        #region Property and Field
        protected CrankLinkMechanism script { get { return target as CrankLinkMechanism; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            Handles.DrawLine(script.transform.position, script.transform.position + script.transform.right * lineLength);
            Handles.DrawLine(script.transform.position, script.transform.position + script.transform.up * lineLength);
        }

        protected void DrawHingeEditorTools()
        {
            GUILayout.BeginHorizontal();
            GUI.color = script.editMode == EditMode.Edit ? blue : Color.white;
            if (GUILayout.Button("Edit"))
            {
                script.enabled = false;
                script.editMode = EditMode.Edit;
            }
            GUI.color = script.editMode == EditMode.Hinge ? blue : Color.white;
            if (GUILayout.Button("Hinge"))
            {
                script.enabled = true;
                script.editMode = EditMode.Hinge;
            }
            GUI.color = script.editMode == EditMode.Lock ? blue : Color.white;
            if (GUILayout.Button("Lock"))
            {
                script.Initialize();
                script.enabled = true;
                script.editMode = EditMode.Lock;
            }
            GUI.color = Color.white;
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}