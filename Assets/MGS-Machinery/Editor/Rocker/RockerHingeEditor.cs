/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: RockerHingeEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 2/16/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       RockerHingeEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     2/16/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(RockerHinge), true)]
    [CanEditMultipleObjects]
    public class RockerHingeEditor : MechanismEditor
    {
        #region Property and Field
        protected RockerHinge script { get { return target as RockerHinge; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = blue;
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
            Handles.CircleCap(0, script.transform.position, script.transform.rotation, areaRadius);
            Handles.DrawWireArc(script.transform.position, script.transform.right, script.axis, -180, areaRadius);
            DrawArrow(script.transform.position, script.transform.up, areaRadius, nodeSize, string.Empty, blue);

            var axisStart = script.transform.position - script.axis * areaRadius;
            var axisEnd = script.transform.position + script.axis * arrowLength;
            DrawArrow(axisStart, axisEnd, nodeSize, "Axis", blue);

            if (script.rockJoint)
                DrawArrow(script.transform.position, script.rockJoint.forward, areaRadius, nodeSize, string.Empty, blue);
        }//OnSceneGUI()_end
        #endregion
    }//class_end
}//namespace_end