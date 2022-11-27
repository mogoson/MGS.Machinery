/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoBezierCurveCacher.cs
 *  Description  :  Cacher for mono bezier curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/30/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Cacher for mono bezier curve.
    /// </summary>
    [RequireComponent(typeof(MonoBezierCurve))]
    public class MonoBezierCurveCacher : MonoCurveCacher
    {
        /// <summary>
        /// Host curve of cacher.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        protected MonoBezierCurve curve;

        /// <summary>
        /// Reset component.
        /// </summary>
        protected virtual void Reset()
        {
            curve = GetComponent<MonoBezierCurve>();
        }

        /// <summary>
        /// Serialize mono curve to string content.
        /// </summary>
        /// <returns></returns>
        protected override string SerializeCurve()
        {
            var anchors = new List<BezierAnchor>() { curve.from, curve.to };
            return AvatarJsonUtility.ToJson(anchors);
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
                var anchors = AvatarJsonUtility.FromJson<BezierAnchor>(contents);
                if (anchors == null || anchors.Count < 2)
                {
                    Debug.LogError("The anchors is null or count is less than 2.");
                    return false;
                }

                curve.from = anchors[0];
                curve.to = anchors[1];
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