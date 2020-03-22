/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MotorHUD.cs
 *  Description  :  Draw scene HUD to control motor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/24/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    [AddComponentMenu("MGS/Machinery/MotorHUD")]
    [RequireComponent(typeof(Motor))]
    public class MotorHUD : MonoBehaviour
    {
        #region Field and Property
        public float top = 10;
        public float left = 10;

        private Motor motor;
        #endregion

        #region Private Method
        private void Start()
        {
            motor = GetComponent<Motor>();
        }

        private void OnGUI()
        {
            GUILayout.Space(top);
            GUILayout.BeginHorizontal();
            GUILayout.Space(left);
            if (GUILayout.Button("Turn On Motor"))
            {
                motor.TurnOn();
            }
            if (GUILayout.Button("Turn Off Motor"))
            {
                motor.TurnOff();
            }
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}