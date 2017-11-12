/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  FreeCrankEditor.cs
 *  Description  :  Custom editor for FreeCrank.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/1/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEditor;
using UnityEngine;

namespace Developer.Machinery
{
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
            DrawSphereCap(script.transform.position, Quaternion.identity, nodeSize);
            DrawCircleCap(script.transform.position, script.transform.rotation, areaRadius);
            DrawArrow(script.transform.position, axis, arrowLength, nodeSize, "Axis", blue);
            DrawArrow(script.transform.position, script.transform.up, areaRadius, nodeSize, string.Empty, blue);
            DrawArea();
            DrawRockers(script.rockers, script.transform, blue);
        }

        protected virtual void DrawArea()
        {
            Handles.color = transparentBlue;
            Handles.DrawSolidDisc(script.transform.position, axis, areaRadius);
        }
        #endregion
    }
}