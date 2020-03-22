/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  EngineHUD.cs
 *  Description  :  Draw scene HUD to control Engine.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    [AddComponentMenu("MGS/Machinery/EngineHUD")]
    [RequireComponent(typeof(Engine))]
    public class EngineHUD : MonoBehaviour
    {
        #region Field and Property
        public float top = 10;
        public float left = 10;

        private Engine engine;
        #endregion

        #region Private Method
        private void Start()
        {
            engine = GetComponent<Engine>();
        }

        private void OnGUI()
        {
            GUILayout.Space(top);
            GUILayout.BeginHorizontal();
            GUILayout.Space(left);
            if (GUILayout.Button("Turn On Engine"))
            {
                engine.TurnOn();
            }
            if (GUILayout.Button("Turn Off Engine"))
            {
                engine.TurnOff();
            }
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}