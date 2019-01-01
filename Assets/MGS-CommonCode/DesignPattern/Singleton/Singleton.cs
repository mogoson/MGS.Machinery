/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Singleton.cs
 *  Description  :  Define base class of singleton.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  2/13/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System;

namespace Mogoson.DesignPattern
{
    /// <summary>
    /// Provide a single instance of the specified type T.
    /// </summary>
    /// <typeparam name="T">Specified type.</typeparam>
    public class Singleton<T> where T : class
    {
        #region Nested Class
        /// <summary>
        /// Inner singleton provide instance.
        /// </summary>
        private class InnerSingleton
        {
            #region Property
            /// <summary>
            /// Single instance of the specified type T created by that type's default constructor.
            /// </summary>
            internal static readonly T Instance = Activator.CreateInstance(typeof(T), true) as T;
            #endregion

            #region Static Method
            /// <summary>
            /// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
            /// </summary>
            static InnerSingleton() { }
            #endregion
        }
        #endregion

        #region Property
        /// <summary>
        /// Single instance of the specified type T.
        /// </summary>
        public static T Instance { get { return InnerSingleton.Instance; } }
        #endregion
    }
}