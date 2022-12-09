/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Node.cs
 *  Description  :  Define Node component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/21/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Chain
{
    /// <summary>
    /// Node component.
    /// </summary>
    public class Node : MonoBehaviour, INode
    {
        #region Field and Property
        /// <summary>
        /// ID of node in the chain.
        /// </summary>
        [SerializeField]
        protected int id = 0;

        /// <summary>
        /// ID of node in the chain.
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        #endregion
    }
}