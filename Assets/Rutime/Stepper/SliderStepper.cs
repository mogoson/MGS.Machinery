/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Slider.cs
 *  Description  :  Define SliderStepper component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  12/08/2022
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machineries
{
    /// <summary>
    /// Stepper motor for SliderMechanism.
    /// </summary>
    public class SliderStepper : Stepper
    {
        /// <summary>
        /// SliderMechanism drive by stepper.
        /// </summary>
        public SliderMechanism mechanism;

        /// <summary>
        /// Mechanism drive by stepper.
        /// </summary>
        protected override Mechanism Mechanism { get { return mechanism; } }

        /// <summary>
        /// Current mileage.
        /// </summary>
        protected override float Current { get { return mechanism.Displacement; } }
    }
}