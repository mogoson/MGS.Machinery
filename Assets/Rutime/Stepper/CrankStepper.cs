/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Slider.cs
 *  Description  :  Define CrankStepper component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  12/08/2022
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Machineries
{
    /// <summary>
    /// Stepper motor for CrankMechanism.
    /// </summary>
    public class CrankStepper : Stepper
    {
        /// <summary>
        /// CrankMechanism drive by stepper.
        /// </summary>
        public CrankMechanism mechanism;

        /// <summary>
        /// Mechanism drive by stepper.
        /// </summary>
        protected override Mechanism Mechanism { get { return mechanism; } }

        /// <summary>
        /// Current mileage.
        /// </summary>
        protected override float Current { get { return mechanism.Angle; } }
    }
}