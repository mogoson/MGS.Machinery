/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  HelpUI.cs
 *  Description  :  Draw scene UI to show help info.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  8/13/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Machinery
{
    [AddComponentMenu("Developer/Machinery/HelpUI")]
    public class HelpUI : MonoBehaviour
    {
        #region Property and Field
        [Multiline]
        public string text = "Help info.";

        public float xOffset = 10;
        public float yOffset = 10;
        #endregion

        #region Private Method
        private void OnGUI()
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