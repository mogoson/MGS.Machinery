/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HelpHUD.cs
 *  Description  :  Draw help HUD in scene.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  4/15/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Packages.Demo
{
    [ExecuteInEditMode]
    public class HelpHUD : MonoBehaviour
    {
        #region Field and Property
        [Multiline]
        public string info = "Help info.";
        public Color color = Color.black;

        public float top = 10;
        public float left = 10;
        #endregion

        #region Private Method
        private void OnGUI()
        {
            DrawHelpHUD();
        }

        private void DrawHelpHUD()
        {
            GUI.color = color;
            GUILayout.Space(top);
            GUILayout.BeginHorizontal();
            GUILayout.Space(left);
            GUILayout.Label(info);
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}