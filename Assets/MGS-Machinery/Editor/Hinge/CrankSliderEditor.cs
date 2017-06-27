/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CrankSliderEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/13/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       CrankSliderEditor          Ignore.
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

    [CustomEditor(typeof(CrankSlider), true)]
    [CanEditMultipleObjects]
    public class CrankSliderEditor : CrankLinkEditor
    {
        #region Property and Field
        protected new CrankSlider script { get { return target as CrankSlider; } }
        protected Vector3 zeroPoint
        {
            get
            {
                if (Application.isPlaying)
                    return script.transform.TransformPoint(script.lsJointPosition);
                else
                    return script.lsJoint.position;
            }//get_end
        }//zeroPoint_end
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
                DrawPositionHandle(script.lsJoint);
                DrawRotationHandle(script.lsJoint);
            }//if()_end
            if(script.editMode == EditMode.Hinge)
                DrawRotationHandle(script.crank.transform);

            Handles.SphereCap(0, script.crank.transform.position, Quaternion.identity, nodeSize);
            Handles.CircleCap(0, script.crank.transform.position, script.crank.transform.rotation, areaRadius);
            DrawArrow(script.crank.transform.position, script.crank.transform.forward, arrowLength, nodeSize, "Axis", blue);

            var offset = (script.linkBar.transform.position - script.crank.transform.position).normalized;
            DrawArrow(script.crank.transform.position, offset, areaRadius, nodeSize, string.Empty, blue);
            DrawArrow(script.crank.transform.position, script.linkBar.transform.position, nodeSize, string.Empty, blue);
            DrawArrow(script.linkBar.transform.position, script.lsJoint.position, nodeSize, string.Empty, blue);

            var axis = script.ProjectDirection(script.lsJoint.forward).normalized;
            Handles.DrawLine(zeroPoint, script.lsJoint.position);
            DrawArrow(zeroPoint, axis, arrowLength, nodeSize, string.Empty, blue);
            DrawArrow(zeroPoint, -axis, arrowLength, nodeSize, string.Empty, blue);

            DrawSceneTool();
        }//OnSceneGUI()_end

        protected virtual void DrawSceneTool()
        {
            var rect = new Rect(Screen.width - 150, Screen.height - 95, 140, 45);
            Handles.BeginGUI();
            GUILayout.BeginArea(rect, "Hinge Editor", "Window");
            DrawHingeEditorTools();
            GUILayout.EndArea();
            Handles.EndGUI();
        }//DrawSceneTool()_end
        #endregion
    }//class_end
}//namespace_end