/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: FreeCrankEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/1/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         FreeCrankEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     3/1/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(FreeCrank), true)]
    [CanEditMultipleObjects]
    public class FreeCrankEditor : MechanismEditor
    {
        #region Property and Field
        protected FreeCrank script { get { return target as FreeCrank; } }
        protected Vector3 axis { get { return script.transform.forward; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            Handles.CircleCap(0, script.transform.position, script.transform.rotation, areaRadius);
            DrawArrow(script.transform.position, axis, arrowLength, nodeSize, "Axis", blue);
            DrawArrow(script.transform.position, script.transform.up, areaRadius, nodeSize, string.Empty, blue);
            DrawArea();
            DrawRockers(script.rockers, script.transform, blue);
        }//OnSceneGUI()_end

        protected virtual void DrawArea()
        {
            Handles.color = transparentBlue;
            Handles.DrawSolidDisc(script.transform.position, axis, areaRadius);
        }//DrawArea()_end
        #endregion
    }//class_end
}//namespace_end