/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TransmissionHUD.cs
 *  Description  :  Draw scene HUD to control transmission.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/30/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Machinery
{
    [AddComponentMenu("MGS/Machinery/TransmissionHUD")]
    public class TransmissionHUD : MonoBehaviour
    {
        #region Field and Property
        public float top = 10;
        public float left = 10;

        public GradeConfig[] grades;
        public Gear synchronizer;

        private Vector3 defaultPosition;
        private ICoaxeMechanism coaxe;
        #endregion

        #region Private Method
        private void Start()
        {
            defaultPosition = synchronizer.transform.localPosition;
        }

        private void OnGUI()
        {
            GUILayout.Space(top);
            GUILayout.BeginHorizontal();
            GUILayout.Space(left);
            GUILayout.BeginVertical(GUILayout.MinWidth(60));
            foreach (var grade in grades)
            {
                if (GUILayout.Button(grade.info))
                {
                    synchronizer.transform.localPosition = grade.adsorb;
                    if (coaxe != null)
                    {
                        coaxe.BreakCoaxe(synchronizer);
                    }

                    coaxe = grade.gear;
                    coaxe.LinkCoaxe(synchronizer);
                }
            }
            if (GUILayout.Button("Default"))
            {
                synchronizer.transform.localPosition = defaultPosition;
                if (coaxe != null)
                {
                    coaxe.BreakCoaxe(synchronizer);
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        #endregion
    }

    [Serializable]
    public struct GradeConfig
    {
        public string info;
        public Gear gear;
        public Vector3 adsorb;
    }
}