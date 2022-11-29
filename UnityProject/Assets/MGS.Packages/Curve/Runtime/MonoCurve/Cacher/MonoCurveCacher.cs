/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MonoCurveCacher.cs
 *  Description  :  Cacher for mono curve.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  9/30/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.IO;
using UnityEngine;

namespace MGS.Curve
{
    /// <summary>
    /// Cacher for mono curve.
    /// </summary>
    public abstract class MonoCurveCacher : MonoBehaviour, IMonoCurveCacher
    {
        /// <summary>
        /// Build cache file from mono curve.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual bool Build(string file)
        {
            var contents = SerializeCurve();
            if (string.IsNullOrEmpty(contents))
            {
                return false;
            }
            return WriteToFile(file, contents);
        }

        /// <summary>
        /// Load cache file to mono curve.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public virtual bool Load(string file)
        {
            var contents = ReadFromFile(file);
            if (string.IsNullOrEmpty(contents))
            {
                return false;
            }
            return DeserializeCurve(contents);
        }

        /// <summary>
        /// Serialize mono curve to string content.
        /// </summary>
        /// <returns></returns>
        protected abstract string SerializeCurve();

        /// <summary>
        /// Deserialize mono curve from string content.
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        protected abstract bool DeserializeCurve(string contents);

        /// <summary>
        /// Write content to local file.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        protected bool WriteToFile(string file, string contents)
        {
            try
            {
                var dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllText(file, contents);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Read content from local file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected string ReadFromFile(string file)
        {
            try
            {
                return File.ReadAllText(file);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
        }
    }
}