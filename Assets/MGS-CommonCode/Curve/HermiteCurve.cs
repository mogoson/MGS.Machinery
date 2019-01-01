/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HermiteCurve.cs
 *  Description  :  Hermite curve in three dimensional space.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/21/2017
 *  Description  :  Initial development version.
 *  
 *  Author       :  Mogoson
 *  Version      :  0.1.1
 *  Date         :  2/28/2018
 *  Description  :  Add static method FromAnchors.
 *************************************************************************/

using Mogoson.IO;
using System;
using UnityEngine;

namespace Mogoson.Curve
{
    /// <summary>
    /// Vector keyframe.
    /// </summary>
    [Serializable]
    public struct VectorKeyFrame
    {
        #region Field and Property
        /// <summary>
        /// Key of keyframe.
        /// </summary>
        public float key;

        /// <summary>
        /// Value of keyframe.
        /// </summary>
        public Vector3 value;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">Key of keyframe.</param>
        /// <param name="value">Value of keyframe.</param>
        public VectorKeyFrame(float key, Vector3 value)
        {
            this.key = key;
            this.value = value;
        }
        #endregion
    }

    /// <summary>
    /// Hermite curve in three dimensional space.
    /// </summary>
    public class HermiteCurve : ICurve
    {
        #region Indexer
        public VectorKeyFrame this[int index]
        {
            get
            {
                return new VectorKeyFrame(xCurve[index].time, new Vector3(xCurve[index].value, yCurve[index].value, zCurve[index].value));
            }
        }
        #endregion

        #region Field and Property
        /// <summary>
        /// Coefficient of delta to lerp key.
        /// </summary>
        protected const float Coefficient = 0.05f;

        /// <summary>
        /// Count of Keyframes.
        /// </summary>
        public int KeyframeCount { get { return xCurve.length; } }

        /// <summary>
        /// Length of curve.
        /// </summary>
        public float Length
        {
            get
            {
                var length = 0.0f;
                var delta = MaxKey * Coefficient;
                for (float key = 0; key < MaxKey; key += delta)
                {
                    length += Vector3.Distance(GetPointAt(key), GetPointAt(key + delta));
                }
                return length;
            }
        }

        /// <summary>
        /// Max key of curve.
        /// </summary>
        public float MaxKey
        {
            get
            {
                if (KeyframeCount == 0)
                {
                    return 0;
                }
                return xCurve[KeyframeCount - 1].time;
            }
        }

        /// <summary>
        /// The behaviour of the animation after the last keyframe.
        /// </summary>
        public WrapMode PostWrapMode
        {
            set { xCurve.postWrapMode = yCurve.postWrapMode = zCurve.postWrapMode = value; }
            get { return xCurve.postWrapMode; }
        }

        /// <summary>
        /// The behaviour of the animation before the first keyframe.
        /// </summary>
        public WrapMode PreWrapMode
        {
            set { xCurve.preWrapMode = yCurve.preWrapMode = zCurve.preWrapMode = value; }
            get { return xCurve.preWrapMode; }
        }

        protected AnimationCurve xCurve;
        protected AnimationCurve yCurve;
        protected AnimationCurve zCurve;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public HermiteCurve()
        {
            xCurve = new AnimationCurve();
            yCurve = new AnimationCurve();
            zCurve = new AnimationCurve();
        }

        /// <summary>
        /// Add a new keyframe to the curve.
        /// </summary>
        /// <param name="keyframe">The keyframe to add to the curve.</param>
        /// <returns>The index of the added keyframe, or -1 if the keyframe could not be added.</returns>
        public int AddKeyframe(VectorKeyFrame keyframe)
        {
            xCurve.AddKey(keyframe.key, keyframe.value.x);
            yCurve.AddKey(keyframe.key, keyframe.value.y);
            return zCurve.AddKey(keyframe.key, keyframe.value.z);
        }

        /// <summary>
        /// Add a new keyframe to the curve.
        /// </summary>
        /// <param name="key">The key of the keyframe.</param>
        /// <param name="value">The value of the keyframe.</param>
        /// <returns>The index of the added keyframe, or -1 if the keyframe could not be added.</returns>
        public int AddKeyframe(float key, Vector3 value)
        {
            xCurve.AddKey(key, value.x);
            yCurve.AddKey(key, value.y);
            return zCurve.AddKey(key, value.z);
        }

        /// <summary>
        /// Removes a keyframe.
        /// </summary>
        /// <param name="index">The index of the keyframe to remove.</param>
        public void RemoveKeyframe(int index)
        {
            xCurve.RemoveKey(index);
            yCurve.RemoveKey(index);
            zCurve.RemoveKey(index);
        }

        /// <summary>
        /// Smooth the in and out tangents of the keyframe at index.
        /// </summary>
        /// <param name="index">The index of the keyframe.</param>
        /// <param name="weight">The smoothing weight to apply to the keyframe's tangents.</param>
        public void SmoothTangents(int index, float weight)
        {
            xCurve.SmoothTangents(index, weight);
            yCurve.SmoothTangents(index, weight);
            zCurve.SmoothTangents(index, weight);
        }

        /// <summary>
        /// Smooth the in and out tangents of keyframes.
        /// </summary>
        /// <param name="weight">The smoothing weight to apply to the keyframe's tangents.</param>
        public void SmoothTangents(float weight)
        {
            for (int i = 0; i < KeyframeCount; i++)
            {
                SmoothTangents(i, weight);
            }
        }

        /// <summary>
        /// Get point by evaluate the curve at key.
        /// </summary>
        /// <param name="key">The key within the curve you want to evaluate.</param>
        /// <returns>The point on the curve at the key.</returns>
        public Vector3 GetPointAt(float key)
        {
            return new Vector3(xCurve.Evaluate(key), yCurve.Evaluate(key), zCurve.Evaluate(key));
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Create a curve base on anchors.
        /// </summary>
        /// <param name="anchors">Anchor points of curve.</param>
        /// <param name="close">Curve is close?</param>
        /// <returns>New curve.</returns>
        public static HermiteCurve FromAnchors(Vector3[] anchors, bool close = false)
        {
            // Creat new curve.
            var curve = new HermiteCurve();

            //No anchor.
            if (anchors == null || anchors.Length == 0)
            {
                LogUtility.LogWarning("Created a curve with no key frame: The anchors is null or empty.");
            }
            else
            {
                //Add frame keys to curve.
                var distance = 0f;
                for (int i = 0; i < anchors.Length - 1; i++)
                {
                    curve.AddKeyframe(distance, anchors[i]);
                    distance += Vector3.Distance(anchors[i], anchors[i + 1]);
                }

                //Add the last key.
                curve.AddKeyframe(distance, anchors[anchors.Length - 1]);

                if (close)
                {
                    //Add the close key(the first key).
                    distance += Vector3.Distance(anchors[anchors.Length - 1], anchors[0]);
                    curve.AddKeyframe(distance, anchors[0]);
                }

                //Smooth curve keys out tangent.
                curve.SmoothTangents(0);
            }
            return curve;
        }
        #endregion
    }
}