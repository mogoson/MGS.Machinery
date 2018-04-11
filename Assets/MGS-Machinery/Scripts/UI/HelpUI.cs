/*************************************************************************
 *  Copyright © 2016-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HelpUI.cs
 *  Description  :  Draw scene UI to show help info.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Mogoson.Machinery
{
    [AddComponentMenu("Mogoson/Machinery/HelpUI")]
    public class HelpUI : MonoBehaviour
    {
        #region Field and Property
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