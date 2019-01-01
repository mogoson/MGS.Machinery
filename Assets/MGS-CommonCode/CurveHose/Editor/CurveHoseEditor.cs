/*************************************************************************
 *  Copyright © 2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CurveHoseEditor.cs
 *  Description  :  Editor for MonoCurveHose component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/20/2018
 *  Description  :  Initial development version.
 *************************************************************************/

using Mogoson.Skin;
using UnityEditor;

namespace Mogoson.CurveHose
{
    [CustomEditor(typeof(MonoCurveHose), true)]
    public class CurveHoseEditor : SkinEditor
    {
        #region Field and Property
        protected new MonoCurveHose Target { get { return target as MonoCurveHose; } }
        protected const float Delta = 0.05f;
        #endregion

        #region Protected Method
        protected virtual void OnSceneGUI()
        {
            DrawHoseCenterCurve();
        }

        protected virtual void DrawHoseCenterCurve()
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