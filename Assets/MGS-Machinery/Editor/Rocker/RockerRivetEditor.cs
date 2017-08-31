/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: RockerRivetEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 1/17/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       RockerRivetEditor          Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     1/17/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(RockerRivet), true)]
    [CanEditMultipleObjects]
    public class RockerRivetEditor : MechanismEditor
    {
        #region Property and Field
        protected RockerRivet script { get { return target as RockerRivet; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            if (!script.rockJoint)
                return;
            GUI.color = blue;
            Handles.color = blue;
            Handles.Label(script.transform.position, "Rivet");
            Handles.SphereCap(0, script.transform.position, Quaternion.identity, nodeSize);
        }
        #endregion
    }
}