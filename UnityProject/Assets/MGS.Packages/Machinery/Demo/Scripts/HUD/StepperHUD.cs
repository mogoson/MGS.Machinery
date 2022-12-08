/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  StepperHUD.cs
 *  Description  :  Draw scene HUD to control Stepper.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  12/08/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries.Demo
{
    [RequireComponent(typeof(Stepper))]
    public class StepperHUD : MonoBehaviour
    {
        public float top = 10;
        public float left = 10;

        [Space(5)]
        public string mileage = "30";
        public string velocity = "10";
        private Stepper stepper;

        private void Start()
        {
            stepper = GetComponent<Stepper>();
        }

        private void OnGUI()
        {
            GUILayout.Space(top);
            GUILayout.BeginHorizontal();
            GUILayout.Space(left);

            GUILayout.Label("mileage:");
            mileage = GUILayout.TextField(mileage, GUILayout.Width(100));

            GUILayout.Space(5);
            GUILayout.Label("velocity:");
            velocity = GUILayout.TextField(velocity, GUILayout.Width(100));

            if (GUILayout.Button("Drive"))
            {
                var mileageValue = float.Parse(mileage);
                var velocityValue = float.Parse(velocity);
                stepper.Drive(mileageValue, velocityValue);
            }
            GUILayout.EndHorizontal();
        }
    }
}