/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SliderMechanism.cs
 *  Description  :  Slider joint mechanism.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  3/20/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Machineries
{
    /// <summary>
    /// Slider joint mechanism.
    /// </summary>
    public abstract class SliderMechanism : LinkRockerMechanism
    {
        #region Field and Property
        /// <summary>
        /// Stroke of slider.
        /// </summary>
        [Tooltip("Stroke of slider.")]
        public Range stroke = new Range(-1, 1);

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (State != TelescopicState.Free)
                {
                    return true;
                }
                return base.IsStuck;
            }
        }

        /// <summary>
        /// Displacement of slider.
        /// </summary>
        public float Displacement { protected set; get; }

        /// <summary>
        /// Telescopic state of slider.
        /// </summary>
        public TelescopicState State
        {
            get
            {
                var state = TelescopicState.Free;
                if (Displacement <= stroke.min)
                {
                    state = TelescopicState.Minimum;
                }
                else if (Displacement >= stroke.max)
                {
                    state = TelescopicState.Maximum;
                }
                return state;
            }
        }

        /// <summary>
        /// Start position of slider.
        /// </summary>
        public Vector3 StartPosition { protected set; get; }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize joint.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            StartPosition = transform.localPosition;
        }
        #endregion
    }
}