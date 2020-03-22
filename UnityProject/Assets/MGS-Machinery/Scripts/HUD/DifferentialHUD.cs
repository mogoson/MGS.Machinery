/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DifferentialHUD.cs
 *  Description  :  Draw scene HUD to control differential.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/6/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machinery
{
    [AddComponentMenu("MGS/Machinery/DifferentialHUD")]
    [RequireComponent(typeof(Differential))]
    public class DifferentialHUD : MonoBehaviour
    {
        #region Field and Property
        public float top = 10;
        public float left = 10;

        private Differential differential;
        private float coefficient = 0;
        #endregion

        #region Private Method
        private void Start()
        {
            differential = GetComponent<Differential>();
        }

        private void OnGUI()
        {
            GUILayout.Space(top);
            GUILayout.BeginHorizontal();
            GUILayout.Space(left);

            var sliderValue = GUILayout.HorizontalSlider(coefficient, -2, 2, GUILayout.Width(240));
            if (coefficient != sliderValue)
            {
                coefficient = sliderValue;
                differential.Coefficient = coefficient;
            }

            GUILayout.EndHorizontal();
        }
        #endregion
    }
}