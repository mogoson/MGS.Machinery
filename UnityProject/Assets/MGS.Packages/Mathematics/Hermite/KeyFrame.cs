/*************************************************************************
 *  Copyright © 2017-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  KeyFrame.cs
 *  Description  :  Key frame base on time and value.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace MGS.Mathematics
{
    /// <summary>
    /// Key frame base on time and value.
    /// </summary>
    [Serializable]
    public struct KeyFrame
    {
        #region Field and Property
        /// <summary>
        /// Time of key frame.
        /// </summary>
        public float time;

        /// <summary>
        /// Value of key frame.
        /// </summary>
        public float value;

        /// <summary>
        /// In tangent of key frame.
        /// </summary>
        public float inTangent;

        /// <summary>
        /// Out tangent of key frame.
        /// </summary>
        public float outTangent;
        #endregion

        #region Public Method
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="time">Time of key frame.</param>
        /// <param name="value">Value of key frame.</param>
        public KeyFrame(float time, float value)
        {
            this.time = time;
            this.value = value;
            inTangent = 0;
            outTangent = 0;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="time">Time of key frame.</param>
        /// <param name="value">Value of key frame.</param>
        /// <param name="inTangent">In tangent of key frame.</param>
        /// <param name="outTangent">Out tangent of key frame.</param>
        public KeyFrame(float time, float value, float inTangent, float outTangent)
        {
            this.time = time;
            this.value = value;
            this.inTangent = inTangent;
            this.outTangent = outTangent;
        }
        #endregion
    }
}