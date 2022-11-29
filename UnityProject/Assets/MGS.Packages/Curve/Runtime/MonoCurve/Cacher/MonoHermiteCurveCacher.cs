/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoHermiteCurveCacher.cs
 *  Description  :  Cacher for mono hermite curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/30/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Cacher for mono hermite curve.
    /// </summary>
    [RequireComponent(typeof(MonoHermiteCurve))]
    public class MonoHermiteCurveCacher : MonoCurveCacher
    {
        /// <summary>
        /// Host curve of cacher.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected MonoHermiteCurve curve;

        /// <summary>
        /// Reset component.
        /// </summary>
        protected virtual void Reset()
        {
            curve = GetComponent<MonoHermiteCurve>();
        }

        /// <summary>
        /// Serialize mono curve to string content.
        /// </summary>
        /// <returns></returns>
        protected override string SerializeCurve()
        {
            return AvatarJsonUtility.ToJson(curve.anchors);
        }

        /// <summary>
        /// Deserialize mono curve from string content.
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        protected override bool DeserializeCurve(string contents)
        {
            try
            {
                var anchors = AvatarJsonUtility.FromJson<HermiteAnchor>(contents);
                if (anchors == null)
                {
                    Debug.LogError("The anchors is null.");
                    return false;
                }

                curve.anchors = anchors;
                curve.Rebuild();
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}