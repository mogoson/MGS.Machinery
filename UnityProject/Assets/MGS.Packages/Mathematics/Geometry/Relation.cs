/*************************************************************************
 *  Copyright © 2015-2019 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Relation.cs
 *  Description  :  Position relation.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/26/2018
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Mathematics
{
    /// <summary>
    /// Position relation.
    /// </summary>
    public enum Relation
    {
        /// <summary>
        /// Undefined relation.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Coincidence.
        /// </summary>
        Coincidence = 1,

        /// <summary>
        /// External.
        /// </summary>
        External = 2,

        /// <summary>
        /// Internal.
        /// </summary>
        Internal = 3,

        /// <summary>
        /// Parallel.
        /// </summary>
        Parallel = 4,

        /// <summary>
        /// Vertical.
        /// </summary>
        Vertical = 5,

        /// <summary>
        /// Intersect
        /// </summary>
        Intersect = 6,

        /// <summary>
        /// Outside tangent.
        /// </summary>
        OutsideTangent = 7,

        /// <summary>
        /// Inside tangent.
        /// </summary>
        InsideTangent = 8
    }
}