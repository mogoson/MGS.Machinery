/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: LimitCrankEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/2/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.       LimitCrankEditor           Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     3/2/2017       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(LimitCrank), true)]
    [CanEditMultipleObjects]
    public class LimitCrankEditor : FreeCrankEditor
    {
        #region Property and Field
        protected new LimitCrank script { get { return target as LimitCrank; } }
        protected Vector3 zeroAxis
        {
            get
            {
                if (Application.isPlaying)
                {
                    var up = Quaternion.Euler(script.startAngles) * Vector3.up;
                    if (script.transform.parent)
                        up = script.transform.parent.rotation * up;
                    return up;
                }
                else
                    return script.transform.up;
            }//get_end
        }//zeroAxis_end
        #endregion

        #region Protected Method
        protected override void DrawArea()
        {
            var minAxis = Quaternion.AngleAxis(script.minAngle, axis) * zeroAxis;
            var maxAxis = Quaternion.AngleAxis(script.maxAngle, axis) * zeroAxis;

            DrawArrow(script.transform.position, zeroAxis, arrowLength, nodeSize, "Zero", blue);
            DrawArrow(script.transform.position, minAxis, arrowLength, nodeSize, "MinAngle", blue);
            DrawArrow(script.transform.position, maxAxis, arrowLength, nodeSize, "MaxAngle", blue);

            Handles.color = transparentBlue;
            Handles.DrawSolidArc(script.transform.position, axis, minAxis, script.maxAngle - script.minAngle, areaRadius);
        }//DrawArea()_end
        #endregion

        #region Public Method
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            script.maxAngle = Mathf.Clamp(script.maxAngle, script.minAngle, float.MaxValue);
        }//OnInspectorGUI()_end
        #endregion
    }//class_end
}//namespace_end