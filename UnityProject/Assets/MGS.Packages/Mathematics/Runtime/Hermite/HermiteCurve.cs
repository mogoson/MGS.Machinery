/*************************************************************************
 *  Copyright © 2017-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  HermiteCurve.cs
 *  Description  :  Define piecewise three hermite spline curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

namespace MGS.Mathematics
{
    /// <summary>
    /// Piecewise three hermite spline curve.
    /// </summary>
    public class HermiteCurve
    {
        #region Indexer
        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="index">Index of key frame.</param>
        /// <returns>The key frame at index.</returns>
        public KeyFrame this[int index]
        {
            get { return frames[index]; }
        }
        #endregion

        #region Field and Property
        /// <summary>
        /// Key frames of curve.
        /// </summary>
        protected List<KeyFrame> frames = new List<KeyFrame>();

        /// <summary>
        /// Count of key frames.
        /// </summary>
        public int KeyFramesCount { get { return frames.Count; } }
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        public HermiteCurve() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="frames">Key frames of curve.</param>
        public HermiteCurve(KeyFrame[] frames)
        {
            this.frames.AddRange(frames);
        }

        /// <summary>
        /// Evaluate the value of hermite curve at time.
        /// </summary>
        /// <param name="t">Time of curve to evaluate value.</param>
        /// <returns>The value of hermite curve at time.</returns>
        public float Evaluate(float t)
        {
            return Evaluate(frames.ToArray(), t);
        }

        /// <summary>
        /// Add key frame to curve.
        /// </summary>
        /// <param name="time">Time of key frame.</param>
        /// <param name="value">Value of key frame.</param>
        public void AddKeyFrame(float time, float value)
        {
            frames.Add(new KeyFrame(time, value));
        }

        /// <summary>
        /// Add key frame to curve.
        /// </summary>
        /// <param name="keyFrame">Key frame to add.</param>
        public void AddKeyFrame(KeyFrame keyFrame)
        {
            frames.Add(keyFrame);
        }

        /// <summary>
        /// Add key frames to curve.
        /// </summary>
        /// <param name="frames"></param>
        public void AddFrames(IEnumerable<KeyFrame> frames)
        {
            this.frames.AddRange(frames);
        }

        /// <summary>
        /// Remove key frame.
        /// </summary>
        /// <param name="frame">Key frame to remove.</param>
        public void RemoveFrame(KeyFrame frame)
        {
            frames.Remove(frame);
        }

        /// <summary>
        /// Remove key frame at index.
        /// </summary>
        /// <param name="index">Index of frame.</param>
        public void RemoveFrame(int index)
        {
            frames.RemoveAt(index);
        }

        /// <summary>
        /// Clear frames.
        /// </summary>
        public void ClearFrames()
        {
            frames.Clear();
        }

        /// <summary>
        /// Smooth in and out tangents.
        /// </summary>
        /// <param name="weight"></param>
        public void SmoothTangents(float weight = 0)
        {
            for (int i = 0; i < frames.Count; i++)
            {
                SmoothTangents(i, weight);
            }
        }

        /// <summary>
        /// Smooth in and out tangents.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="weight"></param>
        public void SmoothTangents(int index, float weight)
        {
            if (index < 0 || index > frames.Count - 1)
            {
                return;
            }

            //Designed By Mogoson.
            KeyFrame k0, k1, k2;
            if (index == 0 || index == frames.Count - 1)
            {
                if (frames.Count < 2 || frames[0].value != frames[frames.Count - 1].value)
                {
                    var frame = frames[index];
                    frame.inTangent = frame.outTangent = 0;
                    frames[index] = frame;
                    return;
                }

                k0 = frames[frames.Count - 2];
                k1 = frames[index];
                k2 = frames[1];

                if (index == 0)
                {
                    k0.time -= frames[frames.Count - 1].time;
                }
                else
                {
                    k2.time += frames[frames.Count - 1].time;
                }
            }
            else
            {
                k0 = frames[index - 1];
                k1 = frames[index];
                k2 = frames[index + 1];
            }

            var weight01 = (1 + weight) / 2;
            var weight12 = (1 - weight) / 2;
            var t01 = (k1.value - k0.value) / (k1.time - k0.time);
            var t12 = (k2.value - k1.value) / (k2.time - k1.time);
            k1.inTangent = k1.outTangent = t01 * weight01 + t12 * weight12;
            frames[index] = k1;
        }
        #endregion

        #region Static Method
        /// <summary>
        /// Evaluate the value of hermite curve at time on the range from start key frame to end key frame.
        /// </summary>
        /// <param name="start">Start key frame of hermite curve.</param>
        /// <param name="end">End key frame of hermite curve.</param>
        /// <param name="t">Time of curve to evaluate value.</param>
        /// <returns>The value of hermite curve at time on the range from start key frame to end key frame.</returns>
        public static float Evaluate(KeyFrame start, KeyFrame end, float t)
        {
            return Hermite.Evaluate(start.time, end.time, start.value, end.value, start.outTangent, end.inTangent, t);
        }

        /// <summary>
        /// Evaluate the value of hermite curve at time.
        /// </summary>
        /// <param name="frames">Key frames of hermite curve.</param>
        /// <param name="t"></param>
        /// <returns>The value of hermite curve at time.</returns>
        public static float Evaluate(KeyFrame[] frames, float t)
        {
            if (frames == null || frames.Length == 0)
            {
                return 0;
            }

            var value = 0f;
            if (frames.Length == 1)
            {
                value = frames[0].value;
            }
            else
            {
                if (t <= frames[0].time)
                {
                    value = frames[0].value;
                }
                else if (t >= frames[frames.Length - 1].time)
                {
                    value = frames[frames.Length - 1].value;
                }
                else
                {
                    for (int i = 0; i < frames.Length; i++)
                    {
                        if (t == frames[i].time)
                        {
                            value = frames[i].value;
                            break;
                        }

                        if (t > frames[i].time && t < frames[i + 1].time)
                        {
                            value = Evaluate(frames[i], frames[i + 1], t);
                            break;
                        }
                    }
                }
            }
            return value;
        }
        #endregion
    }
}