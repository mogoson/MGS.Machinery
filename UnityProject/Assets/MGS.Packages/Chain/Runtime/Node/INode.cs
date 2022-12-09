/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  INode.cs
 *  Description  :  Define interface of node.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  7/3/2017
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Chain
{
    /// <summary>
    /// Interface of node.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// ID of node in the chain.
        /// </summary>
        int ID { set; get; }
    }
}