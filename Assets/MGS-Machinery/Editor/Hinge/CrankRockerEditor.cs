/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CrankRockerEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/3/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         CrankRockerEditor        Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     3/3/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(CrankRocker), true)]
    [CanEditMultipleObjects]
    public class CrankRockerEditor : CrankLinkEditor
    {
        #region Property and Field
        protected new CrankRocker script { get { return target as CrankRocker; } }
        #endregion

        #region Protected Method
        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            if (!script.isIntact)
                return;
            if (script.editMode == EditMode.Edit)
            {
                DrawRotationHandle(script.crank.transform);
                DrawPositionHandle(script.linkBar.transform);
                DrawPositionHandle(script.rocker.transform);
                DrawPositionHandle(script.lrJoint);
            }//if()_end
            if (script.editMode == EditMode.Hinge)
                DrawRotationHandle(script.crank.transform);

            Handles.CircleCap(0, script.crank.transform.position, script.crank.transform.rotation, areaRadius);
            DrawArrow(script.crank.transform.position, script.crank.transform.forward, arrowLength, nodeSize, "Axis", blue);

            var offset = (script.linkBar.transform.position - script.crank.transform.position).normalized;
            DrawArrow(script.crank.transform.position, offset, areaRadius, nodeSize, string.Empty, blue);
            DrawArrow(script.crank.transform.position, script.linkBar.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.linkBar.transform.position, script.lrJoint.position, nodeSize, string.Empty, blue);
            DrawArrow(script.lrJoint.position, script.rocker.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.rocker.transform.position, script.crank.transform.position, nodeSize, string.Empty, blue);

            DrawSceneTool();
        }//OnSceneGUI()_end

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 150, Screen.height - 120, 140, 70);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Hinge Editor", "Window");
            DrawHingeEditorTools();

            GUILayout.BeginHorizontal("TextField");
            script.inertia = GUILayout.Toggle(script.inertia, "Inertia");
            script.restrict = GUILayout.Toggle(script.restrict, "Restrict");
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
            Handles.EndGUI();
        }//DrawSceneTool()_end
        #endregion
    }//class_end
}//namespace_end