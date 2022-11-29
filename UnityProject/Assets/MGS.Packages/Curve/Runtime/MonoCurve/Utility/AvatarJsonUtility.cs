/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AvatarJsonUtility.cs
 *  Description  :  Utility for avatarjson.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  2/28/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine;
#else
using System;
#endif

namespace MGS.Curve
{
    /// <summary>
    /// Utility for avatarjson.
    /// </summary>
    public sealed class AvatarJsonUtility
    {
        /// <summary>
        /// Serialize List to avatar json.
        /// </summary>
        /// <typeparam name="T">Type of List item.
        /// The T must with SerializableAttribute and public field or private property with SerializeField Attribute if custom type.
        /// </typeparam>
        /// <param name="list">Source list.</param>
        /// <returns>Json text of ListAvatar.</returns>
        public static string ToJson<T>(List<T> list)
        {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            var avatar = new ListAvatar<T>(list);
            return JsonUtility.ToJson(avatar);
#else
            throw new NotSupportedException("JsonUtility does not support, update your Untiy to 5.3 or newer.");
#endif
        }

        /// <summary>
        /// Deserialize List from avatar json.
        /// </summary>
        /// <typeparam name="T">Type of List item.
        /// The T must with SerializableAttribute and public field or private property with SerializeField Attribute if custom type.
        /// </typeparam>
        /// <param name="json">Json text of ListAvatar.</param>
        /// <returns>List object.</returns>
        public static List<T> FromJson<T>(string json)
        {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            var avatar = JsonUtility.FromJson<ListAvatar<T>>(json);
            if (avatar == null)
            {
                return null;
            }

            return avatar.source;
#else
            throw new NotSupportedException("JsonUtility does not support, update your Untiy to 5.3 or newer.");
#endif
        }

        /// <summary>
        /// Avatar for List to serialize.
        /// </summary>
        /// <typeparam name="T">Type of list item.
        /// The T must with SerializableAttribute and public field or private property with SerializeField Attribute if custom type.
        /// </typeparam>
        private class ListAvatar<T>
        {
            /// <summary>
            /// Source list.
            /// </summary>
            public List<T> source;

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="source">Source list.</param>
            public ListAvatar(List<T> source)
            {
                this.source = source;
            }
        }
    }
}