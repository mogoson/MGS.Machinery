/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurveChainDriver.cs
 *  Description  :  CurveChainDriver component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  12/09/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Chain.Demo
{
    public class CurveChainDriver : MonoBehaviour
    {
        public MonoCurveChain chain;
        public float velocity = 0.15f;

        void Update()
        {
            if (Input.GetKey(KeyCode.P))
            {
                chain.Drive(velocity * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.N))
            {
                chain.Drive(-velocity * Time.deltaTime);
            }
        }
    }
}