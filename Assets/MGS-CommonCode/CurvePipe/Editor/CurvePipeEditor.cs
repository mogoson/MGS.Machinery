/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurvePipeEditor.cs
 *  Description  :  Editor for MonoCurvePipe component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Skin;
using UnityEditor;

namespace Mogoson.CurvePipe
{
    [CustomEditor(typeof(MonoCurvePipe), true)]
    public class CurvePipeEditor : SkinEditor
    {
        #region Field and Property
        protected new MonoCurvePipe Target { get { return target as MonoCurvePipe; } }
        protected const float Delta = 0.05f;
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            Handles.color = Blue;
            for (float t = 0; t < Target.MaxKey; t += Delta)
            {
                Handles.DrawLine(Target.GetPointAt(t), Target.GetPointAt(t + Delta));
            }
        }
        #endregion
    }
}