/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurve.cs
 *  Description  :  Define mono curve base on curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Mono curve base on curve.
    /// </summary>
    public abstract class MonoCurve : MonoBehaviour, IMonoCurve
    {
        /// <summary>
        /// Length of mono curve.
        /// </summary>
        public abstract float Length { get; }

        /// <summary>
        /// Curve for mono curve.
        /// </summary>
        protected abstract ITimeCurve Curve { get; }

        /// <summary>
        /// Reset component.
        /// </summary>
        protected virtual void Reset()
        {
            Rebuild();
        }

        /// <summary>
        /// Awake component.
        /// </summary>
        protected virtual void Awake()
        {
            StartCoroutine(CheckRebuild());
        }

        /// <summary>
        /// Check RectTransform rebuild mono curve.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator CheckRebuild()
        {
            if (transform is RectTransform)
            {
                //Wait one frame to require the RectTransform is initialized (after Awake).
                yield return null;
            }
            Rebuild();
        }

        /// <summary>
        /// Rebuild mono curve.
        /// </summary>
        public virtual void Rebuild()
        {
            SendMessage("OnMonoCurveRebuild", this, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Evaluate point on mono curve at length.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public virtual Vector3 Evaluate(float len)
        {
            var t = len / Length;
            return EvaluateNormalized(t);
        }

        /// <summary>
        /// Evaluate point on mono curve at normalized time int the range[0,1].
        /// </summary>
        /// <param name="t">The normalized time.</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        public virtual Vector3 EvaluateNormalized(float t)
        {
            return transform.TransformPoint(LocalEvaluateNormalized(t));
        }

        /// <summary>
        /// Evaluate local point on mono curve at length.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public virtual Vector3 LocalEvaluate(float len)
        {
            var t = len / Length;
            return LocalEvaluateNormalized(t);
        }

        /// <summary>
        /// Evaluate local point on mono curve at normalized time int the range[0,1].
        /// </summary>
        /// <param name="t">The normalized time.</param>
        /// <returns>The value of the curve, at the point in time specified.</returns>
        public virtual Vector3 LocalEvaluateNormalized(float t)
        {
            return Curve.Evaluate(t);
        }

        /// <summary>
        /// Evaluate length of mono curve.
        /// </summary>
        /// <param name="differ">Differentiation.</param>
        /// <returns></returns>
        protected virtual float EvaluateLength(float differ = 0.01f)
        {
            var length = 0f;
            var t = 0f;
            var p0 = EvaluateNormalized(t);
            while (t < 1.0f)
            {
                t = Mathf.Min(t + differ, 1.0f);
                var p1 = EvaluateNormalized(t);
                length += Vector3.Distance(p0, p1);
                p0 = p1;
            }
            return length;
        }
    }
}