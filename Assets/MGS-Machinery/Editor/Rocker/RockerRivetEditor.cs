/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  RockerRivetEditor.cs
 *  Description  :  Custom editor for RockerRivet.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  1/17/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
    [CustomEditor(typeof(RockerRivet), true)]
    [CanEditMultipleObjects]
    public class RockerRivetEditor : MechanismEditor
    {
        #region Property and Field
        protected RockerRivet Script { get { return target as RockerRivet; } }
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            if (!Script.rockJoint)
                return;

            GUI.color = blue;
            Handles.color = blue;
            Handles.Label(Script.transform.position, "Rivet");
            DrawSphereCap(Script.transform.position, Quaternion.identity, nodeSize);
        }
        #endregion
    }
}