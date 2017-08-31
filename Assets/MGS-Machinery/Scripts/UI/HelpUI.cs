/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson tech. Co., Ltd.
 *  FileName: HelpUI.cs
 *  Author: Mogoson   Version: 1.0   Date: 8/13/2016
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.            HelpUI                Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     8/13/2016       1.0        Build this file.
 *************************************************************************/

namespace Developer.Machinery
{
    using UnityEngine;

    /// <summary>
    /// Help UI.
    /// </summary>
    [AddComponentMenu("Developer/Machinery/HelpUI")]
    public class HelpUI : MonoBehaviour
    {
        #region Property and Field
        [Multiline]
        public string text;
        public float xOffset = 10;
        public float yOffset = 10;
        #endregion

        #region Private Method
        void OnGUI()
        {
            GUILayout.Space(yOffset);
            GUILayout.BeginHorizontal();
            GUILayout.Space(xOffset);
            GUILayout.Label(text);
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}