/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: CeTelescopicJointEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 3/2/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.     CeTelescopicJointEditor      Ignore.
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

    [CustomEditor(typeof(CeTelescopicJoint), true)]
    [CanEditMultipleObjects]
    public class CeTelescopicJointEditor : TelescopicJointEditor
    {
        #region Protected Method
        protected override void DrawStroke()
        {
            DrawArrow(zeroPoint, axis, script.stroke, nodeSize, "+Stroke", blue);
            DrawArrow(zeroPoint, -axis, script.stroke, nodeSize, "-Stroke", blue);
        }//DrawStroke()_end
        #endregion
    }//class_end
}//namespace_end